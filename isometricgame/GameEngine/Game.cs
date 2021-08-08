using isometricgame.GameEngine;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Exceptions.Services;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.Systems.Serialization;
using OpenTK.Graphics;
using isometricgame.GameEngine.Systems.Input;
using isometricgame.GameEngine.Tools;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine
{
    public class Game : GameWindow
    {
        //PATHS
        public readonly string GAME_DIRECTORY_BASE;
        public readonly string GAME_DIRECTORY_WORLDS;
        public readonly string GAME_DIRECTORY_ASSETS;
        public readonly string GAME_DIRECTORY_SHADERS;
        
        #region Systems

        private readonly List<GameSystem> SYSTEMS = new List<GameSystem>();
        
        /// <summary>
        /// Responsible for loading and unloading textures.
        /// </summary>
        public AssetProvider Game__Asset_Provider { get; private set; }
        /// <summary>
        /// Responsible for recording and recieving loaded sprites.
        /// </summary>
        public SpriteLibrary Game__Sprite_Library { get; private set; }
        internal RenderService Game__Render_Service { get; private set; }
        public TextDisplayer Game__Text_Displayer { get; private set; }
        public InputSystem Game__Input_System { get; private set; }
        public AnimationSchematicLibrary Game__Animation_Schematic_Library { get; private set; }
        public SceneManagementService Game__Scene_Management_Service { get; private set; }
        
        public EventScheduler Game__Event_Scheduler { get; private set; }
        #endregion

        #region Time
        private double renderTime, updateTime;

        public double RenderTime => renderTime;
        public double UpdateTime => updateTime;
        #endregion

        public Vector2 Get__Window_Size__Game()
            => new Vector2(Width, Height);

        public float Get__Window_Hypotenuse__Game()
            => MathHelper.Get__Hypotenuse(Width, Height);
        
        private Scene scene;

        public Game
            (
            int width, 
            int height, 
            string title, 
            string GAME_DIR = "", 
            string GAME_DIR_ASSETS = "", 
            string GAME_DIR_WORLDS = ""
            )
            : base
                (
                width, 
                height, 
                GraphicsMode.Default, 
                title
                )
        {
            GAME_DIRECTORY_BASE = (GAME_DIR == String.Empty) ? AppDomain.CurrentDomain.BaseDirectory : GAME_DIR;
            GAME_DIRECTORY_ASSETS = (GAME_DIR_ASSETS == String.Empty) ? Path.Combine(GAME_DIRECTORY_BASE, "Assets" + Path.DirectorySeparatorChar) : GAME_DIR_ASSETS;
            GAME_DIRECTORY_SHADERS = Path.Combine(GAME_DIRECTORY_ASSETS, "Shaders" + Path.DirectorySeparatorChar);
            GAME_DIRECTORY_WORLDS = (GAME_DIR_WORLDS == String.Empty) ? Path.Combine(GAME_DIRECTORY_BASE, "Worlds" + Path.DirectorySeparatorChar) : GAME_DIR_WORLDS;
            
            Register__Base_Systems__Game();
            Game__Event_Scheduler = new EventScheduler();
            Game__Render_Service.LoadShaders(Get__Shaders__Game());

            //END SERVICES

            Handle_Load__Content__Game();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(ClientRectangle);
            Game__Render_Service.AdjustProjection(Width, Height);
            scene.RescaleScene();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            updateTime += e.Time;
            Game__Event_Scheduler.Progress_Events(e.Time);
            scene.UpdateScene(new Frame_Argument(UpdateTime, e.Time));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            renderTime += e.Time;

            scene.BeginRender(Game__Render_Service);
            Game__Render_Service.BeginRender();

            Game__Render_Service.RenderScene(scene, new Frame_Argument(RenderTime, e.Time));

            Game__Render_Service.EndRender();
            
            SwapBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {

        }

        protected override void OnUnload(EventArgs e)
        {
            foreach (GameSystem gamesys in SYSTEMS)
                gamesys.Unload();
        }

        public T Get_System__Game<T>() where T : GameSystem
        {
            foreach (GameSystem system in SYSTEMS)
                if (system is T && system.Accessable)
                    return system as T;
            throw new ServiceNotFoundException();
        }

        protected void Register__System__Game<T>(T gameService) where T : GameSystem
        {
            if (SYSTEMS.Exists((s) => s is T))
                throw new ExistingServiceException();
            SYSTEMS.Add(gameService);
        }

        protected virtual string[] Get__Shaders__Game()
        {
            return new string[] { "shader" };
        }

        internal virtual void Register__Base_Systems__Game()
        {
            Game__Asset_Provider = new AssetProvider(this);
            Game__Sprite_Library = new SpriteLibrary(this);
            Game__Render_Service = new RenderService(this, Width, Height);
            Game__Text_Displayer = new TextDisplayer(this);
            Game__Input_System = new InputSystem(this);
            Game__Animation_Schematic_Library = new AnimationSchematicLibrary(this);
            Game__Scene_Management_Service = new SceneManagementService(this);

            Register__System__Game(Game__Asset_Provider);
            Register__System__Game(Game__Sprite_Library);
            Register__System__Game(Game__Render_Service);
            Register__System__Game(Game__Text_Displayer);
            Register__System__Game(Game__Input_System);
            Register__System__Game(Game__Animation_Schematic_Library);
            Register__System__Game(Game__Scene_Management_Service);

            Handle_Register__Custom_Systems__Game();

            foreach (GameSystem system in SYSTEMS)
                system.Load();
        }

        protected virtual void Handle_Register__Custom_Systems__Game() { }
        protected virtual void Handle_Load__Content__Game() { }

        internal void Set__Scene__Game(Scene scene)
        {
            this.scene = scene;
        }
        
        protected void Load__Sprite__Game
            (
            string spriteName, 
            float scale, 
            int width, 
            int height, 
            bool throwIf_NotExists = true, 
            string savedName = null
            )
        {
            string path = Path.Combine(
                GAME_DIRECTORY_ASSETS,
                spriteName + ".png"
            );
            if (throwIf_NotExists && !File.Exists(path))
                return;
            Sprite s;
            Game__Sprite_Library.RecordSprite(
                s = Game__Asset_Provider.ExtractSpriteSheet(
                    path,
                    (savedName == null) ? spriteName : savedName,
                    width,
                    height
                )
            );
            s.Scale = scale;
        }
    }
}

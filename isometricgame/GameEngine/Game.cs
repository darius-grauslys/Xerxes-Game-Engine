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
        //SERVICES
        private AssetProvider contentPipe;
        private SpriteLibrary spriteLibrary;
        private RenderService renderService;
        private TextDisplayer textDisplayer;
        private InputSystem inputSystem;

        private List<GameSystem> systems = new List<GameSystem>();
        /// <summary>
        /// Responsible for loading and unloading textures.
        /// </summary>
        protected AssetProvider AssetProvider { get => contentPipe; private set => contentPipe = value; }
        /// <summary>
        /// Responsible for recording and recieving loaded sprites.
        /// </summary>
        protected SpriteLibrary SpriteLibrary { get => spriteLibrary; private set => spriteLibrary = value; }

        protected RenderService RenderService { get => renderService; private set => renderService = value; }

        protected TextDisplayer TextDisplayer { get => textDisplayer; private set => textDisplayer = value; }

        protected InputSystem InputSystem { get => inputSystem; private set => inputSystem = value; }
        #endregion

        #region Time
        private double renderTime, updateTime;

        public double RenderTime => renderTime;
        public double UpdateTime => updateTime;
        #endregion

        private Scene scene;

        public Game(int width, int height, string title, string GAME_DIR = "", string GAME_DIR_ASSETS = "", string GAME_DIR_WORLDS = "")
            : base(width, height, GraphicsMode.Default, title)
        {
            GAME_DIRECTORY_BASE = (GAME_DIR == String.Empty) ? AppDomain.CurrentDomain.BaseDirectory : GAME_DIR;
            GAME_DIRECTORY_ASSETS = (GAME_DIR_ASSETS == String.Empty) ? Path.Combine(GAME_DIRECTORY_BASE, "Assets\\") : GAME_DIR_ASSETS;
            GAME_DIRECTORY_SHADERS = Path.Combine(GAME_DIRECTORY_ASSETS, "Shaders\\");
            GAME_DIRECTORY_WORLDS = (GAME_DIR_WORLDS == String.Empty) ? Path.Combine(GAME_DIRECTORY_BASE, "Worlds\\") : GAME_DIR_WORLDS;

            /*
            gameWindow.Load += GameWindow_Load;
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.Unload += GameWindow_Unload;
            gameWindow.Closing += GameWindow_Closing;
            gameWindow.Resize += GameWindow_Resize;
            */

            //SERVICES

            RegisterSystems();

            //END SERVICES

            LoadContent();
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(ClientRectangle);
            RenderService.AdjustProjection(Width, Height);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            updateTime += e.Time;

            scene.UpdateFrame(new FrameArgument(UpdateTime, e.Time));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            renderTime += e.Time;

            RenderService.BeginRender();

            RenderService.RenderScene(scene, new FrameArgument(RenderTime, e.Time));

            RenderService.EndRender();
            
            SwapBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {

        }

        protected override void OnUnload(EventArgs e)
        {
            foreach (GameSystem gamesys in systems)
                gamesys.Unload();
        }

        public T GetSystem<T>() where T : GameSystem
        {
            foreach (GameSystem system in systems)
                if (system is T)
                    return system as T;
            throw new ServiceNotFoundException();
        }

        protected void RegisterSystem<T>(T gameService) where T : GameSystem
        {
            if (systems.Exists((s) => s is T))
                throw new ExistingServiceException();
            systems.Add(gameService);
        }

        protected virtual void RegisterSystems()
        {
            AssetProvider = new AssetProvider(this);
            SpriteLibrary = new SpriteLibrary(this);
            RenderService = new RenderService(this, Width, Height);
            TextDisplayer = new TextDisplayer(this);
            InputSystem = new InputSystem(this);

            RegisterSystem(AssetProvider);
            RegisterSystem(SpriteLibrary);
            RegisterSystem(RenderService);
            RegisterSystem(TextDisplayer);
            RegisterSystem(InputSystem);
        }

        protected virtual void LoadContent()
        {

        }

        protected void SetScene(Scene scene)
        {
            this.scene = scene;
        }
    }
}

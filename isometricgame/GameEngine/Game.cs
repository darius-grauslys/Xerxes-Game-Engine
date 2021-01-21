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

namespace isometricgame.GameEngine
{
    public class Game
    {
        //PATHS
        public readonly string GAME_DIRECTORY_BASE;
        public readonly string GAME_DIRECTORY_WORLDS;
        public readonly string GAME_DIRECTORY_ASSETS;
        public readonly string GAME_DIRECTORY_SHADERS;

        protected GameWindow gameWindow;
        public int WindowWidth => gameWindow.Width;
        public int WindowHeight => gameWindow.Height;

        #region Systems
        //SERVICES
        private AssetProvider contentPipe;
        private SpriteLibrary spriteLibrary;
        private RenderService renderService;
        private TextDisplayer textDisplayer;

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
        #endregion

        #region Time
        private double renderTime, updateTime;

        public double RenderTime => renderTime;
        public double UpdateTime => updateTime;
        #endregion

        private Scene scene;

        public Game(GameWindow gameWindow, string GAME_DIR = "", string GAME_DIR_ASSETS = "", string GAME_DIR_WORLDS = "")
        {
            this.gameWindow = gameWindow;

            GAME_DIRECTORY_BASE = (GAME_DIR == String.Empty) ? AppDomain.CurrentDomain.BaseDirectory : GAME_DIR;
            GAME_DIRECTORY_ASSETS = (GAME_DIR_ASSETS == String.Empty) ? Path.Combine(GAME_DIRECTORY_BASE, "Assets\\") : GAME_DIR_ASSETS;
            GAME_DIRECTORY_SHADERS = Path.Combine(GAME_DIRECTORY_ASSETS, "Shaders\\");
            GAME_DIRECTORY_WORLDS = (GAME_DIR_WORLDS == String.Empty) ? Path.Combine(GAME_DIRECTORY_BASE, "Worlds\\") : GAME_DIR_WORLDS;

            gameWindow.Load += GameWindow_Load;
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.Unload += GameWindow_Unload;
            gameWindow.Closing += GameWindow_Closing;
            gameWindow.Resize += GameWindow_Resize;

            //SERVICES

            RegisterSystems();

            //END SERVICES

            LoadContent();
        }

        private void GameWindow_Resize(object sender, EventArgs e)
        {
            GL.Viewport(gameWindow.ClientRectangle);
            RenderService.AdjustProjection(gameWindow.Width, gameWindow.Height);
        }

        private void GameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void GameWindow_UpdateFrame(object sender, FrameEventArgs e)
        {
            updateTime += e.Time;

            scene.UpdateFrame(new FrameArgument(UpdateTime, e.Time));
        }

        internal virtual void OnUpdateFrame()
        {

        }
                        
        private void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            renderTime += e.Time;

            RenderService.BeginRender();

            RenderService.RenderScene(scene, new FrameArgument(RenderTime, e.Time));

            RenderService.EndRender();
            
            gameWindow.SwapBuffers();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            OnGameLoad();
        }

        private void GameWindow_Unload(object sender, EventArgs e)
        {
            foreach (GameSystem gamesys in systems)
                gamesys.Unload();
        }

        internal virtual void OnGameLoad()
        {

        }

        public T GetSystem<T>() where T : GameSystem
        {
            foreach (GameSystem system in systems)
                if (system is T)
                    return system as T;
            throw new ServiceNotFoundException();
        }

        internal void RegisterSystem<T>(T gameService) where T : GameSystem
        {
            if (systems.Exists((s) => s is T))
                throw new ExistingServiceException();
            systems.Add(gameService);
        }

        internal virtual void RegisterSystems()
        {
            AssetProvider = new AssetProvider(this);
            SpriteLibrary = new SpriteLibrary(this);
            RenderService = new RenderService(this, gameWindow.Width, gameWindow.Height);
            TextDisplayer = new TextDisplayer(this);

            RegisterSystem(AssetProvider);
            RegisterSystem(SpriteLibrary);
            RegisterSystem(RenderService);
            RegisterSystem(TextDisplayer);
        }

        internal virtual void LoadContent()
        {

        }

        internal void SetScene(Scene scene)
        {
            this.scene = scene;
        }
    }
}

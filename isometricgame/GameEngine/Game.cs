using isometricgame.GameEngine;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Services;
using isometricgame.GameEngine.Services.Serializations;
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

namespace isometricgame.GameEngine
{
    public class Game
    {
        //PATHS
        public readonly string GAME_DIRECTORY_BASE;
        public readonly string GAME_DIRECTORY_WORLDS;

        //SERVICES
        private ContentPipe contentPipe;
        private SpriteLibrary spriteLibrary;
        private TextureLibrary textureLibrary;
        private SerializationManager serializationManager;
        private RenderService renderService;
        private Services.TextWriter textWriter;

        private List<GameService> services = new List<GameService>();

        /// <summary>
        /// Responsible for loading and unloading textures.
        /// </summary>
        protected ContentPipe ContentPipe { get => contentPipe; private set => contentPipe = value; }
        /// <summary>
        /// Responsible for recording and recieving loaded sprites.
        /// </summary>
        protected SpriteLibrary SpriteLibrary { get => spriteLibrary; private set => spriteLibrary = value; }
        /// <summary>
        /// Responsible for recording ad recieving loaded textures.
        /// </summary>
        protected TextureLibrary TextureLibrary { get => textureLibrary; private set => textureLibrary = value; }
        /// <summary>
        /// Responsible for holding references to various services.
        /// </summary>
        protected SerializationManager SerializationManager { get => serializationManager; private set => serializationManager = value; }

        protected RenderService RenderService { get => renderService; private set => renderService = value; }

        protected Services.TextWriter TextWriter { get => textWriter; private set => textWriter = value; }

        protected GameWindow gameWindow;
        private Matrix4 projection;

        private Scene scene;

        //public event EventHandler<KeyboardKeyEventArgs> KeyPressed;

        public int WindowWidth => gameWindow.Width;
        public int WindowHeight => gameWindow.Height;
        
        long time;

        public Game(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;

            GAME_DIRECTORY_BASE = AppDomain.CurrentDomain.BaseDirectory;
            GAME_DIRECTORY_WORLDS = Path.Combine(GAME_DIRECTORY_BASE, "WORLDS\\");

            gameWindow.Load += GameWindow_Load;
            gameWindow.RenderFrame += GameWindow_RenderFrame;
            gameWindow.UpdateFrame += GameWindow_UpdateFrame;
            gameWindow.Closing += GameWindow_Closing;
            //gameWindow.KeyDown += (o, e) => KeyPressed?.Invoke(o, e);
            //gameWindow.KeyUp += (o, e) => KeyPressed?.Invoke(o, e);
            gameWindow.Resize += GameWindow_Resize;

            //SERVICES

            RegisterServices();
            serializationManager = new SerializationManager();

            //END SERVICES

            LoadContent();
        }

        public Matrix4 GetBasicView() { return Matrix4.CreateTranslation(-WindowWidth / 2, -WindowHeight / 2, 0); }

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
            scene.UpdateFrame(e);
        }

        internal virtual void OnUpdateFrame()
        {

        }
                        
        private void GameWindow_RenderFrame(object sender, FrameEventArgs e)
        {
            time++;

            RenderService.BeginRender();

            RenderService.RenderScene(scene, e);

            RenderService.EndRender();
            
            gameWindow.SwapBuffers();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            OnGameLoad();
        }

        internal virtual void OnGameLoad()
        {

        }

        public T GetService<T>() where T : GameService
        {
            foreach (GameService service in services)
                if (service is T)
                    return service as T;
            throw new ServiceNotFoundException();
        }

        internal void RegisterService<T>(T gameService) where T : GameService
        {
            if (services.Exists((s) => s is T))
                throw new ExistingServiceException();
            services.Add(gameService);
        }

        internal virtual void RegisterServices()
        {
            ContentPipe = new ContentPipe(this);
            TextureLibrary = new TextureLibrary(this);
            SpriteLibrary = new SpriteLibrary(this);
            RenderService = new RenderService(this, gameWindow.Width, gameWindow.Height);
            TextWriter = new Services.TextWriter(this);

            RegisterService(ContentPipe);
            RegisterService(TextureLibrary);
            RegisterService(SpriteLibrary);
            RegisterService(RenderService);
            RegisterService(TextWriter);
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

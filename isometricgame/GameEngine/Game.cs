using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using isometricgame.GameEngine.Events;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.Systems.Serialization;
using OpenTK.Graphics;
using isometricgame.GameEngine.Systems.Input;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine
{
    public class Game : GameWindow
    {
        //PATHS
        public string GAME__DIRECTORY__BASE { get; }
        public string GAME__DIRECTORY__ASSETS { get; }
        public string GAME__DIRECTORY__SHADERS { get; }
        
        #region Systems

        private List<GameSystem> GAME__SYSTEMS { get; }
        
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
        
        public Event_Scheduler Game__Event_Scheduler { get; private set; }
        #endregion

        #region Time
        public double Game__Render_Time { get; private set; } 
        public double Game__Update_Time { get; private set; }
        #endregion

        public Vector2 Get__Window_Size__Game()
            => new Vector2(Width, Height);

        public float Get__Window_Hypotenuse__Game()
            => MathHelper.Get__Hypotenuse(Width, Height);
        
        private Scene scene;

        public Game
        (
            Game_Arguments gameArguments
        )
        : base
            (
            (int)
            (
                gameArguments?.Game_Arguments__WINDOW_WIDTH 
                ?? Game_Arguments.Game_Arguments__DEFAULT_WINDOW_WIDTH
            ),
            (int)
            (
                gameArguments?.Game_Arguments__WINDOW_HEIGHT
                ?? Game_Arguments.Game_Arguments__DEFAULT_WINDOW_HEIGHT
            ),

            GraphicsMode.Default, 
            
            gameArguments?.Game_Arguments__WINDOW_TITLE
            ?? Game_Arguments.Game_Arguments__DEFAULT_WINDOW_TITLE
            )
        {
            GAME__SYSTEMS = new List<GameSystem>();
            
            Log.Initalize__Log
            (
                gameArguments
            );

            GAME__DIRECTORY__BASE = AppDomain.CurrentDomain.BaseDirectory; 
            GAME__DIRECTORY__ASSETS = 
                Private_Validate__Directory__Game
                (
                    gameArguments.Game_Arguments__ASSET_DIRECTORY,
                    Game_Arguments.Game_Arguments__DEFAULT_ASSET_DIRECTORY
                );
            GAME__DIRECTORY__SHADERS = 
                Private_Validate__Directory__Game
                (
                    gameArguments.Game_Arguments__SHADER_DIRECTORY,
                    Game_Arguments.Game_Arguments__DEFAULT_SHADER_DIRECTORY
                );
            
            Private_Establish__Base_Systems__Game();
            Private_Establish__Custom_Systems__Game();
            Private_Load__Systems__Game();
            
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__BASE_EVENT_SCHEDULER__LOADING, this);
            Game__Event_Scheduler = new Event_Scheduler();
            
            Game__Render_Service.Internal_Load__Shaders__Render_Service(Get__Shaders__Game());

            //END SERVICES

            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__CONTENT_LOADING, this);
            Handle_Load__Content__Game();
        }

        private string Private_Validate__Directory__Game
        (
            string baseDirectory,
            string recoveryDirectory
        )
        {
            if (baseDirectory == null || !Directory.Exists(baseDirectory))
            {
                Log.Internal_Write__Error__Log
                (
                    Log_Message_Type.Error__Directory_Not_Found,
                    Log.ERROR__GAME__DIRECTORY_NOT_FOUND_1,
                    this,
                    0,
                    baseDirectory
                );

                Log.Internal_Write__Info__Log(Log.INFO__RECOVERY__ASSET_DIRECTORY_NOT_FOUND, this);

                baseDirectory = Private_Get__Validated_Default_Directory__Game(recoveryDirectory);

                if (baseDirectory != null)
                    return baseDirectory;

                Log.Internal_Panic__Log();

                Close();
            }

            return baseDirectory;
        }

        private string Private_Get__Validated_Default_Directory__Game(string defaultDirectory)
        {
            if (!Directory.Exists(defaultDirectory))
            {
                Log.Internal_Write__Error__Log
                (
                    Log_Message_Type.Error__Recovery_Directory_Not_Found,
                    Log.ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1,
                    this,
                    0,
                    defaultDirectory
                );

                return null;
            }

            return defaultDirectory;
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
            Game__Update_Time += e.Time;
            Game__Event_Scheduler.Internal_Progress__Events__Event_Scheduler(e.Time);
            scene.UpdateScene(new Frame_Argument(Game__Update_Time, e.Time));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Game__Render_Time += e.Time;

            scene.BeginRender(Game__Render_Service);
            Game__Render_Service.BeginRender();

            Game__Render_Service.RenderScene(scene, new Frame_Argument(Game__Render_Time, e.Time));

            Game__Render_Service.EndRender();
            
            SwapBuffers();
        }

        protected override void OnLoad(EventArgs e)
        {

        }

        protected override void OnUnload(EventArgs e)
        {
            Private_Unload__Systems__Game();
        }

        private void Private_Unload__Systems__Game()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__UNLOADING, this);

            foreach (GameSystem gamesys in GAME__SYSTEMS)
            {   
                gamesys.Unload();
                Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEM__UNLOADED_1, gamesys);
            }

            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__UNLOADED, this);
        }

        public T Get_System__Game<T>() where T : GameSystem
        {
            foreach (GameSystem system in GAME__SYSTEMS)
                if (system is T && system.Accessable)
                    return system as T;

            Log.Internal_Write__Error__Log(Log_Message_Type.Error__System_Not_Found, Log.ERROR__SYSTEM__NOT_FOUND_1, this, 0, typeof(T).ToString());
            return null;
        }

        protected bool Register__System__Game<T>(T gameService) where T : GameSystem
        {
            if (GAME__SYSTEMS.Exists((s) => s is T))
            {
                Log.Internal_Write__Warning__Log
                (
                    Log_Message_Type.Warning__Game__Load_System__Similar_Typed_System_Already_Loaded,
                    Log.WARNING__GAME__SYSTEM__ALREADY_LOADED_1,
                    this,
                    0,
                    gameService?.ToString()
                );
                return false;
            }
            
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEM__LOADED_1, this, 0, gameService?.ToString());
            
            GAME__SYSTEMS.Add(gameService);

            return true;
        }

        protected virtual string[] Get__Shaders__Game()
        {
            return new string[] { "shader" };
        }

        private void Private_Establish__Base_Systems__Game()
        {
            Private_Initalize__Base_Systems__Game();
            Private_Register__Base_Systems__Game();
        }

        private void Private_Initalize__Base_Systems__Game()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__INITALIZING, this);
            
            Game__Asset_Provider = new AssetProvider(this);
            Game__Sprite_Library = new SpriteLibrary(this);
            Game__Render_Service = new RenderService(this, Width, Height);
            Game__Text_Displayer = new TextDisplayer(this);
            Game__Input_System = new InputSystem(this);
            Game__Animation_Schematic_Library = new AnimationSchematicLibrary(this);
            Game__Scene_Management_Service = new SceneManagementService(this);

            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__INITALIZED, this);
        }

        private void Private_Register__Base_Systems__Game()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__REGISTERING, this);
            
            Register__System__Game(Game__Asset_Provider);
            Register__System__Game(Game__Sprite_Library);
            Register__System__Game(Game__Render_Service);
            Register__System__Game(Game__Text_Displayer);
            Register__System__Game(Game__Input_System);
            Register__System__Game(Game__Animation_Schematic_Library);
            Register__System__Game(Game__Scene_Management_Service);

            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__REGISTERED, this);
        }

        private void Private_Establish__Custom_Systems__Game()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERING, this);

            Handle_Register__Custom_Systems__Game();
    
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERED, this);
        }

        private void Private_Load__Systems__Game()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SYSTEMS__LOADING, this);

            foreach (GameSystem system in GAME__SYSTEMS)
                system.Load();

            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__ALL_SYSTEMS__LOADED, this);
        }

        protected virtual void Handle_Register__Custom_Systems__Game() { }
        protected virtual void Handle_Load__Content__Game() { }

        internal void Internal_Set__Scene__Game(Scene scene)
        {
            this.scene = scene;
        }
        
        protected void Protected_Load__Sprite__Game
            (
            string spriteName, 
            float scale, 
            int width, 
            int height, 
            bool throwIf_NotExists = true, 
            string savedName = null
            )
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__SPRITE_LOAD_1, this, 0, spriteName);
            string path = Path.Combine(
                GAME__DIRECTORY__ASSETS,
                spriteName + ".png"
            );
            if (throwIf_NotExists && !File.Exists(path))
            {
                Log.Internal_Write__Error__Log
                (
                    Log_Message_Type.Error__Content_Path_Not_Found, 
                    Log.ERROR__GAME__CONTENT_SPRITE_NOT_FOUND_2, 
                    this, 
                    0, 
                    spriteName, 
                    path
                );

                return;
            }
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

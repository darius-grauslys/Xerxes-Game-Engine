using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using OpenTK.Graphics;
using Math_Helper = Xerxes.Tools.Math_Helper;
using Xerxes.Xerxes_OpenTK.Exports.Input;
using OpenTK.Input;

namespace Xerxes.Xerxes_OpenTK
{
    /// <summary>
    /// Contains a Game Window object, and hooks events to it.
    /// It downstreams these events using the generic Streamline_Arguments
    /// and downstreams. It takes a SA__Draw upstream.
    /// This object is parentless - Xerxes_Childless
    /// </summary>
    public class Game : 
        Root<SA__Configure_OpenTK_Game, SA__Associate_Game_OpenTK, SA__Dissassociate_Game_OpenTK>
    {
        internal GameWindow Game__Game_Window__Internal { get; private set; }

        //PATHS
        public string Game__Directory_Base { get; private set; }
        public string Game__Directory_Assets { get; private set; }
        public string Game__Directory_Shaders { get; private set; }

        #region Time
        private Timer _Game__UPDATE_TIMER { get; }
        private Timer _Game__RENDER_TIMER { get; }

        public double Game__Elapsed_Time__Update => _Game__UPDATE_TIMER.Timer__Time_Elapsed;
        public double Game__Delta_Time__Update   => _Game__UPDATE_TIMER.Timer__Delta_Time;
        public double Game__Elapsed_Time__Render => _Game__RENDER_TIMER.Timer__Time_Elapsed;
        public double Game__Delta_Time__Render   => _Game__RENDER_TIMER.Timer__Delta_Time;
        #endregion

        public int Game__Window_Width => Game__Game_Window__Internal.Width;
        public int Game__Window_Height => Game__Game_Window__Internal.Height;
        public Vector2 Get__Window_Size__Game()
            => new Vector2(Game__Window_Width, Game__Window_Height);

        public float Get__Window_Hypotenuse__Game()
            => Math_Helper.Get__Hypotenuse(Game__Window_Width, Game__Window_Height);

        public Game()
        {
            _Game__UPDATE_TIMER = new Timer(-1);
            _Game__RENDER_TIMER = new Timer(-1);

            Declare__Streams()
                .Downstream.Extending<SA__Sealed_Under_Game>()

                .Downstream.Extending<SA__Update>()
                .Downstream.Extending<SA__Render_Begin>()
                .Downstream.Extending<SA__Render>()
                .Downstream.Extending<SA__Game_Window_Resized>()

                .Downstream.Extending<SA__Input_Mouse_Button>()
                .Downstream.Extending<SA__Input_Mouse_Move>()

                .Downstream.Extending<SA__Input_Key_Down>()
                .Downstream.Extending<SA__Input_Key_Up>()

                .Upstream  .Extending<SA__Sealed_Under_Game>()
                .Upstream  .Extending<SA__Render_Begin>()
                .Upstream  .Extending<SA__Render_End>();
        }

        protected override SA__Associate_Game_OpenTK Configure(SA__Configure_OpenTK_Game e)
        {
            Game__Game_Window__Internal = 
                new GameWindow
                (
                    (int)
                    (
                        e?.Game_Arguments__Window_Width 
                        ?? SA__Configure_OpenTK_Game.Game_Arguments__DEFAULT_WINDOW_WIDTH
                    ),
                    (int)
                    (
                        e?.Game_Arguments__Window_Height
                        ?? SA__Configure_OpenTK_Game.Game_Arguments__DEFAULT_WINDOW_HEIGHT
                    ),

                    GraphicsMode.Default, 
                    
                    e?.Game_Arguments__Window_Title
                    ?? SA__Configure_OpenTK_Game.Game_Arguments__DEFAULT_WINDOW_TITLE
                );

            Private_Hook__To_Game_Window__Game();

            Game__Directory_Base = AppDomain.CurrentDomain.BaseDirectory; 
            Game__Directory_Assets = 
                Private_Validate__Directory__Game
                (
                    e.Game_Arguments__Asset_Directory,
                    SA__Configure_OpenTK_Game.Game_Arguments__DEFAULT_ASSET_DIRECTORY
                );
            Game__Directory_Shaders = 
                Private_Validate__Directory__Game
                (
                    e.Game_Arguments__Shader_Directory,
                    SA__Configure_OpenTK_Game.Game_Arguments__DEFAULT_SHADER_DIRECTORY
                );

            SA__Associate_Game_OpenTK e_associate = 
                new SA__Associate_Game_OpenTK
                (
                    -1,-1,
                    Game__Directory_Base,
                    Game__Directory_Assets,
                    Game__Directory_Shaders,

                    Game__Window_Width,
                    Game__Window_Height,

                    Internal_Get__Shaders__Game()
                );

            return e_associate;
        }

        protected override void Execute()
        {

            Log.Write__Verbose__Log(Log_Messages__OpenTK.VERBOSE__GAME__CONTENT_LOADING, this);
            Handle_Load__Content__Game();
            Log.Write__Verbose__Log(Log_Messages__OpenTK.VERBOSE__GAME__CONTENT_LOADED, this);

            Invoke__Descending(new SA__Sealed_Under_Game());
            Invoke__Ascending(new SA__Sealed_Under_Game());

            Log.Write__Info__Log
            (
                Log_Messages__OpenTK.INFO__GAME__RUN_INVOKED,
                this
            );

            Game__Game_Window__Internal.Run();
        }

        private void Private_Hook__To_Game_Window__Game()
        {
            Game__Game_Window__Internal.Resize += Private_Handle__Resize_Window__Game;
            Game__Game_Window__Internal.Closed += Private_Handle__Closed_Window__Game;

            Game__Game_Window__Internal.UpdateFrame += Private_Handle__Update_Window__Game;
            Game__Game_Window__Internal.RenderFrame += Private_Handle__Render_Window__Game;

            Game__Game_Window__Internal.Load += Private_Handle__Load_Window__Game;
            Game__Game_Window__Internal.Unload += Private_Handle__Unload_Window__Game;

            Game__Game_Window__Internal.MouseDown += Private_Handle__Mouse_Down__Game;
            Game__Game_Window__Internal.MouseUp   += Private_Handle__Mouse_Up__Game;
            Game__Game_Window__Internal.MouseMove += Private_Handle__Mouse_Move__Game;

            Game__Game_Window__Internal.KeyDown += Private_Handle__Key_Down__Game;
            Game__Game_Window__Internal.KeyUp += Private_Handle__Key_Up__Game;
        }

        private string Private_Validate__Directory__Game
        (
            string baseDirectory,
            string recoveryDirectory
        )
        {
            if (baseDirectory == null || !Directory.Exists(baseDirectory))
            {
                Log.Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log_Messages__OpenTK.ERROR__GAME__DIRECTORY_NOT_FOUND_1,
                    this,
                    baseDirectory
                );

                Log.Write__Warning__Log
                (
                    Log_Messages__OpenTK.WARNING__RECOVERY__ASSET_DIRECTORY_NOT_FOUND, 
                    this
                );


                baseDirectory = Private_Get__Validated_Default_Directory__Game(recoveryDirectory);

                if (baseDirectory != null)
                    return baseDirectory;

                Log.Write__Log
                (
                    Log_Message_Type.Error__Critical,
                    Log_Messages__OpenTK.ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1, 
                    this, 
                    recoveryDirectory
                );

                Game__Game_Window__Internal.Close();
            }

            return baseDirectory;
        }

        private string Private_Get__Validated_Default_Directory__Game(string defaultDirectory)
        {
            if (!Directory.Exists(defaultDirectory))
            {
                Log.Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log_Messages__OpenTK.ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1,
                    this,
                    defaultDirectory
                );

                return null;
            }

            return defaultDirectory;
        }

        private void Private_Handle__Resize_Window__Game(object sender, EventArgs e)
        {
            GL.Viewport(Game__Game_Window__Internal.ClientRectangle);

            SA__Game_Window_Resized resize_2D_Argument = 
                new SA__Game_Window_Resized
                (
                    Game__Elapsed_Time__Update,
                    Game__Delta_Time__Update,
                    Game__Window_Width,
                    Game__Window_Height
                );

            Invoke__Descending
                (resize_2D_Argument);
        }

        private void Private_Handle__Closed_Window__Game(object sender, EventArgs e)
        {
            
        }

        private void Private_Handle__Update_Window__Game(object sender, FrameEventArgs e)
        {
            _Game__UPDATE_TIMER.Progress__Timer(e.Time);
            SA__Update frame_Argument = 
                new SA__Update
                (
                    e.Time,
                    Game__Elapsed_Time__Update
                );

            Invoke__Descending
                <SA__Update>(frame_Argument);
        }

        private void Private_Handle__Render_Window__Game(object sender, FrameEventArgs e)
        {
            _Game__RENDER_TIMER.Progress__Timer(e.Time);
            SA__Render frame_Argument = 
                new SA__Render
                (
                    e.Time,
                    Game__Elapsed_Time__Render 
                );
            SA__Render_Begin render_Begin =
                new SA__Render_Begin(frame_Argument);

            Invoke__Descending
                (render_Begin);
            Invoke__Ascending
                (render_Begin);
            Invoke__Descending
                (frame_Argument);
            Invoke__Ascending
                (new SA__Render_End(frame_Argument));
            
            Game__Game_Window__Internal.SwapBuffers();
        }

        private void Private_Handle__Load_Window__Game(object sender, EventArgs e)
        {
            Handle__Load__Game();
        }

        protected virtual void Handle__Load__Game()
        {

        }

        private void Private_Handle__Unload_Window__Game(object sender, EventArgs e)
        {
            SA__Dissassociate_Game_OpenTK dissassociate_Game =
                new SA__Dissassociate_Game_OpenTK();

            Private_Invoke__Global__Game
            (
                dissassociate_Game
            );
        }

        protected virtual void Handle__Unload__Game()
        {

        }

        private void Private_Handle__Mouse_Move__Game
        (
            object sender,
            MouseMoveEventArgs e
        )
        {
            SA__Input_Mouse_Move input_Mouse_Move = 
                new SA__Input_Mouse_Move
                (
                    _Game__UPDATE_TIMER.Timer__Time_Elapsed,
                    _Game__UPDATE_TIMER.Timer__Delta_Time,
                    e
                );

            Invoke__Descending
            (input_Mouse_Move);
        }

        private void Private_Handle__Mouse_Down__Game
        (
            object sender,
            MouseButtonEventArgs e
        )
        {
            SA__Input_Mouse_Button input_Mouse_Button =
                new SA__Input_Mouse_Button
                (
                    _Game__UPDATE_TIMER.Timer__Time_Elapsed,
                    _Game__UPDATE_TIMER.Timer__Delta_Time,
                    e
                );

            Invoke__Descending
            (input_Mouse_Button);
        }

        private void Private_Handle__Mouse_Up__Game
        (
            object sender,
            MouseButtonEventArgs e
        )
        {
            SA__Input_Mouse_Button input_Mouse_Button =
                new SA__Input_Mouse_Button
                (
                    _Game__UPDATE_TIMER.Timer__Time_Elapsed,
                    _Game__UPDATE_TIMER.Timer__Delta_Time,
                    e
                );

            Invoke__Descending
            (input_Mouse_Button);
        }

        private void Private_Handle__Key_Down__Game
        (
            object sender,
            KeyboardKeyEventArgs e
        )
        {
            SA__Input_Key_Down input_Key_Down =
                new SA__Input_Key_Down
                (
                    _Game__UPDATE_TIMER.Timer__Time_Elapsed,
                    _Game__UPDATE_TIMER.Timer__Delta_Time,
                    e
                );

            Invoke__Descending
            (input_Key_Down);
        }

        private void Private_Handle__Key_Up__Game
        (
            object sender,
            KeyboardKeyEventArgs e
        )
        {
            SA__Input_Key_Up input_Key_Up =
                new SA__Input_Key_Up
                (
                    _Game__UPDATE_TIMER.Timer__Time_Elapsed,
                    _Game__UPDATE_TIMER.Timer__Delta_Time,
                    e
                );

            Invoke__Descending
            (input_Key_Up);
        }

        private void Private_Invoke__Global__Game<T>
        (
            T streamline_Argument
        ) where T : Streamline_Argument
        {
            Invoke__Ascending
                (streamline_Argument);
            Invoke__Descending
                (streamline_Argument);
        }

        internal string[] Internal_Get__Shaders__Game()
        {
            return Handle_Get__Shaders__Game();
        }

        protected virtual string[] Handle_Get__Shaders__Game()
        {
            return new string[] { "shader" };
        }

        protected virtual void Handle_Register__Custom_Systems__Game() { }
        protected virtual void Handle_Load__Content__Game() { }
    }
}

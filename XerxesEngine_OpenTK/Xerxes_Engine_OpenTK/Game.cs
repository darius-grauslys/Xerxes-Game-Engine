using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using OpenTK.Graphics;
using Math_Helper = Xerxes.Tools.Math_Helper;
using Xerxes.Game_Engine;
using Xerxes.Xerxes_OpenTK.Exports.Input;
using OpenTK.Input;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Xerxes_OpenTK
{
    /// <summary>
    /// Contains a Game Window object, and hooks events to it.
    /// It downstreams these events using the generic Streamline_Arguments
    /// and downstreams. It takes a SA__Draw upstream.
    /// This object is parentless - Xerxes_Childless
    /// </summary>
    public class OpenTK_Game : 
        Game
    {
        public const string DEFAULT__ASSET_DIRECTORY_NAME = "Assets";
        public const string DEFAULT__SHADER_DIRECTORY_NAME = "Shaders";
        
        public const string DEFAULT__WINDOW_TITLE = "My XerxesEngine Game";

        public const int DEFAULT__WINDOW_WIDTH = 800;
        public const int DEFAULT__WINDOW_HEIGHT = 600;

        public static readonly string DEFAULT__ASSET_DIRECTORY =
            Path.Combine
            (
                AppDomain.CurrentDomain.BaseDirectory,
                DEFAULT__ASSET_DIRECTORY_NAME
            );
        public static readonly string DEFAULT__SHADER_DIRECTORY =
            Path.Combine
            (
                DEFAULT__ASSET_DIRECTORY,
                DEFAULT__SHADER_DIRECTORY_NAME
            );

        internal GameWindow Game__Game_Window__Internal { get; private set; }

        //PATHS
        public static string Game__Runtime_Directory { get; private set; }
        public static string Game__Directory_Assets { get; private set; }
        public static string Game__Directory_Shaders { get; private set; }

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

        public OpenTK_Game()
        {
            _Game__UPDATE_TIMER = new Timer(-1);
            _Game__RENDER_TIMER = new Timer(-1);

            Declare__Streams()
                .Downstream.Extending<SA__Game_Window_Resized>()

                .Downstream.Extending<SA__Input_Mouse_Button>()
                .Downstream.Extending<SA__Input_Mouse_Move>()

                .Downstream.Extending<SA__Input_Key_Down>()
                .Downstream.Extending<SA__Input_Key_Up>();
        }

        protected override void Handle__Configure__Root_Base(SA__Configure_Root e)
        {
            int window_width = DEFAULT__WINDOW_WIDTH , window_height = DEFAULT__WINDOW_HEIGHT;

            e.Check_For__Flag_Int__Configure_Root(nameof(window_width), ref window_width, false);
            e.Check_For__Flag_Int__Configure_Root(nameof(window_height), ref window_height, false);

            string window_title = DEFAULT__WINDOW_TITLE;

            e.Check_For__Flag_String__Configure_Root(nameof(window_title), ref window_title, false);

            Game__Game_Window__Internal = 
                new GameWindow
                (
                    window_width,
                    window_height,
                    GraphicsMode.Default,
                    window_title
                );

            Private_Hook__To_Game_Window__Game();

            Game__Runtime_Directory = AppDomain.CurrentDomain.BaseDirectory; 

            string asset_directory = 
                DEFAULT__ASSET_DIRECTORY;

            string shader_directory =
                DEFAULT__SHADER_DIRECTORY;

            e.Check_For__Flag_String__Configure_Root(nameof(asset_directory), ref asset_directory, false);
            e.Check_For__Flag_String__Configure_Root(nameof(shader_directory), ref shader_directory, false);

            Game__Directory_Assets = 
                Private_Validate__Directory__Game
                (
                    asset_directory,
                    DEFAULT__ASSET_DIRECTORY
                );
            Game__Directory_Shaders = 
                Private_Validate__Directory__Game
                (
                    shader_directory,
                    DEFAULT__SHADER_DIRECTORY
                );
        }

        protected override void Execute()
        {
            base.Execute();

            Log.Write__Info__Log
            (
                Log_Messages__OpenTK.INFO__GAME__RUN_INVOKED,
                this
            );

            Game__Game_Window__Internal.Run();
        }

        protected void Close()
        {
            Game__Game_Window__Internal
                .Close();
        }

        private void Private_Hook__To_Game_Window__Game()
        {
            Game__Game_Window__Internal.Resize += Private_Handle__Resize_Window__Game;
            Game__Game_Window__Internal.Closed += Private_Handle__Closed_Window__Game;

            Game__Game_Window__Internal.UpdateFrame += Private_Handle__Update_Window__Game;
            Game__Game_Window__Internal.RenderFrame += Private_Handle__Render_Window__Game;

            //Game__Game_Window__Internal.Unload += Private_Handle__Unload_Window__Game;

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

            Descend__Update(e.Time, _Game__UPDATE_TIMER.Timer__Time_Elapsed);
        }

        private void Private_Handle__Render_Window__Game(object sender, FrameEventArgs e)
        {
            _Game__RENDER_TIMER.Progress__Timer(e.Time);

            SA__Render_Begin__OpenTK render_Begin =
                new SA__Render_Begin__OpenTK();

            Descend__Render_Begin<SA__Render_Begin>(render_Begin);
            Ascend__Render_Begin<SA__Render_Begin>(render_Begin);

            Descend__Render(e.Time, _Game__RENDER_TIMER.Timer__Time_Elapsed);

            Ascend__Render_End();
            
            Game__Game_Window__Internal.SwapBuffers();
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
            SA__Input_Mouse_Move__OpenTK input_Mouse_Move = 
                new SA__Input_Mouse_Move__OpenTK
                (
                    e
                );

            Invoke__Descending<SA__Input_Mouse_Move>
            (input_Mouse_Move);
        }

        private void Private_Handle__Mouse_Down__Game
        (
            object sender,
            MouseButtonEventArgs e
        )
        {
            SA__Input_Mouse_Button__OpenTK input_Mouse_Button =
                new SA__Input_Mouse_Button__OpenTK
                (
                    e
                );

            Invoke__Descending<SA__Input_Mouse_Button>
            (input_Mouse_Button);
        }

        private void Private_Handle__Mouse_Up__Game
        (
            object sender,
            MouseButtonEventArgs e
        )
        {
            SA__Input_Mouse_Button__OpenTK input_Mouse_Button =
                new SA__Input_Mouse_Button__OpenTK
                (
                    e
                );

            Invoke__Descending<SA__Input_Mouse_Button>
            (input_Mouse_Button);
        }

        private void Private_Handle__Key_Down__Game
        (
            object sender,
            KeyboardKeyEventArgs e
        )
        {
            SA__Input_Key_Down__OpenTK input_Key_Down =
                new SA__Input_Key_Down__OpenTK
                (
                    e
                );

            Invoke__Descending<SA__Input_Key_Down>
            (input_Key_Down);
        }

        private void Private_Handle__Key_Up__Game
        (
            object sender,
            KeyboardKeyEventArgs e
        )
        {
            SA__Input_Key_Up__OpenTK input_Key_Up =
                new SA__Input_Key_Up__OpenTK
                (
                    e
                );

            Invoke__Descending<SA__Input_Key_Up>
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
    }
}

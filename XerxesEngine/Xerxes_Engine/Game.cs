using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;
using OpenTK.Graphics;
using Math_Helper = Xerxes_Engine.Tools.Math_Helper;
using Xerxes_Engine.Engine_Objects;
using Xerxes_Engine.Exports.Input;
using OpenTK.Input;

namespace Xerxes_Engine
{
    /// <summary>
    /// Contains a Game Window object, and hooks events to it.
    /// It downstreams these events using the generic Streamline_Arguments
    /// and downstreams. It takes a SA__Draw upstream.
    /// This object is parentless - Xerxes_Childless
    /// </summary>
    public class Game : 
        Xerxes_Object<Game>, 
        IXerxes_Ancestor_Of<Scene> 
    {
        internal GameWindow Game__GAME_WINDOW__Internal { get; }

        //PATHS
        public string Game__DIRECTORY__BASE { get; }
        public string Game__DIRECTORY__ASSETS { get; }
        public string Game__DIRECTORY__SHADERS { get; }
        
        #region Systems

        private Export_Dictionary _Game__EXPORTS { get; }

        #endregion

        #region Time
        private Timer _Game__UPDATE_TIMER { get; }
        private Timer _Game__RENDER_TIMER { get; }

        public double Game__Elapsed_Time__Update => _Game__UPDATE_TIMER.Timer__Time_Elapsed;
        public double Game__Delta_Time__Update   => _Game__UPDATE_TIMER.Timer__Delta_Time;
        public double Game__Elapsed_Time__Render => _Game__RENDER_TIMER.Timer__Time_Elapsed;
        public double Game__Delta_Time__Render   => _Game__RENDER_TIMER.Timer__Delta_Time;
        #endregion

        public int Game__Window_Width => Game__GAME_WINDOW__Internal.Width;
        public int Game__Window_Height => Game__GAME_WINDOW__Internal.Height;
        public Vector2 Get__Window_Size__Game()
            => new Vector2(Game__Window_Width, Game__Window_Height);

        public float Get__Window_Hypotenuse__Game()
            => Math_Helper.Get__Hypotenuse(Game__Window_Width, Game__Window_Height);
        
        public Game()
            : this (Game_Arguments.Get__Default__Game_Arguments())
        {}

        public Game(Game_Arguments game_Argument)
        {
            _Game__UPDATE_TIMER = new Timer(-1);
            _Game__RENDER_TIMER = new Timer(-1);

            _Game__EXPORTS =
                new Export_Dictionary();

            Declare__Streams()
                .Downstream.Extending<SA__Associate_Root>()
                .Upstream  .Extending<SA__Associate_Root>()
                .Downstream.Extending<SA__Update>()
                .Downstream.Extending<SA__Render_Begin>()
                .Upstream  .Extending<SA__Render_Begin>()
                .Downstream.Extending<SA__Render>()
                .Upstream  .Extending<SA__Render_End>()
                .Downstream.Extending<SA__Resize_2D>()

                .Downstream.Extending<SA__Input_Mouse_Button>()
                .Downstream.Extending<SA__Input_Mouse_Move>()

                .Downstream.Extending<SA__Input_Key_Down>()
                .Downstream.Extending<SA__Input_Key_Up>();

            Game__GAME_WINDOW__Internal = 
                new GameWindow
                (
                    (int)
                    (
                        game_Argument?.Game_Arguments__WINDOW_WIDTH 
                        ?? Game_Arguments.Game_Arguments__DEFAULT_WINDOW_WIDTH
                    ),
                    (int)
                    (
                        game_Argument?.Game_Arguments__WINDOW_HEIGHT
                        ?? Game_Arguments.Game_Arguments__DEFAULT_WINDOW_HEIGHT
                    ),

                    GraphicsMode.Default, 
                    
                    game_Argument?.Game_Arguments__WINDOW_TITLE
                    ?? Game_Arguments.Game_Arguments__DEFAULT_WINDOW_TITLE
                );

            Log.Initalize__Log
            (
                game_Argument
            );

            Game__DIRECTORY__BASE = AppDomain.CurrentDomain.BaseDirectory; 
            Game__DIRECTORY__ASSETS = 
                Private_Validate__Directory__Game
                (
                    game_Argument.Game_Arguments__ASSET_DIRECTORY,
                    Game_Arguments.Game_Arguments__DEFAULT_ASSET_DIRECTORY
                );
            Game__DIRECTORY__SHADERS = 
                Private_Validate__Directory__Game
                (
                    game_Argument.Game_Arguments__SHADER_DIRECTORY,
                    Game_Arguments.Game_Arguments__DEFAULT_SHADER_DIRECTORY
                );

            Private_Hook__To_Game_Window__Game();
        }

        public void Declare__Export__Game<T>
        (
            T export
        ) where T : Xerxes_Export
        {
            Log.Internal_Write__Verbose__Log
            (
                Log.VERBOSE__GAME__DECLARING_EXPORT_1,
                this,
                export
            );
            _Game__EXPORTS
                .Internal_Declare__Export__Export_Dictionary
                (
                    export
                );
        }
        
        public void Run__Game()
        {
            bool gameHasAncestry = 
                Xerxes_Linker
                .Internal_Seal__Game(this, _Game__EXPORTS);

            if (!gameHasAncestry)
            {
                Private_Log_Error__Game_Lacks_Ancestry
                (
                    this
                );
                return;
            }

            SA__Associate_Root e = 
                new SA__Associate_Root
                (
                    -1,-1,
                    Game__DIRECTORY__BASE,
                    Game__DIRECTORY__ASSETS,
                    Game__DIRECTORY__SHADERS,

                    Game__Window_Width,
                    Game__Window_Height,

                    Internal_Get__Shaders__Game()
                );

            Invoke__Ascending
                (e);

            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__CONTENT_LOADING, this);
            Handle_Load__Content__Game();
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__GAME__CONTENT_LOADED, this);

            Invoke__Descending
                (e);

            Log.Internal_Write__Info__Log
            (
                Log.INFO__GAME__RUN_INVOKED,
                this
            );

            Game__GAME_WINDOW__Internal.Run();
        }

        private void Private_Hook__To_Game_Window__Game()
        {
            Game__GAME_WINDOW__Internal.Resize += Private_Handle__Resize_Window__Game;
            Game__GAME_WINDOW__Internal.Closed += Private_Handle__Closed_Window__Game;

            Game__GAME_WINDOW__Internal.UpdateFrame += Private_Handle__Update_Window__Game;
            Game__GAME_WINDOW__Internal.RenderFrame += Private_Handle__Render_Window__Game;

            Game__GAME_WINDOW__Internal.Load += Private_Handle__Load_Window__Game;
            Game__GAME_WINDOW__Internal.Unload += Private_Handle__Unload_Window__Game;

            Game__GAME_WINDOW__Internal.MouseDown += Private_Handle__Mouse_Down__Game;
            Game__GAME_WINDOW__Internal.MouseUp   += Private_Handle__Mouse_Up__Game;
            Game__GAME_WINDOW__Internal.MouseMove += Private_Handle__Mouse_Move__Game;

            Game__GAME_WINDOW__Internal.KeyDown += Private_Handle__Key_Down__Game;
            Game__GAME_WINDOW__Internal.KeyUp += Private_Handle__Key_Up__Game;
        }

        private string Private_Validate__Directory__Game
        (
            string baseDirectory,
            string recoveryDirectory
        )
        {
            if (baseDirectory == null || !Directory.Exists(baseDirectory))
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log.ERROR__GAME__DIRECTORY_NOT_FOUND_1,
                    this,
                    baseDirectory
                );

                Log.Internal_Write__Warning__Log(Log.WARNING__RECOVERY__ASSET_DIRECTORY_NOT_FOUND, this);


                baseDirectory = Private_Get__Validated_Default_Directory__Game(recoveryDirectory);

                if (baseDirectory != null)
                    return baseDirectory;

                Log.Internal_Panic__Log(Log.ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1, this, recoveryDirectory);

                Game__GAME_WINDOW__Internal.Close();
            }

            return baseDirectory;
        }

        private string Private_Get__Validated_Default_Directory__Game(string defaultDirectory)
        {
            if (!Directory.Exists(defaultDirectory))
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log.ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1,
                    this,
                    defaultDirectory
                );

                return null;
            }

            return defaultDirectory;
        }

        private void Private_Handle__Resize_Window__Game(object sender, EventArgs e)
        {
            GL.Viewport(Game__GAME_WINDOW__Internal.ClientRectangle);

            SA__Resize_2D resize_2D_Argument = 
                new SA__Resize_2D
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
                    Game__Elapsed_Time__Update, 
                    e.Time
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
                    Game__Elapsed_Time__Render, 
                    e.Time
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
            
            Game__GAME_WINDOW__Internal.SwapBuffers();
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
            SA__Dissassociate_Game dissassociate_Game =
                new SA__Dissassociate_Game
                (
                    _Game__UPDATE_TIMER.Timer__Time_Elapsed,
                    _Game__UPDATE_TIMER.Timer__Delta_Time
                );

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

#region Static Logging
        private static void Private_Log_Error__Game_Lacks_Ancestry
        (
            Game game
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__GAME__LACKS_ANCESTRY,
                game
            );
        }
#endregion
    }
}

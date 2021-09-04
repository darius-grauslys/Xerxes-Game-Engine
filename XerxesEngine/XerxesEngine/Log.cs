using System;
using System.Collections.Generic;
using System.IO;

namespace XerxesEngine
{
    public static class Log
    {
#region CONST INTERNAL INFO MESSAGES
        internal const string INFO__LOG_INITALIZE_4                             = "XERXES-ENGINE LOGGER INITALIZED - {0}, {1}, {2}, {3}";

        internal const string INFO__RECOVERY__ASSET_DIRECTORY_NOT_FOUND         = "Asset Directory not found, attempting to use default directory";
#endregion
#region CONST INTERNAL VERBOSE MESSAGES
        internal const string VERBOSE__GAME__SYSTEMS__INITALIZING               = "INITALIZING BASE SYSTEMS";
        internal const string VERBOSE__GAME__SYSTEMS__INITALIZED                = "FINISHED INITALIZING BASE SYSTEMS";

        internal const string VERBOSE__GAME__SYSTEMS__REGISTERING               = "REGISTERING BASE SYSTEMS";
        internal const string VERBOSE__GAME__SYSTEMS__REGISTERED                = "FINISHED REGISTERING BASE SYSTEMS";

        internal const string VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERING        = "REGISTERING CUSTOM SYSTEMS";
        internal const string VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERED         = "FINISHED REGISTERING CUSTOM SYSTEMS";

        internal const string VERBOSE__GAME__SYSTEMS__LOADING                   = "LOADING SYSTEMS";
        internal const string VERBOSE__SYSTEM__LOAD                             = "Loading System";
        internal const string VERBOSE__GAME__SYSTEM__LOADED_1                   = "System Loaded    - {0}";
        internal const string VERBOSE__GAME__ALL_SYSTEMS__LOADED                = "FINISHED LOADING SYSTEMS";
        
        internal const string VERBOSE__GAME__SYSTEMS__UNLOADING                 = "UNLOADING SYSTEMS";
        internal const string VERBOSE__SYSTEM__UNLOAD                           = "Unloading System";
        internal const string VERBOSE__GAME__SYSTEM__UNLOADED_1                 = "System Unloaded  - {0}";
        internal const string VERBOSE__GAME__SYSTEMS__UNLOADED                  = "FINISHED UNLOADING SYSTEMS";
        
        internal const string VERBOSE__GAME__BASE_EVENT_SCHEDULER__LOADING      = "LOADING BASE EVENT SCHEDULER";
        
        internal const string VERBOSE__RENDER_SERVICE__LOAD_SHADERS             = "LOADING SHADERS";
        internal const string VERBOSE__RENDER_SERVICE__LOAD_SHADER_1            = "Loading Shader   - {0}";

        internal const string VERBOSE__GAME__CONTENT_LOADING                    = "LOADING CONTENT";
        internal const string VERBOSE__GAME__SPRITE_LOAD_1                      = "Loading Sprite   - {0}";
        internal const string VERBOSE__GAME__CONTENT_LOADED                     = "FINISHED LOADING CONTENT";
#endregion
#region CONST INTERNAL WARNING MESSAGES
        internal const string WARNING__GAME__SYSTEM__ALREADY_LOADED_1           = "Similar Typed System already loaded - {0}";

        
#endregion
#region CONST INTERNAL ERROR MESSAGES
        internal const string ERROR__PANIC                                      = "An unrecoverable error has occured";

        internal const string ERROR__GAME__DIRECTORY_NOT_FOUND_1                = "The Directory:\"{0}\" was not found!";
        internal const string ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1       = "The Recovery Directory:\"{0}\" was not found!";

        internal const string ERROR__SYSTEM__NOT_FOUND_1                        = "The System:\"{0}\" is not loaded!";

        internal const string ERROR__GAME__CONTENT_SPRITE_NOT_FOUND_2           = "The Sprite:\"{0}\" was not found under path:\"{1}\"!";
#endregion
        private static readonly Queue<Log_Message> _Log__MESSAGES = new Queue<Log_Message>(); 
        private static readonly Queue<Log_Message> _Log__WARNINGS = new Queue<Log_Message>();
        private static readonly Queue<Log_Message> _Log__ERRORS   = new Queue<Log_Message>();

        public static Log_Verbosity Log__Verbosity { get; private set; }

        public static bool Log__Throw_On_Error   { get; private set; }
        public static bool Log__Throw_On_Warning { get; private set; }

        private static TextWriter Log__Out { get; set; }

        internal static void Initalize__Log
        (  
            Game_Arguments game_Arguments
        )
        {
            Log__Verbosity = game_Arguments.Game_Arguments__LOG_VERBOSITY;
            Log__Throw_On_Error = game_Arguments.Game_Arguments__LOG__THROW_ON_ERRORS;
            Log__Throw_On_Warning = game_Arguments.Game_Arguments__LOG__THROW_ON_WARNINGS;
            Log__Out = game_Arguments.Game_Arguments__LOG__OUT;

            Internal_Write__Info__Log
            (
                String.Format
                (
                    INFO__LOG_INITALIZE_4,
                    Log__Verbosity,
                    String.Format("Log__Throw_On_Error:{0}", Log__Throw_On_Error),
                    String.Format("Log__Throw_On_Warning:{0}", Log__Throw_On_Warning),
                    String.Format("Log__Out:{0}", Log__Out)
                ),
                null,
                0
            );
        }

        public static void Write__Log(Log_Message message)
        {
            Private_Write__Log(message);
        }

        public static void Write__Verbose__Log(string format, object source, double time=0, params string[] args)
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Message__Verbose,
                    source,
                    time,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Info__Log(string format, object source, double time=0, params string[] args)
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Message__Info,
                    time,
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Verbose__Log(string format, object source, double time=0, params string[] args)
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Message__Verbose, 
                    time, 
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Warning__Log(Log_Message_Type messageType, string format, object source, double time=0, params string[] args)
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    messageType,
                    time,
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Error__Log(Log_Message_Type messageType, string format, object source, double time=0, params string[] args)
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    messageType,
                    time,
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Panic__Log()
        {
            Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Error__Unrecoverable,
                    0,
                    null,
                    ERROR__PANIC
                )
            );
        }

        private static void Private_Write__Log(Log_Message message)
        {
            if ((int)message.Log_Message__VERBOSITY >= (int)Log__Verbosity)
                Private_Write__To_Out__Log(message);

            switch(message.Log_Message__VERBOSITY)
            {
                case Log_Verbosity.Normal:
                case Log_Verbosity.Verbose:
                    Private_Write__Normal__Log(message);
                    return;
                case Log_Verbosity.Strict:
                    Private_Write__Strict__Log(message);
                    return;
                case Log_Verbosity.Critical:
                    Private_Write__Critical__Log(message);
                    return;
            }
        }

        private static void Private_Write__Normal__Log(Log_Message message)
        {
            _Log__MESSAGES.Enqueue(message);
        }

        private static void Private_Write__Strict__Log(Log_Message message)
        {
            _Log__WARNINGS.Enqueue(message);
            if (Log__Throw_On_Warning)
                throw new XerxesEngine_Exception(message);
        }

        private static void Private_Write__Critical__Log(Log_Message message)
        {
            _Log__ERRORS.Enqueue(message);
            if(Log__Throw_On_Error)
                throw new XerxesEngine_Exception(message);
        }

        private static void Private_Write__To_Out__Log(Log_Message message)
        {
            Log__Out?.WriteLine(message);            
            Log__Out?.WriteLine();
        }
    }
}

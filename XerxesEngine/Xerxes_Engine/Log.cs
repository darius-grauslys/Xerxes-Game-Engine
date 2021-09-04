using System;
using System.Collections.Generic;
using System.IO;

namespace Xerxes_Engine
{
    public static class Log
    {
#region CONST INTERNAL INFO MESSAGES
        internal const string 
        INFO__LOG_INITALIZE_4                                     = "XERXES-ENGINE LOGGER INITALIZED - {0}, {1}, {2}, {3}",

        INFO__EVENT_HANDLER__RECURRING_NAME_2                     = "An event handle by the name of:\"{0}\" already exists. Using:\"{1}\" instead.",

        INFO__COMPONENT__ENABLED_ON_PARENT_BIND                   = "Component was enabled when bounded to a non-null parent.",
#endregion
#region CONST INTERNAL VERBOSE MESSAGES
        VERBOSE__GAME__SYSTEMS__INITALIZING                       = "INITALIZING BASE SYSTEMS.",
        VERBOSE__GAME__SYSTEMS__INITALIZED                        = "FINISHED INITALIZING BASE SYSTEMS.",

        VERBOSE__GAME__SYSTEMS__REGISTERING                       = "REGISTERING BASE SYSTEMS.",
        VERBOSE__GAME__SYSTEMS__REGISTERED                        = "FINISHED REGISTERING BASE SYSTEMS.",

        VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERING                = "REGISTERING CUSTOM SYSTEMS.",
        VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERED                 = "FINISHED REGISTERING CUSTOM SYSTEMS.",

        VERBOSE__GAME__SYSTEMS__LOADING                           = "LOADING SYSTEMS.",
        VERBOSE__SYSTEM__LOAD                                     = "Loading System.",
        VERBOSE__GAME__SYSTEM__LOADED_1                           = "System Loaded    - {0}.",
        VERBOSE__GAME__ALL_SYSTEMS__LOADED                        = "FINISHED LOADING SYSTEMS.",
        
        VERBOSE__GAME__SYSTEMS__UNLOADING                         = "UNLOADING SYSTEMS.",
        VERBOSE__SYSTEM__UNLOAD                                   = "Unloading System.",
        VERBOSE__GAME__SYSTEM__UNLOADED_1                         = "System Unloaded  - {0}.",
        VERBOSE__GAME__SYSTEMS__UNLOADED                          = "FINISHED UNLOADING SYSTEMS.",
        
        VERBOSE__GAME__BASE_EVENT_SCHEDULER__LOADING              = "LOADING BASE EVENT SCHEDULER.",
        
        VERBOSE__RENDER_SERVICE__LOAD_SHADERS                     = "LOADING SHADERS.",
        VERBOSE__RENDER_SERVICE__LOAD_SHADER_1                    = "Loading Shader   - {0}.",

        VERBOSE__GAME__CONTENT_LOADING                            = "LOADING CONTENT.",
        VERBOSE__GAME__SPRITE_LOAD_1                              = "Loading Sprite   - {0}.",
        VERBOSE__GAME__CONTENT_LOADED                             = "FINISHED LOADING CONTENT.",
#endregion
#region CONST INTERNAL WARNING MESSAGES
        WARNING__RECOVERY__ASSET_DIRECTORY_NOT_FOUND              = "Asset Directory not found, attempting to use default directory.",

        WARNING__FLAGGED_TO_THROW                                 = "Logger is flagged to throw in this state.",
        WARNING__GAME__SYSTEM__ALREADY_LOADED_1                   = "Similar Typed System already loaded - {0}.", 

        WARNING__EVENT_SCHEDULER__CHECK__EVENT_NOT_FOUND_1        = "Event handle:\"{0}\" was not found when seeing if it was active.",
        WARNING__EVENT_SCHEDULER__REMOVE__EVENT_NOT_FOUND_1       = "Event handle:\"{0}\" was not found when trying to remove it.",
        WARNING__EVENT_SCHEDULER__INVOKE__EVENT_NOT_FOUND_1       = "Event handle:\"{0}\" was not found when tyring to invoke it.",

        WARNING__SYSTEM__SPRITE_LIBRARY__RECOVERING_TO_DEFAULT    = "Sprite Library resorted to returning the sprite at index 0.",

        WARNING__COMPONENT__PARENT_IS_NULL                        = "Component was disabled when attached to a null parent.",

        WARNING__ANIMATION_RENDER_COMPONENT__BAD_NEGATIVE_SPEED_1 = "An animation node speed of non-default negative value:\"{0}\" was given. Using default.",
#endregion
#region CONST INTERNAL ERROR MESSAGES
        ERROR__GAME__DIRECTORY_NOT_FOUND_1                        = "The Directory:\"{0}\" was not found!",
        ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1               = "The Recovery Directory:\"{0}\" was not found!",

        ERROR__SYSTEM__NOT_FOUND_1                                = "The System:\"{0}\" is not loaded!",

        ERROR__GAME__CONTENT_SPRITE_NOT_FOUND_2                   = "The Sprite:\"{0}\" was not found under path:\"{1}\"!",
        ERROR__SYSTEM__SPRITE_LIBRARY__SPRITE_NOT_FOUND_1         = "The Sprite:\"{0}\" was not found in the library.",
        ERROR__SYSTEM__SPRITE_LIBRARY__SPRITE_ID_NOT_FOUND_1      = "The Sprite ID:\"{0}\" is out of bounds.",

        ERROR__ANIMATION__NODE_DEFINITION__OUT_OF_BOUNDS_2        = "Attempted to define node:\"{0}\" when total node count is:\"{0}\"",

        ERROR__PANIC                                              = "An unrecoverable error has occured.";
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

            Private_Write__Logger_Settings__Log();
        }

        private static void Private_Write__Logger_Settings__Log()
        {
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

        internal static void Internal_Write__Log
        (
            Log_Message_Type messageType, 
            string format="", 
            object source=null, 
            params object[] args
        )
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    messageType,
                    source,
                    String.Format(format, args)
                )
            );
        }

        public static void Write__Verbose__Log
        (
            string format="", 
            object source=null, 
            params object[] args
        )
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Message__Verbose,
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Info__Log
        (
            string format, 
            object source, 
            params object[] args
        )
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Message__Info,
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Verbose__Log
        (
            string format, 
            object source, 
            params object[] args
        )
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Message__Verbose, 
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Write__Warning__Log
        (
            string format, 
            object source, 
            params object[] args
        )
        {
            Private_Write__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Warning__Alert,
                    source,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Panic__Log
        (
            string format, 
            object sender, 
            params object[] args
        )
        {
            Internal_Panic__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Error__Unrecoverable,
                    sender,
                    String.Format(format, args)
                )
            );
        }

        internal static void Internal_Panic__Log(Log_Message panicMessage)
        {
            Private_Write__Panic__Log();
            throw new Xerxes_Engine_Exception(panicMessage);
        }

        private static void Private_Write__Panic__Log()
        {
            Private_Write__To_Out__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Error__Unrecoverable,
                    internal_message: ERROR__PANIC
                )
            );
        }

        private static void Private_Write__Log(Log_Message message)
        {
            if ((int)message.Log_Message__VERBOSITY < (int)Log__Verbosity)
                return;

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
            Private_CheckIf__Should_Throw__Log(message, Log__Throw_On_Warning);
        }

        private static void Private_Write__Critical__Log(Log_Message message)
        {
            _Log__ERRORS.Enqueue(message);
            Private_CheckIf__Should_Throw__Log(message, Log__Throw_On_Error);
        }

        private static void Private_CheckIf__Should_Throw__Log(Log_Message message, bool flag)
        {
            if(!flag)
                return;

            Private_Write__To_Out__Log
            (
                new Log_Message
                (
                    Log_Message_Type.Warning__Alert,
                    internal_message: WARNING__FLAGGED_TO_THROW
                )
            );
            Internal_Panic__Log(message);
        }

        private static void Private_Write__To_Out__Log(Log_Message message)
        {
            Log__Out?.WriteLine(message);            
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace Xerxes_Engine
{
    public static class Log
    {
#region CONST INTERNAL INFO MESSAGES
        internal const string 
        INFO__LOG_INITALIZE_4                                       = "XERXES-ENGINE LOGGER INITALIZED - {0}, {1}, {2}, {3}",

        INFO__GAME__RUN_INVOKED                                     = "The game has begun running.",

        INFO__EVENT_HANDLER__RECURRING_NAME_2                       = "An event handle by the name of:\"{0}\" already exists. Using:\"{1}\" instead.",

        INFO__COMPONENT__ENABLED_ON_PARENT_BIND                     = "Component was enabled when bounded to a non-null parent.",
#endregion
#region CONST INTERNAL VERBOSE MESSAGES
        VERBOSE__GAME__SYSTEMS__INITALIZING                         = "INITALIZING BASE SYSTEMS.",
        VERBOSE__GAME__SYSTEMS__INITALIZED                          = "FINISHED INITALIZING BASE SYSTEMS.",

        VERBOSE__GAME__SYSTEMS__REGISTERING                         = "REGISTERING BASE SYSTEMS.",
        VERBOSE__GAME__SYSTEMS__REGISTERED                          = "FINISHED REGISTERING BASE SYSTEMS.",

        VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERING                  = "REGISTERING CUSTOM SYSTEMS.",
        VERBOSE__GAME__SYSTEMS_CUSTOM__REGISTERED                   = "FINISHED REGISTERING CUSTOM SYSTEMS.",

        VERBOSE__GAME__SYSTEMS__LOADING                             = "LOADING SYSTEMS.",
        VERBOSE__SYSTEM__LOAD                                       = "Loading System.",
        VERBOSE__GAME__SYSTEM__LOADED_1                             = "System Loaded    - {0}.",
        VERBOSE__GAME__ALL_SYSTEMS__LOADED                          = "FINISHED LOADING SYSTEMS.",
        
        VERBOSE__GAME__SYSTEMS__UNLOADING                           = "UNLOADING SYSTEMS.",
        VERBOSE__SYSTEM__UNLOAD                                     = "Unloading System.",
        VERBOSE__GAME__SYSTEM__UNLOADED_1                           = "System Unloaded  - {0}.",
        VERBOSE__GAME__SYSTEMS__UNLOADED                            = "FINISHED UNLOADING SYSTEMS.",
        
        //VERBOSE__GAME__BASE_EVENT_SCHEDULER__LOADING                = "LOADING BASE EVENT SCHEDULER.",
        
        VERBOSE__RENDER_SERVICE__LOAD_SHADERS                       = "LOADING SHADERS.",
        VERBOSE__RENDER_SERVICE__LOAD_SHADER_1                      = "Loading Shader   - {0}.",

        VERBOSE__GAME__CONTENT_LOADING                              = "LOADING CONTENT.",
        VERBOSE__GAME__SPRITE_LOAD_1                                = "Loading Sprite   - {0}.",
        VERBOSE__GAME__CONTENT_LOADED                               = "FINISHED LOADING CONTENT.",

        VERBOSE__SCENE_MANAGER__LOUSY_LOOKUP_1                      = "Lousy scene lookup:\"{0}\". Not loop friendly. Consider using Distinct_Handle over string?",

        VERBOSE__SPRITE_SHEET__USING_COUNT_CONSTRAINT_2             = "Utilizing count constraint:{0}. Available sprite count:{1}.",
#endregion
#region CONST INTERNAL WARNING MESSAGES
        WARNING__RECOVERY__ASSET_DIRECTORY_NOT_FOUND                = "Asset Directory not found, attempting to use default directory.",

        WARNING__FLAGGED_TO_THROW                                   = "Logger is flagged to throw in this state.",
        WARNING__GAME__SYSTEM__ALREADY_LOADED_1                     = "Similar Typed System already loaded - {0}.", 

        WARNING__EVENT_SCHEDULER__CHECK__EVENT_NOT_FOUND_1          = "Event handle:\"{0}\" was not found when seeing if it was active.",
        WARNING__EVENT_SCHEDULER__REMOVE__EVENT_NOT_FOUND_1         = "Event handle:\"{0}\" was not found when trying to remove it.",
        WARNING__EVENT_SCHEDULER__INVOKE__EVENT_NOT_FOUND_1         = "Event handle:\"{0}\" was not found when tyring to invoke it.",

        WARNING__SCENE_MANAGER__LOUSY_LOOKUP_FAILED_1               = "Lousy lookup failed:\"{0}\". Consider using Distinct_Handle over string?",
        WARNING__SCENE_MANAGER__ADDING_SCENE_UNDER_NULL_ALIAS_1     = "Adding scene:\"{0}\" under a null alias! Is this intentional? Use String.Empty instead.",

        WARNING__SPRITE_LIBRARY__RECOVERING_TO_DEFAULT              = "Sprite Library resorted to returning the sprite at index 0.",

        WARNING__SPRITE_SHEET__SUB_DIMENSION_DOES_NOT_DIVIDE_3      = "The given sub-dimension:{0} for parameter {1} does not nicely divide {2}.",
        WARNING__VERTEX_OBJECT_LIBRARY__COUNT_CONSTRAINT_INVALID_2  = "The given sprite-count constraint of:{0} exceeds that which is available:{1}.",

        WARNING__VERTEX_OBJECT__ATYPICAL_INDEXING_2                 = "Modifying vertex object with an atypical index. This might cause unexpected behavior."
                                                                    + "Was given index for modifcation:{0}, but typical generator in index set:{1}.",

        WARNING__STREAMLINE__INVOKED_BUT_NOT_SOURCE_1               = "Streamline was invoked with:{0} but the point of invocation is not a source."
                                                                    + "Invocation was ignored.",

        WARNING__XERXES_ENGINE_OBJECT__REDUNDANT_SEALING            = "This engine object has already been sealed. Ignoring sealing invokation.",
        WARNING__XERXES_ENGINE_OBJECT__UNLINKED_STREAMLINE_2        = "The streamline:{0} did not find a destination during association to:{1}.",
        
        WARNING__COMPONENT__PARENT_IS_NULL                          = "Component was disabled when attached to a null parent.",
        WARNING__GAME_OBJECT_COMPONENT__UTILIZED_WHILE_DISABLED_1C  = "Component was not intended to be used while disabled. May cause problems. Action:\"{0}\".",

        WARNING__ANIMATION_RENDER_COMPONENT__BAD_NEGATIVE_SPEED_1   = "An animation node speed of non-default negative value:\"{0}\" was given. Using default.",
#endregion
#region CONST INTERNAL ERROR MESSAGES
        ERROR__GAME__DIRECTORY_NOT_FOUND_1                          = "The Directory:\"{0}\" was not found!",
        ERROR__GAME__RECOVERY_DIRECTORY_NOT_FOUND_1                 = "The Recovery Directory:\"{0}\" was not found!",

        ERROR__SYSTEM__NOT_FOUND_1                                  = "The System:\"{0}\" is not loaded!",

        ERROR__ASSET_PIPE__FILE_NOT_FOUND_1                         = "The file:\"{0}\" was not found!",

        ERROR__SCENE_MANAGER__SWITCHED_TO_NULL_SCENE_1              = "Attemped to switch to null scene under lousy alias:\"{0}\". Not switching scenes!",
        ERROR__SCENE_MANAGER__CANNOT_ADD_NULL_SCENE_1               = "Attempted to add a null scene under alias:\"{0}\". Returning error scene handle!",

        ERROR__SPRITE_LIBRARY__SPRITE_NOT_FOUND_1                   = "The Sprite:\"{0}\" was not found in the library!",
        ERROR__SPRITE_LIBRARY__SPRITE_ID_NOT_FOUND_1                = "The Sprite ID:\"{0}\" is out of bounds!",

        ERROR__XERXES_ENGINE_OBJECT__INVALID_PARENT_ASSOCIATION_2   = "This engine object already has a parental association!" 
                                                                    + "Tried to associate to:{0} - is already associated to {1}!",
        ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION             = "Cannot be associated to! I am sealed!",
        ERROR__XERXES_ENGINE_OBJECT__FAILED_TO_DECLARE_STREAMLINE_1 = "Failed to declare streamline:{0}!",
        ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED_TO_ROOT      = "Tried to access root object prior to being rooted!",
        ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED              = "Tried to access associated parent, but is not associated!",
        ERROR__XERXES_ENGINE_OBJECT__INVALID_ASSOCIATION_4          = "Engine object:{0} tried to associate to object:{1} which is lower in hierarchy!"
                                                                    + "Target object's hierarchy score:{2} - Associating object's score:{3}!",
        ERROR__XERXES_ENGINE_OBJECT__FAILED_ASSOCIATION_2           = "Engine objects {0} -> {1} failed to associate!",
        ERROR__XERXES_ENGINE_OBJECT__UNLINKED_MANDATORY_STREAMLINE_2= "Mandatory streamline:{0} did not find a destination during association to:{1}!",

        ERROR__GAME_OBJECT__FAILED_TO_ASSOCIATE_COMPONENT_2C        = "Failed to associate component:{0}! Contextual Message:{1}!",
        ERROR__GAME_OBJECT__ASSOCIATED_ANCESTOR_IS_INVALID_1        = "Tried to associate with:{0}, but it is not a Scene Layer! Games Objects can "
                                                                    + "only associate with Scene Layers!",
        
        ERROR__GAME_COMPONENT__FAILED_TO_ASSOCIATE_1         = "Failed to associate to engine object:{0}! Components can only associate to Game Objects!",
        ERROR__GAME_OBJECT_COMPONENT__NOT_ASSOCIATED_TO_ROOT_1C     = "This component was utilized while not rooted, and depends on a rooted enviroment!"
                                                                    + "Contextual Message:{0}!",
        ERROR__GAME_OBJECT_COMPONENT__UTILIZED_WHILE_DISABLED_1C    = "This component was utilized while disabled, and only functions while enabled!"
                                                                    + "Contextual Message:{0}!",

        ERROR__STATE_MACHINE__PANIC_ON_STATE_ENTRY_1                = "State Machine Panicked! Current state:{0}!",
        ERROR__STATE_MACHINE__FAILED_TO_DEFINE_FLOW_1C              = "Failed to define State Flow! Contextual Message:{0}!",
        ERROR__STATE_MACHINE__FAILED_TO_REQUEST_STATE_1C            = "Failed to request State Transition! Contextual Message:{0}!",

        ERROR__TYPED_STATE_MACHINE__REPETATIVE_KEY_1                = "Another state of an equivalent type has already been registered! Tried to register:{0}!",
        ERROR__TYPED_STATE_MACHINE__TYPE_NOT_PRESENT_1              = "The given type:{0} is not present!",

        ERROR__VERTEX_OBJECT__INVALID_SUB_LENGTH_2                  = "The provided sub-length:{0} has an invalid value:{1}!",
        ERROR__VERTEX_OBJECT__INVALID_MODIFICATION_INDEX_2          = "Modification of the Vertex Object was out of bounds! The given modification index:{0}"
                                                                    + "is less than 0 or greater than the current size:{1}!",
        ERROR__VERTEX_OBJECT__MODIFICATION_OUT_OF_BOUNDS_3          = "Modification of this Vertex Object is out of bounds! The given modication index:{0} "
                                                                    + "plus the length of the modification:{1} exceeds the current size:{2}!",
        ERROR__VERTEX_OBJECT__MODIFICATION_METHOD_IS_INVALID_1      = "The given modification method for vertex modification - {0} is invalid!",

        ERROR__ANIMATION__NODE_DEFINITION__OUT_OF_BOUNDS_2          = "Attempted to define node:\"{0}\" when total node count is:\"{0}\"!",

        ERROR__PANIC                                                = "An unrecoverable error has occured.";
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
                    String.Format(format, args),
                    source
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
                    String.Format(format, args),
                    source
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
            throw new Xerxes_Exception(panicMessage);
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

using System;
using System.Collections.Generic;
using System.IO;

namespace Xerxes
{
    public static class Log
    {
#region CONST INTERNAL INFO MESSAGES
        internal const string 
        INFO__LOG_INITALIZE_4                                       = "XERXES-ENGINE LOGGER INITALIZED - {0}, {1}, {2}, {3}",
#endregion
#region CONST INTERNAL VERBOSE MESSAGES
        VERBOSE__XERXES_ANCESTRY__USING_GLOBAL_DECLARATION_1        = "Using global definition for ancestry of {0}.",
        VERBOSE__XERXES_ANCESTRY__USING_ANONYMOUS_DECLARATION_1     = "Using anonymous definition for ancestry of {0}.",

        VERBOSE__XERXES_LINKER__SEALING_OBJECT_1                    = "Sealing object {0}.",

        VERBOSE__ROOT__DECLARING_EXPORT_1                           = "Declaring Export    - {0}.",
#endregion
#region CONST INTERNAL WARNING MESSAGES
        WARNING__FLAGGED_TO_THROW                                   = "Logger is flagged to throw in this state.",

        WARNING__STREAMLINE__INVOKED_BUT_NOT_SOURCE_1               = "Streamline was invoked with:{0} but the point of invocation is not a source."
                                                                    + "Invocation was ignored.",

        WARNING__XERXES_ENGINE_OBJECT__REDUNDANT_SEALING            = "This engine object has already been sealed. Ignoring sealing invokation.",
        WARNING__XERXES_ENGINE_OBJECT__UNLINKED_STREAMLINE_2        = "The streamline:{0} did not find a destination during association to:{1}.",

        WARNING__XERXES_LINKER_CONTEXT__UNCAUGHT_STREAMLINE_3C      = "Uncaught streamline:{1} of type {0}! Missing receiver on {2}!",

        WARNING__XERXES_CHILDLESS__REDUNDANT_CONSTRUCTION           = "This Xerxes_Childless object was constructed. There is no need to construct them.",
        
#endregion
#region CONST INTERNAL ERROR MESSAGES
        ERROR__ROOT__LACKS_ANCESTRY                                 = "Root does not possess an ancestry!",

        ERROR__XERXES_ENGINE_OBJECT__INVALID_PARENT_ASSOCIATION_2   = "This engine object already has a parental association!" 
                                                                    + "Tried to associate to:{0} - is already associated to {1}!",
        ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION             = "Cannot be associated to! I am sealed!",
        ERROR__XERXES_ENGINE_OBJECT__FAILED_TO_DECLARE_STREAMLINE_2C= "Failed to declare streamline: {0}, under context [{1}]!",
        ERROR__XERXES_OBJECT_BASE__FAILED_TO_SUBSCRIBE_STREAMLINE_2C= "Failed to subscribe onto streamline: {0}, under context [{1}]!",
        ERROR__XERXES_OBJECT_BASE__ARGUMENT_CONSUMED_2C             = "Streamline Argument {0} was invoked but is consumed!",
        ERROR__XERXES_ENGINE_OBJECT__UNLINKED_MANDATORY_STREAMLINE_2= "Mandatory streamline:{0} did not find a destination during association to:{1}!",
        ERROR__XERXES_ENGINE_OBJECT__STREAMLINE_NOT_FOUND_1         = "Streamline: {0} is not defined!",

        ERROR__XERXES_ANCESTRY__FAILED_ASSOCIATION_2                = "Engine objects {0} -> {1} failed to associate!",
        ERROR__XERXES_ANCESTRY__FAILED_TO_FIND_ANCESTOR_1           = "Failed to find {0} during hierarchy declaration!",
        ERROR__XERXES_ANCESTRY__FAILED_TO_FIND_DESCENDANT_1         = "Failed to find {0} during hierarchy declaration!",

        ERROR__EXPORT_DICTIONARY__DUPLICATE_DECLARATION_1           = "Duplicate export declaration of {0} detected!",

        ERROR__XERXES_EXPORT__DECLARED_BUT_NOT_ROOTED_1             = "Tried to declare exportline receiver of type {0} but is not rooted!",

        ERROR__PANIC                                                = "An unrecoverable error has occured.",
#endregion
#region CONST INTERNAL BUG MESSAGES
        //These are errors which can only occur
        //independent of API client's code.
        
        BUG__XERXES_LINKER_CONTEXT__INCOHERENT_CONTEXT_POP_1        = "Linker context popped with incoherence for streamline type: {0}.",
#endregion
#region CONST INTERNAL CRITICAL MESSAGES
        CRITICAL__XERXES_ENGINE_OBJECT__ROOT_CANNOT_ASSOCIATE_1     = "Root cannot associate to another object! Root can only be associated to!",
        CRITICAL__XERXES_ENGINE_OBJECT__ILLEGAL_DEFINITION_1        = "Is illegally defined where it's reflective generic parameter is {0} and not itself!";
#endregion
#region CONTEXT DEFINTIONS
        public enum Context__Declare_Streamline 
        { Receieve, Extend, Source }
        public enum Context__Stream
        { Upstream, Downstream }
#endregion
        private static readonly Queue<Log_Message> _Log__MESSAGES = new Queue<Log_Message>(); 
        private static readonly Queue<Log_Message> _Log__WARNINGS = new Queue<Log_Message>();
        private static readonly Queue<Log_Message> _Log__ERRORS   = new Queue<Log_Message>();

        public static Log_Verbosity Log__Verbosity { get; private set; }

        public static bool Log__Throw_On_Error   { get; private set; }
        public static bool Log__Throw_On_Warning { get; private set; }

        private static TextWriter Log__Out { get; set; }

        private static bool _Log__Is_Initalized { get; set; }

        public static void Initalize__Log
        (  
            Log_Arguments log_Arguments
        )
        {
            if (_Log__Is_Initalized)
                return;
            _Log__Is_Initalized = false;

            Log__Verbosity = log_Arguments.Log_Arguments__Verbosity;
            Log__Throw_On_Error = log_Arguments.Log_Arguments__Throw_On_Error;
            Log__Throw_On_Warning = log_Arguments.Log_Arguments__Throw_On_Warning;
            Log__Out = log_Arguments.Log_Arguments__Log_Out;

            Private_Write__Logger_Settings__Log();
        }

        private static void Private_Write__Logger_Settings__Log()
        {
            Write__Info__Log
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

        public static void Write__Log
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

        public static void Write__Info__Log
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

        public static void Write__Warning__Log
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

        public static void Write__Error__Log
        (
            string format,
            object source,
            Log_Message_Type error_type = Log_Message_Type.Error__System,
            params object[] args
        )
        {
            int error_code = (int)error_type;
            if (error_code % 2 != 0)
                error_type = Log_Message_Type.Error__System;
            Private_Write__Log
            (
                new Log_Message
                (
                    error_type,
                    source,
                    String.Format(format, args)
                )
            );
        }

        public static void Panic__Log
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

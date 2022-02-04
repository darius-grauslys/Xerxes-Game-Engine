using System;
using System.Collections.Generic;
using System.Text;

namespace Xerxes
{
    public struct Log_Message
    {
        public const string Log_Message__TAG_VERBOSE            = "Verbose";
        public const string Log_Message__TAG_INFO               = "Info";
        public const string Log_Message__TAG_WARNING            = "Warning";
        public const string Log_Message__TAG_ERROR              = "Error";
        public const string Log_Message__TAG_INTERNAL           = "Internal";
        public const string Log_Message__TAG_EXTERNAL           = "External";

        public const char Log_Message__META_OPEN_DELIMITER      = '[';
        public const char Log_Message__META_CLOSE_DELIMITER     = ']';

        public const string Log_Message__TAG_SEPERATOR          = "::";

        private static readonly Dictionary<string, Log_Verbosity> _Log_Message__VERBOSITY_BY_TAG_TABLE
            = new Dictionary<string, Log_Verbosity>()
        {
            {Log_Message__TAG_VERBOSE, Log_Verbosity.Verbose},
            {Log_Message__TAG_INFO, Log_Verbosity.Normal},
            {Log_Message__TAG_WARNING, Log_Verbosity.Strict},
            {Log_Message__TAG_ERROR, Log_Verbosity.Critical}
        };

        public Log_Message_Type Log_Message__TYPE           { get; }
        public bool             Log_Message__Is_INTERNAL    { get; }
        public Type             Log_Message__SOURCE         { get; }
        public string           Log_Message__TIME           { get; }
        public string           Log_Message__MESSAGE        { get; }
        internal Log_Verbosity  Log_Message__VERBOSITY      { get; }

        internal Log_Message
        (
            Log_Message_Type messageType,
            object source = null,
            string internal_message = ""
        )
        {
            Log_Message__TYPE = messageType;
            Log_Message__Is_INTERNAL = true;
            Log_Message__SOURCE = source?.GetType();
            Log_Message__TIME = DateTime.Now.ToString("hh:mm:ss tt");
            Log_Message__MESSAGE = internal_message;
            Log_Message__VERBOSITY = Determine__Verbosity(messageType);
        }

        public Log_Message
        (
            Log_Message_Type messageType,
            string message = "",
            object source = null
        )
        {
            Log_Message__TYPE = messageType;
            Log_Message__Is_INTERNAL = false;
            Log_Message__SOURCE = source?.GetType();
            Log_Message__TIME = DateTime.Now.ToString("hh:mm:ss tt");
            Log_Message__MESSAGE = message;
            Log_Message__VERBOSITY = Determine__Verbosity(messageType);
        }

        public override string ToString()
        {
            string source = Log_Message__SOURCE?.ToString() ?? "";
            int sourceCutOffIndex = source.LastIndexOf('.')+1;
            source = source.Substring(sourceCutOffIndex);

            string metaString = Get__MetaString
            (
                Log_Message__Is_INTERNAL 
                    ? Log_Message__TAG_INTERNAL 
                    : Log_Message__TAG_EXTERNAL,
                Log_Message__TYPE.ToString(),
                source,
                Log_Message__TIME
            );

            return String.Format
            (
                "{0}{1}", 
                metaString, 
                (Log_Message__MESSAGE != null && Log_Message__MESSAGE.Length > 0) 
                    ? String.Format(" - {0}", Log_Message__MESSAGE) 
                    : ""
            );
        }

        private static string Get__MetaString(params string[] args)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            for(int i=args.Length-1;i>=0;i--)
            {
                if (args[i] == null || args[i].Length == 0)
                    continue;

                stringBuilder.Insert(0, args[i]);

                if (i>0)
                    stringBuilder.Insert(0,Log_Message.Log_Message__TAG_SEPERATOR);
            }

            return String.Format
            (
                "{0}{1}{2}",
                Log_Message__META_OPEN_DELIMITER,
                stringBuilder.ToString(),
                Log_Message__META_CLOSE_DELIMITER
            );
        }

        public static Log_Verbosity Determine__Verbosity(string message)
        {
            foreach(string key in _Log_Message__VERBOSITY_BY_TAG_TABLE.Keys)
                if (message.Contains(key))
                    return _Log_Message__VERBOSITY_BY_TAG_TABLE[key];
            return Log_Verbosity.Verbose;
        }

        public static Log_Verbosity Determine__Verbosity(Log_Message_Type messageType)
        {
            int messageCode = (int)messageType;

            if (messageCode == 0)
                return Log_Verbosity.Normal;
            if (messageCode > 0)
                return Log_Verbosity.Verbose;
            if (messageCode < 0 && messageCode % 2 == 0)
                return Log_Verbosity.Strict;

            return Log_Verbosity.Critical;
        }
    }
}

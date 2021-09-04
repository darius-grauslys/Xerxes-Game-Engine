using System;

namespace XerxesEngine
{
    public struct Log_Message
    {
        public Log_Message_Type Log_Message__TYPE           { get; }
        public bool             Log_Message__Is_INTERNAL    { get; }
        private string          Log_Message__SOURCE         { get; }
        public double           Log_Message__TIME           { get; }
        public string           Log_Message__MESSAGE        { get; }
        internal Log_Verbosity  Log_Message__VERBOSITY      { get; }

        internal Log_Message
        (
            Log_Message_Type messageType,
            double time,
            object source,
            string message = ""
        )
        {
            Log_Message__TYPE = messageType;
            Log_Message__Is_INTERNAL = true;
            Log_Message__SOURCE = source?.ToString() ?? "";
            Log_Message__TIME = time;
            Log_Message__MESSAGE = message;
            Log_Message__VERBOSITY = Determine__Verbosity(messageType);
        }

        public Log_Message
        (
            Log_Message_Type messageType,
            object source,
            double time,
            string message = ""
        )
        {
            Log_Message__TYPE = messageType;
            Log_Message__Is_INTERNAL = false;
            Log_Message__SOURCE = source?.ToString() ?? "";
            Log_Message__TIME = time;
            Log_Message__MESSAGE = message;
            Log_Message__VERBOSITY = Determine__Verbosity(messageType);
        }

        public override string ToString()
        {
            return String.Format
            (
                "[{0}{1}::{2}::({3})]{4}{5}",
                (Log_Message__Is_INTERNAL) ? "" : "External-",
                Log_Message__TYPE,
                Log_Message__SOURCE,
                Log_Message__TIME,
                Log_Message__MESSAGE.Length > 0 ? " - " : "",
                Log_Message__MESSAGE
            );
        }

        private static Log_Verbosity Determine__Verbosity(Log_Message_Type messageType)
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

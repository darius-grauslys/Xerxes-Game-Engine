using System;

namespace XerxesEngine
{
    public class XerxesEngine_Exception : Exception
    {
        public Log_Message Exception__MESSAGE { get; }

        internal XerxesEngine_Exception
        (
            Log_Message message
        )
        : base
        (
            message.Log_Message__MESSAGE
        )
        {
        }

        public override string ToString()
        {
            return String.Format
            (
                "{0}{1}",
                "[!!THROWN!!]",
                Exception__MESSAGE.ToString()
            );
        }
    }
}

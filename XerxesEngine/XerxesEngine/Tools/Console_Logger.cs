using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XerxesEngine.Tools
{
    public class Console_Logger : TextWriter
    {
        private Dictionary<string,ConsoleColor> _Console_Logger__COLORING_KEYWORDS { get; }

        public override Encoding Encoding => Encoding.Default;

        public Console_Logger()
        {
            _Console_Logger__COLORING_KEYWORDS = new Dictionary<string,ConsoleColor>()
            {
                {Log_Message.Log_Message__PREFIX_VERBOSE, ConsoleColor.DarkCyan},
                {Log_Message.Log_Message__PREFIX_INFO, ConsoleColor.Cyan},
                {Log_Message.Log_Message__PREFIX_WARNING, ConsoleColor.Yellow},
                {Log_Message.Log_Message__PREFIX_ERROR, ConsoleColor.Red}
            };
        }

        public override void Write(string value)
        {
            int indexOfDivider = value.IndexOf(':');
            int indexOfDelimiter = value.IndexOf(']');
            int indexOfExternal = value.IndexOf(Log_Message.Log_Message__PREFIX_EXTERNAL);
            
            if (indexOfDivider < 0)
                indexOfDivider = value.Length-1;
            if (indexOfDelimiter < 0)
                indexOfDelimiter = value.Length-1;
            if (indexOfExternal < 0)
                indexOfExternal = 0;

            int startIndexOfMetaString = 
            (
                indexOfExternal > 0
                ? indexOfExternal + Log_Message.Log_Message__PREFIX_EXTERNAL.Length
                : 0
            );

            string bracket = value.Substring(0, indexOfExternal);
            string externalTag = (indexOfExternal > 0) ? Log_Message.Log_Message__PREFIX_EXTERNAL : "";
            string metaString_Prefix = value.Substring(startIndexOfMetaString, indexOfDivider);
            string metaString_Suffix = value.Substring(indexOfDivider, indexOfDelimiter - indexOfDivider);
            string infoString = value.Substring(indexOfDelimiter+1);
            
            ConsoleColor metaColor = ConsoleColor.DarkCyan;

            foreach(string key in _Console_Logger__COLORING_KEYWORDS.Keys)
            {
                if(metaString_Prefix.Contains(key))
                    metaColor = _Console_Logger__COLORING_KEYWORDS[key];
            }

            Console.ForegroundColor = metaColor;

            Console.Write(bracket);

            ConsoleColor infoStringColor =
                (indexOfExternal > 0)
                ? Console.ForegroundColor = ConsoleColor.Gray
                : Console.ForegroundColor = ConsoleColor.White;

            Console.ForegroundColor = infoStringColor;

            Console.Write(externalTag);

            Console.ForegroundColor = metaColor;

            Console.Write(metaString_Prefix);
            Console.Write(metaString_Suffix);

            Console.ForegroundColor = infoStringColor;

            Console.Write(infoString);
        }

        public override void Write(char value)
        {
            Console.Write(value);
        }
    }
}

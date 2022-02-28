using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Xerxes.Tools
{
    public class Console_Logger : TextWriter
    {
        private const ConsoleColor Console_Logger__INTERIOR_INFO_COLOR  = ConsoleColor.Gray;
        private const ConsoleColor Console_Logger__EXTERIOR_INFO_COLOR  = ConsoleColor.White;
        private const ConsoleColor Console_Logger__CONSOLE_COLOR        = ConsoleColor.Black;

        private const string 
            Console_Logger__VERBOSE_FLAG    = "[V]",
            Console_Logger__INFO_FLAG       = "[I]",
            Console_Logger__WARNING_FLAG    = "[W]",
            Console_Logger__ERROR_FLAG      = "[E]";


        private Dictionary<string,ConsoleColor> _Console_Logger__META_COLORING_KEYWORDS { get; }

        private Dictionary<Log_Verbosity,string> _Console_Logger__FLAG_TABLE { get; }

        public override Encoding Encoding => Encoding.Default;

        public Console_Logger()
        {
            Console.BackgroundColor = Console_Logger__CONSOLE_COLOR;
            Console.ForegroundColor = Console_Logger__INTERIOR_INFO_COLOR;

            _Console_Logger__META_COLORING_KEYWORDS = new Dictionary<string,ConsoleColor>()
            {
                {Log_Message.Log_Message__TAG_VERBOSE,  ConsoleColor.DarkCyan   },
                {Log_Message.Log_Message__TAG_INFO,     ConsoleColor.Cyan       },
                {Log_Message.Log_Message__TAG_WARNING,  ConsoleColor.DarkYellow },
                {Log_Message.Log_Message__TAG_ERROR,    ConsoleColor.Red        }
            };

            _Console_Logger__FLAG_TABLE = new Dictionary<Log_Verbosity, string>()
            {
                {Log_Verbosity.Verbose,     Console_Logger__VERBOSE_FLAG        },
                {Log_Verbosity.Normal,      Console_Logger__INFO_FLAG           },
                {Log_Verbosity.Strict,      Console_Logger__WARNING_FLAG        },
                {Log_Verbosity.Critical,    Console_Logger__ERROR_FLAG          }
            };
        }

        public override void WriteLine(string value)
        {
            Write(value);
            Console.Write('\n');
        }

        public override void Write(string value)
        {
            //value is in the form of:
            //  [`{IsInternal}`-{messageType}`:`:`{source}`:`:`{time}] `- `{message}

            int indexOfMetaDelimiter = value.IndexOf(Log_Message.Log_Message__META_CLOSE_DELIMITER);
            if (0 > indexOfMetaDelimiter)
            {
                Console.Write(value);
                return;
            }

            //First Get the meta string, sub[0 - indexOf(']')]
            string metaString = value.Substring(0, indexOfMetaDelimiter+1);
            string infoString = value.Substring(indexOfMetaDelimiter+1);

            string[] interiorStrings = metaString.Split
            (
                Log_Message.Log_Message__META_OPEN_DELIMITER,
                Log_Message.Log_Message__META_CLOSE_DELIMITER
            );
            string interiorMeta = interiorStrings[1];

            string[] tags = interiorMeta.Split
            (
                new string[] {Log_Message.Log_Message__TAG_SEPERATOR}, 
                StringSplitOptions.RemoveEmptyEntries
            );

            bool isInternal = tags[0] == Log_Message.Log_Message__TAG_INTERNAL;

            string messageTypeTag = tags[1];

            Log_Verbosity verbosity = Log_Message.Determine__Verbosity(messageTypeTag);
            string flag = _Console_Logger__FLAG_TABLE[verbosity];

            ConsoleColor metaTextColor = ConsoleColor.DarkCyan;
            foreach(string key in _Console_Logger__META_COLORING_KEYWORDS.Keys)
                if(messageTypeTag.Contains(key))
                    metaTextColor = _Console_Logger__META_COLORING_KEYWORDS[key];

            Console.BackgroundColor =
                metaTextColor;
            Console.ForegroundColor = 
                isInternal
                ? Console_Logger__INTERIOR_INFO_COLOR
                : Console_Logger__EXTERIOR_INFO_COLOR;
            Console.Write
            (
                flag
            );
            Console.BackgroundColor = Console_Logger__CONSOLE_COLOR;

            Console.ForegroundColor = metaTextColor;

            Console.Write(metaString);

            Console.BackgroundColor = Console_Logger__CONSOLE_COLOR;

            Console.Write(infoString);
        }

        public override void Write(char value)
        {
            Console.Write(value);
        }
    }
}

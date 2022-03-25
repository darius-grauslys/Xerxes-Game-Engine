
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xerxes
{
    public sealed class SA__Configure_Root :
        Streamline_Argument
    {
        private string[] _Configure_Root__ARGUMENTS { get; }
        public string[] Raw_ARGUMENTS
            => _Configure_Root__ARGUMENTS.ToArray();

        public bool Has_Flags 
            => _Configure_Root__PARSED_ARGUMENTS.Count > 0;

        private Dictionary<string, List<string>> _Configure_Root__PARSED_ARGUMENTS { get; }

        public SA__Configure_Root(params string[] arguments)
        {
            _Configure_Root__ARGUMENTS =
                arguments.ToArray();

            _Configure_Root__PARSED_ARGUMENTS =
                Private_Parse__Arguments(arguments);
        }

        public bool Check_For__Flag(string flag, bool log_failure_find=true)
        {
            bool contains =
                _Configure_Root__PARSED_ARGUMENTS.ContainsKey(flag);

            if (log_failure_find && !contains)
                Log.Write__Warning__Log($"Missing flag:{flag} for configuration!", this);
            
            return contains;
        }

        public bool Check_For__Flag_String(string flag, ref string flag_value, bool log_failure_find=true)
        {
            bool contains = 
                Check_For__Flag(flag, log_failure_find);

            if (contains)
                flag_value = _Configure_Root__PARSED_ARGUMENTS[flag][0];

            return contains;
        }

        public bool Check_For__Flag_Enum<TEnum>
        (
            string flag,
            ref TEnum flag_value,
            bool log_failure_find = true,
            bool log_failure_parse = true,
            bool ignore_case = false
        )
        where TEnum : struct
        {
            string flag_string_value = "";

            bool contains =
                Check_For__Flag_String(flag, ref flag_string_value, log_failure_find);

            if (!contains)
                return false;

            TEnum try_value = default(TEnum);

            contains = 
                Enum.TryParse(flag_string_value, ignore_case, out try_value);

            if (!contains && log_failure_parse)
            {
                //TODO: Standardize these logs.
                Log.Write__Warning__Log($"Found flag:{flag} but flag value:{flag_string_value} is not an enum!", this);
                return false;
            }

            flag_value = try_value;

            return true;
        }

        public bool Check_For__Flag_Int
        (
            string flag, 
            ref int flag_value,
            bool log_failure_find = true,
            bool log_failure_parse = true
        )
        {
            string flag_string_value = "";

            bool contains =
                Check_For__Flag_String(flag, ref flag_string_value, log_failure_find);

            if (!contains)
                return false;

            int try_value = 0;

            contains = 
                int.TryParse(flag_string_value, out try_value);

            if (!contains && log_failure_parse)
            {
                Log.Write__Warning__Log($"Found flag:{flag} but flag value:{flag_string_value} is not an integer!", this);
                return false;
            }

            flag_value = try_value;

            return true;
        }

        public bool Check_For__Flag_List
        (
            string flag,
            ref string[] strings,
            bool log_failure_find = true
        )
        {
            if(!Check_For__Flag(flag, log_failure_find))
                return false;

            strings = 
                _Configure_Root__PARSED_ARGUMENTS[flag]
                    .ToArray();

            return true;
        }

        private const string FLAG_IDENTIFIER = "--";

        private static Dictionary<string, List<string>> Private_Parse__Arguments(string[] arguments)
        {
            Dictionary<string, List<string>> parsed_arguments =
                new Dictionary<string, List<string>>();

            if (arguments == null)
                return parsed_arguments;

            string flag = null;
            string parsed_string;

            for(int i=0;i < arguments.Length;i++)
            {
                bool is_flag =
                    Private_Check_If__Flag(arguments[i], out parsed_string);

                if (is_flag)
                {
                    flag = parsed_string;
                    if (!parsed_arguments.ContainsKey(flag))
                        parsed_arguments.Add(flag, new List<string>());

                    continue;
                }

                if (flag == null)
                    continue;

                parsed_arguments[flag]
                    .Add(parsed_string);
            }

            return parsed_arguments;
        }

        private static bool Private_Check_If__Flag(string arg, out string flag)
        {
            bool is_flag = arg.IndexOf(FLAG_IDENTIFIER) > -1;

            if (!is_flag)
            {
                flag = arg;

                return false;
            }

            flag = arg.Substring(FLAG_IDENTIFIER.Length);

            return true;
        }
    }
}

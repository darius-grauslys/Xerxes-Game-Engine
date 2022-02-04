
using System;
using System.Collections.Generic;
using System.Linq;

namespace Xerxes
{
    public class SA__Configure_Root :
        Streamline_Argument
    {
        private string[] _Configure_Root__ARGUMENTS { get; }
        public string[] Configure_Root__ARGUMENTS
            => _Configure_Root__ARGUMENTS.ToArray();

        private Dictionary<string, string> _Configure_Root__PARSED_ARGUMENTS { get; }

        public SA__Configure_Root(params string[] arguments)
        {
            _Configure_Root__ARGUMENTS =
                arguments.ToArray();

            _Configure_Root__PARSED_ARGUMENTS =
                Private_Parse__Arguments(arguments);
        }

        public bool Check_For__Flag__Configure_Root(string flag, bool log_failure_find=true)
        {
            bool contains =
                _Configure_Root__PARSED_ARGUMENTS.ContainsKey(flag);

            if (!contains)
                Log.Write__Warning__Log($"Missing flag:{flag} for configuration!", this);
            
            return contains;
        }

        public bool Check_For__Flag_String__Configure_Root(string flag, ref string flag_value, bool log_failure_find=true)
        {
            bool contains = 
                Check_For__Flag__Configure_Root(flag, log_failure_find);

            if (contains)
                flag_value = _Configure_Root__PARSED_ARGUMENTS[flag];

            return contains;
        }

        public bool Check_For__Flag_Enum__Configure_Root<TEnum>
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
                Check_For__Flag_String__Configure_Root(flag, ref flag_string_value, log_failure_find);

            if (!contains)
                return false;

            TEnum try_value = default(TEnum);

            contains = 
                Enum.TryParse(flag_string_value, ignore_case, out try_value);

            if (!contains && log_failure_parse)
            {
                //TODO: Standardize these logs.
                Log.Write__Info__Log($"Found flag:{flag} but flag value:{flag_string_value} is not an enum!", this);
                return false;
            }

            flag_value = try_value;

            return true;
        }

        public bool Check_For__Flag_Int__Configure_Root
        (
            string flag, 
            ref int flag_value,
            bool log_failure_find = true,
            bool log_failure_parse = true
        )
        {
            string flag_string_value = "";

            bool contains =
                Check_For__Flag_String__Configure_Root(flag, ref flag_string_value, log_failure_find);

            if (!contains)
                return false;

            int try_value = 0;

            contains = 
                int.TryParse(flag_string_value, out try_value);

            if (!contains && log_failure_parse)
            {
                Log.Write__Info__Log($"Found flag:{flag} but flag value:{flag_string_value} is not an integer!", this);
                return false;
            }

            flag_value = try_value;

            return true;
        }

        private static Dictionary<string, string> Private_Parse__Arguments(string[] arguments)
        {
            Dictionary<string, string> parsed_arguments =
                new Dictionary<string, string>();

            if (arguments == null)
                return parsed_arguments;

            foreach(string arg in arguments)
            {
                if (arg != string.Empty && arg[0] == '-')
                {
                    int delimiter_index =
                        arg.IndexOf(':');

                    string flag;
                    string value;

                    if (delimiter_index < 0 || delimiter_index == arg.Length -1)
                    {
                        flag = arg.Substring(1);
                        value = flag;
                    }
                    else
                    {
                        flag = arg.Substring(1,delimiter_index-1);
                        value = arg.Substring(delimiter_index+1);
                    }

                    parsed_arguments.Add(flag, value);
                }
            }

            return parsed_arguments;
        }
    }
}

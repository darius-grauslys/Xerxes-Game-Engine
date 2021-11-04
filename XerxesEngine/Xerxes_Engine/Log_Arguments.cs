using System.IO;

namespace Xerxes_Engine
{
    public sealed class Log_Arguments
    {
        public Log_Verbosity    Log_Arguments__Verbosity { get; private set; }
        public bool             Log_Arguments__Throw_On_Error { get; private set; }
        public bool             Log_Arguments__Throw_On_Warning { get; private set; }

        internal TextWriter     Log_Arguments__Log_Out  { get; private set; }

        public Log_Arguments
        (
            Log_Verbosity verbosity = Log_Verbosity.Verbose,
            bool throwOnError = true,
            bool throwOnWarning = false,
            TextWriter logOut = null
        )
        {
            Log_Arguments__Verbosity = verbosity;
            Log_Arguments__Throw_On_Error = throwOnError;
            Log_Arguments__Throw_On_Warning = throwOnWarning;
            Log_Arguments__Log_Out = logOut ?? System.Console.Out;
        }
    }
}

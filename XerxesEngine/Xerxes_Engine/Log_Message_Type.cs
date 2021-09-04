namespace Xerxes_Engine
{
    public enum Log_Message_Type
    {
        // Noise

        Message__Info = 0,

        Message__Verbose = 1,
        
        // Warnings
        
        Warning__Alert = -2,
        Warning__Depreciated = -4,
        Warning__Critical = -6,

        // Errors
        
        Error__Critical = -1,
        Error__Unrecoverable = -3,

        Error__IO = -5,

        Error__System = -7,

        Error__Animation = -9
    }
}

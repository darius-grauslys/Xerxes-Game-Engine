namespace isometricgame.GameEngine
{
    public enum Log_Message_Type
    {
        // Noise

        Message__Info = 0,

        Message__Verbose = 1,
        
        // Warnings
        
        Warning__Info = -2,
        Warning__Game__Load_System__Similar_Typed_System_Already_Loaded = -4,

        // Errors
        
        Error__Critical = -1,
        Error__Unrecoverable = -3,

        Error__Directory_Not_Found = -5,
        Error__Recovery_Directory_Not_Found = -7,

        Error__System_Not_Found = -9,
        
        Error__Content_Path_Not_Found = -11
    }
}

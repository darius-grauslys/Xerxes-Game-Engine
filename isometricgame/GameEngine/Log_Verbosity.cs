namespace isometricgame.GameEngine
{
    public enum Log_Verbosity
    {
        // Info, Warnings, and Errors.
        Normal = 0,
        
        // Actions, Info, Warnings, and Errors.
        Verbose = -1,

        // Warnings, and Errors.
        Strict = 2,

        // Only Errors.
        Critical = 3,
    }
}

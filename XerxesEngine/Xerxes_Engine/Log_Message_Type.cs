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

        Error__Infastructure = -7,
        Error__Engine_Object = -9,
        Error__System = -11,

        /// <summary>
        /// For errors which involve setting up rendering objects
        /// such as Sprites, Meshes, etc.
        /// </summary>
        Error__Rendering_Setup = -13
    }
}

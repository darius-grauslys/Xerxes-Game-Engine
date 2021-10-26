namespace Xerxes_Engine
{
    /// <summary>
    /// Represents a type in Xerxes_Engine that depends on the
    /// Update/Render control flow. All internalized logging messages
    /// are related to such objects - or systems.
    ///
    /// Xerxes_Object requires a self reference to T. Any other type
    /// will cause a critical error.
    ///
    /// Calls to Update and Render are internalized. Exposure to
    /// handling these calls are given via protected virtual definitions.
    /// </summary>
    public class Xerxes_Object<T> : Xerxes_Object_Base where T : Xerxes_Object_Base
    {
        public Xerxes_Object() 
        {
            if (!(this is T))
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Critical,
                    Log.CRITICAL__XERXES_ENGINE_OBJECT__ILLEGAL_DEFINITION_1,
                    this,
                    typeof(T)
                );
                return;
            }
        }
    }
}

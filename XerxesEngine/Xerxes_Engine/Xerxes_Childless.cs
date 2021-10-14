namespace Xerxes_Engine
{
    public sealed class Xerxes_Childless
        : Xerxes_Object    
    {
        public Xerxes_Childless()
        {
            Log.Internal_Write__Verbose__Log
            (
                Log.WARNING__XERXES_CHILDLESS__REDUNDANT_CONSTRUCTION,
                this
            );
        }
    }
}

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
    public class Xerxes_Object<TThis> : Xerxes_Object_Base where TThis : Xerxes_Object_Base, new()
    {
        public Xerxes_Object() 
        {
            if (!(this is TThis))
            {
                Log.Write__Log
                (
                    Log_Message_Type.Error__Critical,
                    Log.CRITICAL__XERXES_ENGINE_OBJECT__ILLEGAL_DEFINITION_1,
                    this,
                    typeof(TThis)
                );
                return;
            }
        }

        protected Xerxes_Ancestry<TThis> Declare__Hierarchy()
        {
            Xerxes_Ancestry<TThis> hierarchy = 
                new Xerxes_Ancestry<TThis>(this);

            Xerxes_Linker
                .Internal_Set__Declaration(this, hierarchy);

            return hierarchy;
        }

        protected void Declare__Ancestor<Xerxes_Ancestor>()
        where Xerxes_Ancestor : Xerxes_Object_Base
        {
            Xerxes_Association_Rule_Dictionary
                .Internal_Declare__Ruling(this, new Xerxes_Ancestry_Rule<Xerxes_Ancestor,TThis>());
        }

        protected void Declare__Descendant<Xerxes_Descendant>()
        where Xerxes_Descendant : Xerxes_Object_Base
        {
            Xerxes_Association_Rule_Dictionary
                .Internal_Declare__Ruling(this, new Xerxes_Ancestry_Rule<TThis,Xerxes_Descendant>());
        }
    }
}

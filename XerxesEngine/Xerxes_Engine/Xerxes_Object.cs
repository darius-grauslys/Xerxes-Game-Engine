using System;

namespace Xerxes
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
    public class Xerxes_Object<TThis> : Xerxes_Object_Base where TThis : Xerxes_Object_Base
    {
        internal Xerxes_Ancestry Xerxes_Object__ANCESTRY__Internal { get; }

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

            Xerxes_Object__ANCESTRY__Internal =
                new Xerxes_Ancestry(this);

            Xerxes_Linker
                .Internal_Set__Declaration(this, Xerxes_Object__ANCESTRY__Internal);
        }

        protected internal Xerxes_Ancestry Declare__Hierarchy()
        {
            return Xerxes_Object__ANCESTRY__Internal;
        }

        protected void Declare__Field<TType>
        (
            Func<TType> getter, 
            Func<TType,TType> setter = null
        )
        {
            //TODO: Log
            if (getter == null)
                return;

            Declare__Streams()
                .Downstream.Receiving
                <SA__Field_Get<TThis, TType>>
                ((e) => e.Field_Get__Returning_Value = getter());

            if (setter == null)
                return;

            Declare__Streams()
                .Downstream.Receiving
                <SA__Field_Set<TThis, TType>>
                ((e) => e.Field_Get__Returning_Value = setter(e.Field__SET_VALUE));
        }

        protected TType Get__Descendent_Field<Target, TType>()
            where Target : Xerxes_Object_Base
        {
            SA__Field_Get<Target, TType> getter = 
                new SA__Field_Get<Target, TType>();

            Invoke__Descending(getter);

            return getter.Field_Get__Returning_Value;
        }

        protected TType Set__Descendent_Field<Target, TType>
        (
            TType value
        )
        where Target : Xerxes_Object_Base
        {
            SA__Field_Set<Target, TType> setter =
                new SA__Field_Set<Target, TType>(value);

            Invoke__Descending(setter);

            return setter.Field_Get__Returning_Value;
        }

        protected TType Get__Ancestor_Field<Target, TType>()
            where Target : Xerxes_Object_Base
        {
            SA__Field_Get<Target, TType> getter = 
                new SA__Field_Get<Target, TType>();

            Invoke__Ascending(getter);

            return getter.Field_Get__Returning_Value;
        }

        protected TType Set__Ancestor_Field<Target, TType>
        (
            TType value
        )
        where Target : Xerxes_Object_Base
        {
            SA__Field_Set<Target, TType> setter =
                new SA__Field_Set<Target, TType>(value);

            Invoke__Ascending(setter);

            return setter.Field_Get__Returning_Value;
        }

        protected void Extend__Getter<Target, TType>()
            where Target : Xerxes_Object_Base
            => Declare__Streams().Downstream.Extending<SA__Field_Get<Target, TType>>();

        protected void Extend__Setter<Target, TType>()
            where Target : Xerxes_Object_Base
            => Declare__Streams().Downstream.Extending<SA__Field_Set<Target, TType>>();
    }
}

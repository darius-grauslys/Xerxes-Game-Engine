using System;

namespace Xerxes_Engine
{
    public class Xerxes_Engine_Object
    {
        internal const int Xerxes_Engine_Object__ASSOCIATION_PRIORITY__GAME = 0;

        internal bool Xerxes_Engine_Object__Is_Disabled__Internal { get; set; }
        internal bool Xerxes_Engine_Object__Is_Sealed__Internal { get; set; }

        internal int Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal { get; }

        internal Xerxes_Engine_Object Xerxes_Engine_Object__PARENT__Internal { get; }
        internal event Action<Frame_Argument> Xerxes_Engine_Object__UPDATE_SUBSCRIPTION__Internal;
        internal event Action<Frame_Argument> Xerxes_Engine_Object__RENDER_SUBSCRIPTION__Internal;

        internal Xerxes_Engine_Object(int associationPriority)
        {
            associationPriority = 
                Xerxes_Engine.Tools.Math_Helper
                .Clamp__Integer
                (
                    Xerxes_Engine_Object__ASSOCIATION_PRIORITY__GAME+1
                );
            Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal = associationPriority;
        }

        internal virtual void Internal_Update__Xerxes_Engine_Object(Frame_Argument e)
        {
            if(Xerxes_Engine_Object__Is_Disabled__Internal)
                return;

            Handle_Update__Xerxes_Engine_Object(e);

            Xerxes_Engine_Object__UPDATE_SUBSCRIPTION__Internal?
                .Invoke(e);
        }

        protected virtual void Handle_Update__Xerxes_Engine_Object(Frame_Argument e)
        { }

        internal virtual void Internal_Render__Xerxes_Engine_Object(Frame_Argument e)
        {
            if(Xerxes_Engine_Object__Is_Disabled__Internal)
                return;

            Handle_Render__Xerxes_Engine_Object(e);

            Xerxes_Engine_Object__RENDER_SUBSCRIPTION__Internal?
                .Invoke(e);
        }
        protected virtual void Handle_Render__Xerxes_Engine_Object(Frame_Argument e)
        { }

        internal virtual bool Internal_Seal__Xerxes_Engine_Object()
        {
            if (Xerxes_Engine_Object__Is_Sealed__Internal)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__XERXES_ENGINE_OBJECT__REDUNDANT_SEALING,
                    this
                );
                return true;
            }

            Xerxes_Engine_Object__Is_Sealed__Internal = true;
            return true;
        }

        internal bool Internal_Associate__Xerxes_Engine_Object
        (
            Xerxes_Engine_Object association
        )
        {
            if (Xerxes_Engine_Object__PARENT__Internal != null)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Engine_Object,
                    Log.ERROR__XERXES_ENGINE_OBJECT__INVALID_PARENT_ASSOCIATION_2,
                    this,
                    association,
                    Xerxes_Engine_Object__PARENT__Internal
                );

                return false;
            }

            return Handle_Association__Xerxes_Engine_Object();
        }

        /// <summary>
        /// Implementation control for rejecting outgoing associations.
        /// This is not meant to check the object which you are associating to.
        /// Instead, this is meant to reject associations for cases beyond
        /// being already associated or violating hierarchy.
        /// </summary>
        protected virtual bool Handle_Association__Xerxes_Engine_Object()
            => true;

        internal bool Internal_Validate__Association__Xerxes_Engine_Object
        (
            Xerxes_Engine_Object objectThatAssociated
        )
        {
            if (Xerxes_Engine_Object__Is_Sealed__Internal)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Engine_Object,
                    Log.ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION_1,
                    this,
                    objectThatAssociated
                );
                return false;
            }
            return true;
        }

        /// <summary>
        /// Implementation control for rejecting incoming associations.
        /// This is not to check what you are being associated with but rather
        /// to reject assocations made to you on cases beyond being sealed.
        /// </summary>
        protected virtual bool Handle_Associated__Xerxes_Engine_Object()
            => true;

        internal static void Internal_Associate
        (
            Xerxes_Engine_Object thisObject,
            Xerxes_Engine_Object toThisObject
        )
        {
            int associationTypeDifference =
                toThisObject.Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal
                -
                thisObject.Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal
                ;
            if (associationTypeDifference < 0)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Engine_Object,
                    Log.ERROR__XERXES_ENGINE_OBJECT__INVALID_ASSOCIATION_4,
                    null,
                    toThisObject,
                    thisObject,
                    toThisObject.Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal,
                    thisObject.Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal
                );

                return;
            }

            bool acceptsChild_To_Parent_Association
                = thisObject
                .Internal_Associate__Xerxes_Engine_Object
                (
                    toThisObject
                );

            bool acceptsParent_To_Child_Association
                = toThisObject
                .Internal_Validate__Association__Xerxes_Engine_Object
                (
                    thisObject
                );

            if
            (
                acceptsChild_To_Parent_Association
                &&
                acceptsParent_To_Child_Association
            )
            {
                toThisObject
                    .Xerxes_Engine_Object__UPDATE_SUBSCRIPTION__Internal
                    += thisObject.Internal_Update__Xerxes_Engine_Object;
                toThisObject
                    .Xerxes_Engine_Object__RENDER_SUBSCRIPTION__Internal
                    += thisObject.Internal_Render__Xerxes_Engine_Object;
                return;
            }

            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_ASSOCIATION_2,
                null,
                thisObject,
                toThisObject
            );
        }
    }
}

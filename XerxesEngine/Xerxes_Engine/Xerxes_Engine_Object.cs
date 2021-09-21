using System;

namespace Xerxes_Engine
{
    /// <summary>
    /// Represents a type in Xerxes_Engine that depends on the
    /// Update/Render control flow. All internalized logging messages
    /// are related to such objects - or systems.
    ///
    /// Calls to Update and Render are internalized. Exposure to
    /// handling these calls are given via protected virtual definitions.
    /// </summary>
    public class Xerxes_Engine_Object
    {
        /// <summary>
        /// Games cannot associate to anything. They are the thing
        /// which objects associate to.
        /// </summary>
        private const int Xerxes_Engine_Object__ASSOCIATION_PRIORITY__GAME = 0;

        internal bool Xerxes_Engine_Object__Is_Disabled__Internal { get; set; }
        internal bool Xerxes_Engine_Object__Is_Sealed__Internal { get; set; }

        internal int Xerxes_Engine_Object__ASSOCIATION_PRIORITY__Internal { get; }

        internal Game Xerxes_Engine_Object__Root__Internal { get; set; }
        internal bool Internal_Check_If__Rooted__Xerxes_Engine_Object()
            => Xerxes_Engine_Object__Root__Internal != null;
        protected Game Protected_Get__Root__Xerxes_Engine_Object()
        {
            if (Xerxes_Engine_Object__Root__Internal == null)
            {
                Private_Log_Error__Is_Not_Associate_To_Root(this);
                return null;
            }
            return Xerxes_Engine_Object__Root__Internal;
        }
        internal Xerxes_Engine_Object Xerxes_Engine_Object__Parent__Internal { get; set; }
        internal T Internal_Get__Parent_As__Xerxes_Engine_Object<T>() where T : Xerxes_Engine_Object
        {
            if (Xerxes_Engine_Object__Parent__Internal == null)
            {
                Private_Log_Error__Is_Not_Associated(this);
                return null;
            }

            return Xerxes_Engine_Object__Parent__Internal as T;
        }
        /// <summary>
        /// This is invoked when the entire association chain is
        /// connected to a Game instance.
        /// </summary>
        internal event Action<Event_Argument_Associate_Game> Xerxes_Engine_Object__ASSOCIATE_GAME_SUBSCRIPTION__Internal;
        internal event Action<Event_Argument_Frame>          Xerxes_Engine_Object__UPDATE_SUBSCRIPTION__Internal;
        internal event Action<Event_Argument_Frame>          Xerxes_Engine_Object__RENDER_SUBSCRIPTION__Internal;
        internal event Action<Event_Argument_Resize_2D>      Xerxes_Engine_Object__RESIZE_2D_SUBSCRIPTION__Internal;

        private void Private_Evaluate__Subscription__Xerxes_Engine_Object<T>
        (
            Action<T> internal_Handler,
            Action<T> subscription,
            T e
        ) where T : Event_Argument
        {
            internal_Handler.Invoke(e);

            subscription?.Invoke(e);
        }
        private void Private_Evaluate__Enabled_Subscription__Xerxes_Engine_Object<T>
        (
            Action<T> internal_Handler,
            Action<T> subscription,
            T e
        ) where T : Event_Argument
        {
            if(Xerxes_Engine_Object__Is_Disabled__Internal)
                return;

            Private_Evaluate__Subscription__Xerxes_Engine_Object
            (
                internal_Handler,
                subscription,
                e
            );
        }

        internal Xerxes_Engine_Object(Xerxes_Engine_Object_Association_Type hierarchyType)
            : this((int)hierarchyType)
        { }
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


#region Subscriptions
        internal virtual void Internal_Associate__To_Game__Xerxes_Engine_Object(Event_Argument_Associate_Game e)
        {
            Xerxes_Engine_Object__Root__Internal = e.Event_Argument_Associate_Game__GAME;

            Private_Evaluate__Subscription__Xerxes_Engine_Object
            (
                Handle_Associate__To_Game__Xerxes_Engine_Object,
                Xerxes_Engine_Object__ASSOCIATE_GAME_SUBSCRIPTION__Internal,
                e
            );
        }
        protected virtual void Handle_Associate__To_Game__Xerxes_Engine_Object(Event_Argument_Associate_Game e) { }



        internal virtual void Internal_Update__Xerxes_Engine_Object(Event_Argument_Frame e)
        {
            Private_Evaluate__Enabled_Subscription__Xerxes_Engine_Object
            (
                Handle_Update__Xerxes_Engine_Object,
                Xerxes_Engine_Object__UPDATE_SUBSCRIPTION__Internal,
                e
            );
        }
        protected virtual void Handle_Update__Xerxes_Engine_Object(Event_Argument_Frame e) { }



        internal virtual void Internal_Render__Xerxes_Engine_Object(Event_Argument_Frame e)
        {
            Private_Evaluate__Enabled_Subscription__Xerxes_Engine_Object
            (
                Handle_Render__Xerxes_Engine_Object,
                Xerxes_Engine_Object__RENDER_SUBSCRIPTION__Internal,
                e
            );
        }
        protected virtual void Handle_Render__Xerxes_Engine_Object(Event_Argument_Frame e) { }



        internal virtual void Internal_Resize__2D__Xerxes_Engine_Object(Event_Argument_Resize_2D e)
        {
            Private_Evaluate__Enabled_Subscription__Xerxes_Engine_Object
            (
                Handle_Resize__2D__Xerxes_Engine_Object,
                Xerxes_Engine_Object__RESIZE_2D_SUBSCRIPTION__Internal,
                e
            );
        }
        protected virtual void Handle_Resize__2D__Xerxes_Engine_Object(Event_Argument_Resize_2D e) {}
#endregion



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

        /// <summary>
        /// This is invoked when THIS object is being treated as an ancestor.
        /// </summary>
        internal virtual bool Internal_Handle_Associate__As_Ancestor__Xerxes_Engine_Object
        (
            Xerxes_Engine_Object descendantAssociation
        )
            => Handle_Associate__As_Ancestor__Xerxes_Engine_Object();

        /// <summary>
        /// Implementation control for rejecting outgoing associations.
        /// This is not meant to check the object which you are associating to.
        /// Instead, this is meant to reject associations for cases beyond
        /// being already associated or violating hierarchy.
        /// </summary>
        protected virtual bool Handle_Associate__As_Ancestor__Xerxes_Engine_Object()
            => true;

        /// <summary>
        /// This is invoked when THIS object is being treated as a descedent.
        /// </summary>
        internal virtual bool Internal_Handle_Associate__As_Descendant__Xerxes_Engine_Object
        (
            Xerxes_Engine_Object ancestorAssociation
        )
            => Handle_Associate__As_Descendant__Xerxes_Engine_Object();

        /// <summary>
        /// Implementation control for rejecting incoming associations.
        /// This is not to check what you are being associated with but rather
        /// to reject assocations made to you on cases beyond being sealed.
        /// </summary>
        protected virtual bool Handle_Associate__As_Descendant__Xerxes_Engine_Object()
            => true;

        /// <summary>
        /// Returns true if the association is formed, otherwise returns false.
        /// </summary>
        internal static bool Internal_Associate
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

                return false;
            }

            if (thisObject.Xerxes_Engine_Object__Is_Sealed__Internal)
            {
                Private_Log_Error__Is_Sealed(thisObject);
                return false;
            }
            if (toThisObject.Xerxes_Engine_Object__Is_Sealed__Internal)
            {
                Private_Log_Error__Is_Sealed(toThisObject);
                return false;
            }

            bool accepts_Association_As_Ancestor
                = toThisObject
                .Internal_Handle_Associate__As_Ancestor__Xerxes_Engine_Object
                (
                    thisObject
                );

            bool accepts_Association_As_Descendant
                = thisObject
                .Internal_Handle_Associate__As_Descendant__Xerxes_Engine_Object
                (
                    toThisObject
                );

            if
            (
                accepts_Association_As_Ancestor
                &&
                accepts_Association_As_Descendant
            )
            {
                thisObject.Xerxes_Engine_Object__Parent__Internal
                    = toThisObject;
                toThisObject
                    .Xerxes_Engine_Object__ASSOCIATE_GAME_SUBSCRIPTION__Internal
                    += thisObject.Internal_Associate__To_Game__Xerxes_Engine_Object;
                toThisObject
                    .Xerxes_Engine_Object__UPDATE_SUBSCRIPTION__Internal
                    += thisObject.Internal_Update__Xerxes_Engine_Object;
                toThisObject
                    .Xerxes_Engine_Object__RENDER_SUBSCRIPTION__Internal
                    += thisObject.Internal_Render__Xerxes_Engine_Object;
                toThisObject
                    .Xerxes_Engine_Object__RESIZE_2D_SUBSCRIPTION__Internal
                    += thisObject.Internal_Resize__2D__Xerxes_Engine_Object;
                return true;
            }

            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_ASSOCIATION_2,
                null,
                thisObject,
                toThisObject
            );
            
            return false;
        }

        private static void Private_Log_Error__Is_Sealed(Xerxes_Engine_Object obj)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION,
                obj
            );
        }

        private static void Private_Log_Error__Is_Not_Associate_To_Root(Xerxes_Engine_Object obj)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED_TO_ROOT,
                obj
            );
        }

        private static void Private_Log_Error__Is_Not_Associated(Xerxes_Engine_Object obj)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED,
                obj
            );
        }
    }
}

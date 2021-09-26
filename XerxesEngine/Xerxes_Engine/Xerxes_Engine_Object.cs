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
        internal Streamline_Dictionary
            Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal { get; }
        internal Streamline_Dictionary 
            Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal { get; }

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

#region Streamline Management
        /// <summary>
        /// Declares a streamline that flows down the
        /// hierarchy.
        /// </summary>
        protected void Protected_Declare__Descending_Streamline__Xerxes_Engine_Object<T>
        ( 
            Action<T> listener = null
        ) where T : Streamline_Argument 
        {
            Private_Declare__Streamline__Xerxes_Engine_Object
                (
                    Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
                    listener
                );
        }

        /// <summary>
        /// Declares a streamline that flows up the
        /// hierarchy.
        /// </summary>
        protected void Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object<T>
        ( 
            Action<T> listener = null
        ) where T : Streamline_Argument
        {
            Private_Declare__Streamline__Xerxes_Engine_Object
            (
                Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                listener
            );
        }

        private void Private_Declare__Streamline__Xerxes_Engine_Object<T>
        (
            Streamline_Dictionary streamline_Dictionary,
            Action<T> listener = null
        ) where T : Streamline_Argument
        {
            streamline_Dictionary
                .Internal_Declare__Streamline__Streamline_Dictionary
                (
                    new Streamline<T>(listener)
                );
        }

        protected void Protected_Invoke__Descending_Streamline__Xerxes_Engine_Object<T>
        (
            T streamline_Argument
        ) where T : Streamline_Argument
        {
            Private_Invoke__Streamline__Xerxes_Engine_Object
            (
                Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
                streamline_Argument
            );
        }

        protected void Protected_Invoke__Ascending_Streamline__Xerxes_Engine_Object<T>
        (
            T streamline_Argument
        ) where T : Streamline_Argument
        {
            Private_Invoke__Streamline__Xerxes_Engine_Object<T>
            (
                Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                streamline_Argument
            );
        }

        private void Private_Invoke__Streamline__Xerxes_Engine_Object<T>
        (
            Streamline_Dictionary streamline_Dictionary,
            T streamline_Argument
        ) where T : Streamline_Argument
        {
            Streamline<T> streamline =
                Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline.Streamline_Base__IS_SOURCE)
            {
                streamline.Internal_Stream__Streamline(streamline_Argument);
                return;
            }

            Private_Log_Warning__Streamline_Invoked_But_Source_1
            (
                this,
                streamline
            );
        }
#endregion




#region Sealing
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
#endregion

#region Association
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
#endregion




#region Static Association
        /// <summary>
        /// Returns true if the association is formed, otherwise returns false.
        /// </summary>
        internal static bool Internal_Associate__Objects<T,Y>
        (
            T thisObject,
            Y toThisObject,
            Action<T,Y> additionalAssociation = null
        ) where T : Xerxes_Engine_Object where Y : Xerxes_Engine_Object
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
                Private_Associate__Objects(thisObject, toThisObject);
                additionalAssociation?.Invoke(thisObject, toThisObject);
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

        private static void Private_Associate__Objects
        (
            Xerxes_Engine_Object thisObject,
            Xerxes_Engine_Object toThisObject
        )
        {
            // Link downstreams.
            Private_Link__Streamlines
            (
                toThisObject.Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
                thisObject.Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
                (unlinked_Streamline) => 
                Private_Handle__Streamline_Link_Failure
                (
                    toThisObject,
                    thisObject,
                    unlinked_Streamline
                )
            );
            // Link upstreams.
            Private_Link__Streamlines
            (
                thisObject.Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                toThisObject.Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                (unlinked_Streamline) =>
                Private_Handle__Streamline_Link_Failure
                (
                    thisObject,
                    toThisObject,
                    unlinked_Streamline
                )
            );
        }

        private static void Private_Handle__Streamline_Link_Failure
        (
            Xerxes_Engine_Object source,
            Xerxes_Engine_Object target,
            Streamline_Base unlinked_Streamline
        )
        {
            if (unlinked_Streamline.Streamline_Base__IS_MANDATORY)
            {
                Private_Log_Error__Unlinked_Mandatory_Streamline_2
                (
                    source,
                    target,
                    unlinked_Streamline
                );
                return;
            }

            Private_Log_Warning__Unlinked_Streamline_2
            (
                source,
                target,
                unlinked_Streamline
            );
        }

        private static void Private_Link__Streamlines
        (
            Distinct_Type_Dictionary<Streamline_Argument, Streamline_Base> source,
            Distinct_Type_Dictionary<Streamline_Argument, Streamline_Base> mouth,
            Action<Streamline_Base> fail_To_Link
        )
        {
            Distinct_Type_Dictionary<Streamline_Argument, Streamline_Base>
                .Internal_On_All__Matching_Keys
                (
                    source,
                    mouth,
                    Streamline_Base.Internal_Link__Streamline_Bases,
                    fail_To_Link
                );
        }
#endregion

#region Static Logging
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

        private static void Private_Log_Warning__Unlinked_Streamline_2
        (
            Xerxes_Engine_Object source,
            Xerxes_Engine_Object associatedObject,
            Streamline_Base unlinkedStreamline
        )
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__XERXES_ENGINE_OBJECT__UNLINKED_STREAMLINE_2,
                source,
                associatedObject,
                unlinkedStreamline
            );
        }

        private static void Private_Log_Error__Unlinked_Mandatory_Streamline_2
        (
            Xerxes_Engine_Object source,
            Xerxes_Engine_Object associatedObject,
            Streamline_Base unlinkedStreamline
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__UNLINKED_MANDATORY_STREAMLINE_2,
                source,
                associatedObject,
                unlinkedStreamline
            );
        }

        private static void Private_Log_Warning__Streamline_Invoked_But_Source_1
        (
            Xerxes_Engine_Object source,
            Streamline_Base streamline
        )
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__STREAMLINE__INVOKED_BUT_NOT_SOURCE_1,
                source,
                streamline
            );
        }
#endregion
    }
}

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
    public class Xerxes_Object
    {
        /// <summary>
        /// Games cannot associate to anything. They are the thing
        /// which objects associate to.
        /// </summary>
        private const int Xerxes_Engine_Object__ASSOCIATION_PRIORITY__GAME = 0;

        public bool Xerxes_Engine_Object__Is_Disabled { get; protected set; }
        public bool Xerxes_Engine_Object__Is_Sealed { get; private set; }

        internal Game Xerxes_Engine_Object__Root__Internal { get; set; }
        protected bool Protected_Check_If__Rooted__Xerxes_Engine_Object()
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
        internal Xerxes_Object Xerxes_Engine_Object__Parent__Internal { get; set; }
        internal T Internal_Get__Parent_As__Xerxes_Engine_Object<T>() where T : Xerxes_Object
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

        internal Xerxes_Object() 
        {
            Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal = new Streamline_Dictionary();
            Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal = new Streamline_Dictionary();
        }

        public override string ToString()
        {
            string str = base.ToString();
            str = str.Substring(str.LastIndexOf('.')+1);
            return str;
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
            bool success = streamline_Dictionary
                .Internal_Declare__Streamline__Streamline_Dictionary
                (
                    new Streamline<T>(listener)
                );

            if (success)
                return;

            Private_Log_Error__Failed_To_Declare_Streamline_1
            (
                this,
                typeof(T)
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
                streamline_Dictionary
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline == null)
            {
                Log.Internal_Write__Verbose__Log
                (
                    "Streamline {0} was not found in:\n{1}",
                    this,
                    streamline_Dictionary.ToString()
                );
                return;
            }

            streamline.Internal_Stream__Streamline(streamline_Argument);
            
            /* TODO: look at this
            if (streamline.Streamline_Base__IS_SOURCE)
            {
                return;
            }

            Private_Log_Warning__Streamline_Invoked_But_Source_1
            (
                this,
                streamline
            );
            */
        }

        protected bool Protected_Subscribe__Descending_Streamline__Xerxes_Engine_Object<T>
        (
            Action<T> listener
        ) where T : Streamline_Argument
        {
            return Private_Subscribe__Streamline__Xerxes_Engine_Object
                (
                    Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
                    listener
                );
        }

        protected bool Protected_Subscribe__Ascending_Streamline__Xerxes_Engine_Object<T>
        (
            Action<T> listener
        ) where T : Streamline_Argument
        {
            return Private_Subscribe__Streamline__Xerxes_Engine_Object<T>
                (
                    Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                    listener
                );
        }

        private bool Private_Subscribe__Streamline__Xerxes_Engine_Object<T>
        (
            Streamline_Dictionary streamlines,
            Action<T> listener
        ) where T : Streamline_Argument
        {
            Streamline<T> streamline = 
                streamlines
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline == null)
            {
                //TODO: Log
                return false;
            }

            streamline.Streamline__SUBSCRIPTION__Internal +=
                listener;

            return true;
        }
#endregion




#region Sealing
        internal bool Internal_Seal__Xerxes_Engine_Object()
        {
            if (Xerxes_Engine_Object__Is_Sealed)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__XERXES_ENGINE_OBJECT__REDUNDANT_SEALING,
                    this
                );
                return true;
            }

            Xerxes_Engine_Object__Is_Sealed = true;
            Internal_Handle__Sealed__Xerxes_Engine_Object();
            return true;
        }

        internal virtual void Internal_Handle__Sealed__Xerxes_Engine_Object()
        {
            
        }
        protected virtual void Handle__Sealed__Xerxes_Engine_Object() { }
#endregion

#region Association
        /// <summary>
        /// This is invoked when THIS object is being treated as an ancestor.
        /// </summary>
        internal virtual bool Internal_Handle__Associate_As_Ancestor__Xerxes_Engine_Object
        (
            Xerxes_Object descendantAssociation
        )
            => true; 

        /// <summary>
        /// Internal control for after a successful association.
        /// </summary>
        internal virtual void Internal_Handle__Associated_As_Ancestor__Xerxes_Engine_Object
        (
            Xerxes_Object associatedDescendant
        )
        {
            Handle__Associated_As_Ancestor__Xerxes_Engine_Object();
        }
        /// <summary>
        /// Implementation control for responding to being
        /// associated as an ancestor.
        /// </summary>
        protected virtual void Handle__Associated_As_Ancestor__Xerxes_Engine_Object() { } 

        /// <summary>
        /// This is invoked when THIS object is being treated as a descedent.
        /// </summary>
        internal virtual bool Internal_Handle__Associate_As_Descendant__Xerxes_Engine_Object
        (
            Xerxes_Object ancestorAssociation
        )
            => true;

        internal virtual void Internal_Handle__Associated_As_Descendant__Xerxes_Engine_Object
        (
            Xerxes_Object ancestorAssociation
        )
        {
            Handle_Associated__As_Descendant__Xerxes_Engine_Object();
        }
        /// <summary>
        /// Implementation control for handling a successful
        /// association as a descendant.
        /// </summary>
        protected virtual void Handle_Associated__As_Descendant__Xerxes_Engine_Object() { }
#endregion




#region Static Association
        /// <summary>
        /// Returns true if the association is formed, otherwise returns false.
        /// </summary>
        internal static bool Internal_Associate__Objects<T,Y>
        (
            T thisObject,
            Y toThisObject
        ) where T : Xerxes_Object where Y : Xerxes_Object
        {
            if (thisObject.Xerxes_Engine_Object__Is_Sealed)
            {
                Private_Log_Error__Is_Sealed(thisObject);
                return false;
            }
            if (toThisObject.Xerxes_Engine_Object__Is_Sealed)
            {
                Private_Log_Error__Is_Sealed(toThisObject);
                return false;
            }

            bool accepts_Association_As_Ancestor
                = toThisObject
                .Internal_Handle__Associate_As_Ancestor__Xerxes_Engine_Object
                (
                    thisObject
                );

            bool accepts_Association_As_Descendant
                = thisObject
                .Internal_Handle__Associate_As_Descendant__Xerxes_Engine_Object
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
            Xerxes_Object thisObject,
            Xerxes_Object toThisObject
        )
        {
            thisObject.Xerxes_Engine_Object__Parent__Internal = toThisObject;

            // Link downstreams.
            Private_Link__Streamlines
            (
                thisObject.Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
                toThisObject.Xerxes_Engine_Object__DESCENDING_STREAMLINES__Internal,
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
                toThisObject.Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                thisObject.Xerxes_Engine_Object__ASCENDING_STREAMLINES__Internal,
                (unlinked_Streamline) =>
                Private_Handle__Streamline_Link_Failure
                (
                    thisObject,
                    toThisObject,
                    unlinked_Streamline
                )
            );

            toThisObject
                .Internal_Handle__Associated_As_Ancestor__Xerxes_Engine_Object
                (
                    thisObject
                );
            thisObject
                .Internal_Handle__Associated_As_Descendant__Xerxes_Engine_Object
                (
                    toThisObject
                );
        }

        private static void Private_Handle__Streamline_Link_Failure
        (
            Xerxes_Object source,
            Xerxes_Object target,
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
            Streamline_Dictionary source,
            Streamline_Dictionary mouth,
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
        private static void Private_Log_Error__Is_Sealed(Xerxes_Object obj)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION,
                obj
            );
        }

        private static void Private_Log_Error__Failed_To_Declare_Streamline_1
        (
            Xerxes_Object obj,
            Type streamlineType
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_TO_DECLARE_STREAMLINE_1,
                obj,
                streamlineType
            );
        }

        private static void Private_Log_Error__Is_Not_Associate_To_Root(Xerxes_Object obj)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED_TO_ROOT,
                obj
            );
        }

        private static void Private_Log_Error__Is_Not_Associated(Xerxes_Object obj)
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
            Xerxes_Object source,
            Xerxes_Object associatedObject,
            Streamline_Base unlinkedStreamline
        )
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__XERXES_ENGINE_OBJECT__UNLINKED_STREAMLINE_2,
                source,
                unlinkedStreamline,
                associatedObject
            );
        }

        private static void Private_Log_Error__Unlinked_Mandatory_Streamline_2
        (
            Xerxes_Object source,
            Xerxes_Object associatedObject,
            Streamline_Base unlinkedStreamline
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__UNLINKED_MANDATORY_STREAMLINE_2,
                source,
                unlinkedStreamline,
                associatedObject
            );
        }

        private static void Private_Log_Warning__Streamline_Invoked_But_Source_1
        (
            Xerxes_Object source,
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

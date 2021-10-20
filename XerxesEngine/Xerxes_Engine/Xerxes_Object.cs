using System;

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
    public class Xerxes_Object<T> where T : Xerxes_Object<T>
    {
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

        private Stream _Xerxes_Engine_Object__UPSTREAM { get; }
        private Stream _Xerxes_Engine_Object__DOWNSTREAM { get; }

        internal Xerxes_Object() 
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

            _Xerxes_Engine_Object__UPSTREAM = new Stream();
            _Xerxes_Engine_Object__DOWNSTREAM = new Stream();
        }

        public override string ToString()
        {
            string str = base.ToString();
            str = str.Substring(str.LastIndexOf('.')+1);
            return str;
        }

#region Streamline Management
        /// <summary>
        /// Declares a descending streamline that
        /// is invokable under the generic type.
        /// </summary>
        protected bool Protected_Declare__Downstream_Source__Xerxes_Engine_Object<S>
        (
            bool isReceiving = false
        ) where S : Streamline_Argument
        {
            bool success = 
                Private_Declare__Streamline__Xerxes_Engine_Object<S>
                (
                    _Xerxes_Engine_Object__DOWNSTREAM,
                    Log.Context__Stream.Downstream,
                    isReceiving: isReceiving,
                    isExtending: true,
                    isSourcing: true
                );

            return success;
        }

        protected bool Protected_Declare__Downstream_Catch__Xerxes_Engine_Object<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            bool success =
                Private_Declare__Streamline__Xerxes_Engine_Object
                (
                    _Xerxes_Engine_Object__DOWNSTREAM,
                    Log.Context__Stream.Downstream,
                    listener,
                    isReceiving: true,
                    isExtending: false
                );

            return true;
        }

        /// <summary>
        /// Declares a streamline that flows down the
        /// hierarchy.
        /// </summary>
        protected bool Protected_Declare__Descending_Streamline__Xerxes_Engine_Object<S>
        ( 
            Action<S> listener = null,
            bool isReceiving = true,
            bool isExtending = true,
            bool isSourcing = false
        ) where S : Streamline_Argument 
        {
            bool success =
                Private_Declare__Streamline__Xerxes_Engine_Object
                (
                    _Xerxes_Engine_Object__DOWNSTREAM,
                    Log.Context__Stream.Downstream,
                    listener,
                    isReceiving,
                    isExtending,
                    isSourcing
                );

            return success;
        }

        /// <summary>
        /// Declares a descending streamline that
        /// is invokable under the generic type.
        /// </summary>
        protected bool Protected_Declare__Upstream_Source__Xerxes_Engine_Object<S>
        (
            bool isReceiving = false
        ) where S : Streamline_Argument
        {
            bool success = 
                Private_Declare__Streamline__Xerxes_Engine_Object<S>
                (
                    _Xerxes_Engine_Object__UPSTREAM,
                    Log.Context__Stream.Upstream,
                    isReceiving: isReceiving,
                    isExtending: true,
                    isSourcing: true
                );

            return success;
        }

        protected bool Protected_Declare__Upstream_Catch__Xerxes_Engine_Object<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            bool success =
                Private_Declare__Streamline__Xerxes_Engine_Object
                (
                    _Xerxes_Engine_Object__UPSTREAM,
                    Log.Context__Stream.Upstream,
                    listener,
                    isReceiving: true,
                    isExtending: false
                );

            return true;
        }

        /// <summary>
        /// Declares a streamline that flows up the
        /// hierarchy.
        /// </summary>
        protected bool Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object<S>
        ( 
            Action<S> listener = null,
            bool isReceiving = true,
            bool isExtending = true,
            bool isSourcing = false
        ) where S : Streamline_Argument
        {
            bool success = 
                Private_Declare__Streamline__Xerxes_Engine_Object
                (
                    _Xerxes_Engine_Object__UPSTREAM,
                    Log.Context__Stream.Upstream,
                    listener,
                    isReceiving,
                    isExtending,
                    isSourcing
                );

            return success;
        }

        private bool Private_Declare__Streamline__Xerxes_Engine_Object<S>
        (
            Stream stream,
            Log.Context__Stream stream_context,
            Action<S> listener = null,
            bool isReceiving = true,
            bool isExtending = true,
            bool isSourcing  = false
        ) where S : Streamline_Argument
        {
            bool success = 
                stream
                .Internal_Declare__Streamline__Stream<S>
                (
                    listener,
                    isReceiving,
                    isExtending,
                    isSourcing,
                    (type_context) => Private_Log_Error__Failed_To_Declare_Streamline_2C
                          (this, typeof(S), stream_context, type_context),
                    (type_context) => Private_Log_Error__Failed_To_Declare_Streamline_2C
                          (this, typeof(S), stream_context, type_context),
                    (type_context) => Private_Log_Error__Failed_To_Declare_Streamline_2C
                          (this, typeof(S), stream_context, type_context)
                );

            return success;
        }

        protected bool Protected_Invoke__Descending_Streamline__Xerxes_Engine_Object<S>
        (
            S streamline_Argument
        ) where S : Streamline_Argument
        {
            return Private_Invoke__Streamline__Xerxes_Engine_Object
            (
                _Xerxes_Engine_Object__DOWNSTREAM,
                streamline_Argument
            );
        }

        protected bool Protected_Invoke__Ascending_Streamline__Xerxes_Engine_Object<S>
        (
            S streamline_Argument
        ) where S : Streamline_Argument
        {
            return Private_Invoke__Streamline__Xerxes_Engine_Object
            (
                _Xerxes_Engine_Object__UPSTREAM,
                streamline_Argument
            );
        }

        private bool Private_Invoke__Streamline__Xerxes_Engine_Object<S>
        (
            Stream stream,
            S streamline_Argument
        ) where S : Streamline_Argument
        {
            bool success =
                stream
                .Internal_Invoke__Streamline__Stream<S>
                (
                    streamline_Argument
                );

            //TODO: Make this better
            if (!success)
            {
                Log.Internal_Write__Verbose__Log
                (
                    "Streamline {0} was not found.",
                    this,
                    typeof(S).ToString()
                );
            }

            return success;
        }

        protected bool Protected_Subscribe__Descending_Streamline__Xerxes_Engine_Object<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            return Private_Subscribe__Streamline__Xerxes_Engine_Object
                (
                    _Xerxes_Engine_Object__DOWNSTREAM,
                    listener
                );
        }

        protected bool Protected_Subscribe__Ascending_Streamline__Xerxes_Engine_Object<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            return Private_Subscribe__Streamline__Xerxes_Engine_Object
                (
                    _Xerxes_Engine_Object__UPSTREAM,
                    listener
                );
        }

        private bool Private_Subscribe__Streamline__Xerxes_Engine_Object<S>
        (
            Stream stream,
            Action<S> listener
        ) where S : Streamline_Argument
        {
            bool success =
                stream
                .Internal_Subscribe__Streamline__Stream
                (
                    listener
                );

            if (!success)
            {
                //TODO: Log
            }

            return success;
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
        public bool Associate__Xerxes_Engine_Object<A>
        (
            Xerxes_Object<A> descendant
        ) where A : Xerxes_Object<A>
        {
            bool success =
                Internal_Associate__Objects
                (
                    this as T,
                    descendant as A
                );

            return success;
        }

        /// <summary>
        /// Internal control for after a successful association.
        /// </summary>
        internal virtual void Internal_Handle__Became_Ancestor__Xerxes_Engine_Object
        (
            IXerxes_Descendant_Of<T> associatedDescendant
        )
        {
            Handle_Became__Ancestor__Xerxes_Engine_Object();
        }
        /// <summary>
        /// Implementation control for responding to being
        /// associated as an ancestor.
        /// </summary>
        protected virtual void Handle_Became__Ancestor__Xerxes_Engine_Object() { } 

        internal virtual void Internal_Handle__Became_Descendant__Xerxes_Engine_Object
        (
            IXerxes_Ancestor_Of<T> ancestorAssociation
        )
        {
            Handle_Became__Descendant__Xerxes_Engine_Object();
        }
        /// <summary>
        /// Implementation control for handling a successful
        /// association as a descendant.
        /// </summary>
        protected virtual void Handle_Became__Descendant__Xerxes_Engine_Object() { }
#endregion




#region Static Association
        /// <summary>
        /// Returns true if the association is formed, otherwise returns false.
        /// </summary>
        internal static bool Internal_Associate__Objects<A>
        (
            T ancestor,
            A descendant
        ) where A : Xerxes_Object<A>
        {
            if (ancestor.Xerxes_Engine_Object__Is_Sealed)
            {
                Private_Log_Error__Is_Sealed(ancestor);
                return false;
            }
            if (descendant.Xerxes_Engine_Object__Is_Sealed)
            {
                Xerxes_Object<A>.Private_Log_Error__Is_Sealed(descendant);
                return false;
            }

            if
            (
                ancestor is IXerxes_Ancestor_Of<A>
                &&
                descendant is IXerxes_Descendant_Of<T>
            )
            {
                Private_Associate__Objects(ancestor, descendant);
                return true;
            }

            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_ASSOCIATION_2,
                null,
                ancestor,
                descendant
            );
            
            return false;
        }

        private static void Private_Associate__Objects<A>
        (
            T ancestor,
            A descendant
        ) where A : Xerxes_Object<A>
        {
            // Link downstreams.
            Stream.Internal__Link
            (
                ancestor._Xerxes_Engine_Object__DOWNSTREAM,
                descendant._Xerxes_Engine_Object__DOWNSTREAM,
                (unlinked_Streamline) => 
                Private_Handle__Streamline_Link_Failure
                (
                    ancestor,
                    descendant,
                    unlinked_Streamline
                )
            );
            // Link upstreams.
            Stream.Internal__Link
            (
                descendant._Xerxes_Engine_Object__UPSTREAM,
                ancestor._Xerxes_Engine_Object__UPSTREAM,
                (unlinked_Streamline) =>
                Private_Handle__Streamline_Link_Failure
                (
                    ancestor,
                    descendant,
                    unlinked_Streamline
                )
            );

            descendant
                .Internal_Handle__Became_Ancestor__Xerxes_Engine_Object
                (
                    ancestor as IXerxes_Descendant_Of<A>
                );
            ancestor
                .Internal_Handle__Became_Descendant__Xerxes_Engine_Object
                (
                    descendant as IXerxes_Ancestor_Of<T>
                );
        }

        private static void Private_Handle__Streamline_Link_Failure<A>
        (
            Xerxes_Object<T> ancestor,
            Xerxes_Object<A> descendant,
            Streamline_Base unlinked_Streamline
        ) where A : Xerxes_Object<A>
        {
            if (unlinked_Streamline.Streamline_Base__IS_MANDATORY)
            {
                Private_Log_Error__Unlinked_Mandatory_Streamline_2
                (
                    ancestor,
                    descendant,
                    unlinked_Streamline
                );
                return;
            }

            Private_Log_Warning__Unlinked_Streamline_2
            (
                ancestor,
                descendant,
                unlinked_Streamline
            );
        }
#endregion

#region Static Logging
        private static void Private_Log_Error__Is_Sealed
        (
            Xerxes_Object<T> obj
        ) 
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION,
                obj
            );
        }

        private static void Private_Log_Error__Failed_To_Declare_Streamline_2C
        (
            Xerxes_Object<T> obj,
            Type streamlineType,
            Log.Context__Stream stream_context,
            Log.Context__Declare_Streamline type_context
        ) 
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_TO_DECLARE_STREAMLINE_2C,
                obj,
                streamlineType,
                stream_context,
                type_context
            );
        }

        private static void Private_Log_Error__Is_Not_Associate_To_Root
        (
            Xerxes_Object<T> obj
        ) 
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED_TO_ROOT,
                obj
            );
        }

        private static void Private_Log_Error__Is_Not_Associated
        (
            Xerxes_Object<T> obj
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__IS_NOT_ASSOCIATED,
                obj
            );
        }

        private static void Private_Log_Warning__Unlinked_Streamline_2<A>
        (
            Xerxes_Object<T> ancestor,
            Xerxes_Object<A> descendant,
            Streamline_Base unlinkedStreamline
        ) where A : Xerxes_Object<A>
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__XERXES_ENGINE_OBJECT__UNLINKED_STREAMLINE_2,
                ancestor,
                unlinkedStreamline,
                descendant
            );
        }

        private static void Private_Log_Error__Unlinked_Mandatory_Streamline_2<A>
        (
            Xerxes_Object<T> ancestor,
            Xerxes_Object<A> descendant,
            Streamline_Base unlinkedStreamline
        ) where A : Xerxes_Object<A>
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__UNLINKED_MANDATORY_STREAMLINE_2,
                ancestor,
                unlinkedStreamline,
                descendant
            );
        }

        private static void Private_Log_Warning__Streamline_Invoked_But_Source_1
        (
            Xerxes_Object<T> source,
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

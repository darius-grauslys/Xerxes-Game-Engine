using System;

namespace Xerxes_Engine
{
    public class Xerxes_Object_Base
    {
        private Stream _Xerxes_Engine_Object__UPSTREAM { get; }
        internal Streamline_Dictionary 
            Xerxes_Object_Base__ASCENDING_RECEIVING_STREAMLINES__Internal
            => _Xerxes_Engine_Object__UPSTREAM.Stream__RECEIVING_STREAMLINES__Internal;
        internal Streamline_Dictionary 
            Xerxes_Object_Base__ASCENDING_EXTENDING_STREAMLINES__Internal
            => _Xerxes_Engine_Object__UPSTREAM.Stream__EXTENDING_STREAMLINES__Internal;
        
        private Stream _Xerxes_Engine_Object__DOWNSTREAM { get; }
        internal Streamline_Dictionary 
            Xerxes_Object_Base__DESCENDING_RECEIVING_STREAMLINES__Internal
            => _Xerxes_Engine_Object__DOWNSTREAM.Stream__RECEIVING_STREAMLINES__Internal;
        internal Streamline_Dictionary 
            Xerxes_Object_Base__DESCENDING_EXTENDING_STREAMLINES__Internal
            => _Xerxes_Engine_Object__DOWNSTREAM.Stream__EXTENDING_STREAMLINES__Internal;

        public bool Xerxes_Engine_Object__Is_Disabled { get; protected set; }
        public bool Xerxes_Engine_Object__Is_Sealed { get; private set; }

        internal Game Xerxes_Engine_Object__Root__Internal { get; set; }
        protected bool Protected_Check_If__Rooted__Xerxes_Engine_Object()
            => Xerxes_Engine_Object__Root__Internal != null;

        internal Xerxes_Object_Base()
        {
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
        protected bool Protected_Declare__Downstream_Extender__Xerxes_Engine_Object<S>
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

        protected bool Protected_Declare__Downstream_Receiver__Xerxes_Engine_Object<S>
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
        /// Declares a descending streamline that
        /// is invokable under the generic type.
        /// </summary>
        protected bool Protected_Declare__Upstream_Extender__Xerxes_Engine_Object<S>
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

        protected bool Protected_Declare__Upstream_Receiver__Xerxes_Engine_Object<S>
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

        protected bool Protected_Invoke__Descending_Extender__Xerxes_Engine_Object<S>
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

        protected bool Protected_Invoke__Ascending_Extender__Xerxes_Engine_Object<S>
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

        protected bool Protected_Subscribe__Descending_Receiver__Xerxes_Engine_Object<S>
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

        protected bool Protected_Subscribe__Ascending_Receiver__Xerxes_Engine_Object<S>
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

#region Static Logging
        private static void Private_Log_Error__Is_Sealed
        (
            Xerxes_Object_Base obj
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
            Xerxes_Object_Base obj,
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
#endregion
    }
}

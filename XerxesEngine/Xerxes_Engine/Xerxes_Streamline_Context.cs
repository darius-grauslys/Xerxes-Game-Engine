using System;

namespace Xerxes_Engine
{
    public sealed class Xerxes_Streamline_Context
    {
        private Xerxes_Stream_Context _Xerxes_Streamline_Context__STREAM_CONTEXT { get; }
        private Stream _Xerxes_Streamline_Context__STREAM { get; }

        internal Xerxes_Streamline_Context
        (
            Xerxes_Stream_Context stream_Context,
            Stream stream
        )
        {
            _Xerxes_Streamline_Context__STREAM_CONTEXT =
                stream_Context;
            _Xerxes_Streamline_Context__STREAM = 
                stream;
        }

        public Xerxes_Stream_Context Receiving<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            Private_Declare__Receiving__Xerxes_Streamline_Context(listener);

            return _Xerxes_Streamline_Context__STREAM_CONTEXT;
        }

        public Xerxes_Stream_Context Receiving<S>
        (
            Action<S> listener,
            out bool success
        ) where S : Streamline_Argument
        {
            success =
                Private_Declare__Receiving__Xerxes_Streamline_Context(listener);

            return _Xerxes_Streamline_Context__STREAM_CONTEXT;
        }

        public Xerxes_Stream_Context Extending<S>
        () where S : Streamline_Argument
        {
            Private_Declare__Extending__Xerxes_Streamline_Context<S>();

            return _Xerxes_Streamline_Context__STREAM_CONTEXT;
        }

        public Xerxes_Stream_Context Extending<S>
        (
            out bool success
        ) where S : Streamline_Argument
        {
            success =
                Private_Declare__Extending__Xerxes_Streamline_Context<S>();

            return _Xerxes_Streamline_Context__STREAM_CONTEXT;
        }

        private bool Private_Declare__Receiving__Xerxes_Streamline_Context<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            bool success = 
                _Xerxes_Streamline_Context__STREAM
                    .Internal_Declare__Streamline__Stream
                    (
                        listener,
                        isExtending: false,
                        declaration_Failure_Receiving: (s,c) => 
                            Private_Log_Error__Fail_To_Declare_Streamline
                            (this,s,c)
                    );

            return success;
        }

        private bool Private_Declare__Extending__Xerxes_Streamline_Context<S>
        () where S : Streamline_Argument
        {
            bool success =
                _Xerxes_Streamline_Context__STREAM
                    .Internal_Declare__Streamline__Stream<S>
                    (
                        isReceiving: false,
                        declaration_Failure_Extending: (s,c) => 
                            Private_Log_Error__Fail_To_Declare_Streamline 
                            (this,s,c)
                    );

            return success;
        }

#region Static Logging
        private static void Private_Log_Error__Fail_To_Declare_Streamline
        (
            Xerxes_Streamline_Context sc,
            Streamline_Base failedStreamline,
            Log.Context__Declare_Streamline c
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_TO_DECLARE_STREAMLINE_2C,
                sc,
                failedStreamline,
                c
            );
        }
#endregion
    }
}

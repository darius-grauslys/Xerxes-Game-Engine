using System;

namespace Xerxes_Engine
{
    internal sealed class Stream
    {
        private Streamline_Dictionary _Stream__RECEIVING_STREAMLINES { get; }
        private Streamline_Dictionary _Stream__EXTENDING_STREAMLINES { get; }
        private Streamline_Dictionary _Stream__SOURCING_STREAMLINES  { get; }

        internal Stream()
        {
            _Stream__RECEIVING_STREAMLINES =
                new Streamline_Dictionary();
            _Stream__EXTENDING_STREAMLINES =
                new Streamline_Dictionary();
            _Stream__SOURCING_STREAMLINES  =
                new Streamline_Dictionary();
        }

        internal bool Internal_Declare__Streamline__Stream<T>
        (
            Action<T> listener = null,
            bool isReceiving = true,
            bool isExtending = true,
            bool isSourcing  = false,
            Action<Log.Context__Declare_Streamline> 
                declaration_Failure_Receiving = null,
            Action<Log.Context__Declare_Streamline> 
                declaration_Failure_Extending = null,
            Action<Log.Context__Declare_Streamline> 
                declaration_Failure_Sourcing  = null
        ) where T : Streamline_Argument
        {
            Streamline<T> streamline = 
                new Streamline<T>
                (
                    listener, 
                    isReceiving, 
                    isExtending, 
                    isSourcing
                );

            bool success = true;

            success = 
                Private_Declare__If__Stream
                (
                    streamline,
                    _Stream__RECEIVING_STREAMLINES,
                    streamline.Streamline_Base__IS_RECEIVING,
                    () => declaration_Failure_Receiving
                        (Log.Context__Declare_Streamline.Receieve)
                );
            success = success & 
                Private_Declare__If__Stream
                (
                    streamline,
                    _Stream__EXTENDING_STREAMLINES,
                    streamline.Streamline_Base__IS_EXTENDING,
                    () => declaration_Failure_Extending
                        (Log.Context__Declare_Streamline.Extend)
                );
            success = success &
                Private_Declare__If__Stream
                (
                    streamline,
                    _Stream__SOURCING_STREAMLINES,
                    streamline.Streamline_Base__IS_SOURCING,
                    () => declaration_Failure_Sourcing
                        (Log.Context__Declare_Streamline.Source)
                );

            return success;
        }

        private bool Private_Declare__If__Stream<T>
        (
            Streamline<T> streamline,
            Streamline_Dictionary target,
            bool condition,
            Action declaration_Failure_Handler
        ) where T : Streamline_Argument
        {
            if (condition)
            {
                bool success = 
                    target
                    .Internal_Declare__Streamline__Streamline_Dictionary
                    (
                        streamline
                    );

                if (!success)
                    declaration_Failure_Handler?.Invoke();
                return success;
            }
            return true;
        }

        internal bool Internal_Invoke__Streamline__Stream<T>
        (
            T streamline_Argument
        ) where T : Streamline_Argument
        {
            Streamline<T> streamline =
                _Stream__SOURCING_STREAMLINES 
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline != null)
            {
                streamline.Internal_Stream__Streamline(streamline_Argument);
                return true;
            }

            return false;
        }

        internal bool Internal_Subscribe__Streamline__Stream<T>
        (
            Action<T> listener
        ) where T : Streamline_Argument
        {
            Streamline<T> streamline =
                _Stream__RECEIVING_STREAMLINES
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline == null)
                return false;

            streamline.Streamline__SUBSCRIPTION__Internal +=
                listener;

            return true;
        }

        internal static void Internal__Link
        (
            Stream extender,
            Stream receiver,
            Action<Streamline_Base> fail_Find_Extending_Endpoint
        )
        {
            Streamline_Dictionary.Internal_On_All__Matching_Keys
            (
                receiver._Stream__RECEIVING_STREAMLINES,
                extender._Stream__EXTENDING_STREAMLINES,
                Streamline_Base.Internal_Link__Streamline_Bases,
                fail_Find_Extending_Endpoint
            );
        }
    }
}

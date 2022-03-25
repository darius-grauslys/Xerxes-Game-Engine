using System;

namespace Xerxes
{
    internal sealed class Stream
    {
        internal Streamline_Dictionary Stream__RECEIVING_STREAMLINES__Internal { get; }
        internal Streamline_Dictionary Stream__EXTENDING_STREAMLINES__Internal { get; }

        internal Stream()
        {
            Stream__RECEIVING_STREAMLINES__Internal =
                new Streamline_Dictionary();
            Stream__EXTENDING_STREAMLINES__Internal =
                new Streamline_Dictionary();
        }

        internal Streamline_Base Internal_Get__Receiving_Streamline_Base__Stream<T>
        () where T : Streamline_Argument
        {
            Streamline_Base streamline_Base =
                Private_Get__Streamline_Base__Stream<T>
                (
                    Stream__RECEIVING_STREAMLINES__Internal
                );

            return streamline_Base;
        }

        internal Streamline_Base Internal_Get__Extending_Streamline_Base__Stream<T>
        () where T : Streamline_Argument
        {
            Streamline_Base streamline_Base =
                Private_Get__Streamline_Base__Stream<T>
                (
                    Stream__EXTENDING_STREAMLINES__Internal
                );

            return streamline_Base;
        }

        private Streamline_Base Private_Get__Streamline_Base__Stream<T>
        (
            Streamline_Dictionary streamline_Dictionary
        ) where T : Streamline_Argument
        {
            Streamline_Base streamline_Base =
                streamline_Dictionary
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            return streamline_Base;
        }

        internal bool Internal_Declare__Streamline__Stream<SA>
        (
            Action<SA> listener = null,
            bool isRecieving = true,
            bool isExtending = true,
            Action<Streamline_Base, Log.Context__Declare_Streamline> 
                declaration_Failure_Receiving = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline> 
                declaration_Failure_Extending = null
        ) where SA : Streamline_Argument
        {
            Streamline<SA> streamline = 
                new Streamline<SA>
                (
                    listener, 
                    isRecieving, 
                    isExtending
                );

            bool success = true;

            success = 
                Private_Declare__If__Stream
                (
                    streamline,
                    Stream__RECEIVING_STREAMLINES__Internal,
                    streamline.Streamline_Base__IS_RECEIVING,
                    () => declaration_Failure_Receiving?.Invoke
                        (streamline, Log.Context__Declare_Streamline.Receieve)
                );
            success = success && 
                Private_Declare__If__Stream
                (
                    streamline,
                    Stream__EXTENDING_STREAMLINES__Internal,
                    streamline.Streamline_Base__IS_EXTENDING,
                    () => declaration_Failure_Extending?.Invoke
                        (streamline, Log.Context__Declare_Streamline.Extend)
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
                Stream__EXTENDING_STREAMLINES__Internal
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline != null)
            {
                streamline
                    .Internal_Stream__Streamline(streamline_Argument);
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
                Stream__RECEIVING_STREAMLINES__Internal
                .Internal_Get__Streamline__Streamline_Dictionary<T>();

            if (streamline == null)
                return false;

            streamline.Streamline__STREAMLINE__Internal +=
                listener;

            return true;
        }
    }
}

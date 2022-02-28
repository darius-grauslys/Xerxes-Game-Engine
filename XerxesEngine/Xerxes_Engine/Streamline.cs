using System;

namespace Xerxes
{
    /// <summary>
    /// Represents a communication stream through the
    /// engine hierarchy. The purpose is to keep 
    /// communication fully transparent and minimize
    /// out of hierarchy side-effects.
    /// </summary>
    public class Streamline<T> : Streamline_Base where T : Streamline_Argument
    {
        internal event Action<T> Streamline__STREAMLINE__Internal;

        internal Streamline
        (
            Action<T> listening_receiver = null,
            bool isReceiving = true,
            bool isExtending = true
        ) 
        : base
        (
            typeof(Streamline<T>),
            isReceiving,
            isExtending
        )
        {
            if (listening_receiver != null)
            {
                Streamline__STREAMLINE__Internal += listening_receiver;
            }
        }        

#region Internal Streamline Functionality
        internal override Streamline_Base Internal_Create__Virtual__Streamline_Base()
        {
            Streamline<T> virtual_target =
                new Streamline<T>
                (
                    null,
                    Streamline_Base__IS_RECEIVING,
                    Streamline_Base__IS_EXTENDING
                );

            return virtual_target;
        }

        internal override bool Internal_Link__Extend_Target__Streamline_Base
        (
            Streamline_Base target
        )
        {
            Streamline<T> target_streamline = target as Streamline<T>;
            if (target_streamline == null)
                return false;

            //TODO: This tomfoolery is needed.
            //look to fix this by linking to
            //ancestral receiver on PUSH
            //then linking to ancestral
            //extender on POP.
            Streamline__STREAMLINE__Internal +=
                target_streamline
                .Internal_Stream__Streamline;

            return true;
        }

        internal bool Internal_Subscribe__Streamline_Base
        (
            Action<T> listening_receiver
        )
        {
            if (!Streamline_Base__IS_RECEIVING)
                return false;

            Streamline__STREAMLINE__Internal +=
                listening_receiver;

            return true;
        }

        internal void Internal_Stream__Streamline
        (
            T streamline_Argument
        )
        {
            Streamline__STREAMLINE__Internal?
                .Invoke(streamline_Argument);
        }
#endregion
    }
}

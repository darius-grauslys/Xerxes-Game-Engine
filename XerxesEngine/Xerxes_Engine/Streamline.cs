using System;

namespace Xerxes_Engine
{
    /// <summary>
    /// Represents a communication stream through the
    /// engine hierarchy. The purpose is to keep 
    /// communication fully transparent and minimize
    /// out of hierarchy side-effects.
    /// </summary>
    public class Streamline<T> : Streamline_Base where T : Streamline_Argument
    {
        internal event Action<T> Streamline__SUBSCRIPTION__Internal;


        internal Streamline(Action<T> listener = null) 
        {
            if (listener != null)
                Streamline__SUBSCRIPTION__Internal += listener;
        }        

        

#region Internal Streamline Functionality
        internal override bool Internal_Link__Streamline_Base 
        (
            Streamline_Base target
        )
        { 
            Streamline<T> target_Streamline = target as Streamline<T>;
            if (target_Streamline == null)
                return false;

            target_Streamline
                .Streamline__SUBSCRIPTION__Internal +=
                Internal_Stream__Streamline;

            return true;
        }

        internal void Internal_Stream__Streamline
        (
            dynamic streamline_Argument
        )
        {
            if (Streamline_Base__Is_Disabled)
                return;

            Streamline__SUBSCRIPTION__Internal?
                .Invoke(streamline_Argument);
        }
#endregion
    }
}
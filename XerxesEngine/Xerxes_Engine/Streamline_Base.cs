
using System;

namespace Xerxes
{
    public abstract class Streamline_Base
    {
        public bool Streamline_Base__IS_RECEIVING { get; }
        public bool Streamline_Base__IS_EXTENDING { get; }

        internal Type Streamline_Base__IDENTIFYING_TYPE { get; }

        internal Streamline_Base
        (
            Type identifying_type,
            bool isReceiving = true,
            bool isExtending = true
        ) 
        { 
            Streamline_Base__IDENTIFYING_TYPE = identifying_type;

            Streamline_Base__IS_RECEIVING = isReceiving;
            Streamline_Base__IS_EXTENDING = isExtending;
        }

        public override string ToString()
        {
            string str = base.ToString();
            int index = str.LastIndexOf('.')+1;
            str = str.Substring(index, str.Length - index - 1);
            return str;
        }

        internal abstract Streamline_Base Internal_Create__Virtual__Streamline_Base();

        internal virtual Streamline_Base Internal_Link__Virtual_Target__Streamline_Base()
        {
            Streamline_Base virtual_target =
                Internal_Create__Virtual__Streamline_Base();

            Internal_Link__Extend_Target__Streamline_Base(virtual_target);

            return virtual_target;
        }

        internal abstract bool Internal_Link__Extend_Target__Streamline_Base
        (
            Streamline_Base target
        );
    }
}

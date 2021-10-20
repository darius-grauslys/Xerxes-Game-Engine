namespace Xerxes_Engine
{
    public abstract class Streamline_Base
    {
        public bool Streamline_Base__IS_MANDATORY { get; }
        public bool Streamline_Base__Is_Disabled { get; protected set; }

        public bool Streamline_Base__IS_RECEIVING { get; }
        public bool Streamline_Base__IS_EXTENDING { get; }
        public bool Streamline_Base__IS_SOURCING  { get; }

        internal Streamline_Base
        (
            bool isReceiving = true,
            bool isExtending = true,
            bool isSourcing  = false
        ) 
        { 
            Streamline_Base__IS_RECEIVING = isReceiving;
            Streamline_Base__IS_EXTENDING = isExtending;
            Streamline_Base__IS_SOURCING  = isSourcing;
        }

        public override string ToString()
        {
            string str = base.ToString();
            int index = str.LastIndexOf('.')+1;
            str = str.Substring(index, str.Length - index - 1);
            return str;
        }

        internal abstract bool Internal_Link__Streamline_Base
        (
            Streamline_Base target
        );

        internal static void Internal_Link__Streamline_Bases
        (
            Streamline_Base streamline_Base_1,
            Streamline_Base streamline_Base_2
        )
        {
            streamline_Base_1
                .Internal_Link__Streamline_Base
                (
                    streamline_Base_2
                );
        }
    }
}

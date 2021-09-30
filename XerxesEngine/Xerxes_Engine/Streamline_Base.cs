namespace Xerxes_Engine
{
    public abstract class Streamline_Base
    {
        public bool Streamline_Base__IS_MANDATORY { get; }
        public bool Streamline_Base__Is_Disabled { get; protected set; }

        internal Streamline_Base() { }

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

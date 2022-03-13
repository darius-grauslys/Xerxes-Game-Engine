
namespace Xerxes
{
    public class SA__Mediate
    <
        XTarget,
        SA
    > :
    Streamline_Argument
    where XTarget :
    Xerxes_Object_Base, new()
    where SA :
    Streamline_Argument
    {
        public SA Mediate__Mediated_Streamline_Argument { get; internal set; }

        public SA__Mediate() {}

        public static implicit operator SA(SA__Mediate<XTarget, SA> e)
            => e.Mediate__Mediated_Streamline_Argument;
    }
}


namespace Xerxes
{
    public class SA__Mediate
    <
        TTarget,
        SA
    > :
    Streamline_Argument
    where TTarget : Xerxes_Object_Base
    where SA      : Streamline_Argument
    {
        public SA Mediate__Streamline_Argument { get; set; }

        public SA__Mediate(){}

        public SA__Mediate(SA streamline_argument)
        {
            Mediate__Streamline_Argument = streamline_argument;
        }
    }
}

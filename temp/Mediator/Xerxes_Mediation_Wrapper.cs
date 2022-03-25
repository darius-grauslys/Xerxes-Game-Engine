
namespace Xerxes
{
    public class Xerxes_Mediation_Wrapper<XTarget> :
    Xerxes_Object
    where XTarget : 
    Xerxes_Object_Base, new()
    {
        public Xerxes_Mediation_Wrapper()
        {
            Genology
                .Declare__Associations
                    .Associate<XTarget>();
        }

        internal void Internal_Mediate__Mediation_Wrapper<SA>()
        where SA : 
        Streamline_Argument
        {
            Genology
                .Declare__Streamlines
                    .With__Descendants
                        .Recieving<SA__Mediate<SA, XTarget>>(Private_Handle__Mediation__Mediation_Wrapper)
                        .Extending<SA>();
        }

        private void Private_Handle__Mediation__Mediation_Wrapper<SA>(SA__Mediate<SA, XTarget> e)
        where SA :
        Streamline_Argument
        {
            Invoke__Descending(e.Internal_Mediate__Mediated_Streamline_Argument);
        }
    }
}

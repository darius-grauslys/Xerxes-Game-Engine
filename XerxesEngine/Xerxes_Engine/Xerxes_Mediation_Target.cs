
namespace Xerxes
{
    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA
    > :
    Xerxes_Object
    <
        Xerxes_Mediation_Target
        <
            TTarget,
            SA
        >
    >
    where TTarget : Xerxes_Object_Base, new()
    where SA      : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA>>
                (Handle_Mediation__Xerxes_Mediation_Target)
                .Downstream.Extending<SA>();

            Declare__Hierarchy()
                .Associate<TTarget>();
        }

        protected virtual void Handle_Mediation__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }
}

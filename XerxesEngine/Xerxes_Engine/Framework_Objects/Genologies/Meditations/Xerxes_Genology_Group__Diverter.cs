
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Diverter
    <
        TThis,
        TGenology,
        XReciever
    > :
    Xerxes_Genology_Group__Associations
    <
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Diverter
    <
        TThis,
        TGenology,
        XReciever
    >
    where TGenology :
    Xerxes_Genology
    where XReciever :
    Xerxes_Genology_Group__Recieving_Mediator
    <
        TGenology,
        TThis
    >, new()
    {
        protected internal XReciever Protected_Divert__Descendant__Diverter
        <
            XDescendant
        >()
        where XDescendant :
        Xerxes_Object_Base, new()
        {
            Xerxes_Object mediation_wrapper = 
                new Xerxes_Object();

            mediation_wrapper
                .Genology
                    .Genology__ASSOCIATIONS__Protected
                        .Associate<XDescendant>();

            XReciever reciever =
                new XReciever();

            reciever
                .Genology_Group__Enclosing_Genology__Internal =
                Genology_Group__Enclosing_Genology__Internal;

            reciever
                .Genology_Group__Enclosing_Object__Internal =
                mediation_wrapper;

            Internal_Associate__Associations(mediation_wrapper);

            return reciever;
        }
    }
}


namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Diverter
    <
        TThis,
        TGenealogy,
        XReciever
    > :
    Xerxes_Genealogy_Group__Associations
    <
        TGenealogy
    >
    where TThis :
    Xerxes_Genealogy_Group__Diverter
    <
        TThis,
        TGenealogy,
        XReciever
    >
    where TGenealogy :
    Xerxes_Genealogy
    where XReciever :
    Xerxes_Genealogy_Group__Wrapped_Mediator
    <
        TGenealogy,
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
                .Genealogy
                    .Genealogy__Associations__Protected
                        .Associate<XDescendant>();

            XReciever reciever =
                new XReciever();

            reciever
                .Wrapped_Mediator__Streamline_Linker__Internal =
                new Wrapper_Streamline_Linker<XDescendant>();


            reciever
                .Genealogy_Group__Enclosing_Genealogy__Internal =
                Genealogy_Group__Enclosing_Genealogy__Internal;

            reciever
                .Genealogy_Group_Child__Enclosing_Parent =
                this as TThis;

            reciever
                .Genealogy_Group__Enclosing_Object__Internal =
                mediation_wrapper;

            Internal_Associate__Associations(mediation_wrapper);

            return reciever;
        }
    }
}

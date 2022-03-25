
namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Diverter
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Diverter
    <
        Xerxes_Genealogy_Group__Standard_Diverter
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Wrapped_Mediator
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Diverter
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Wrapped_Mediator
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Diverter
                <
                    TGenealogy
                >
            >
            Associate__Diverted
        <
            XDescendant
        >()
        where XDescendant :
        Xerxes_Object_Base, new()
            => Protected_Divert__Descendant__Diverter<XDescendant>();

        public Xerxes_Genealogy_Group__Standard_Diverter<TGenealogy> Associate<XDescendant>()
        where XDescendant :
        Xerxes_Object_Base, new()
        {
            Protected_Associate__Associations<XDescendant>();

            return this;
        }



        public TGenealogy Finish__With_Associations
            => Genealogy_Group__Enclosing_Genealogy;

        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }
    }
}


namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Wrapped_Mediator
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Wrapped_Mediator
    <
        TGenealogy,
        TParent
    >
    where TGenealogy :
    Xerxes_Genealogy
    where TParent :
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    {
        public 
            Xerxes_Genealogy_Group__Standard_Wrapped_Mediator
            <
                TGenealogy,
                TParent
            >
            Recieve__Ancestor_Mediation
            <SA>()
        where SA :
        Streamline_Argument
        {
            Protected_Mediate__From_Ancestors__Wrapper_Mediator<SA>();

            return this;
        }




        public TParent Finish__With_Mediations
            => Genealogy_Group_Child__Enclosing_Parent;

        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }
    }
}

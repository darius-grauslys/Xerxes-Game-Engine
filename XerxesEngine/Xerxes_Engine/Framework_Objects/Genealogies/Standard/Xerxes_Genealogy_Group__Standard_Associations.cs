
namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Associations
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Associations
    <
        TGenealogy
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }

        public Xerxes_Genealogy_Group__Standard_Associations<TGenealogy> Associate<XTarget>()
        where XTarget : Xerxes_Object_Base, new()
        {
            Protected_Associate__Associations<XTarget>();
            return this;
        }



        public TGenealogy Finish__With_Associations
            => Genealogy_Group__Enclosing_Genealogy;
    }
}

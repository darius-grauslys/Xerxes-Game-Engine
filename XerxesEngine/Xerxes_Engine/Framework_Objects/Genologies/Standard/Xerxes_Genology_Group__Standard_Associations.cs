
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Associations
    <
        TGenology
    > :
    Xerxes_Genology_Group__Associations
    <
        TGenology
    >
    where TGenology :
    Xerxes_Genology
    {
        protected internal override void Handle_Linking__Genology_Group()
        {
        }

        public Xerxes_Genology_Group__Standard_Associations<TGenology> Associate<XTarget>()
        where XTarget : Xerxes_Object_Base, new()
        {
            Protected_Associate__Associations<XTarget>();
            return this;
        }



        public TGenology Finish__With_Associations
            => Genology_Group__Enclosing_Genology;
    }
}


namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Associations
    <
        TGenology
    > :
    Xerxes_Genology_Group__Associations
    <
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
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
            return Protected_Associate__Associations<XTarget>();
        }



        public TGenology Finish__With_Associations
            => Genology_Group__Enclosing_Genology;
    }
}

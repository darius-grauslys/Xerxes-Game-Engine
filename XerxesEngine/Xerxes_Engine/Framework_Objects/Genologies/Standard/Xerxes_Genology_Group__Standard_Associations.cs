
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Associations
    <
        TGenology
    > :
    Xerxes_Genology_Group__Associations
    <
        Xerxes_Genology_Group__Standard_Associations<TGenology>,
        TGenology
    >
    where TGenology :
    Xerxes_Genology
    {
        public Xerxes_Genology_Group__Standard_Associations<TGenology> Associate<XObject>()
        where XObject : Xerxes_Object_Base, new()
        {
            return Protected_Associate__Associations<XObject>();
        }
    }
}

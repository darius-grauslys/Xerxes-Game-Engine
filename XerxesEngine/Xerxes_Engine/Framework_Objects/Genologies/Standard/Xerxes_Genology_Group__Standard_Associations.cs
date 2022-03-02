
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Associations :
    Xerxes_Genology_Group__Associations
    <
        Xerxes_Genology_Group__Standard_Associations,
        Xerxes_Genology__Standard
    >
    {
        public Xerxes_Genology_Group__Standard_Associations Associate<XObject>()
        where XObject : Xerxes_Object_Base, new()
        {
            return Protected_Associate__Associations<XObject>();
        }
    }
}

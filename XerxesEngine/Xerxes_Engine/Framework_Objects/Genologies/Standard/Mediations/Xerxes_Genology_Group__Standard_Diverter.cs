
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Diverter
    <
        TGenology,
        XTarget
    > :
    Xerxes_Genology_Group__Diverter
    <
        Xerxes_Genology_Group__Standard_Diverter
        <
            TGenology,
            XTarget
        >,
        TGenology,
        Xerxes_Genology_Group__Standard_Mediated_Reciever
        <
            TGenology
        >,
        XTarget
    >
    where TGenology :
    Xerxes_Genology
    where XTarget :
    Xerxes_Object_Base, new()
    {

    }
}

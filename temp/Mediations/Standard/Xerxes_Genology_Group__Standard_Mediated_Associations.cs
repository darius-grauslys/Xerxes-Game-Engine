
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Mediated_Associations
    <
        TGenology,
        XBase
    > :
    Xerxes_Genology_Group__Mediated_Associations
    <
        Xerxes_Genology_Group__Standard_Mediated_Associations
        <
            TGenology,
            XBase
        >,
        TGenology,
        XBase,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>
    >
    where TGenology :
    Xerxes_Genology
    {
        public void Mediate<XTarget>()
        where XTarget : Xerxes_Object_Base, new()
        {
            
        }
    }
}

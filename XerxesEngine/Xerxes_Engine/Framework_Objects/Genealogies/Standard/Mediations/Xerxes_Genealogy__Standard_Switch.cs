
namespace Xerxes
{
    public class Xerxes_Genealogy__Standard_Switch :
    Xerxes_Genealogy__Switch
    <
        Xerxes_Genealogy__Standard_Switch,
        Xerxes_Genealogy_Group__Standard_Switch_Table__Descendants
        <
            Xerxes_Genealogy__Standard_Switch
        >,
        Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
        <
            Xerxes_Genealogy__Standard_Switch
        >,
        Xerxes_Genealogy_Group__Standard_Diverter
        <
            Xerxes_Genealogy__Standard_Switch
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            Xerxes_Genealogy__Standard_Switch
        >
    >
    {
        public Xerxes_Genealogy_Group__Standard_Switch_Table__Descendants<Xerxes_Genealogy__Standard_Switch> With__Switches
            => Genealogy_Switch__Switcher__Protected;

        public Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate<Xerxes_Genealogy__Standard_Switch> With__Streamlines
            => Genealogy__Streamlines__Protected;

        public Xerxes_Genealogy_Group__Standard_Diverter<Xerxes_Genealogy__Standard_Switch> With__Associations
            => Genealogy__Associations__Protected;
        
        public Xerxes_Genealogy_Group__Standard_Endpoints<Xerxes_Genealogy__Standard_Switch> With__Endpoints
            => Genealogy__Endpoints__Protected;
    }
}

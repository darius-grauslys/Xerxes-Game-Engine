
namespace Xerxes
{
    public class Xerxes_Genealogy__Standard_Mediator :
    Xerxes_Genealogy
    <
        Xerxes_Genealogy__Standard_Mediator,
        Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
        <
            Xerxes_Genealogy__Standard_Mediator
        >,
        Xerxes_Genealogy_Group__Standard_Diverter
        <
            Xerxes_Genealogy__Standard_Mediator
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            Xerxes_Genealogy__Standard_Mediator
        >
    >
    {
        public Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate<Xerxes_Genealogy__Standard_Mediator> With__Streamlines
            => Genealogy__Streamlines__Protected;

        public Xerxes_Genealogy_Group__Standard_Diverter<Xerxes_Genealogy__Standard_Mediator> With__Associations
            => Genealogy__Associations__Protected;

        public Xerxes_Genealogy_Group__Standard_Endpoints<Xerxes_Genealogy__Standard_Mediator> With__Endpoints
            => Genealogy__Endpoints__Protected;
    }
}

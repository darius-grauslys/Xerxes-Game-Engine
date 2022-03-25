
namespace Xerxes
{
    public class Xerxes_Genealogy__Standard :
    Xerxes_Genealogy
    <
        Xerxes_Genealogy__Standard,
        Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
        <
            Xerxes_Genealogy__Standard
        >,
        Xerxes_Genealogy_Group__Standard_Associations
        <
            Xerxes_Genealogy__Standard
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            Xerxes_Genealogy__Standard
        >
    >
    {
        public Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate<Xerxes_Genealogy__Standard> With__Streamlines
            => Genealogy__Streamlines__Protected;

        public Xerxes_Genealogy_Group__Standard_Associations<Xerxes_Genealogy__Standard> With__Associations
            => Genealogy__Associations__Protected;

        public Xerxes_Genealogy_Group__Standard_Endpoints<Xerxes_Genealogy__Standard> With__Endpoints
            => Genealogy__Endpoints__Protected;
    }

    public class Xerxes_Genealogy__Standard_Endpoint :
    Xerxes_Genealogy
    <
        Xerxes_Genealogy__Standard_Endpoint,
        Xerxes_Genealogy_Group__Standard_Streamlines_Descending
        <
            Xerxes_Genealogy__Standard_Endpoint
        >
    >
    {
        public Xerxes_Genealogy_Group__Standard_Streamlines_Descending<Xerxes_Genealogy__Standard_Endpoint> With__Streamlines
            => Genealogy__Streamlines__Protected;
    }
}


namespace Xerxes
{
    public class Xerxes_Genology__Standard :
    Xerxes_Genology
    <
        Xerxes_Genology__Standard,
        Xerxes_Genology_Group__Standard_Streamlines_Intermediate
        <
            Xerxes_Genology__Standard
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            Xerxes_Genology__Standard
        >
    >
    {}

    public class Xerxes_Genology__Standard_Endpoint :
    Xerxes_Genology
    <
        Xerxes_Genology__Standard_Endpoint,
        Xerxes_Genology_Group__Standard_Streamlines_Descending
        <
            Xerxes_Genology__Standard_Endpoint
        >
    >
    {}

    public class Xerxes_Genology__Standard_Root :
    Xerxes_Genology
    <
        Xerxes_Genology__Standard_Root,
        Xerxes_Genology_Group__Standard_Streamlines_Intermediate
        <
            Xerxes_Genology__Standard_Root
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            Xerxes_Genology__Standard_Root
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            Xerxes_Genology__Standard_Root
        >
    >
    {}
}

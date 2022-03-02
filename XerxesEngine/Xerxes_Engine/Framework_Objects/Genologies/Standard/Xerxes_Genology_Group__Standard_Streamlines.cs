
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Streamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        Xerxes_Genology_Group__Standard_Streamlines,
        Xerxes_Genology__Standard,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors,
        Xerxes_Genology_Group__Standard_Streamline_Descendants
    >
    {
    }

    public class Xerxes_Genology_Group__Standard_Streamline_Descendants :
    Xerxes_Genology_Group__Descending_Streams
    <
        Xerxes_Genology_Group__Standard_Streamline_Descendants,
        Xerxes_Genology__Standard,
        Xerxes_Genology_Group__Standard_Streamlines
    >
    {}

    public class Xerxes_Genology_Group__Standard_Streamline_Ancestors :
    Xerxes_Genology_Group__Ascending_Streams
    <
        Xerxes_Genology_Group__Standard_Streamline_Ancestors,
        Xerxes_Genology__Standard,
        Xerxes_Genology_Group__Standard_Streamlines
    >
    {}
}

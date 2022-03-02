
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Streamlines
    <
        TGenology
    > :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        TGenology,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>
    >
    where TGenology :
    Xerxes_Genology__Intermediate
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>
    >, new()
    {
    }

    public class Xerxes_Genology_Group__Standard_Streamline_Descendants
    <
        TGenology
    > :
    Xerxes_Genology_Group__Descending_Streams
    <
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>,
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>
    >
    where TGenology :
    Xerxes_Genology__Intermediate
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>
    >, new()
    {}

    public class Xerxes_Genology_Group__Standard_Streamline_Ancestors
    <
        TGenology
    > :
    Xerxes_Genology_Group__Ascending_Streams
    <
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>
    >
    where TGenology :
    Xerxes_Genology__Intermediate
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>
    >, new()
    {}
}

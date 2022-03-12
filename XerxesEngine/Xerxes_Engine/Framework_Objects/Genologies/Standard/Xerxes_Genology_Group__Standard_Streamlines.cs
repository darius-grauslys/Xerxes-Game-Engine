
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Streamlines_Descending
    <
        TGenology
    > :
    Xerxes_Genology_Group__Streamlines
    <
        Xerxes_Genology_Group__Standard_Streamlines_Descending
        <
            TGenology
        >,
        TGenology,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Descending
            <
                TGenology
            >
        >
    >
    where TGenology :
    Xerxes_Genology
    {}

    public class Xerxes_Genology_Group__Standard_Streamlines_Intermediate
    <
        TGenology
    > :
    Xerxes_Genology_Group__Streamlines
    <
        Xerxes_Genology_Group__Standard_Streamlines_Intermediate
        <
            TGenology
        >,
        TGenology,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Intermediate
            <
                TGenology
            >
        >,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Intermediate
            <
                TGenology
            >
        >
    >
    where TGenology :
    Xerxes_Genology
    {}
}

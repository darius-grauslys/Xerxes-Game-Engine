
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Mediated_Streamlines
    <
        TGenology,
        XBase
    > :
    Xerxes_Genology_Group__Standard_Streamlines
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Mediated_Ascending_Stream
        <
            TGenology,
            XBase
        >,
        Xerxes_Genology_Group__Standard_Mediated_Descending_Streams
        <
            TGenology,
            XBase
        >
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        XBase
    >, new()
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        TGenology
    >, new()
    /*
    where TGenology :
    Xerxes_Genology__Intermediate
    <
        TGenology,
        XBase,
        Xerxes_Genology_Group__Standard_Mediated_Streamlines
        <
            TGenology,
            XBase
        >,
        Xerxes_Genology_Group__Standard_Mediated_Ascending_Stream
        <
            TGenology,
            XBase
        >,
        Xerxes_Genology_Group__Standard_Mediated_Descending_Streams
        <
            TGenology,
            XBase
        >
    >, new()
    */
    {

    }
}

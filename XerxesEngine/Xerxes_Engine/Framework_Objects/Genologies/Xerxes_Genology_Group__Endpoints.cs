
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Endpoints
    <
        TGenology
    > :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where TGenology :
    Xerxes_Genology
    {
        protected void Protected_Declare__Endpoint__Endpoints
        <
            XEndpoint,
            XGenology,
            XStreamlines,
            XDescending_Streams
        >()
        where XEndpoint :
        Xerxes_Object
        <
            XGenology
        >, new()
        where XGenology :
        Xerxes_Genology
        <
            XGenology,
            XStreamlines
        >, new()
        where XStreamlines :
        Xerxes_Genology_Group__Streamlines
        <
            XStreamlines,
            XGenology,
            XDescending_Streams
        >, new()
        where XDescending_Streams :
        Xerxes_Genology_Group__Descending_Streams
        <
            XDescending_Streams,
            XGenology,
            XStreamlines
        >, new()
        {
            XEndpoint descendant = new XEndpoint();

            Genology_Group__Enclosing_Genology
                .Genology__DESCENDANT_GENOLOGIES__Internal
                .Add(descendant.Xerxes_Object_Base__Genology__Internal);
        }
    }
}

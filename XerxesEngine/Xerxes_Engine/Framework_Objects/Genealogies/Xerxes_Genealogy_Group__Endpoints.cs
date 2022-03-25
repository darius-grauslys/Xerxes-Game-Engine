
namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Endpoints
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        protected void Protected_Declare__Endpoint__Endpoints
        <
            XEndpoint,
            XGenealogy,
            XStreamlines,
            XDescending_Streams
        >()
        where XEndpoint :
        Xerxes_Object
        <
            XGenealogy
        >, new()
        where XGenealogy :
        Xerxes_Genealogy
        <
            XGenealogy,
            XStreamlines
        >, new()
        where XStreamlines :
        Xerxes_Genealogy_Group__Streamlines
        <
            XStreamlines,
            XGenealogy,
            XDescending_Streams
        >, new()
        where XDescending_Streams :
        Xerxes_Genealogy_Group__Streams
        <
            XGenealogy,
            XStreamlines
        >, new()
        {
            XEndpoint descendant = new XEndpoint();

            Genealogy_Group__Enclosing_Genealogy
                .Genealogy__DESCENDANT_GENOLOGIES__Internal
                .Add(descendant.Xerxes_Object_Base__Genealogy__Internal);
        }
    }
}

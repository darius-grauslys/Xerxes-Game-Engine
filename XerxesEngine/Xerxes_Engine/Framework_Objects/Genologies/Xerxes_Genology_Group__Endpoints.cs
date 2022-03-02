
namespace Xerxes
{
    public class Xerxes_Genology_Group__Endpoints
    <
        TThis,
        TGenology,
        RAssociations,
        RStreamlines,
        RAscending_Stream,
        RDescending_Stream
    > :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Endpoints
    <
        TThis, 
        TGenology,
        RAssociations,
        RStreamlines,
        RAscending_Stream,
        RDescending_Stream
    >, new()
    where TGenology :
    Xerxes_Genology__Root
    <
        TGenology,
        TThis,
        RAssociations,
        RStreamlines,
        RAscending_Stream,
        RDescending_Stream
    >, new()
    where RAssociations :
    Xerxes_Genology_Group__Associations
    <
        RAssociations,
        TGenology
    >, new()
    where RStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        RStreamlines,
        TGenology,
        RAscending_Stream,
        RDescending_Stream
    >, new()
    where RAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        RAscending_Stream,
        TGenology,
        RStreamlines
    >, new()
    where RDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        RDescending_Stream,
        TGenology,
        RStreamlines
    >, new()
    {
        public TGenology Finish__With_Endpoints
            => Genology_Group__Enclosing_Genology;



        protected TThis Protected_Declare__Endpoint
        <
            TEndpoint, 
            TGenology_Endpoint,
            EEndpoint_Streamlines,
            EEndpoint_Descending_Stream
        >()
        where TEndpoint : 
        Xerxes_Object
        <
            TGenology_Endpoint
        >, new()
        where TGenology_Endpoint :
        Xerxes_Genology__Endpoint
        <
            TGenology_Endpoint,
            EEndpoint_Streamlines,
            EEndpoint_Descending_Stream
        >, new()
        where EEndpoint_Streamlines :
        Xerxes_Genology_Group__Endpoint_Streamlines
        <
            EEndpoint_Streamlines,
            TGenology_Endpoint,
            EEndpoint_Descending_Stream
        >, new()
        where EEndpoint_Descending_Stream :
        Xerxes_Genology_Group__Descending_Streams
        <
            EEndpoint_Descending_Stream,
            TGenology_Endpoint,
            EEndpoint_Streamlines
        >, new()
        {
            TEndpoint endpoint = new TEndpoint();

            Genology_Group__Enclosing_Genology
                .Genology_Root__Root__Internal
                .Internal_ROOT__ENDPOINTS
                .Internal_Declare__Endpoint__Endpoint_Dictionary(endpoint);

            return this as TThis;
        }
    }
}

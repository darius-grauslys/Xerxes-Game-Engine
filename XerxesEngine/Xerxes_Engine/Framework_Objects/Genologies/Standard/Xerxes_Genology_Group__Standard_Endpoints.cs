
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Endpoints
    <
        TGenology
    > :
    Xerxes_Genology_Group__Endpoints
    <
        Xerxes_Genology_Group__Standard_Endpoints<TGenology>,
        TGenology,
        Xerxes_Genology_Group__Standard_Associations<TGenology>,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>
    >
    where TGenology :
    Xerxes_Genology__Root
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Endpoints<TGenology>,
        Xerxes_Genology_Group__Standard_Associations<TGenology>,
        Xerxes_Genology_Group__Standard_Streamlines<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<TGenology>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<TGenology>
    >, new()
    {
        public Xerxes_Genology_Group__Standard_Endpoints<TGenology> Add__Endpoint
        <
            TEndpoint
        >()
        where TEndpoint :
        Xerxes_Object
        <
            Xerxes_Genology__Standard_Endpoint
        >, new()
        {
            return Protected_Declare__Endpoint
                <
                    TEndpoint,
                    Xerxes_Genology__Standard_Endpoint,
                    Xerxes_Genology_Group__Standard_Endpoint_Streamlines<Xerxes_Genology__Standard_Endpoint>,
                    Xerxes_Genology_Group__Standard_Endpoint_Descending_Stream<Xerxes_Genology__Standard_Endpoint>
                >();
        }

        public Xerxes_Genology_Group__Standard_Endpoints<TGenology> Add__Endpoint
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
            return Protected_Declare__Endpoint
                <
                    TEndpoint,
                    TGenology_Endpoint,
                    EEndpoint_Streamlines,
                    EEndpoint_Descending_Stream
                >();
        }
    }
}

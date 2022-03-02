
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Endpoint_Streamlines
    <
        TGenology_Endpoint
    >:
    Xerxes_Genology_Group__Endpoint_Streamlines
    <
        Xerxes_Genology_Group__Standard_Endpoint_Streamlines<TGenology_Endpoint>,
        TGenology_Endpoint,
        Xerxes_Genology_Group__Standard_Endpoint_Descending_Stream<TGenology_Endpoint>
    >
    where TGenology_Endpoint :
    Xerxes_Genology__Endpoint
    <
        TGenology_Endpoint,
        Xerxes_Genology_Group__Standard_Endpoint_Streamlines<TGenology_Endpoint>,
        Xerxes_Genology_Group__Standard_Endpoint_Descending_Stream<TGenology_Endpoint>
    >, new()
    {

    }

    public class Xerxes_Genology_Group__Standard_Endpoint_Descending_Stream
    <
        TGenology_Endpoint
    > :
    Xerxes_Genology_Group__Descending_Streams
    <
        Xerxes_Genology_Group__Standard_Endpoint_Descending_Stream<TGenology_Endpoint>,
        TGenology_Endpoint,
        Xerxes_Genology_Group__Standard_Endpoint_Streamlines<TGenology_Endpoint>
    >
    where TGenology_Endpoint :
    Xerxes_Genology__Endpoint
    <
        TGenology_Endpoint,
        Xerxes_Genology_Group__Standard_Endpoint_Streamlines<TGenology_Endpoint>,
        Xerxes_Genology_Group__Standard_Endpoint_Descending_Stream<TGenology_Endpoint>
    >, new()
    {

    }
}

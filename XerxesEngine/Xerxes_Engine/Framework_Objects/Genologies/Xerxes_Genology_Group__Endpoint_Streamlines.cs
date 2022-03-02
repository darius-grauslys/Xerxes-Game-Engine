
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Endpoint_Streamlines
    <
        TThis,
        TGenology_Endpoint,
        TStreams_Descending
    >:
    Xerxes_Genology_Group__Streamlines_Descending
    <
        TThis,
        TGenology_Endpoint,
        TStreams_Descending
    >
    where TThis :
    Xerxes_Genology_Group__Endpoint_Streamlines
    <
        TThis,
        TGenology_Endpoint,
        TStreams_Descending
    >, new()
    where TGenology_Endpoint :
    Xerxes_Genology__Endpoint
    <
        TGenology_Endpoint,
        TThis,
        TStreams_Descending
    >, new()
    where TStreams_Descending :
    Xerxes_Genology_Group__Descending_Streams
    <
        TStreams_Descending,
        TGenology_Endpoint,
        TThis
    >, new()
    {

    }
}

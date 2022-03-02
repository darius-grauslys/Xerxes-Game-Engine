
namespace Xerxes
{
    public abstract class Xerxes_Genology__Endpoint
    <
        TThis, 
        EStreamline, 
        EDownstream
    > :
    Xerxes_Genology__Descending
    <
        TThis,
        EStreamline,
        EDownstream
    >
    where TThis :
    Xerxes_Genology__Endpoint
    <
        TThis,
        EStreamline,
        EDownstream
    >, new()
    where EStreamline :
    Xerxes_Genology_Group__Streamlines_Descending
    <
        EStreamline,
        TThis,
        EDownstream
    >, new()
    where EDownstream :
    Xerxes_Genology_Group__Descending_Streams
    <
        EDownstream,
        TThis,
        EStreamline
    >, new()
    {
    }
}

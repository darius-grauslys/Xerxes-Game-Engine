
namespace Xerxes
{
    public class Xerxes_Endpoint
    <
        TGenology,
        EStreamline,
        EDescending_Streams
    > :
    Xerxes_Object<TGenology>
    where TGenology : 
    Xerxes_Genology__Endpoint
    <
        TGenology,
        EStreamline,
        EDescending_Streams
    >, new()
    where EStreamline :
    Xerxes_Genology_Group__Streamlines_Descending
    <
        EStreamline,
        TGenology,
        EDescending_Streams
    >, new()
    where EDescending_Streams :
    Xerxes_Genology_Group__Descending_Streams
    <
        EDescending_Streams,
        TGenology,
        EStreamline
    >, new()
    {
        protected Xerxes_Endpoint()
        {
        }
    }
}

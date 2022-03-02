
namespace Xerxes
{
    public abstract class Xerxes_Genology__Descending
    <
        TThis,
        GStreamlines_Descending,
        GDescending_Stream
    >:
    Xerxes_Genology
    where TThis : 
    Xerxes_Genology__Descending
    <
        TThis, 
        GStreamlines_Descending, 
        GDescending_Stream
    >, new()
    where GStreamlines_Descending : 
    Xerxes_Genology_Group__Streamlines_Descending
    <
        GStreamlines_Descending, 
        TThis, 
        GDescending_Stream
    >, new()
    where GDescending_Stream : 
    Xerxes_Genology_Group__Descending_Streams
    <
        GDescending_Stream, 
        TThis, 
        GStreamlines_Descending
    >, new()
    {
        public GStreamlines_Descending Declare__Streamlines { get; }

        public Xerxes_Genology__Descending()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology<TThis, GStreamlines_Descending>();
        }
    }
}

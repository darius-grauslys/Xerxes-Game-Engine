
namespace Xerxes
{
    public abstract class Xerxes_Genology__Intermediate
    <
        TThis,
        TStreamlines,
        TStream_Ascending,
        TStream_Descending
    >:
    Xerxes_Genology
    where TThis :
    Xerxes_Genology__Intermediate
    <
        TThis,
        TStreamlines,
        TStream_Ascending,
        TStream_Descending
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis
    >, new()
    where TStream_Ascending :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TStream_Ascending,
        TThis,
        TStreamlines
    >, new()
    where TStream_Descending :
    Xerxes_Genology_Group__Descending_Streams
    <
        TStream_Descending,
        TThis,
        TStreamlines
    >, new()
    {
        public TStreamlines Declare__Streamlines { get; }

        public Xerxes_Genology__Intermediate()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology<TThis, TStreamlines>();
        }
    }
}

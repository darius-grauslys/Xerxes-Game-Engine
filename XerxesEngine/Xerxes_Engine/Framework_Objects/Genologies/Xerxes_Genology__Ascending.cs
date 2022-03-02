
namespace Xerxes
{
    public abstract class Xerxes_Genology__Ascending
    <
        TThis,
        TStreamlines,
        TStream_Ascending
    > :
    Xerxes_Genology
    where TThis :
    Xerxes_Genology__Ascending
    <
        TThis,
        TStreamlines,
        TStream_Ascending
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis
    >, new()
    where TStream_Ascending :
    Xerxes_Genology_Group__Streams
    <
        TStream_Ascending,
        TThis,
        TStreamlines
    >, new()
    {
        public TStreamlines Declare__Streamlines { get; }

        public Xerxes_Genology__Ascending()
        {
            Declare__Streamlines =
                Protected_Link__Genology_Group__Genology<TThis, TStreamlines>();
        }
    }
}


namespace Xerxes
{
    /// <summary>
    /// For all genology patterns on Xerxes
    /// it is typical for Genologies to possess a
    /// streamline genology group that has at least
    /// a group child for descending streams.
    /// </summary>
    public abstract class Xerxes_Genology_Group__Streamlines_Descending
    <
        TThis,
        TGenology,
        TStreams_Descending
    > :
    Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology
    >
    where TThis : 
    Xerxes_Genology_Group__Streamlines_Descending
    <
        TThis, 
        TGenology, 
        TStreams_Descending
    >, new()
    where TGenology : 
    Xerxes_Genology__Descending
    <
        TGenology,
        TThis,
        TStreams_Descending
    >, new()
    where TStreams_Descending : 
    Xerxes_Genology_Group__Descending_Streams
    <
        TStreams_Descending,
        TGenology,
        TThis
    >, new()
    {
        public TStreams_Descending With__Descendants { get; }

        public Xerxes_Genology_Group__Streamlines_Descending()
        {
            With__Descendants =
                Protected_Link__Child_Group__Genology_Group<TThis, TStreams_Descending>();
        }
    }
}

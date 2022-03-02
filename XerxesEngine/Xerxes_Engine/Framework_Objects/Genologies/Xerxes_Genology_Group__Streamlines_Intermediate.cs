
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Streamlines_Intermediate
    <
        TThis,
        TGenology,
        TStreams_Ascending,
        TStreams_Descending
    >:
    Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        TThis,
        TGenology,
        TStreams_Ascending,
        TStreams_Descending
    >, new()
    where TGenology :
    Xerxes_Genology__Intermediate
    <
        TGenology,
        TThis,
        TStreams_Ascending,
        TStreams_Descending
    >, new()
    where TStreams_Ascending :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TStreams_Ascending,
        TGenology,
        TThis
    >, new()
    where TStreams_Descending :
    Xerxes_Genology_Group__Descending_Streams
    <
        TStreams_Descending,
        TGenology,
        TThis
    >, new()
    {
        public TStreams_Ascending With__Ancestors { get; }
        public TStreams_Descending With__Descendants { get; }

        public Xerxes_Genology_Group__Streamlines_Intermediate()
        {
            With__Ancestors =
                Protected_Link__Child_Group__Genology_Group<TThis, TStreams_Ascending>();

            With__Descendants =
                Protected_Link__Child_Group__Genology_Group<TThis, TStreams_Descending>();
        }
    }
}

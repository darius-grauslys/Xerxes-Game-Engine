
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TDescending_Stream,
        TAscending_Stream
    > :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TDescending_Stream,
        TAscending_Stream
    >
    where TGenology : 
    Xerxes_Genology
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TThis
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TGenology,
        TThis
    >, new()
    {
        public TDescending_Stream With__Descendants { get; private set; }
        public TAscending_Stream With__Ancestors { get; private set; }

        protected internal override void Handle_Linking__Genology_Group()
        {
            With__Descendants =
                Protected_Link__Child_Group__Genology_Group
                <
                    TThis,
                    TDescending_Stream
                >();
            With__Ancestors =
                Protected_Link__Child_Group__Genology_Group
                <
                    TThis,
                    TAscending_Stream
                >();
        }
    }

    public abstract class Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TDescending_Stream
    >:
    Xerxes_Genology_Group
    <
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TDescending_Stream
    >
    where TGenology :
    Xerxes_Genology
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TThis
    >, new()
    {
        public TDescending_Stream With__Descendants { get; private set; }

        protected internal override void Handle_Linking__Genology_Group()
        {
            With__Descendants =
                Protected_Link__Child_Group__Genology_Group
                <
                    TThis,
                    TDescending_Stream
                >();
        }
    }
}


namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TPrimary_Stream,
        TSecondary_Stream
    > :
    Xerxes_Genology_Group__Streamlines
    <
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TPrimary_Stream,
        TSecondary_Stream
    >
    where TGenology : 
    Xerxes_Genology
    where TPrimary_Stream :
    Xerxes_Genology_Group__Streams
    <
        TGenology,
        TThis
    >, new()
    where TSecondary_Stream :
    Xerxes_Genology_Group__Streams
    <
        TGenology,
        TThis
    >, new()
    {
        protected internal TPrimary_Stream Streamlines__Primary_Stream__Protected { get; private set; }
        protected internal TSecondary_Stream Streamlines__Secondary_Stream__Protected { get; private set; }

        protected internal override void Handle_Linking__Genology_Group()
        {
            Streamlines__Primary_Stream__Protected =
                Protected_Link__Child_Group__Genology_Group
                <
                    TThis,
                    TPrimary_Stream
                >();
            Streamlines__Secondary_Stream__Protected =
                Protected_Link__Child_Group__Genology_Group
                <
                    TThis,
                    TSecondary_Stream
                >();
        }
    }

    public abstract class Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TPrimary_Stream
    >:
    Xerxes_Genology_Group__Streamlines
    <
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Streamlines
    <
        TThis,
        TGenology,
        TPrimary_Stream
    >
    where TGenology :
    Xerxes_Genology
    where TPrimary_Stream :
    Xerxes_Genology_Group__Streams
    <
        TGenology,
        TThis
    >, new()
    {
        protected internal TPrimary_Stream Streamlines__Primary_Stream__Protected { get; private set; }

        protected internal override void Handle_Linking__Genology_Group()
        {
            Streamlines__Primary_Stream__Protected =
                Protected_Link__Child_Group__Genology_Group
                <
                    TThis,
                    TPrimary_Stream
                >();
        }
    }

    public abstract class Xerxes_Genology_Group__Streamlines
    <
        TGenology
    > :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where TGenology :
    Xerxes_Genology
    {

    }
}


namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Streamlines
    <
        TThis,
        TGenealogy,
        TPrimary_Stream,
        TSecondary_Stream
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >
    where TThis :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis,
        TGenealogy,
        TPrimary_Stream,
        TSecondary_Stream
    >
    where TGenealogy : 
    Xerxes_Genealogy
    where TPrimary_Stream :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TThis
    >, new()
    where TSecondary_Stream :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TThis
    >, new()
    {
        protected internal TPrimary_Stream Streamlines__Primary_Stream__Protected { get; private set; }
        protected internal TSecondary_Stream Streamlines__Secondary_Stream__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy_Group()
        {
            Streamlines__Primary_Stream__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TPrimary_Stream
                >();
            Streamlines__Secondary_Stream__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TSecondary_Stream
                >();
        }
    }

    public abstract class Xerxes_Genealogy_Group__Streamlines
    <
        TThis,
        TGenealogy,
        TPrimary_Stream
    >:
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >
    where TThis :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis,
        TGenealogy,
        TPrimary_Stream
    >
    where TGenealogy :
    Xerxes_Genealogy
    where TPrimary_Stream :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TThis
    >, new()
    {
        protected internal TPrimary_Stream Streamlines__Primary_Stream__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy_Group()
        {
            Streamlines__Primary_Stream__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TPrimary_Stream
                >();
        }
    }

    public abstract class Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    where TGenealogy :
    Xerxes_Genealogy
    {

    }
}

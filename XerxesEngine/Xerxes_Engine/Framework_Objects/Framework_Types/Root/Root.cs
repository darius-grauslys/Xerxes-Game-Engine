
namespace Xerxes
{
    public abstract class Root :
    Root
    <
        Xerxes_Genology__Standard_Root
    >
    {

    }

    public abstract class Root
    <
        TGenology
    > :
    Root
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines_Intermediate
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Intermediate
            <
                TGenology
            >
        >,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Intermediate
            <
                TGenology
            >
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        Xerxes_Genology_Group__Standard_Streamlines_Intermediate
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Intermediate
            <
                TGenology
            >
        >,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            Xerxes_Genology_Group__Standard_Streamlines_Intermediate
            <
                TGenology
            >
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines
    > :
    Root
    <
        TGenology,
        TStreamlines,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        TStreamlines,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TGenology,
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            TStreamlines
        >
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream
    > :
    Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            TStreamlines
        >,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TGenology,
        TDescending_Stream,
        Xerxes_Genology_Group__Standard_Ascending_Streams
        <
            TGenology,
            TStreamlines
        >
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TStreamlines
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream
    > :
    Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        Xerxes_Genology_Group__Standard_Associations
        <
            TGenology
        >,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TGenology,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TGenology,
        TStreamlines
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations
    > :
    Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TGenology,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TGenology,
        TStreamlines
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TAssociations,
        TGenology
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations,
        TEndpoints
    > :
    Xerxes_Object
    <
        TGenology
    >
    where TGenology :
    Xerxes_Genology
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream,
        TAssociations,
        TEndpoints
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TGenology,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TGenology,
        TStreamlines
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TAssociations,
        TGenology
    >, new()
    where TEndpoints :
    Xerxes_Genology_Group__Endpoints
    <
        TEndpoints,
        TGenology
    >, new()
    {
        internal virtual void Internal_Configure__Root_Base(SA__Configure_Root e)
        {
            Handle__Configure__Root_Base(e);

            Invoke__Ascending(e);
            Invoke__Descending(e);
        }

        protected virtual void Handle__Configure__Root_Base(SA__Configure_Root e){}

        protected internal abstract void Execute();
    }

}

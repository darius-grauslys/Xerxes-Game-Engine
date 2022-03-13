
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
        TGenology
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines,
        TAssociations
    > :
    Root
    <
        TGenology,
        TStreamlines,
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
        TAssociations,
        Xerxes_Genology_Group__Standard_Endpoints
        <
            TGenology
        >
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TGenology
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TGenology
    >, new()
    {

    }

    public abstract class Root
    <
        TGenology,
        TStreamlines,
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
        TAssociations,
        TEndpoints
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TGenology
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TGenology
    >, new()
    where TEndpoints :
    Xerxes_Genology_Group__Endpoints
    <
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

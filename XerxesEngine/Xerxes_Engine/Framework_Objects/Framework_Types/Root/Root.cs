
namespace Xerxes
{
    public abstract class Root :
    Root
    <
        Xerxes_Genealogy__Standard
    >
    {
        public Root()
        {
            Genealogy
                .With__Streamlines
                    .With__Descendants
                        .Extending<SA__Configure_Root>()
                    .Finish__With_Descendants
                    .With__Ancestors
                        .Extending<SA__Configure_Root>();
        }
    }

    public abstract class Root
    <
        TGenealogy
    > :
    Root
    <
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
        <
            TGenealogy
        >,
        Xerxes_Genealogy_Group__Standard_Associations
        <
            TGenealogy
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            TGenealogy
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    <
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
        <
            TGenealogy
        >,
        Xerxes_Genealogy_Group__Standard_Associations
        <
            TGenealogy
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            TGenealogy
        >
    >, new()
    {

    }

    public abstract class Root
    <
        TGenealogy,
        TStreamlines
    > :
    Root
    <
        TGenealogy,
        TStreamlines,
        Xerxes_Genealogy_Group__Standard_Associations
        <
            TGenealogy
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            TGenealogy
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    <
        TGenealogy,
        TStreamlines,
        Xerxes_Genealogy_Group__Standard_Associations
        <
            TGenealogy
        >,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            TGenealogy
        >
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >, new()
    {

    }

    public abstract class Root
    <
        TGenealogy,
        TStreamlines,
        TAssociations
    > :
    Root
    <
        TGenealogy,
        TStreamlines,
        TAssociations,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            TGenealogy
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    <
        TGenealogy,
        TStreamlines,
        TAssociations,
        Xerxes_Genealogy_Group__Standard_Endpoints
        <
            TGenealogy
        >
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TGenealogy
    >, new()
    {

    }

    public abstract class Root
    <
        TGenealogy,
        TStreamlines,
        TAssociations,
        TEndpoints
    > :
    Xerxes_Object
    <
        TGenealogy
    >
    where TGenealogy :
    Xerxes_Genealogy
    <
        TGenealogy,
        TStreamlines,
        TAssociations,
        TEndpoints
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TGenealogy
    >, new()
    where TEndpoints :
    Xerxes_Genealogy_Group__Endpoints
    <
        TGenealogy
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

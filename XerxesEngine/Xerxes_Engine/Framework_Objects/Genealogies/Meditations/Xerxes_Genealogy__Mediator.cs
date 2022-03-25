
namespace Xerxes
{
    public abstract class Xerxes_Genealogy__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever
    > :
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations
    >
    where TThis :
    Xerxes_Genealogy__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TStreamlines,
        TThis,
        TExtender
    >, new()
    where TExtender :
    Xerxes_Genealogy_Group__Stream_Mediator
    <
        TThis,
        TStreamlines
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Diverter
    <
        TAssociations,
        TThis,
        TReciever
    >, new()
    where TReciever :
    Xerxes_Genealogy_Group__Wrapped_Mediator
    <
        TThis,
        TAssociations
    >, new()
    {
        
    }

    public abstract class Xerxes_Genealogy__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever,
        TEndpoints
    > :
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations,
        TEndpoints
    >
    where TThis :
    Xerxes_Genealogy__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever,
        TEndpoints
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TThis
    >, new()
    where TEndpoints :
    Xerxes_Genealogy_Group__Endpoints
    <
        TThis
    >, new()
    {

    }
}

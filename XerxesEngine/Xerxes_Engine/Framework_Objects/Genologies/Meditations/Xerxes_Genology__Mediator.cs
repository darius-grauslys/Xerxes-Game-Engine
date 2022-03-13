
namespace Xerxes
{
    public abstract class Xerxes_Genology__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever
    > :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TAssociations
    >
    where TThis :
    Xerxes_Genology__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TThis,
        TExtender
    >, new()
    where TExtender :
    Xerxes_Genology_Group__Extending_Mediator
    <
        TThis,
        TStreamlines
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Diverter
    <
        TAssociations,
        TThis,
        TReciever
    >, new()
    where TReciever :
    Xerxes_Genology_Group__Recieving_Mediator
    <
        TThis,
        TAssociations
    >, new()
    {
        
    }

    public abstract class Xerxes_Genology__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever,
        TEndpoints
    > :
    Xerxes_Genology
    <
        TThis,
        TStreamlines,
        TAssociations,
        TEndpoints
    >
    where TThis :
    Xerxes_Genology__Mediator
    <
        TThis,
        TStreamlines,
        TExtender,
        TAssociations,
        TReciever,
        TEndpoints
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genology_Group__Associations
    <
        TThis
    >, new()
    where TEndpoints :
    Xerxes_Genology_Group__Endpoints
    <
        TThis
    >, new()
    {

    }
}

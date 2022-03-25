
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Mediated_Associations 
    <
        TThis,
        TGenology,
        XBase,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >:
    Xerxes_Genology_Group__Associations
    <
        TThis,
        TGenology
    >
    where TThis :
    Xerxes_Genology_Group__Mediated_Associations
    <
        TThis,
        TGenology,
        XBase,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where TGenology :
    Xerxes_Genology__Generic
    <
        TGenology,
        XBase,
        TThis,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        TGenology
    >, new()
    where GStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        GStreamlines,
        TGenology,
        XBase,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        GAscending_Stream,
        TGenology,
        GStreamlines
    >, new()
    where GDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        GDescending_Stream,
        TGenology,
        GStreamlines
    >, new()
    {
        protected TMediator Protected_Declare__Mediation__Mediated_Associations
        <
            SA,
            XTarget,
            TMediator
        >()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        where TMediator :
        Xerxes_Genology_Group__Mediator
        <
            TMediator,
            TGenology,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream,
            TThis,
            XBase,
            XTarget
        >, new()
        {
            return
                Protected_Link__Child_Group__Genology_Group
                <TThis, TMediator>();
        }
    }
}

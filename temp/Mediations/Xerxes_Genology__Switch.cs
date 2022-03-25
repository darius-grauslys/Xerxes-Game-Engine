
namespace Xerxes
{
    /*
    public class Xerxes_Genology__Switch
    <
        XBase,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >:
    Xerxes_Genology__Switch
    <
        Xerxes_Genology__Switch
        <
            XBase,
            GAssociations,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream
        >,
        XBase,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        Xerxes_Genology__Switch
        <
            XBase,
            GAssociations,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream
        >
    >, new()
    where GAssociations :
    Xerxes_Genology_Group__Mediated_Associations
    <
        GAssociations,
        Xerxes_Genology__Switch
        <
            XBase,
            GAssociations,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream
        >,
        XBase,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        GStreamlines,
        Xerxes_Genology__Switch
        <
            XBase,
            GAssociations,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream
        >,
        XBase,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GAscending_Stream :
    Xerxes_Genology_Group__Mediated_Ascending_Streams
    <
        GAscending_Stream,
        Xerxes_Genology__Switch
        <
            XBase,
            GAssociations,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream
        >,
        XBase,
        GStreamlines
    >, new()
    where GDescending_Stream :
    Xerxes_Genology_Group__Mediated_Descending_Streams
    <
        GDescending_Stream,
        Xerxes_Genology__Switch
        <
            XBase,
            GAssociations,
            GStreamlines,
            GAscending_Stream,
            GDescending_Stream
        >,
        XBase,
        GStreamlines
    >, new()
    {

    }
    */

    public abstract class Xerxes_Genology__Switch
    <
        TThis,
        XBase,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >:
    Xerxes_Genology__Generic
    <
        TThis,
        XBase,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >
    where TThis :
    Xerxes_Genology__Switch
    <
        TThis,
        XBase,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        TThis
    >, new()
    where GAssociations :
    Xerxes_Genology_Group__Mediated_Associations
    <
        GAssociations,
        TThis,
        XBase,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        GStreamlines,
        TThis,
        XBase,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GAscending_Stream :
    Xerxes_Genology_Group__Mediated_Ascending_Streams
    <
        GAscending_Stream,
        TThis,
        XBase,
        GStreamlines
    >, new()
    where GDescending_Stream :
    Xerxes_Genology_Group__Mediated_Descending_Streams
    <
        GDescending_Stream,
        TThis,
        XBase,
        GStreamlines
    >, new()
    {
        
    }
}

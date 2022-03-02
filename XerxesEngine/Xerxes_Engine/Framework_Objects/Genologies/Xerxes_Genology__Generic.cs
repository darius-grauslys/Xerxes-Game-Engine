
namespace Xerxes
{
    public class Xerxes_Genology__Generic
    <
        TThis,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >:
    Xerxes_Genology__Intermediate
    <
        TThis,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >
    where TThis : 
    Xerxes_Genology__Generic
    <
        TThis, 
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GAssociations : 
    Xerxes_Genology_Group__Associations
    <
        GAssociations, 
        TThis
    >, new()
    where GStreamlines  : 
    Xerxes_Genology_Group__Streamlines
    <
        GStreamlines, 
        TThis
    >, new()
    where GAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        GAscending_Stream,
        TThis,
        GStreamlines
    >, new()
    where GDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        GDescending_Stream,
        TThis,
        GStreamlines
    >, new()
    {
        public GAssociations Declare__Associations { get; }
        
        public Xerxes_Genology__Generic()
        {
            Declare__Associations = 
                Protected_Link__Genology_Group__Genology<TThis, GAssociations>();
        }
    }
}

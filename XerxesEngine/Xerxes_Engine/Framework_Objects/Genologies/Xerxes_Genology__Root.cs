
namespace Xerxes
{
    public abstract class Xerxes_Genology__Root
    <
        TThis,
        TEndpoints,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >:
    Xerxes_Genology__Generic
    <
        TThis,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >
    where TThis : 
    Xerxes_Genology__Root
    <
        TThis, 
        TEndpoints,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where TEndpoints :
    Xerxes_Genology_Group__Endpoints
    <
        TEndpoints,
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
    where GStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        GStreamlines,
        TThis,
        GAscending_Stream,
        GDescending_Stream
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
        public TEndpoints Declare__Endpoints { get; }

        internal Root_Base Genology_Root__Root__Internal
            => Genology__Enclosing_Object__Internal as Root_Base;



        public Xerxes_Genology__Root()
        {
            Declare__Endpoints =
                new TEndpoints();
        }
    }
}

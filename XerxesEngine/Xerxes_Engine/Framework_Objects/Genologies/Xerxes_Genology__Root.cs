
namespace Xerxes
{
    public abstract class Xerxes_Genology__Root
    <
        TThis,
        TExports,
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
        TExports,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where TExports :
    Xerxes_Genology_Group__Exports
    <
        TExports,
        TThis
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
        public TExports Declare__Exports { get; }



        public Xerxes_Genology__Root()
        {
            Declare__Exports =
                new TExports();
        }
    }
}

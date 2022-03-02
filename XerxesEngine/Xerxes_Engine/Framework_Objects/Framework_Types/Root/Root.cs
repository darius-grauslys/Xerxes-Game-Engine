
namespace Xerxes
{
    public abstract class Root :
    Root
    <
        Xerxes_Genology__Standard_Root,
        Xerxes_Genology_Group__Standard_Endpoints<Xerxes_Genology__Standard_Root>,
        Xerxes_Genology_Group__Standard_Associations<Xerxes_Genology__Standard_Root>,
        Xerxes_Genology_Group__Standard_Streamlines<Xerxes_Genology__Standard_Root>,
        Xerxes_Genology_Group__Standard_Streamline_Ancestors<Xerxes_Genology__Standard_Root>,
        Xerxes_Genology_Group__Standard_Streamline_Descendants<Xerxes_Genology__Standard_Root>
    >
    {
    }

    public abstract class Root
    <
        TGenology,
        TExports,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    > :
    Root_Base
    where TGenology : 
    Xerxes_Genology__Root
    <
        TGenology, 
        TExports,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where TExports  : 
    Xerxes_Genology_Group__Endpoints
    <
        TExports, 
        TGenology,
        GAssociations,
        GStreamlines,
        GAscending_Stream,
        GDescending_Stream
    >, new()
    where GAssociations :
    Xerxes_Genology_Group__Associations
    <
        GAssociations,
        TGenology
    >, new()
    where GStreamlines :
    Xerxes_Genology_Group__Streamlines_Intermediate
    <
        GStreamlines,
        TGenology,
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
        protected TGenology Root_Genology { get; }

        protected Root()
        {
            Root_Genology =
                new TGenology();

            Xerxes_Object_Base__Genology__Internal =
                Root_Genology;

            //TODO: enforce ancestries onto genology?
            //probably not, leave it up to the genology
            //to handle SA__Configure_Root
            
            Root_Genology
                .Declare__Streamlines
                    .With__Ancestors
                        .Extending<SA__Configure_Root>()
                    .Finish__With_Ancestors
                    .With__Descendants
                        .Extending<SA__Configure_Root>();
        }
    }
}

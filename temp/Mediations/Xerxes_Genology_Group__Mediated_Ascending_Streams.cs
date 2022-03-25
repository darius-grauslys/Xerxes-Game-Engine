
namespace Xerxes 
{
    public abstract class Xerxes_Genology_Group__Mediated_Ascending_Streams
    <
        TThis,


        TGenology,
        TParent
    >:
    Xerxes_Genology_Group__Ascending_Streams
    <
        TThis,
        TGenology,
        TParent
    >
    where TThis :
    Xerxes_Genology_Group__Mediated_Ascending_Streams
    <
        TThis,
        TGenology,
        TParent
    >
    where TGenology : 
    Xerxes_Genology
    where TParent :
    Xerxes_Genology_Group
    <
        TGenology
    >
    {
        protected void Protected_Divert__Mediated_Ascending_Stream<SA>()
        where SA : Streamline_Argument
        {
            
        }
    }
}

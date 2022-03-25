
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Mediated_Descending_Streams
    <
        TThis,
        TGenology,
        XBase,
        TParent
    >:
    Xerxes_Genology_Group__Descending_Streams
    <
        TThis,
        TGenology,
        TParent
    >
    where TThis :
    Xerxes_Genology_Group__Mediated_Descending_Streams
    <
        TThis,
        TGenology,
        XBase,
        TParent
    >
    where TGenology : 
    Xerxes_Genology
    <
        TGenology,
        XBase
    >, new()
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        TGenology
    >, new()
    where TParent :
    Xerxes_Genology_Group__Template_Streamlines
    <
        TParent,
        TGenology
    >
    {
        protected void Protected_Mediate__Extending__Mediated_Descending_Stream
        <
            SA, 
            XTarget
        >()
        where SA : Streamline_Argument
        where XTarget : Xerxes_Object_Base, new()
        {

        }

        protected void Protected_Mediate__Recieving__Mediated_Descending_Stream
        <
            SA,
            XTarget
        >()
        where SA : Streamline_Argument
        where XTarget : Xerxes_Object_Base, new()
        {
            
        }
    }
}


namespace Xerxes 
{
    public abstract class Xerxes_Genology_Group__Extending_Mediator
    <
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Streams
    <
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
        protected internal void Protected_Mediate__Ancestors__Mediator
        <
            SA,
            XTarget
        >()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Extend__To_Ancestors__Streams
            <
                SA__Mediate
                <
                    XTarget,
                    SA
                >
            >();
        }

        protected internal void Protected_Mediate__Descendants__Mediator
        <
            SA,
            XTarget
        >()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Extend__To_Descendants__Streams
            <
                SA__Mediate
                <
                    XTarget,
                    SA
                >
            >();
        }
    }
}

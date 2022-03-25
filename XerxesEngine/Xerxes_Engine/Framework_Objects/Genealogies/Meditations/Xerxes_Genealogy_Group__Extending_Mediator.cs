
namespace Xerxes 
{
    /// <summary>
    /// This is not to be confused with what Wrapper_Mediator does.
    /// Wrapper_Mediator helps configure an anonymous wrapper Xerxes_Object
    /// who is Ancestor to a desired Descendant (see Mediation for more details.)
    ///
    /// What Stream_Mediator does, is to be a public member of a
    /// Genealogy_Group to help establish streamlines for the
    /// Genealogy's Xerxes_Object.
    /// </summary>
    public abstract class Xerxes_Genealogy_Group__Stream_Mediator
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TParent
    >
    where TGenealogy : 
    Xerxes_Genealogy
    where TParent :
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    {
        protected internal void Protected_Extend__Mediation_To_Ancestors__Stream_Mediator
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

        protected internal void Protected_Extend__Mediation_To_Descendants__Stream_Mediator
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

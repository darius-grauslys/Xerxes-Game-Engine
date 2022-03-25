
namespace Xerxes
{
    /// <summary>
    /// A Wrapped Mediator is returned by a
    /// Diverter to help determine what streamlines
    /// should be mediated towards the wrapped descendant.
    ///
    /// In other words, this should not be a public member of
    /// a Genealogy group but rather a returned constructed
    /// instance to help match the XDescendant generic for the
    /// given association on Diverter.
    /// </summary>
    public abstract class Xerxes_Genealogy_Group__Wrapped_Mediator
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
        /// <summary>
        /// Dependency Injection
        /// </summary>
        internal Wrapper_Streamline_Linker_Base Wrapped_Mediator__Streamline_Linker__Internal { get; set; }

        /// <summary>
        /// Since Wrapper_Mediator is attached only to
        /// Xerxes_Object anonymous instances, we can
        /// cast the Enclosing Genealogy object safely.
        /// </summary>
        protected internal void Protected_Mediate__From_Ancestors__Wrapper_Mediator
        <
            SA
        >()
        where SA :
        Streamline_Argument
        {
            Wrapped_Mediator__Streamline_Linker__Internal
                .Internal_Recieve__Ancestor_Mediation__Wrapper
                <SA>
                (
                    Genealogy_Group__Enclosing_Object__Internal
                    as
                    Xerxes_Object
                );
        }
    }
}

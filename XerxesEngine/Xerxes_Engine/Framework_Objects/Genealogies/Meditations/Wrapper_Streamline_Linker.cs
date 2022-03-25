
namespace Xerxes
{
    internal class Wrapper_Streamline_Linker<XDescendant> :
    Wrapper_Streamline_Linker_Base
    where XDescendant :
    Xerxes_Object_Base, new()
    {
        internal override void Internal_Recieve__Ancestor_Mediation__Wrapper 
        <SA>(Xerxes_Object wrapper_instance)
        {
            wrapper_instance
                .Genealogy
                    .With__Streamlines
                        .With__Ancestors
                            .Recieving
                            <SA__Mediate<XDescendant, SA>>
                            (
                                (e) =>
                                Handle_Mediation__From_Ancestor__Wrapper
                                <SA, XDescendant>
                                (
                                    e,
                                    wrapper_instance
                                )
                            )
                        .Finish__With_Ancestors
                        .With__Descendants
                            .Extending<SA>();
        }
    }
}

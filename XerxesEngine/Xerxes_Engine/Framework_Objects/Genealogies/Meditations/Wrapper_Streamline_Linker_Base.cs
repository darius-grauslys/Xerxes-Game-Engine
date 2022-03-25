
namespace Xerxes
{
    internal abstract class Wrapper_Streamline_Linker_Base
    {
        internal abstract void Internal_Recieve__Ancestor_Mediation__Wrapper
        <SA>(Xerxes_Object wrapper_instance)
        where SA : Streamline_Argument;

        protected void Handle_Mediation__From_Ancestor__Wrapper
        <
            SA, 
            XDescendant
        >
        (
            SA__Mediate<XDescendant, SA> e,
            Xerxes_Object_Base wrapper_instance
        )
        where SA :
        Streamline_Argument
        where XDescendant :
        Xerxes_Object_Base, new()
            => wrapper_instance.Invoke__Descending(e.Mediate__Mediated_Streamline_Argument); 
    }
}

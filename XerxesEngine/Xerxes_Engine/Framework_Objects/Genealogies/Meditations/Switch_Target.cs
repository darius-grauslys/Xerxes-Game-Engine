
namespace Xerxes
{
    internal sealed class Switch_Target
    <
        XTarget
    > :
    Switch_Target_Base
    where XTarget :
    Xerxes_Object_Base, new()
    {
        internal override void Internal_Invoke__Descending__Switch_Target_Base
        <SA>
        (
            SA e,
            Xerxes_Object_Base invoking_instance
        )
        {
            SA__Mediate<XTarget, SA> e_mediate =
                new SA__Mediate<XTarget, SA>();

            e_mediate
                .Mediate__Mediated_Streamline_Argument =
                e;

            invoking_instance
                .Invoke__Descending(e_mediate);
        }
    }
}

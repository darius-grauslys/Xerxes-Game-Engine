
namespace Xerxes
{
    internal sealed class Switch_Table :
    Distinct_Type_Dictionary
    <Xerxes_Object_Base, Switch_Target_Base>
    {
        internal Switch_Target_Base Switch_Table__Active_Object__Internal { get; private set; }

        /// <summary>
        /// Assumes that the instance has already
        /// gone through the mediation setup process.
        /// </summary>
        internal void Internal_Define__Switch_Entry__Switch_Table
        <XTarget>()
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Define__Element__Distinct_Type_Dictionary
                <XTarget>(new Switch_Target<XTarget>());
        }

        internal Switch_Target<XTarget> Internal_Get__Switch_Entry
        <XTarget>()
        where XTarget :
        Xerxes_Object_Base, new()
            => Protected_Get__Element__Distinct_Type_Dictionary<XTarget>() as Switch_Target<XTarget>;

        internal void Internal_Set__Switch_Entry<XTarget>()
        where XTarget :
        Xerxes_Object_Base, new()
            => Switch_Table__Active_Object__Internal =
            Internal_Get__Switch_Entry<XTarget>();
    }
}

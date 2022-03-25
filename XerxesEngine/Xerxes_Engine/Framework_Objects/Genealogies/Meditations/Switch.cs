
namespace Xerxes
{
    internal sealed class Switch :
    Distinct_Type_Dictionary
    <Streamline_Argument, Switch_Table>
    {
        internal void Internal_Declare__Switch_Table__Switch<SA>()
        where SA : Streamline_Argument
        {
            Protected_Define__Element__Distinct_Type_Dictionary
                <SA>(new Switch_Table());
        }

        internal void Internal_Add__Switch_Table_Entry__Switch
        <SA, XTarget>()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            bool contains_table =
                Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary<SA>();

            if (!contains_table)
                Internal_Declare__Switch_Table__Switch<SA>();

            Protected_Get__Element__Distinct_Type_Dictionary
                <SA>()
                .Internal_Define__Switch_Entry__Switch_Table
                <XTarget>();
        }

        internal void Internal_Set__Active_Object__Switch
        <SA, XTarget>()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Get__Element__Distinct_Type_Dictionary
                <SA>()
                .Internal_Set__Switch_Entry
                <XTarget>();
        }

        internal void Internal_Set__All__Switch
        <XTarget>()
        where XTarget :
        Xerxes_Object_Base, new()
        {
            foreach(Switch_Table table in Protected_Get__Elements__Distinct_Typed_Dictionary())
            {
                table
                    .Internal_Set__Switch_Entry<XTarget>();
            }
        }

        internal void Internal_Mediate__Descending__Switch
        <SA>(SA e, Xerxes_Object_Base invoking_instance)
        where SA :
        Streamline_Argument
        {
            Protected_Get__Element__Distinct_Type_Dictionary
                <SA>()
                .Switch_Table__Active_Object__Internal
                .Internal_Invoke__Descending__Switch_Target_Base(e, invoking_instance);
        }
    }
}

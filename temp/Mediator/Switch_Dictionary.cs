
using System.Collections.Generic;

namespace Xerxes
{
    internal sealed class Switch_Dictionary :
    Distinct_Type_Dictionary<Streamline_Argument, Target_Table>
    {
        internal void Internal_Add__Switch<SA, XTarget>(XTarget target)
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base
        {
            bool contains_switch_type =
                Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary<SA>();

            if (!contains_switch_type)
                Protected_Define__Element__Distinct_Type_Dictionary<SA>(new Target_Table());

            Protected_Get__Element__Distinct_Type_Dictionary<SA>()
                .Internal_Add__Target(target);
        }

        internal IEnumerable<Xerxes_Object_Base> Internal_Get__Targets<SA, XTarget>()
        where SA : 
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base
        {
            return Protected_Get__Element__Distinct_Type_Dictionary<SA>()
                .Internal_Get__Targets<XTarget>();
        }
    }
}

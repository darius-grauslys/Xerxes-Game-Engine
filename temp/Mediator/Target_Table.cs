
using System.Collections.Generic;
using System.Linq;

namespace Xerxes
{
    internal sealed class Target_Table :
    Distinct_Type_Dictionary<Xerxes_Object_Base, List<Xerxes_Object_Base>>
    {
        internal void Internal_Add__Target<XTarget>(XTarget target)
        where XTarget :
        Xerxes_Object_Base
        {
            bool contains_target_type =
                Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary<XTarget>();

            if (!contains_target_type)
                Protected_Define__Element__Distinct_Type_Dictionary
                    <XTarget>(new List<Xerxes_Object_Base>());

            Protected_Get__Element__Distinct_Type_Dictionary<XTarget>()
                .Add(target);
        }

        internal List<Xerxes_Object_Base> Internal_Get__Targets<XTarget>()
        where XTarget :
        Xerxes_Object_Base
        {
            return
                Protected_Get__Element__Distinct_Type_Dictionary<XTarget>()
                .ToList();
        }
    }
}

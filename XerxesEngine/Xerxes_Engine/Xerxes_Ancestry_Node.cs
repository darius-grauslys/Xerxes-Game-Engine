using System.Collections.Generic;

namespace Xerxes_Engine
{
    public class Xerxes_Ancestry_Node
    {
        internal Xerxes_Ancestry_Node Xerxes_Ancestry__ANCESTOR_NODE__Internal { get; }

        internal Xerxes_Object_Base 
            Xerxes_Ancestry_Node__TREE_MEMBER__Internal { get; }

        internal List<Xerxes_Ancestry_Node> 
            Xerxes_Ancestry_Node__DESCENDANTS__Internal { get; }

        internal Xerxes_Ancestry_Node
        (
            Xerxes_Ancestry_Node parent,
            Xerxes_Object_Base treeMember
        )
        {
            Xerxes_Ancestry__ANCESTOR_NODE__Internal =
                parent;

            Xerxes_Ancestry_Node__TREE_MEMBER__Internal = 
                treeMember;

            Xerxes_Ancestry_Node__DESCENDANTS__Internal =
                new List<Xerxes_Ancestry_Node>();
        }

        protected void Protected_Associate__Descendant__Xerxes_Ancestry_Node
        (
            Xerxes_Ancestry_Node descendant
        )
        {
            Xerxes_Ancestry_Node__DESCENDANTS__Internal
                .Add(descendant);
        }
    }
}

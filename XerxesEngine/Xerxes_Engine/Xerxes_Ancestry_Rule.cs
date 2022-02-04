namespace Xerxes
{
    public sealed class Xerxes_Ancestry_Rule<XAncestor, XDescendant> :
    Xerxes_Association_Rule
    {
        internal Xerxes_Ancestry_Rule(){}

        internal override bool Internal_Check_If__Valid_Descendant__Xerxes_Association_Rule
        (Xerxes_Object_Base descendant)
        {
            return descendant is XDescendant;
        }

        internal override bool Internal_Check_If__Valid_Ancestor__Xerxes_Association_Rule
        (Xerxes_Object_Base ancestor)
        {
            return ancestor is XAncestor;
        }
    }
}

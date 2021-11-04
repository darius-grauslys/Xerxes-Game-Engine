using System.Collections.Generic;

namespace Xerxes_Engine
{
    internal static class Xerxes_Association_Rule_Dictionary 
    {
        private static readonly Dictionary<Xerxes_Object_Base, List<Xerxes_Association_Rule>>
            INSTANCE_RULING_TABLE =
            new Dictionary<Xerxes_Object_Base, List<Xerxes_Association_Rule>>();

        internal static void Internal_Declare__Ruling
        (
            Xerxes_Object_Base instance,
            Xerxes_Association_Rule ruling
        )
        {
            bool instancePresent =
                INSTANCE_RULING_TABLE
                .ContainsKey(instance);

            if (!instancePresent)
            {
                INSTANCE_RULING_TABLE
                .Add
                (
                    instance,
                    new List<Xerxes_Association_Rule>()
                );
            }

            INSTANCE_RULING_TABLE[instance]
            .Add(ruling);
        }

        internal static bool? Internal_Check_If__Is_Valid_Association
        (
            Xerxes_Object_Base ancestor,
            Xerxes_Object_Base descendant,
            out Xerxes_Object_Base invalidAncestor,
            out Xerxes_Object_Base invalidDescendant
        )
        {
            bool hasRulesFor_Ancestor =
                INSTANCE_RULING_TABLE
                .ContainsKey(ancestor);
            bool hasRulesFor_Descendant =
                INSTANCE_RULING_TABLE
                .ContainsKey(descendant);

            invalidAncestor = invalidDescendant = null;

            if (!hasRulesFor_Ancestor)
            {
                invalidAncestor = ancestor;
            }

            if (!hasRulesFor_Descendant)
            {
                invalidDescendant = descendant;
            }

            if (!hasRulesFor_Ancestor || !hasRulesFor_Descendant)
            {
                return null;
            }

            IEnumerator<Xerxes_Association_Rule> ancestor_Rulings =
                INSTANCE_RULING_TABLE[ancestor].GetEnumerator();
            IEnumerator<Xerxes_Association_Rule> descendant_Rulings =
                INSTANCE_RULING_TABLE[descendant].GetEnumerator();

            bool successAncestor = 
                Private_Check_If__Is_Valid_Ancestor
                (
                    ancestor,
                    descendant_Rulings
                );

            bool successDescendant =
                Private_Check_If__Is_Valid_Descendant
                (
                    descendant,
                    ancestor_Rulings
                );

            if (!successAncestor)
            {
                invalidAncestor = ancestor;
            }

            if (!successDescendant)
            {
                invalidDescendant = descendant;
            }

            return successAncestor && successDescendant;
        }


        private static bool Private_Check_If__Is_Valid_Ancestor
        (
            Xerxes_Object_Base ancestor,
            IEnumerator<Xerxes_Association_Rule> descendantRulings
        )
        {
            bool success = false;

            while (!success && descendantRulings.MoveNext())
            {
                success = 
                    descendantRulings
                    .Current
                    .Internal_Check_If__Valid_Ancestor__Xerxes_Association_Rule
                    (
                        ancestor
                    );
            }

            return success;
        }

        private static bool Private_Check_If__Is_Valid_Descendant
        (
            Xerxes_Object_Base descendant,
            IEnumerator<Xerxes_Association_Rule> ancestorRulings
        )
        {
            bool success = false;

            while (!success && ancestorRulings.MoveNext())
            {
                success = 
                    ancestorRulings
                    .Current
                    .Internal_Check_If__Valid_Descendant__Xerxes_Association_Rule
                    (
                        descendant
                    );
            }

            return success;
        }
    }
}

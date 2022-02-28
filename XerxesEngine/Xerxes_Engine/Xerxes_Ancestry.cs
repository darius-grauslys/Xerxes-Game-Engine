using System;

namespace Xerxes
{
    public sealed class Xerxes_Ancestry
    {
        internal Xerxes_Ancestry Xerxes_Ancestry__ANCESTOR_NODE__Internal { get; }
        internal Xerxes_Ancestry_Node Xerxes_Ancestry__NODE__Internal  { get; }

        public Xerxes_Ancestry Pop__Focus
            => Xerxes_Ancestry__ANCESTOR_NODE__Internal;

        internal Xerxes_Ancestry
        (
            Xerxes_Object_Base node
        )
        : this
        (
            null,
            new Xerxes_Ancestry_Node(node)
        )
        {}

        internal Xerxes_Ancestry
        (
            Xerxes_Ancestry_Node node
        )
        : this
        (
            null,
            node
        )
        {
        }

        private Xerxes_Ancestry
        (
            Xerxes_Ancestry ancestor,
            Xerxes_Ancestry_Node node
        )
        {
            Xerxes_Ancestry__ANCESTOR_NODE__Internal = ancestor;
            Xerxes_Ancestry__NODE__Internal = node;
        }

        public Xerxes_Ancestry Associate<Descendant>
        (ref object identifier) where Descendant : Xerxes_Object_Base, new()
        {
            Descendant descendant = new Descendant();

            identifier = descendant.Xerxes_Object_Base__IDENTIFIER;

            Internal_Associate__Descendant__Xerxes_Ancestry<Descendant>
            (
                descendant
            );

            return this;
        }

        public Xerxes_Ancestry Associate<Descendant>
        () where Descendant : Xerxes_Object_Base, new()
        {
            Descendant descendant = new Descendant();

            Internal_Associate__Descendant__Xerxes_Ancestry<Descendant>
            (
                descendant
            );

            return this;
        }

        public Xerxes_Ancestry Associate__And_Focus<Descendant>
        () where Descendant : Xerxes_Object_Base, new()
        {
            Descendant descendantAndNewFocus = new Descendant();

            Xerxes_Ancestry newFocus =
                Internal_Associate__Descendant__Xerxes_Ancestry<Descendant>
                (
                    descendantAndNewFocus
                );

            return newFocus;
        }

        public Xerxes_Ancestry Focus<Descendant>()
        where Descendant : Xerxes_Object_Base
        {
            Xerxes_Ancestry wrapper = 
                Private_Focus__Xerxes_Ancestry<Descendant>(Xerxes_Ancestry__NODE__Internal);

            if (wrapper != null)
                return wrapper;

            Private_Log_Error__Failure_To_Find_Descendant
            (
                this,
                typeof(Descendant)
            );

            return null;
        }

        private Xerxes_Ancestry Private_Focus__Xerxes_Ancestry<Descendant>
        (Xerxes_Ancestry_Node node)
        where Descendant : Xerxes_Object_Base
        {
            foreach(Xerxes_Ancestry_Node subnode in node.Xerxes_Ancestry_Node__DESCENDANTS__Internal)
            {
                if (subnode.Xerxes_Ancestry_Node__TREE_MEMBER__Internal is Descendant)
                {
                    return new Xerxes_Ancestry(subnode);
                }
            }

            foreach(Xerxes_Ancestry_Node subnode in node.Xerxes_Ancestry_Node__DESCENDANTS__Internal)
            {
                Xerxes_Ancestry wrapper =
                    Private_Focus__Xerxes_Ancestry<Descendant>(subnode);

                if (wrapper != null)
                    return wrapper;
            }

            return null;
        }

        internal Xerxes_Ancestry Internal_Associate__Descendant__Xerxes_Ancestry<A>
        (
            Xerxes_Object_Base descendant
        ) where A : Xerxes_Object_Base
        {
            Xerxes_Ancestry_Node node =
                Xerxes_Linker
                .Internal_Get__Global_Declaration
                (
                    descendant
                );

            if (node != null)
            {
                Private_Log_Verbose__Associated_With_Global_Declaration
                (
                    this,
                    descendant
                );
            }
            else
            {
                node = new Xerxes_Ancestry_Node(descendant);
                Private_Log_Verbose__Associated_With_Anonymous_Declaration
                (
                    this,
                    descendant
                );
            }
            
            Xerxes_Ancestry wrapping = new Xerxes_Ancestry(this, node);

            Xerxes_Ancestry__NODE__Internal
                .Internal_Associate__Descendant__Xerxes_Ancestry_Node
                (
                    node
                );

            return wrapping;
        }

#region Static Logging
        private static void Private_Log_Verbose__Associated_With_Global_Declaration
        (
            Xerxes_Ancestry ancestryNode,
            Xerxes_Object_Base objectOfInterest
        )
        {
            Log.Write__Verbose__Log
            (
                Log.VERBOSE__XERXES_ANCESTRY__USING_GLOBAL_DECLARATION_1,
                ancestryNode,
                objectOfInterest
            );
        }
        
        private static void Private_Log_Verbose__Associated_With_Anonymous_Declaration
        (
            Xerxes_Ancestry ancestryNode,
            Xerxes_Object_Base objectOfInterest
        )
        {
            Log.Write__Verbose__Log
            (
                Log.VERBOSE__XERXES_ANCESTRY__USING_ANONYMOUS_DECLARATION_1,
                ancestryNode,
                objectOfInterest
            );
        }

        private static void Private_Log_Error__Invalid_Association
        (
            Xerxes_Ancestry ancestryNode,
            Xerxes_Object_Base offendingDescendant
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ANCESTRY__FAILED_ASSOCIATION_2,
                ancestryNode,
                ancestryNode.Xerxes_Ancestry__NODE__Internal.Xerxes_Ancestry_Node__TREE_MEMBER__Internal,
                offendingDescendant
            );
        }

        private static void Private_Log_Error__Failure_To_Find_Descendant
        (
            Xerxes_Ancestry ancestryNode,
            Type missingDescendant
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ANCESTRY__FAILED_TO_FIND_DESCENDANT_1,
                ancestryNode,
                missingDescendant
            );
        }

        private static void Private_Log_Error__Failure_To_Find_Ancestor
        (
            Xerxes_Ancestry ancestryNode,
            Type missingAncestor
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ANCESTRY__FAILED_TO_FIND_ANCESTOR_1,
                ancestryNode,
                missingAncestor
            );
        }
#endregion
    }
}

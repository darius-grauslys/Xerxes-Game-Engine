using System;

namespace Xerxes
{
    public sealed class Xerxes_Ancestry<T> :
        Xerxes_Ancestry_Node
        where T : Xerxes_Object_Base 
    {
        internal Xerxes_Ancestry
        (
            Xerxes_Object_Base node
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
            Xerxes_Ancestry_Node ancestor,
            Xerxes_Object_Base node
        )
        : base
        (
            ancestor,
            node
        )
        {
        }

        public Xerxes_Ancestry<T> Associate<Descendant>
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

        public Xerxes_Ancestry<T> Associate<Descendant>
        () where Descendant : Xerxes_Object_Base, new()
        {
            Descendant descendant = new Descendant();

            Internal_Associate__Descendant__Xerxes_Ancestry<Descendant>
            (
                descendant
            );

            return this;
        }

        public Xerxes_Ancestry<Descendant> Associate__And_Focus<Descendant>
        () where Descendant : Xerxes_Object_Base, new()
        {
            Descendant descendantAndNewFocus = new Descendant();

            Xerxes_Ancestry<Descendant> newFocus =
                Internal_Associate__Descendant__Xerxes_Ancestry<Descendant>
                (
                    descendantAndNewFocus
                );

            return newFocus;
        }

        public Xerxes_Ancestry<Descendant> Focus__Descendant<Descendant>
        () where Descendant : Xerxes_Object_Base, new() 
        {
            foreach(Xerxes_Ancestry_Node node in Xerxes_Ancestry_Node__DESCENDANTS__Internal)
            {
                if (!(node.Xerxes_Ancestry_Node__TREE_MEMBER__Internal is Descendant))
                    continue;
                return node as Xerxes_Ancestry<Descendant>;
            }

            Private_Log_Error__Failure_To_Find_Descendant
            (
                this,
                typeof(Descendant)
            );

            return null;
        }

        public Xerxes_Ancestry<Ancestor> Pop__Focus<Ancestor>
        () where Ancestor : Xerxes_Object_Base, new()
        {
            Xerxes_Ancestry_Node ancesterNode = Xerxes_Ancestry__ANCESTOR_NODE__Internal;
            while (ancesterNode != null)
            {
                if (ancesterNode.Xerxes_Ancestry_Node__TREE_MEMBER__Internal is Ancestor)
                    return ancesterNode as Xerxes_Ancestry<Ancestor>;
                ancesterNode = ancesterNode.Xerxes_Ancestry__ANCESTOR_NODE__Internal;
            }

            Private_Log_Error__Failure_To_Find_Ancestor
            (
                this,
                typeof(Ancestor)
            );
            return null;
        }

        internal Xerxes_Ancestry<A> Internal_Associate__Descendant__Xerxes_Ancestry<A>
        (
            Xerxes_Object_Base descendant
        ) where A : Xerxes_Object_Base
        {
            Xerxes_Ancestry<A> node =
                Xerxes_Linker
                .Internal_Get__Global_Declaration
                (
                    descendant
                ) as Xerxes_Ancestry<A>;

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
                Private_Log_Verbose__Associated_With_Anonymous_Declaration
                (
                    this,
                    descendant
                );
            }

            if (node == null)
                node = new Xerxes_Ancestry<A>(this, descendant);

            Protected_Associate__Descendant__Xerxes_Ancestry_Node
            (
                node
            );

            return node;
        }

#region Static Logging
        private static void Private_Log_Verbose__Associated_With_Global_Declaration
        (
            Xerxes_Ancestry<T> ancestryNode,
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
            Xerxes_Ancestry<T> ancestryNode,
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
            Xerxes_Ancestry<T> ancestryNode,
            Xerxes_Object_Base offendingDescendant
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ANCESTRY__FAILED_ASSOCIATION_2,
                ancestryNode,
                ancestryNode.Xerxes_Ancestry_Node__TREE_MEMBER__Internal,
                offendingDescendant
            );
        }

        private static void Private_Log_Error__Failure_To_Find_Descendant
        (
            Xerxes_Ancestry<T> ancestryNode,
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
            Xerxes_Ancestry<T> ancestryNode,
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

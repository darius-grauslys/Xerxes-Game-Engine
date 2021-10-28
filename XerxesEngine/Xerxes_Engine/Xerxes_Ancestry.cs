namespace Xerxes_Engine
{
    public sealed class Xerxes_Ancestry<T> :
        Xerxes_Ancestry_Node
        where T : Xerxes_Object_Base 
    {
        public Xerxes_Ancestry
        (
            Xerxes_Object<T> node
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

        public Xerxes_Ancestry<T> Associate<A>
        (
            Xerxes_Object<A> descendant
        ) where A : Xerxes_Object<A>, IXerxes_Descendant_Of<T>
        {
            Xerxes_Ancestry_Node node = 
                Private_Associate__Descendant__Xerxes_Ancestry
                (
                    descendant
                );

            Protected_Associate__Descendant__Xerxes_Ancestry_Node
            (
                node
            );

            return this;
        }

        public Xerxes_Ancestry<A> Associate__And_Focus<A>
        (
            Xerxes_Object<A> descendantAndNewFocus
        ) where A : Xerxes_Object<A>, IXerxes_Descendant_Of<T>
        {
            Xerxes_Ancestry_Node node =
                Private_Associate__Descendant__Xerxes_Ancestry
                (
                    descendantAndNewFocus
                );

            Xerxes_Ancestry<A> newFocus =
                new Xerxes_Ancestry<A>
                (
                    this, 
                    node
                    .Xerxes_Ancestry_Node__TREE_MEMBER__Internal
                );

            return newFocus;
        }

        public Xerxes_Ancestry<A> Focus__Descendant<A>
        (
            Xerxes_Object<A> descendant
        ) where A : Xerxes_Object<A>, IXerxes_Descendant_Of<T>
        {
            foreach(Xerxes_Ancestry_Node node in Xerxes_Ancestry_Node__DESCENDANTS__Internal)
            {
                if (node.Xerxes_Ancestry_Node__TREE_MEMBER__Internal == descendant)
                    return node as Xerxes_Ancestry<A>;
            }

            Private_Log_Error__Failure_To_Find_Descendant
            (
                this,
                descendant
            );
            return null;
        }

        public Xerxes_Ancestry<A> Focus__Ancestor<A>
        (
            Xerxes_Object<A> ancestor
        ) where A : Xerxes_Object<A>
        {
            Xerxes_Ancestry_Node ancesterNode = Xerxes_Ancestry__ANCESTOR_NODE__Internal;
            while (ancesterNode != null)
            {
                if (ancesterNode.Xerxes_Ancestry_Node__TREE_MEMBER__Internal == ancestor)
                    return ancesterNode as Xerxes_Ancestry<A>;
                ancesterNode = ancesterNode.Xerxes_Ancestry__ANCESTOR_NODE__Internal;
            }

            Private_Log_Error__Failure_To_Find_Ancestor
            (
                this,
                ancestor
            );
            return null;
        }

        private bool Private_Check_If__Is_Not_Valid_Association__Xerxes_Ancestry<A>
        (
            Xerxes_Object<A> descendant
        ) where A : Xerxes_Object<A>, IXerxes_Descendant_Of<T>
        {
            bool thisIsAncestorOfA =
                Xerxes_Ancestry_Node__TREE_MEMBER__Internal is IXerxes_Ancestor_Of<A>;
            return !thisIsAncestorOfA;
        }

        private Xerxes_Ancestry<A> Private_Associate__Descendant__Xerxes_Ancestry<A>
        (
            Xerxes_Object<A> descendant
        ) where A : Xerxes_Object<A>, IXerxes_Descendant_Of<T>
        {
            if (Private_Check_If__Is_Not_Valid_Association__Xerxes_Ancestry(descendant))
            {
                Private_Log_Error__Invalid_Association
                (
                    this,
                    descendant
                );
                return null;
            }
            
            Xerxes_Ancestry<A> globalDeclaration =
                Xerxes_Linker
                .Internal_Get__Global_Declaration
                (
                    descendant
                ) as Xerxes_Ancestry<A>;

            if (globalDeclaration != null)
            {
                Private_Log_Verbose__Associated_With_Global_Declaration
                (
                    this,
                    descendant
                );
                return globalDeclaration;
            }

            Private_Log_Verbose__Associated_With_Anonymous_Declaration
            (
                this,
                descendant
            );
            Xerxes_Ancestry<A> node =
                new Xerxes_Ancestry<A>(this, descendant);

            return node;
        }

#region Static Logging
        private static void Private_Log_Verbose__Associated_With_Global_Declaration
        (
            Xerxes_Ancestry<T> ancestryNode,
            Xerxes_Object_Base objectOfInterest
        )
        {
            Log.Internal_Write__Verbose__Log
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
            Log.Internal_Write__Verbose__Log
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
            Log.Internal_Write__Log
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
            Xerxes_Object_Base missingDescendant
        )
        {
            Log.Internal_Write__Log
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
            Xerxes_Object_Base missingAncestor
        )
        {
            Log.Internal_Write__Log
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

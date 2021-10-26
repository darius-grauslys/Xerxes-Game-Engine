using System;
using System.Collections.Generic;

namespace Xerxes_Engine
{
    /// <summary>
    /// Performs the sealing process of Xerxes_Objects
    /// prior to engine runtime.
    /// </summary>
    public static class Xerxes_Linker
    {
        private static readonly Dictionary<Xerxes_Object_Base, Xerxes_Ancestry_Node>
            _Xerxes_Linker__GLOBAL_ANCESTRIES = new Dictionary<Xerxes_Object_Base, Xerxes_Ancestry_Node>();

        private static Dictionary<Type, Stack<Xerxes_Object_Base>>
            _Xerxes_Upstream__CATCHER_CONTEXT { get; }

        public static Xerxes_Ancestry<A> Begin__Ancestry<A>
        (
            Xerxes_Object<A> root
        ) where A : Xerxes_Object<A>
        {
            Xerxes_Ancestry<A> ancestry =
                new Xerxes_Ancestry<A>(root);

            _Xerxes_Linker__GLOBAL_ANCESTRIES
                .Add(root, ancestry);

            return ancestry;
        }

        internal static Xerxes_Ancestry_Node Internal_Get__Global_Declaration
        (
            Xerxes_Object_Base declaration
        )
        {
            bool hasGlobalDeclaration =
                _Xerxes_Linker__GLOBAL_ANCESTRIES
                .ContainsKey(declaration);

            if (!hasGlobalDeclaration)
                return null;

            return _Xerxes_Linker__GLOBAL_ANCESTRIES[declaration];
        }
        
        internal static bool Internal_Seal__Game
        (
            Game game,
            Export_Dictionary game_Exports
        )
        {
            bool rootDoesNotHave_DefinedDescendants =
                !_Xerxes_Linker__GLOBAL_ANCESTRIES
                .ContainsKey(game);

            if (rootDoesNotHave_DefinedDescendants)
                return false;

            Xerxes_Ancestry_Node rootNode =
                _Xerxes_Linker__GLOBAL_ANCESTRIES[game];

            Xerxes_Linker_Context linker_Context =
                new Xerxes_Linker_Context(game_Exports);

            Private_Internal_Seal
            (
                rootNode,
                linker_Context
            );

            return true;
        }

        private static void Private_Internal_Seal
        (
            Xerxes_Ancestry_Node treeMember,
            Xerxes_Linker_Context linker_Context
        )
        {
            Xerxes_Object_Base treeMemberObject =
                treeMember
                .Xerxes_Ancestry_Node__TREE_MEMBER__Internal;

            Private_Log_Verbose__Sealing_Object
            (
                treeMemberObject
            );

            Private_Link__Object__To_Context
            (
                treeMemberObject,
                linker_Context
            );
            
            if (treeMember.Xerxes_Ancestry_Node__DESCENDANTS__Internal.Count == 0)
                return;

            Private_Push__Object__To_Context
            (
                treeMemberObject,
                linker_Context
            );

            foreach(Xerxes_Ancestry_Node childNode in treeMember.Xerxes_Ancestry_Node__DESCENDANTS__Internal)
            {
                Private_Internal_Seal(childNode, linker_Context);
            }

            Private_Pop__Object__From_Context
            (
                treeMemberObject,
                linker_Context
            );
        }

        /// <summary>
        /// This will first link receivers to the context
        /// then it will push extenders to the context.
        /// </summary>
        private static void Private_Link__Object__To_Context
        (
            Xerxes_Object_Base treeMemberObject,
            Xerxes_Linker_Context linker_Context
        )
        {
            Private_Operate__Table__On_Context
            (
                treeMemberObject.Xerxes_Object_Base__ASCENDING_EXTENDING_STREAMLINES__Internal,
                linker_Context.Internal_Link__Ascending_Extender__Xerxes_Linker_Context
            );

            Private_Operate__Table__On_Context
            (
                treeMemberObject.Xerxes_Object_Base__DESCENDING_RECEIVING_STREAMLINES__Internal,
                linker_Context.Internal_Link__Descending_Receiver__Xerxes_Linker_Context
            );
        }

        private static void Private_Push__Object__To_Context
        (
            Xerxes_Object_Base treeMemberObject,
            Xerxes_Linker_Context linker_Context
        )
        {
            Private_Operate__Table__On_Context
            (
                treeMemberObject.Xerxes_Object_Base__ASCENDING_RECEIVING_STREAMLINES__Internal,
                linker_Context.Internal_Push__Upstream_Receiver__Xerxes_Linker_Context
            );

            Private_Operate__Table__On_Context
            (
                treeMemberObject.Xerxes_Object_Base__DESCENDING_EXTENDING_STREAMLINES__Internal,
                linker_Context.Internal_Push__Downstream_Extender__Xerxes_Linker_Context
            );
        }

        private static void Private_Pop__Object__From_Context
        (
            Xerxes_Object_Base treeMemberObject,
            Xerxes_Linker_Context linker_Context
        )
        {
            Private_Operate__Table__On_Context
            (
                treeMemberObject
                .Xerxes_Object_Base__ASCENDING_RECEIVING_STREAMLINES__Internal
                .Internal_Get__Types__Distinct_Typed_Dictionary(),
                linker_Context.Internal_Pop__Upstream_Receiver__Xerxes_Linker_Context
            );

            Private_Operate__Table__On_Context
            (
                treeMemberObject
                .Xerxes_Object_Base__DESCENDING_EXTENDING_STREAMLINES__Internal
                .Internal_Get__Types__Distinct_Typed_Dictionary(),
                linker_Context.Internal_Pop__Downstream_Extender__Xerxes_Linker_Context
            );
        }

        private static void Private_Operate__Table__On_Context
        (
            Streamline_Dictionary dictionary,
            Action<Type,Streamline_Base> contextOperation
        )
        {
            IEnumerable<KeyValuePair<Type,Streamline_Base>> streamlines =
                dictionary
                .Internal_Get__Entries__Distinct_Typed_Dictionary();

            foreach(KeyValuePair<Type,Streamline_Base> pair in streamlines)
            {
                contextOperation
                (
                    pair.Key,
                    pair.Value
                );
            }
        }

        private static void Private_Operate__Table__On_Context
        (
            IEnumerable<Type> streamline_Types,
            Action<Type> contextOperation
        )
        {
            foreach(Type t in streamline_Types)
            {
                contextOperation(t);
            }
        }
#region Static Logging
        private static void Private_Log_Verbose__Sealing_Object
        (
            Xerxes_Object_Base xerxes_Object_Base
        )
        {
            Log.Internal_Write__Verbose__Log
            (
                Log.VERBOSE__XERXES_LINKER__SEALING_OBJECT_1,
                nameof(Xerxes_Linker),
                xerxes_Object_Base
            );
        }
#endregion 
    }
}

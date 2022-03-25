using System;
using System.Collections.Generic;

namespace Xerxes
{
    /// <summary>
    /// Performs the sealing process of Xerxes_Objects
    /// prior to engine runtime.
    /// </summary>
    internal static class Xerxes_Linker
    {
        private static Dictionary<Type, Stack<Xerxes_Object_Base>>
            _Xerxes_Upstream__CATCHER_CONTEXT { get; }
        
        internal static bool Internal_Seal
        (
            Xerxes_Object_Base xobj
        )
        {
            Xerxes_Genealogy xobj_genology =
                xobj.Xerxes_Object_Base__Genealogy__Internal;

            Xerxes_Linker_Context linker_Context =
                new Xerxes_Linker_Context(xobj.Xerxes_Object_Base__ENDPOINTS__Internal);

            Private_Seal__Recursively
            (
                xobj_genology,
                linker_Context
            );

            return true;
        }

        private static void Private_Seal__Recursively
        (
            Xerxes_Genealogy genealogy,
            Xerxes_Linker_Context linker_Context
        )
        {
            Xerxes_Object_Base treeMemberObject =
                genealogy
                .Genealogy__Enclosing_Object__Internal;

            Private_Log_Verbose__Sealing_Object
            (
                treeMemberObject
            );

            Private_Link__Object__To_Context
            (
                treeMemberObject,
                linker_Context
            );

            treeMemberObject.Internal_Root__Xerxes_Engine_Object();

            if 
            (
                genealogy.Genealogy__DESCENDANT_GENOLOGIES__Internal.Count == 0
            )
            {
                Log.Write__Verbose__Log("Has no children.", treeMemberObject);
                return;
            }

            Private_Push__Object__To_Context
            (
                treeMemberObject,
                linker_Context
            );

            foreach(Xerxes_Genealogy child_genology in genealogy.Genealogy__DESCENDANT_GENOLOGIES__Internal)
            {
                Private_Seal__Recursively(child_genology, linker_Context);
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
                .Protected_Get__Types__Distinct_Typed_Dictionary(),
                linker_Context.Internal_Pop__Upstream_Receiver__Xerxes_Linker_Context
            );

            Private_Operate__Table__On_Context
            (
                treeMemberObject
                .Xerxes_Object_Base__DESCENDING_EXTENDING_STREAMLINES__Internal
                .Protected_Get__Types__Distinct_Typed_Dictionary(),
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
                .Protected_Get__Entries__Distinct_Typed_Dictionary();

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
            Log.Write__Verbose__Log
            (
                Log.VERBOSE__XERXES_LINKER__SEALING_OBJECT_1,
                xerxes_Object_Base,
                xerxes_Object_Base
            );
        }
#endregion 
    }
}

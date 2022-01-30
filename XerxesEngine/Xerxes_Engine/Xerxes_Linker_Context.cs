using System;
using System.Collections.Generic;

namespace Xerxes_Engine
{
    internal sealed class Xerxes_Linker_Context
    {
        private Dictionary<Type, Stack<Streamline_Base>> _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK { get; }
        private Dictionary<Type, Stack<Streamline_Base>> _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK { get; }

        internal Xerxes_Linker_Context
        (
            Export_Dictionary exports
        )
        {
            _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK =
                new Dictionary<Type, Stack<Streamline_Base>>();
            _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK =
                new Dictionary<Type, Stack<Streamline_Base>>();

            if(exports == null)
                return;

            // The export stream only has upstream catchers.
            IEnumerable<KeyValuePair<Type, Streamline_Base>> exportline_Table =
                exports
                .Export_Dictionary__EXPORTLINES__Internal
                .Internal_Get__Entries__Distinct_Typed_Dictionary();

            foreach(KeyValuePair<Type, Streamline_Base> pair in exportline_Table)
            {
                Internal_Push__Upstream_Receiver__Xerxes_Linker_Context
                (
                    pair.Key,
                    pair.Value
                );
            }
        }

        /// <summary>
        /// Links the upstream extender to the
        /// peek of the upstream receiver stack.
        /// </summary>
        internal void Internal_Link__Ascending_Extender__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_Base
        )
        {
            bool hasDeclaration =
                Private_Check_If__Has_Declaration
                (
                    t,
                    _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK
                );
            
            if (!hasDeclaration)
            {
                Private_Log_Warning__Uncaught_Streamline
                (
                    this,
                    t,
                    streamline_Base,
                    nameof(_Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK)
                );
                return;
            }

            Streamline_Base upstreamReceiver =
                Private_Peek__Xerxes_Linker_Context
                (
                    t,
                    _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK
                );

            upstreamReceiver
                .Internal_Link__Streamline_Base
                (
                    streamline_Base
                );
        }

        internal void Internal_Link__Descending_Receiver__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_Base
        )
        {
            bool hasDeclaration =
                Private_Check_If__Has_Declaration
                (
                    t,
                    _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK
                );

            if (!hasDeclaration)
            {
                Private_Log_Warning__Uncaught_Streamline
                (
                    this,
                    t,
                    streamline_Base,
                    nameof(_Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK)
                );
                return;
            }

            Streamline_Base downstreamExtender =
                Private_Peek__Xerxes_Linker_Context
                (
                    t,
                    _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK
                );

            streamline_Base
                .Internal_Link__Streamline_Base
                (
                    downstreamExtender
                );
        }

        internal void Internal_Push__Upstream_Receiver__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_Base
        ) 
        {
            Private_Push__Xerxes_Linker_Context
            (
                t,
                streamline_Base,
                _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK
            );
        }

        internal void Internal_Push__Downstream_Extender__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_Base
        )
        {
            Private_Push__Xerxes_Linker_Context
            (
                t,
                streamline_Base,
                _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK
            );
        }

        internal void Internal_Pop__Upstream_Receiver__Xerxes_Linker_Context
        (
            Type t
        ) 
        {
            Private_Pop__Xerxes_Linker_Context
            (
                t,
                _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK
            );
        }

        internal void Internal_Pop__Downstream_Extender__Xerxes_Linker_Context
        (
            Type t
        )
        {
            Private_Pop__Xerxes_Linker_Context
            (
                t,
                _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK
            );
        }

        private Streamline_Base Private_Peek__Xerxes_Linker_Context
        (
            Type t,
            Dictionary<Type,Stack<Streamline_Base>> table
        )
        {
            Stack<Streamline_Base> stack = table[t];

            if (stack == null)
            {
                Private_Log_Bug__Incoherent_Context_Pop
                (
                    this,
                    t
                );
            }

            return stack?.Peek();
        }

        private void Private_Push__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_Base,
            Dictionary<Type,Stack<Streamline_Base>> table
        )
        {
            bool hasDeclaration =
                Private_Check_If__Has_Declaration
                (
                    t,
                    table
                );

            if (!hasDeclaration)
                table.Add
                    (
                        t,
                        new Stack<Streamline_Base>()
                    );

            Stack<Streamline_Base> stack = table[t];

            stack.Push(streamline_Base);

            Log.Write__Verbose__Log($"Pushed onto streamline stack: {streamline_Base.GetType()}", this);
        }

        private void Private_Pop__Xerxes_Linker_Context
        (
            Type t,
            Dictionary<Type,Stack<Streamline_Base>> table
        )
        {
            bool hasDeclaration =
                Private_Check_If__Has_Declaration
                (
                    t,
                    table
                );

            if(!hasDeclaration)
                return;

            Stack<Streamline_Base> stack = table[t];

            stack.Pop();
        }

        private bool Private_Check_If__Has_Declaration
        (
            Type t,
            Dictionary<Type,Stack<Streamline_Base>> table
        )
        {
            bool hasDeclaration =
                table
                .ContainsKey(t);

            return hasDeclaration;
        }

#region Static Logging
        private static void Private_Log_Warning__Uncaught_Streamline
        (
            Xerxes_Linker_Context source,
            Type t,
            Streamline_Base streamline_Base,
            string context
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Warning__Alert,
                Log.WARNING__XERXES_LINKER_CONTEXT__UNCAUGHT_STREAMLINE_3C,
                source,
                t,
                streamline_Base,
                context
            );
        }

        private static void Private_Log_Bug__Incoherent_Context_Pop
        (
            Xerxes_Linker_Context source,
            Type t
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Infastructure,
                Log.BUG__XERXES_LINKER_CONTEXT__INCOHERENT_CONTEXT_POP_1,
                source,
                t
            );
        }
#endregion
    }
}

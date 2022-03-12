using System;
using System.Collections.Generic;

namespace Xerxes
{
    internal sealed class Xerxes_Linker_Context
    {
        private Dictionary<Type, Stack<Streamline_Base>> _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK { get; }
        private Dictionary<Type, Stack<Streamline_Base>> _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK { get; }

        internal Xerxes_Linker_Context
        (
            Endpoint_Dictionary exports = null
        )
        {
            _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK =
                new Dictionary<Type, Stack<Streamline_Base>>();
            _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK =
                new Dictionary<Type, Stack<Streamline_Base>>();

            if(exports == null)
                return;

            // --The export stream only has upstream catchers.-- valid from forever ago to 2/4/2022
            // "export" now called endpoint are both ancestral recievers (upstream catchers)
            // and ancestral extenders (downstream extenders)
            IEnumerable<KeyValuePair<Type, Streamline_Base>> exportline_Table =
                exports
                .Internal_Get__Endpoint_Streamlines__Endpoint_Dictionary();

            foreach(KeyValuePair<Type, Streamline_Base> pair in exportline_Table)
            {
                if (pair.Value.Streamline_Base__IS_RECEIVING)
                {
                    Private_Establish__Endpoint_Streamline__Xerxes_Linker_Context
                    (
                        pair.Key,
                        pair.Value,
                        _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK
                    );
                }
                else
                {
                    Private_Establish__Endpoint_Streamline__Xerxes_Linker_Context
                    (
                        pair.Key,
                        pair.Value,
                        _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK
                    );
                }
            }
        }

        private void Private_Establish__Endpoint_Streamline__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_base,
            Dictionary<Type, Stack<Streamline_Base>> table
        )
        {
            if(!table.ContainsKey(t))
            {
                table.Add(t, new Stack<Streamline_Base>());
                table[t].Push
                (
                    streamline_base.Internal_Create__Virtual__Streamline_Base()
                );
            }

            Streamline_Base virtual_streamline =
                table[t].Peek() as Streamline_Base;

            if (streamline_base.Streamline_Base__IS_RECEIVING)
            {
                virtual_streamline
                    .Internal_Link__Extend_Target__Streamline_Base(streamline_base);
                return;
            }

            streamline_base
                .Internal_Link__Extend_Target__Streamline_Base(virtual_streamline);
        }

        /// <summary>
        /// Links the upstream extender to the
        /// peek of the upstream receiver stack.
        /// </summary>
        internal void Internal_Link__Ascending_Extender__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_extender
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
                    streamline_extender,
                    nameof(_Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK)
                );
                return;
            }

            Streamline_Base upstream_receiver =
                Private_Peek__Xerxes_Linker_Context
                (
                    t,
                    _Xerxes_Linker_Context__UPSTREAM_RECEIVING_STACK
                );

            streamline_extender
                .Internal_Link__Extend_Target__Streamline_Base
                (
                    upstream_receiver
                );
        }

        internal void Internal_Link__Descending_Receiver__Xerxes_Linker_Context
        (
            Type t,
            Streamline_Base streamline_receiver
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
                    streamline_receiver,
                    nameof(_Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK)
                );
                return;
            }

            Streamline_Base downstream_extender =
                Private_Peek__Xerxes_Linker_Context
                (
                    t,
                    _Xerxes_Linker_Context__DOWNSTREAM_EXTENDING_STACK
                );

            bool s = downstream_extender
                .Internal_Link__Extend_Target__Streamline_Base
                (
                    streamline_receiver
                );

            string status = s ? "success" : "failure";
            Log.Write__Verbose__Log($"Linking {streamline_receiver}: {status}.", this);
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

            Log.Write__Verbose__Log($"Pushing {t}.", this);

            Stack<Streamline_Base> stack = table[t];

            stack.Push(streamline_Base);
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

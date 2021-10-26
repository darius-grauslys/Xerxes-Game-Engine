using System;
using System.Collections.Generic;

namespace Xerxes_Engine
{
    internal class Xerxes_Sealing_Context
    {
        private Dictionary<Type,Stack<Xerxes_Object_Base>>
            _Xerxes_Sealing_Context__STREAMLINE_CATCH_STACK { get; }

        internal void Internal_Push__Upstream_Catcher__Xerxes_Sealing_Context<T>
        (
            Xerxes_Object_Base catcher
        ) where T : Streamline_Argument
        {
            Type t = typeof(T);
            bool containsKey =
                _Xerxes_Sealing_Context__STREAMLINE_CATCH_STACK
                .ContainsKey(t);
            if (!containsKey)
            {
                _Xerxes_Sealing_Context__STREAMLINE_CATCH_STACK[t] =
                    new Stack<Xerxes_Object_Base>();
            }
            _Xerxes_Sealing_Context__STREAMLINE_CATCH_STACK[t]
                .Push(catcher);
        }

        internal void Internal_Pop__Upstream_Catcher__Xerxes_Sealing_Context<T>
        () where T : Streamline_Argument
        {
            Type t = typeof(T);
            bool containsKey =
                _Xerxes_Sealing_Context__STREAMLINE_CATCH_STACK
                .ContainsKey(t);
            if (!containsKey)
                return;
            Stack<Xerxes_Object_Base> catcherStack =
                _Xerxes_Sealing_Context__STREAMLINE_CATCH_STACK[t];
            if (catcherStack.Peek() == null)
                return;
            catcherStack.Pop();
        }
    }
}

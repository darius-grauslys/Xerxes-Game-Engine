using System;
using System.Collections.Generic;

namespace Xerxes_Engine.State_Management.Implemented_State_Machines
{
    public class Typed_State_Machine : State_Machine
    {
        protected const string Typed_State_Machine__CONTEXTUAL_MESSAGE__FAILED_DEFINE
            = "{0}:{1} -> {2}:{3}";
        protected const string Typed_State_Machine__CONTEXTUAL_MESSAGE__FAILED_REQUEST
            = "{0}:{1} - Not present";

        private Dictionary<Type, State_Handle> _Typed_State_Dictionary__TYPE_TO_HANDLE { get; }

        public Typed_State_Machine(State defaultState = null)
            : base(defaultState)
        {
            _Typed_State_Dictionary__TYPE_TO_HANDLE = new Dictionary<Type, State_Handle>();
            _Typed_State_Dictionary__TYPE_TO_HANDLE
                .Add
                (
                    typeof(State),
                    State_Machine__DEFAULT_STATE_HANDLE
                );
        }

        public void Request__Transition_To_State__Typed_State_Machine<T>() where T : State
        {
            State_Handle handle = Private_Get__State_Handle__Typed_State_Machine<T>();
            
            if (handle == null)
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Engine_Object,
                    Log.ERROR__STATE_MACHINE__FAILED_TO_REQUEST_STATE_1C,
                    this,
                    String.Format
                    (
                        Typed_State_Machine__CONTEXTUAL_MESSAGE__FAILED_REQUEST,
                        typeof(T),
                        handle
                    )
                );

                return;
            }

            Protected_Request__State_Transition__State_Machine(handle);
        }

        public State_Handle Register__State__Typed_State_Machine<T>(T state) where T : State
        {
            Type t = typeof(T);
            if (_Typed_State_Dictionary__TYPE_TO_HANDLE.ContainsKey(t))
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__Engine_Object,
                    Log.ERROR__TYPED_STATE_MACHINE__REPETATIVE_KEY_1,
                    this,
                    t.ToString()
                );
                return State_Machine__DEFAULT_STATE_HANDLE;
            }

            State_Handle handle = Protected_Register__State__State_Machine(state);

            _Typed_State_Dictionary__TYPE_TO_HANDLE.Add
            (
                t,
                handle
            );

            return handle;
        }

        public void Define__State_Flow__Typed_State_Machine<T,Y>() where T : State where Y : State
        {
            State_Handle handleT = Private_Get__State_Handle__Typed_State_Machine<T>();
            State_Handle handleY = Private_Get__State_Handle__Typed_State_Machine<Y>();

            if (handleT == null || handleY == null)
            {
                Protected_Log_Error__FAILED_TO_DEFINE_FLOW__State_Machine
                (
                    String.Format
                    (
                        Typed_State_Machine__CONTEXTUAL_MESSAGE__FAILED_DEFINE,
                        typeof(T),
                        handleT,
                        typeof(Y),
                        handleY
                    )
                );
                return;
            }

            Protected_Define__State_Flow__State_Machine(handleT, handleY);
        }

        private State_Handle Private_Get__State_Handle__Typed_State_Machine<T>() where T : State
        {
            Type t = typeof(T);
            bool containsT = _Typed_State_Dictionary__TYPE_TO_HANDLE.ContainsKey(t);

            if (!containsT)
            {
                Private_Log__Type_Not_Present__Typed_State_Machine(t);
                return null;
            }

            return _Typed_State_Dictionary__TYPE_TO_HANDLE[t];
        }

        private void Private_Log__Type_Not_Present__Typed_State_Machine(Type t)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__TYPED_STATE_MACHINE__TYPE_NOT_PRESENT_1,
                this,
                t.ToString()
            );
        }
    }
}

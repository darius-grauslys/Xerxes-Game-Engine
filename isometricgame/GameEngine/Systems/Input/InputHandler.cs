using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Systems.Input
{
    public class InputHandler
    {
        private bool enabled;
        private int handlerID;
        private InputType inputType;

        //switches[0,1,2] is the "any" switches. 0:anything, 1:keyboard, 2:mouse
        private List<InputState> switches = new List<InputState>()
        {
            InputState.RepeatUp,
            InputState.RepeatUp,
            InputState.RepeatUp
        };

        private List<InputState> pulses = new List<InputState>()
        {
            InputState.InitalUp,
            InputState.InitalUp,
            InputState.InitalUp
        };

        private Dictionary<string, int> switchCatalog = new Dictionary<string, int>()
        {
            { "any", 0 },
            { "keyboard_any", 1 },
            { "mouse_any", 2 }
        };

        private Dictionary<string, int> pulseCatalog = new Dictionary<string, int>()
        {
            { "any", 0 },
            { "keyboard_any", 1 },
            { "mouse_any", 2 }
        };
        
        public bool Enabled { get => enabled; set => enabled = value; }
        public int HandlerID { get => handlerID; private set => handlerID = value; }
        public InputType InputType { get => inputType; set => inputType = value; }

        private KeyboardKeyEventArgs keyboard_UpDown;
        private KeyPressEventArgs keyboard_Press;
        private MouseButtonEventArgs mouse_Button;
        private MouseMoveEventArgs mouse_Move;
        private MouseWheelEventArgs mouse_Wheel;

        public KeyboardKeyEventArgs Keyboard_UpDown { get => keyboard_UpDown; private set => keyboard_UpDown = value; }
        public KeyPressEventArgs Keyboard_Press { get => keyboard_Press; private set => keyboard_Press = value; }
        public MouseButtonEventArgs Mouse_Button { get => mouse_Button; private set => mouse_Button = value; }
        public MouseMoveEventArgs Mouse_Move { get => mouse_Move; private set => mouse_Move = value; }
        public MouseWheelEventArgs Mouse_Wheel { get => mouse_Wheel; private set => mouse_Wheel = value; }

        internal InputHandler(int handlerID, InputType inputType, bool enabled = true)
        {
            this.enabled = enabled;
            this.handlerID = handlerID;
            this.inputType = inputType;
        }

        private void addInterest(
            string enumTag, 
            InputState initalState, 
            List<InputState> stateList, 
            Dictionary<string, int> catalog
            )
        {
            if (catalog.ContainsKey(enumTag))
                return;
            
            stateList.Add(initalState);
            catalog.Add(enumTag, stateList.Count - 1);
        }

        public void DeclareSwitch(string enumTag)
        {
            addInterest(enumTag, InputState.RepeatUp, switches, switchCatalog);
        }

        public void DeclarePulse(string enumTag)
        {
            addInterest(enumTag, InputState.InitalUp, pulses, pulseCatalog);
        }

        private void removeInterest(string enumTag, List<InputState> stateList, Dictionary<string,int> catalog)
        {
            int switchId = catalog[enumTag];
            for (int i = switchId; i < stateList.Count; i++)
            {
                stateList[i]--;
                foreach (string tag in catalog.Keys)
                    if (catalog[tag] > switchId)
                        catalog[tag]--;
            }
            stateList.RemoveAt(switchId);
            catalog.Remove(enumTag);
        }

        public void RemoveSwitch(string enumTag)
        {
            removeInterest(enumTag, switches, switchCatalog);
        }

        public void RemovePulse(string enumTag)
        {
            removeInterest(enumTag, pulses, pulseCatalog);
        }

        private bool evaluateSwitchState(InputState switchState) 
            => (switchState == InputState.InitalDown || switchState == InputState.InitalUp);
        public bool EvaluateSwitchState(string enumTag, bool peek=false, InputState resetState = InputState.RepeatDown)
        {
            int switchId = switchCatalog[enumTag];
            bool ret = evaluateSwitchState(switches[switchId]);
            if (!peek && ret)
                switches[switchId] = resetState;
            return ret;

        }
        public InputState GetSwitchState(string enumTag) => switches[switchCatalog[enumTag]];
        
        private bool evaluatePulseState(InputState pulseState)
            => pulseState == InputState.InitalDown;
        public bool EvaluatePulseState(string enumTag, bool peek=false, InputState resetState = InputState.RepeatDown)
        {
            int pulseId = pulseCatalog[enumTag];
            bool ret = evaluatePulseState(pulses[pulseId]);
            if (!peek && ret)
                pulses[pulseId] = resetState;
            return ret;
        }
        public InputState GetPulseState(string enumTag) => pulses[pulseCatalog[enumTag]];

        private void handleInterestLogic(
            int anyIndex, 
            string enumTag, 
            bool isDown,
            List<InputState> stateList,
            Dictionary<string, int> catalog,
            Func<bool, InputState, InputState> logicHandler)
        {
            stateList[0] = logicHandler(isDown, stateList[0]);
            stateList[anyIndex] = logicHandler(isDown, stateList[1]);
            if (catalog.ContainsKey(enumTag))
            {
                int id = catalog[enumTag];
                stateList[id] = logicHandler(isDown, stateList[id]);
            }
        }

        private void handleSwitchStates(int anyIndex, string enumTag, bool isDown)
        {
            handleInterestLogic(
                anyIndex,
                enumTag,
                isDown,
                switches,
                switchCatalog,
                getNextSwitchState
                );
        }

        private void handlePulseStates(int anyIndex, string enumTag, bool isDown)
        {
            handleInterestLogic(
                anyIndex,
                enumTag,
                isDown,
                pulses,
                pulseCatalog,
                getNextPulseState
                );
        }

        internal virtual void Handle_Keyboard_UpDown(object sender, KeyboardKeyEventArgs e)
        {
            keyboard_UpDown = (enabled) ? e : null;
            if (keyboard_UpDown == null)
                return;
            string enumTag = e.Key.ToString();
            bool isDown = e.Keyboard.IsKeyDown(e.Key);
            handleSwitchStates(1, enumTag, isDown);
            handlePulseStates(1, enumTag, isDown);
        }
        internal virtual void Handle_Keyboard_Press(object sender, KeyPressEventArgs e) 
            => keyboard_Press = (enabled) ? e : null;

        internal virtual void Handle_Mouse_Button(object sender, MouseButtonEventArgs e)
        {
            mouse_Button = (enabled) ? e : null;
            if (mouse_Button == null)
                return;
            string enumTag = e.Button.ToString();
            bool isDown = e.Mouse.IsButtonDown(e.Button);
            handleSwitchStates(2, enumTag, isDown);
            handlePulseStates(2, enumTag, isDown);
        }
        internal virtual void Handle_Mouse_Move(object sender, MouseMoveEventArgs e) 
            => mouse_Move = (enabled) ? e : null;
        internal virtual void Handle_Mouse_Wheel(object sender, MouseWheelEventArgs e) 
            => mouse_Wheel = (enabled) ? e : null;

        private InputState getNextSwitchState(bool isDown, InputState state)
        {
            if (isDown)
            {
                if (state == InputState.InitalUp)
                    return InputState.RepeatDown;
                else if (state == InputState.RepeatUp)
                    return InputState.InitalDown;
            }

            if (state == InputState.InitalDown)
                return InputState.InitalUp;
            else if (state == InputState.RepeatDown)
                return InputState.RepeatUp;

            return InputState.NoRead;
        }

        private InputState getNextPulseState(bool isDown, InputState state)
        {
            if (isDown)
            {
                if (state == InputState.InitalUp)
                    return InputState.InitalDown;
                return state;
            }

            if (state == InputState.RepeatDown)
                return InputState.InitalUp;
            return state;
        }
    }

    public enum InputType
    {
        Keyboard_UpDown = 1,
        Keyboard_Press = 2,
        Mouse_Button = 4,
        Mouse_Move = 8,
        Mouse_Wheel = 16
    };

    public enum InputState
    {
        NoRead = 0,
        InitalDown = 1,
        InitalUp = 2,
        RepeatDown = 3,
        RepeatUp = 4
    }
}

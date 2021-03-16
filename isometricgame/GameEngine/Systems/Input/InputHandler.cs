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

        private Dictionary<Key, InputSwitchType> keyboard_UpDownSwitch = new Dictionary<Key, InputSwitchType>();
        private Dictionary<MouseButton, InputSwitchType> mouse_UpDownToggle = new Dictionary<MouseButton, InputSwitchType>();

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

        public void DeclareKeySwitch(Key key)
        {
            keyboard_UpDownSwitch.Add(key, InputSwitchType.RepeatUp);
        }

        public InputSwitchType Keyboard_SwitchState(Key key) => keyboard_UpDownSwitch[key];

        public bool Keyboard_SwitchState_Bool(Key key) => (keyboard_UpDownSwitch[key] == InputSwitchType.InitalDown || keyboard_UpDownSwitch[key] == InputSwitchType.InitalUp) ? true : false;
        
        public bool Keyboard_SwitchState_BoolReset(Key key)
        {
            bool ret = Keyboard_SwitchState_Bool(key);
            if (ret)
                keyboard_UpDownSwitch[key] = InputSwitchType.RepeatDown;
            return ret;
        }

        public bool Keyboard_SwitchState_BoolResetFree(Key key)
        {
            bool ret = Keyboard_SwitchState_Bool(key);
            if (ret)
                keyboard_UpDownSwitch[key] = InputSwitchType.RepeatUp;
            return ret;
        }
        
        public void RemoveKeySwitch(Key key)
        {
            keyboard_UpDownSwitch.Remove(key);
        }

        public void DeclareMouseToggle(MouseButton button)
        {
            mouse_UpDownToggle.Add(button, InputSwitchType.RepeatUp);
        }

        public void RemoveMouseToggle(MouseButton button)
        {
            mouse_UpDownToggle.Remove(button);
        }

        internal virtual void Handle_Keyboard_UpDown(object sender, KeyboardKeyEventArgs e)
        {
            if (keyboard_UpDownSwitch.ContainsKey(e.Key))
            {
                InputSwitchType state = handleSwitch(e.Keyboard.IsKeyDown(e.Key), keyboard_UpDownSwitch[e.Key]);
                if (state != InputSwitchType.NoRead)
                    keyboard_UpDownSwitch[e.Key] = state;
            }
            keyboard_UpDown = (enabled) ? e : null;
        }
        internal virtual void Handle_Keyboard_Press(object sender, KeyPressEventArgs e) => keyboard_Press = (enabled) ? e : null;

        internal virtual void Handle_Mouse_Button(object sender, MouseButtonEventArgs e)
        {
            if (mouse_UpDownToggle.ContainsKey(e.Button))
            {

            }
            mouse_Button = (enabled) ? e : null;
        }
        internal virtual void Handle_Mouse_Move(object sender, MouseMoveEventArgs e) => mouse_Move = (enabled) ? e : null;
        internal virtual void Handle_Mouse_Wheel(object sender, MouseWheelEventArgs e) => mouse_Wheel = (enabled) ? e : null;

        private InputSwitchType handleSwitch(bool isDown, InputSwitchType state)
        {
            if (isDown)
            {
                if (state == InputSwitchType.InitalUp)
                    return InputSwitchType.RepeatDown;
                else if (state == InputSwitchType.RepeatUp)
                    return InputSwitchType.InitalDown;
            }
            else
            {
                if (state == InputSwitchType.InitalDown)
                    return InputSwitchType.InitalUp;
                else if (state == InputSwitchType.RepeatDown)
                    return InputSwitchType.RepeatUp;
            }
            return InputSwitchType.NoRead;
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

    public enum InputSwitchType
    {
        NoRead = 0,
        InitalDown = 1,
        InitalUp = 2,
        RepeatDown = 3,
        RepeatUp = 4
    }
}

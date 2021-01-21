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

        internal void Handle_Keyboard_UpDown(object sender, KeyboardKeyEventArgs e) => keyboard_UpDown = (enabled) ? e : null;
        internal void Handle_Keyboard_Press(object sender, KeyPressEventArgs e) => keyboard_Press = (enabled) ? e : null;
        internal void Handle_Mouse_Button(object sender, MouseButtonEventArgs e) => mouse_Button = (enabled) ? e : null;
        internal void Handle_Mouse_Move(object sender, MouseMoveEventArgs e) => mouse_Move = (enabled) ? e : null;
        internal void Handle_Mouse_Wheel(object sender, MouseWheelEventArgs e) => mouse_Wheel = (enabled) ? e : null;
    }

    public enum InputType
    {
        Keyboard_UpDown = 1,
        Keyboard_Press = 2,
        Mouse_Button = 4,
        Mouse_Move = 8,
        Mouse_Wheel = 16
    };
}

using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Mouse
{
    public class MouseButtonEventArgument : MouseEventArgument
    {
        private MouseButtonEventArgs mouseButton;

        public MouseButtonEventArgs MouseButton { get => mouseButton; }

        public MouseButtonEventArgument(MouseButtonEventArgs mouseButton, bool isClosingEvent = false)
            : base (isClosingEvent)
        {
            this.mouseButton = mouseButton;
        }
    }
}

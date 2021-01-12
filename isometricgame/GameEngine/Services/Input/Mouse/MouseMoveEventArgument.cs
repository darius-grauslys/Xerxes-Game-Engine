using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Mouse
{
    public class MouseMoveEventArgument : MouseEventArgument
    {
        private MouseMoveEventArgs mouseMove;

        public MouseMoveEventArgs MouseMove { get => mouseMove; private set => mouseMove = value; }

        public MouseMoveEventArgument(MouseMoveEventArgs mouseMove, bool isClosingEvent = false) : base(isClosingEvent)
        {
            this.mouseMove = mouseMove;
        }
    }
}

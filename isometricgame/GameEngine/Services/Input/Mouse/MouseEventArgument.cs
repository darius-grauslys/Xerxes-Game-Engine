using isometricgame.GameEngine.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input.Mouse
{
    public class MouseEventArgument : GameEventArgument
    {
        public MouseEventArgument(bool isClosingEvent = false) : base(isClosingEvent) { }
    }
}

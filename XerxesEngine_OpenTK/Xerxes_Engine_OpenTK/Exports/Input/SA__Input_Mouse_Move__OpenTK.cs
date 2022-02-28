
using OpenTK.Input;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Xerxes_OpenTK.Exports.Input
{
    public sealed class SA__Input_Mouse_Move__OpenTK :
        SA__Input_Mouse_Move
    {
        internal SA__Input_Mouse_Move__OpenTK
        (
            MouseMoveEventArgs mouseMoveEventArgs
        )
        { 
            Input_Mouse_Move__Mouse_X = mouseMoveEventArgs.X;
            Input_Mouse_Move__Mouse_Y = mouseMoveEventArgs.Y;
        }

        public override int Input_Mouse_Move__Mouse_X { get; protected set; }
        public override int Input_Mouse_Move__Mouse_Y { get; protected set; }
    }
}

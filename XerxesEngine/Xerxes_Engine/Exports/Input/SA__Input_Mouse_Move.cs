using OpenTK.Input;

namespace Xerxes_Engine.Systems.OpenTK_Input
{
    public sealed class SA__Input_Mouse_Move :
        SA__Input_Mouse
    {
        internal SA__Input_Mouse_Move
        (
            double elapsedTime,
            double deltaTime,
            MouseMoveEventArgs mouseMoveEventArgs
        )
        : base
        (
            elapsedTime,
            deltaTime,
            mouseMoveEventArgs
        )
        { }
    }
}

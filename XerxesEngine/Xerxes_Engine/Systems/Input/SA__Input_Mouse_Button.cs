using OpenTK.Input;

namespace Xerxes_Engine.Systems.OpenTK_Input
{
    public sealed class SA__Input_Mouse_Button :
        SA__Input_Mouse
    {
        internal SA__Input_Mouse_Button
        (
            double elapsedTime,
            double deltaTime,
            MouseButtonEventArgs mouseButtonEventArgs
        )
        : base
        (
            elapsedTime,
            deltaTime,
            mouseButtonEventArgs
        )
        { }
    }
}

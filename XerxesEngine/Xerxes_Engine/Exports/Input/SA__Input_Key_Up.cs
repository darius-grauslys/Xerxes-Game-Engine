using OpenTK.Input;

namespace Xerxes_Engine.Exports.Input
{
    public sealed class SA__Input_Key_Up :
        SA__Input_Key
    {
        internal SA__Input_Key_Up
        (
            double elapsedTime,
            double deltaTime,
            KeyboardKeyEventArgs e
        )
        : base
        (
            elapsedTime,
            deltaTime,
            e
        )
        {

        }
    }
}

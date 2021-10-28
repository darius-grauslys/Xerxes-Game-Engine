using OpenTK.Input;

namespace Xerxes_Engine.Exports.Input
{
    public class SA__Input_Key :
        Streamline_Argument
    {
        private KeyboardKeyEventArgs _SA__Input_Keyboard__EVENT_ARGS { get; }
        public Key Input_Keyboard__KEY => _SA__Input_Keyboard__EVENT_ARGS.Key;

        internal SA__Input_Key
        (
            double elapsedTime,
            double deltaTime,
            KeyboardKeyEventArgs keyboardKeyEventArgs
        )
        : base
        (
            elapsedTime,
            deltaTime
        )
        {
            _SA__Input_Keyboard__EVENT_ARGS =
                keyboardKeyEventArgs;
        }
    }
}

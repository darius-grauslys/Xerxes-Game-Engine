using OpenTK.Input;

namespace Xerxes.Xerxes_OpenTK.Exports.Input
{
    public class SA__Input_Key :
        SA__Chronical
    {
        private KeyboardKeyEventArgs _SA__Input_Keyboard__EVENT_ARGS { get; }
        public Key Input_Key__KEY => _SA__Input_Keyboard__EVENT_ARGS.Key;
        public KeyboardState Input_Key__KEYBOARD => _SA__Input_Keyboard__EVENT_ARGS.Keyboard;

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

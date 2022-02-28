
using OpenTK.Input;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Xerxes_OpenTK.Exports.Input
{
    public sealed class SA__Input_Key_Down__OpenTK :
        SA__Input_Key_Down
    {
        private KeyboardKeyEventArgs _Input_Key_Down__EVENT_ARGUMENTS { get; }
        public override Game_Engine.Input.Key Input_Key__Event_Key { get; protected set; }

        internal SA__Input_Key_Down__OpenTK
        (
            KeyboardKeyEventArgs keyboardKeyEventArgs
        ) 
        {
            _Input_Key_Down__EVENT_ARGUMENTS =
                keyboardKeyEventArgs;

            Input_Key__Event_Key =
                (Game_Engine.Input.Key)((int)keyboardKeyEventArgs.Key);
        }

        public override bool Check_If__Key_Down__Input_Key(Game_Engine.Input.Key key)
        {
            return _Input_Key_Down__EVENT_ARGUMENTS
                .Keyboard
                .IsKeyDown
                (
                    (OpenTK.Input.Key)
                    ((int)key)
                );
        }
    }
}

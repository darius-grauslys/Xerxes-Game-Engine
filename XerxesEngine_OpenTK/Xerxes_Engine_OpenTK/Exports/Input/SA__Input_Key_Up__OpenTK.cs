
using OpenTK.Input;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Xerxes_OpenTK.Exports.Input
{
    public sealed class SA__Input_Key_Up__OpenTK :
        SA__Input_Key_Up
    {
        private KeyboardKeyEventArgs _Input_Key_Up__EVENT_ARGUMENTS { get; }
        public override Game_Engine.Input.Key Input_Key__Event_Key { get; protected set; }

        internal SA__Input_Key_Up__OpenTK
        (
            KeyboardKeyEventArgs e
        )
        {
            _Input_Key_Up__EVENT_ARGUMENTS =
                e;

            Input_Key__Event_Key =
                (Game_Engine.Input.Key)
                ((int)e.Key);
        }

        public override bool Check_If__Key_Down__Input_Key(Game_Engine.Input.Key key)
        {
            return
                _Input_Key_Up__EVENT_ARGUMENTS
                .Keyboard
                .IsKeyDown
                (
                    (OpenTK.Input.Key)
                    ((int)key)
                );
        }
    }
}

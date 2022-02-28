
using OpenTK.Input;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Xerxes_OpenTK.Exports.Input
{
    public sealed class SA__Input_Mouse_Button__OpenTK :
        SA__Input_Mouse_Button
    {
        internal MouseButtonEventArgs  Input_Mouse_Button__EVENT_ARGUMENTS__Internal;

        public override Mouse_Button Input_Mouse_Button__Mouse_Button { get; protected set; }

        internal SA__Input_Mouse_Button__OpenTK
        (
            MouseButtonEventArgs mouseButtonEventArgs
        )
        {
            Input_Mouse_Button__EVENT_ARGUMENTS__Internal =
                mouseButtonEventArgs;

            Input_Mouse_Button__Mouse_Button =
                (Mouse_Button)((int)mouseButtonEventArgs.Button);
        }

        public override bool Check_If__Mouse_Button_Down__Input_Mouse_Button(Mouse_Button button)
        {
            return 
                Input_Mouse_Button__EVENT_ARGUMENTS__Internal
                .Mouse
                .IsButtonDown((MouseButton)((int)button));
        }
    }
}

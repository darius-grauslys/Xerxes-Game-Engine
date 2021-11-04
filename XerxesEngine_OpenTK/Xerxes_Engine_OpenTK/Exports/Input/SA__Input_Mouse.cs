using OpenTK.Input;

namespace Xerxes_Engine.Export_OpenTK.Exports.Input
{
    public class SA__Input_Mouse :
        SA__Chronical
    {
        private MouseEventArgs _SA__Input_Mouse__EVENT_ARGS { get; }

        public float SA__Input_Mouse_Move__X =>
            _SA__Input_Mouse__EVENT_ARGS.X;
        public float SA__Input_Mouse_Move__Y =>
            _SA__Input_Mouse__EVENT_ARGS.Y;

        internal SA__Input_Mouse
        (
            double elapsedTime,
            double deltaTime,
            MouseEventArgs mouseEventArgs
        )
        : base
        (
            elapsedTime,
            deltaTime
        )
        {
            _SA__Input_Mouse__EVENT_ARGS =
                mouseEventArgs;
        }

        public bool Check_If__Button_Down__Input_Mouse
        (
            MouseButton b
        )
        {
            bool result = 
                _SA__Input_Mouse__EVENT_ARGS
                .Mouse
                .IsButtonDown
                (
                    b
                );

            return result;
        }

        public bool Check_If__Button_Up__Input_Mouse
        (
            MouseButton b
        )
        {
            bool result =
                _SA__Input_Mouse__EVENT_ARGS
                .Mouse
                .IsButtonUp
                (
                    b
                );

            return result;
        }
    }
}

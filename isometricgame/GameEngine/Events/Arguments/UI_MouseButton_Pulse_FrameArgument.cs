using OpenTK;
using OpenTK.Input;

namespace isometricgame.GameEngine.Events.Arguments
{
    public class UI_MouseButton_Pulse_FrameArgument : UI_Pulse_FrameArgument
    {
        public readonly Vector3 UI_MouseButton_Pulse__Mouse_Position;
        public readonly MouseButton UI_MouseButton_Pulse__Button;
        
        internal UI_MouseButton_Pulse_FrameArgument
            (
            FrameArgument frameArgument,
            Vector3 mouseButtonPosition,
            MouseButton mouseButton
            ) 
            : base(frameArgument)
        {
            UI_MouseButton_Pulse__Mouse_Position = mouseButtonPosition;
            UI_MouseButton_Pulse__Button = mouseButton;
        }
    }
}
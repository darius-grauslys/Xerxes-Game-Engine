using OpenTK;
using OpenTK.Input;

namespace Xerxes_Engine.UI.UI_Streamline_Argument_Frames
{
    public class UI_MouseButton_Pulse_FrameArgument : UI_Pulse_Streamline_Argument_Frame
    {
        public Vector3 UI_MouseButton_Pulse__MOUSE_POSITION { get; }
        public MouseButton UI_MouseButton_Pulse__BUTTON { get; }
        
        internal UI_MouseButton_Pulse_FrameArgument
            (
            Streamline_Argument_Frame frameArgument,
            Vector3 mouseButtonPosition,
            MouseButton mouseButton
            ) 
            : base(frameArgument)
        {
            UI_MouseButton_Pulse__MOUSE_POSITION = mouseButtonPosition;
            UI_MouseButton_Pulse__BUTTON = mouseButton;
        }
    }
}

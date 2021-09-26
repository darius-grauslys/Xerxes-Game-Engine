using OpenTK.Input;

namespace Xerxes_Engine.UI.UI_Streamline_Argument_Frames
{
    public class UI_Keyboard_Pulse_Frame_Arguement : UI_Pulse_Streamline_Argument_Frame
    {
        public Key UI_Keyboard_Pulse__KEY { get; }
        
        internal UI_Keyboard_Pulse_Frame_Arguement
            (
            Streamline_Argument_Frame frameArgument,
            Key keyButton
            ) 
            : base(frameArgument)
        {
            UI_Keyboard_Pulse__KEY = keyButton;
        }
    }
}

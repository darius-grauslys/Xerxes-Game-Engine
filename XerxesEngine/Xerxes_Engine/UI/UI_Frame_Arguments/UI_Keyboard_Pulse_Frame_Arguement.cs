using OpenTK.Input;

namespace Xerxes_Engine.UI.UI_Frame_Arguments
{
    public class UI_Keyboard_Pulse_Frame_Arguement : UI_Pulse_Frame_Argument
    {
        public Key UI_Keyboard_Pulse__KEY { get; }
        
        internal UI_Keyboard_Pulse_Frame_Arguement
            (
            Frame_Argument frameArgument,
            Key keyButton
            ) 
            : base(frameArgument)
        {
            UI_Keyboard_Pulse__KEY = keyButton;
        }
    }
}

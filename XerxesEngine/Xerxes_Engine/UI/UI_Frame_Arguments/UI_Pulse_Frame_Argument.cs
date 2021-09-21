namespace Xerxes_Engine.UI.UI_Event_Argument_Frames
{
    public class UI_Pulse_Event_Argument_Frame : Event_Argument_Frame 
    {
        public bool UI_Pulse_FrameArgument__Frame_Evaluates_Pulse { get; private set; }
        public void Consume__UI_Pulse__UI_Pulse_FrameArgument()
            => UI_Pulse_FrameArgument__Frame_Evaluates_Pulse = true;
        
        internal UI_Pulse_Event_Argument_Frame(Event_Argument_Frame frameArgument) 
            : base(frameArgument.Event_Argument_Frame__DELTA_TIME, frameArgument.Event_Argument_Frame__SENDER_TIME)
        {
            
        }
    }
}

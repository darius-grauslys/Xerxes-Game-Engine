namespace XerxesEngine.UI.Frame_Arguments
{
    public class UI_Pulse_Frame_Argument : Frame_Argument
    {
        public bool UI_Pulse_FrameArgument__Frame_Evaluates_Pulse { get; private set; }
        public void Consume__UI_Pulse__UI_Pulse_FrameArgument()
            => UI_Pulse_FrameArgument__Frame_Evaluates_Pulse = true;
        
        internal UI_Pulse_Frame_Argument(Frame_Argument frameArgument) 
            : base(frameArgument.Time, frameArgument.DeltaTime)
        {
            
        }
    }
}

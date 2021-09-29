namespace Xerxes_Engine.Events.Streamline_Arguments
{
    public sealed class Event_Progression_Argument : Event_Argument 
    {
        public double Event_Progression_Argument__DELTA_TIME { get; }
        
        public double Event_Progression_Argument__ELAPSED_TIME { get; }
        
        public double Event_Progression_Argument__TIME_LIMIT { get; }

        public double Get__Percentage_Of_Duration_Progressed__Event_Progression_Argument
            => Event_Progression_Argument__ELAPSED_TIME / Event_Progression_Argument__TIME_LIMIT;

        internal Event_Progression_Argument
        (
            double deltaTime,
            double elapsedTime,
            double duration
        )
        {
            Event_Progression_Argument__DELTA_TIME = deltaTime;
            Event_Progression_Argument__ELAPSED_TIME = elapsedTime;
            Event_Progression_Argument__TIME_LIMIT = duration;
        }
    }
}

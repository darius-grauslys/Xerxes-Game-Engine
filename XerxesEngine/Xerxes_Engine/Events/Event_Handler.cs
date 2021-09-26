using Xerxes_Engine.Events.Streamline_Arguments;

namespace Xerxes_Engine.Events
{
    public class Event_Handler
    {
        private Event Event_Handler__EVENT { get; }

        protected Event_Handler
        (
            double timeLimit
        )
        {
            Event_Handler__EVENT = new Event
            (
                timeLimit,
                Handle_Progression__Event_Handler,
                Handle_Elapse__Event_Handler,
                Handle_Reset__Event_Handler
            );
        }

        protected virtual void Handle_Progression__Event_Handler
        (
            Event_Progression_Argument progressionArgument
        )
        {
            
        }

        protected virtual void Handle_Elapse__Event_Handler
        (
        )
        {
            
        }

        protected virtual void Handle_Reset__Event_Handler
        (
            Event_Reset_Argument resetArgument
        )
        {
            
        }

        public static implicit operator Event(Event_Handler eventHandler)
            => eventHandler.Event_Handler__EVENT;
    }
}

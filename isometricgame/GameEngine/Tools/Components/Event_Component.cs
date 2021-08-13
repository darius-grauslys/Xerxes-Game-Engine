using isometricgame.GameEngine.Tools;

namespace isometricgame.GameEngine.Components
{
    public class Event_Component : GameObject_Component
    {
        private string _Event_Component__BINDING_TAG { get; }
        public TimedCallback Event { get; private set; }

        public Event_Component(string tag, double defaultTime = 1)
        {
            _Event_Component__BINDING_TAG = tag;
            
            Event = new TimedCallback(
                defaultTime,
                PerformFrame,
                FinishComponent,
                ResetComponent
                );
        }

        protected virtual void ResetComponent(double newLimit)
        {

        }

        protected virtual void PerformFrame(Timer timer)
        {

        }

        protected virtual void FinishComponent()
        {

        }

        public void Invoke() => Event.Invoke();

        protected override void Handle_Attach_To__GameObject__Component()
        {
            base.Handle_Attach_To__GameObject__Component();

            EventScheduler eventScheduler =
                Component__Attached_GameObject.GameObject__Scene_Layer.Scene_Layer__Parent_Scene.Game
                    .Game__Event_Scheduler;

            eventScheduler.Register_Event(_Event_Component__BINDING_TAG, Event);
        }
    }
}

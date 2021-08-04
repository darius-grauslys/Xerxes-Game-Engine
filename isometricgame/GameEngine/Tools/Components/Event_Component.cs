using isometricgame.GameEngine.Tools;

namespace isometricgame.GameEngine.Components
{
    public class Event_Component : GameObject_Component
    {
        public TimedCallback EventTimer { get; private set; }

        public Event_Component(string tag, double defaultTime = 1)
        {
            EventTimer = new TimedCallback(
                tag,
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

        public void Invoke() => EventTimer.Invoke();

        protected override void Handle_Attach_To__GameObject__Component()
        {
            base.Handle_Attach_To__GameObject__Component();
            EventTimer.Bind_To_Schedule(Component__Attached_GameObject.GameObject__Scene_Layer.Scene_Layer__Parent_Scene.Game.Game__Event_Scheduler);
        }
    }
}

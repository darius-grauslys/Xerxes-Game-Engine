using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Rendering
{
    public class EventComponent : GameComponent
    {
        public TimedCallback EventTimer { get; private set; }

        public EventComponent(string tag, double defaultTime = 1)
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

        protected override void Handle_NewParent()
        {
            base.Handle_NewParent();
            EventTimer.Bind_To_Schedule(ParentObject.SceneLayer.ParentScene.Game.EventScheduler);
        }
    }
}

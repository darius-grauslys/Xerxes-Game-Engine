using isometricgame.GameEngine.Events;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Attributes
{
    /// <summary>
    /// Attributes are added to GameObjects to give additional functionalities. Such as hitboxes, physics, and more.
    /// </summary>
    public class GameAttribute
    {
        private readonly GameObject parentObject;
        private bool enabled = true;
        
        public GameObject ParentObject { get => parentObject; }

        public GameAttribute(GameObject parentObject)
        {
            this.parentObject = parentObject;
        }

        /// <summary>
        /// Logical update frame.
        /// </summary>
        public void OnUpdate(FrameEventArgs args)
        {
            if (enabled)
                HandleOnUpdate(args);
        }

        protected virtual void HandleOnUpdate(FrameEventArgs args)
        {

        }

        public void Toggle()
        {
            enabled = !enabled;
        }

        public void Toggle(bool b)
        {
            enabled = b;
        }
    }
}

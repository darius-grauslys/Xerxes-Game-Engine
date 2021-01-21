using isometricgame.GameEngine.Events.Arguments;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine
{
    /// <summary>
    /// Attributes are added to GameObjects to give additional functionalities. Such as hitboxes, physics, and more.
    /// </summary>
    public class GameComponent
    {
        private readonly GameObject parentObject;
        private bool enabled = true;
        
        public GameObject ParentObject { get => parentObject; }

        public GameComponent(GameObject parentObject)
        {
            this.parentObject = parentObject;
        }

        /// <summary>
        /// Logical update frame.
        /// </summary>
        internal void Update(FrameArgument args)
        {
            if (enabled)
                OnUpdate(args);
        }

        protected virtual void OnUpdate(FrameArgument args)
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

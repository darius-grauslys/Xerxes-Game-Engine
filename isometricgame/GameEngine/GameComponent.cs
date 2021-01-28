using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Scenes;
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
        private SceneObject parentObject;
        private bool enabled = true;

        public event EventHandler<FrameArgument> OnPostUpdate;

        public SceneObject ParentObject { get => parentObject; internal set => parentObject = value; }

        public GameComponent(SceneObject parentObject)
        {
            this.parentObject = parentObject;
        }

        /// <summary>
        /// Logical update frame.
        /// </summary>
        internal void Update(FrameArgument args)
        {
            if (enabled)
            {
                OnUpdate(args);
                OnPostUpdate?.Invoke(this, args);
            }
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

        public virtual GameComponent Clone(SceneObject newParent)
        {
            GameComponent newComp = new GameComponent(newParent);
            newComp.enabled = enabled;
            newComp.OnPostUpdate += OnPostUpdate;

            return newComp;
        }
    }
}

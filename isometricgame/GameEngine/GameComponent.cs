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
        private GameObject parentObject;
        private bool enabled = true;

        public GameObject ParentObject { get => parentObject; internal set => parentObject = value; }

        public GameComponent()
        {

        }

        internal void _initalize()
        {
            Initalize();
        }

        protected void Initalize()
        {

        }

        /// <summary>
        /// Logical update frame.
        /// </summary>
        internal void Update(FrameArgument args)
        {
            if (enabled)
            {
                OnUpdate(args);
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

        public virtual GameComponent Clone()
        {
            GameComponent newComp = new GameComponent();
            newComp.enabled = enabled;

            return newComp;
        }
    }
}

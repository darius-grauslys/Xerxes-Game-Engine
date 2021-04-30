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
        public bool Enabled { get; set; }

        public GameObject ParentObject { get => parentObject; internal set => parentObject = value; }

        public GameComponent()
        {
            Enabled = true;
        }

        internal void _newParent()
        {
            Handle_NewParent();
        }

        protected virtual void Handle_NewParent()
        {

        }

        /// <summary>
        /// Logical update frame.
        /// </summary>
        internal void Update(FrameArgument args)
        {
            if (Enabled)
            {
                OnUpdate(args);
            }
        }

        protected virtual void OnUpdate(FrameArgument args)
        {

        }

        public void Toggle()
        {
            Enabled = !Enabled;
        }

        public void Toggle(bool b)
        {
            Enabled = b;
        }

        public virtual GameComponent Clone()
        {
            GameComponent newComp = new GameComponent();
            newComp.Enabled = Enabled;

            return newComp;
        }
    }
}

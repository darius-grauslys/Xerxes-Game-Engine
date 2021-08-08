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
    public class GameObject_Component
    {
        private bool Component__Enabled__Private { get; set; }
        protected bool Component__Hardlocked { get; private set; }
        
        /// <summary>
        /// If set true, component is disabled and cannot be enabled externally.
        /// </summary>
        protected void Set__Hardlocked_Status__Component(bool state)
        {
            Component__Hardlocked = state;
            
            Component__Enabled__Private = !Component__Hardlocked;
        }
        
        public bool Component__Enabled 
        { 
            get => Component__Enabled__Private;
            set
            {
                if (Component__Hardlocked)
                    return;
                Component__Enabled__Private = value;
            } 
        }
        public bool Component__Attached => Component__Attached_GameObject != null;

        public GameObject Component__Attached_GameObject { get; private set; }

        internal void Attach_To__GameObject__Component(GameObject obj)
        {
            Component__Attached_GameObject = obj;
            Handle_Attach_To__GameObject__Component();
        }
        
        public GameObject_Component()
        {
            Component__Enabled = true;
        }

        protected virtual void Handle_Attach_To__GameObject__Component()
        {

        }

        /// <summary>
        /// Logical update frame.
        /// </summary>
        internal void Update(Frame_Argument args)
        {
            if (Component__Enabled)
            {
                Handle__Update__Component(args);
            }
        }

        protected virtual void Handle__Update__Component(Frame_Argument args)
        {

        }

        public void Toggle__Component()
        {
            Component__Enabled = !Component__Enabled;
        }

        public void Toggle__Component(bool b)
        {
            Component__Enabled = b;
        }

        public virtual GameObject_Component Clone__Component()
        {
            GameObject_Component newComp = new GameObject_Component();
            newComp.Component__Enabled = Component__Enabled;

            return newComp;
        }
    }
}

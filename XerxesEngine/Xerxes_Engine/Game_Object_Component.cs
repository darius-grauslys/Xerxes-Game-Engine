namespace Xerxes_Engine
{
    /// <summary>
    /// Attributes are added to Game_Objects to give additional functionalities. Such as hitboxes, physics, and more.
    /// </summary>
    public class Game_Object_Component
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
        public bool Component__Attached => Component__Attached_Game_Object != null;

        public bool Component__Has_Been_Attached_Once { get; private set; }

        public Game_Object Component__Attached_Game_Object { get; private set; }

        internal void Attach_To__Game_Object__Component(Game_Object obj)
        {
            if (!Component__Has_Been_Attached_Once)
                Component__Has_Been_Attached_Once = true;
            Component__Attached_Game_Object = obj;
            Handle_Attach_To__Game_Object__Component();
        }
        
        public Game_Object_Component()
        {
            Component__Enabled = true;
        }

        protected virtual void Handle_Attach_To__Game_Object__Component()
        {
            if (Component__Attached_Game_Object == null)
            {
                Component__Enabled = false;

                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__COMPONENT__PARENT_IS_NULL,
                    this
                );
            }
            else if (!Component__Enabled)
            {
                Component__Enabled = true;

                Log.Internal_Write__Info__Log
                (
                    Log.INFO__COMPONENT__ENABLED_ON_PARENT_BIND,
                    this
                );
            }
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

        public virtual Game_Object_Component Clone__Component()
        {
            Game_Object_Component newComp = new Game_Object_Component();
            newComp.Component__Enabled = Component__Enabled;

            return newComp;
        }
    }
}

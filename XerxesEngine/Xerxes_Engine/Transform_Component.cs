using OpenTK;

namespace Xerxes_Engine
{
    public class Transform_Component : Game_Object_Component
    {
        public Vector3 Position
        {
            get => Component__Attached_Game_Object?.Position ?? Vector3.Zero;
            set
            {
                if (Component__Attached) Component__Attached_Game_Object.Position = value;
            }
        }
    }
}

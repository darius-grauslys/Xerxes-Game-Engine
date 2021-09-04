using OpenTK;

namespace XerxesEngine.Scenes.Components
{
    public class Transform_Component : GameObject_Component
    {
        public Vector3 Position
        {
            get => Component__Attached_GameObject?.Position ?? Vector3.Zero;
            set
            {
                if (Component__Attached) Component__Attached_GameObject.Position = value;
            }
        }
    }
}
using OpenTK;

namespace Xerxes.Xerxes_OpenTK.Engine_Objects.Entities
{
    public class Entity
    {
        public Vector3 Entity__Position { get; internal set; }

        public Entity(Vector3? position = null)
        {
            Entity__Position = position ?? new Vector3();
        }
    }
}

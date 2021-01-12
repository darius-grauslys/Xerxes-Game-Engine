using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace isometricgame.GameEngine.Attributes.Physics
{
    /// <summary>
    /// This attribute is given the GameObjects that possess both a PhysicsBody and a CollisionBox. This will allow for force vector collisions.
    /// </summary>
    public class RigidBodyAttribute : GameAttribute
    {
        private PhysicsBodyAttribute physicsBody;
        private CollisonBoxAttribute collisionBox;

        public RigidBodyAttribute(GameObject parentObject) 
            : base(parentObject)
        {
            physicsBody = parentObject.GetAttribute<PhysicsBodyAttribute>();
            collisionBox = parentObject.GetAttribute<CollisonBoxAttribute>();
        }

        protected override void HandleOnUpdate(FrameEventArgs args)
        {
            base.HandleOnUpdate(args);
        }
    }
}

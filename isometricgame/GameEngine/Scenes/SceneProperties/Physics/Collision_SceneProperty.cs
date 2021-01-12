using isometricgame.GameEngine.Attributes.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Scenes.SceneProperties.Physics
{
    public class Collision_SceneProperty : SceneProperty<RigidBodyAttribute>
    {
        private List<CollisonBoxAttribute> staticCollisionRegions = new List<CollisonBoxAttribute>(); 

        public override void OnUpdate()
        {

        }
    }
}

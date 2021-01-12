using isometricgame.GameEngine.Events;
using isometricgame.GameEngine.WorldSpace.Geometry;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Attributes.Physics
{
    /// <summary>
    /// This attribute is given to GameObjects that wish to know when they collide with other collision boxes.
    /// </summary>
    public class CollisonBoxAttribute : GameAttribute
    {
        private Region boxRegion;
        public Region BoxRegion => boxRegion;

        public void Rotate_Radian(float thetaRadian) => boxRegion.RotateRegion_Radian(thetaRadian);
        public void Rotate_Euler(float thetaEuler) => boxRegion.RotateRegion_Euler(thetaEuler);

        public CollisonBoxAttribute(GameObject parentObject, Region boxRegion) 
            : base(parentObject)
        {
            this.boxRegion = boxRegion;
        }

        public bool IsColliding(CollisonBoxAttribute opposing, out Vector3 collisionPoint)
        {
            collisionPoint = new Vector3();
            return false;
        }
    }
}

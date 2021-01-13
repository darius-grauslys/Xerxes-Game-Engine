using isometricgame.GameEngine.WorldSpace.Physics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Physics
{
    /// <summary>
    /// This attribute is given to objects that can be given force vectors and as a result - move.
    /// </summary>
    public class PhysicsBodyAttribute : GameComponent
    {
        /// <summary>
        /// for stuff like gravity.
        /// </summary>
        private Dictionary<ForceType, Vector3> staticForceVectors = new Dictionary<ForceType, Vector3>()
        {
            {ForceType.Gravitational, Vector3.Zero },
            {ForceType.Kenetic, Vector3.Zero },
            {ForceType.Magnetic, Vector3.Zero }
        };
        private Dictionary<ForceType, Vector3> dynamicForceVectors = new Dictionary<ForceType, Vector3>()
        {
            {ForceType.Gravitational, Vector3.Zero },
            {ForceType.Kenetic, Vector3.Zero },
            {ForceType.Magnetic, Vector3.Zero }
        };
        private Vector3 velocityVector, accelerationVector;
        private float mass;
        
        public float Mass { get => mass; set => mass = value; }

        public PhysicsBodyAttribute(GameObject parentObject, float mass = 1) 
            : base(parentObject)
        {
            this.mass = mass;
        }

        public void SetStaticForce(ForceType type, Vector3 force)
        {
            staticForceVectors[type] = force;
        }

        public void ApplyDynmaicForce(ForceType type, Vector3 force)
        {
            dynamicForceVectors[type] += force;
        }

        public Vector3 GetFrameForceVector()
        {
            Vector3 ret = new Vector3(0,0,0);

            foreach (Vector3 force in staticForceVectors.Values)
                ret += force;
            foreach (Vector3 force in dynamicForceVectors.Values)
                ret += force;

            return ret;
        }

        //spaghetti
        public Vector3 GetAccelerationVector(Vector3 forceVector)
        {
            return forceVector / mass;
        }

        //spaghetti
        public Vector3 GetVelocityVector(Vector3 accelerationVector, float deltaT)
        {
            return accelerationVector * deltaT;
        }
    }
}

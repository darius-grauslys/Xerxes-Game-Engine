using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.WorldSpace.Geometry
{
    /// <summary>
    /// Represents an XY plane orientation from 0 to pi radians.
    /// </summary>
    public struct Orientation
    {
        private float theta;
        /// <summary>
        /// XY planar orientation in radians.
        /// </summary>
        public float Theta => theta;
        public float ThetaEuler => Services.MathHelper.Radian_To_Euler(theta);

        public Orientation(float theta)
        {
            this.theta = theta;
        }

        public static Orientation AddEulerAngle(Orientation orientation, float thetaEuler)
        {
            return AddRadianAngle(orientation, Services.MathHelper.Euler_To_Radian(thetaEuler));
        }

        public static Orientation AddRadianAngle(Orientation orientation, float thetaRadian)
        {
            //if theta is less than 0 get the positive counterpart. -pi/2 -> 3pi/2
            if (thetaRadian < 0)
                thetaRadian = (float)((thetaRadian % Math.PI) + Math.PI);
            
            return new Orientation((float)((orientation.theta + thetaRadian) % Math.PI));
        }

        public static Orientation FromEuler(float thetaEuler)
        {
            return new Orientation(Services.MathHelper.Euler_To_Radian(thetaEuler));
        }

        public static Orientation operator +(Orientation a, Orientation b)
        {
            return AddRadianAngle(a, b.theta);
        }

        public static Orientation operator -(Orientation a, Orientation b)
        {
            return AddRadianAngle(a, -b.theta);
        }

        public static Orientation operator +(Orientation a, float theta)
        {
            return AddRadianAngle(a, theta);
        }

        public static Orientation operator -(Orientation a, float theta)
        {
            return AddRadianAngle(a, -theta);
        }

        public static implicit operator float(Orientation a) => a.theta;
    }
}

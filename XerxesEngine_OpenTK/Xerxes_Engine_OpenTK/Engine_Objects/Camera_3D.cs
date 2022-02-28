
using System;
using OpenTK;

namespace Xerxes.Xerxes_OpenTK.Engine_Objects
{
    public class Camera_3D :
        Camera
    {
        public struct Camera_Target
        {
            public readonly Vector3 CAMERA_TARGET;

            public Camera_Target(Vector3 camera_target)
            {
                CAMERA_TARGET = camera_target;
            }

            public static implicit operator Vector3(Camera_Target camera_target)
                => camera_target.CAMERA_TARGET;
            public static implicit operator Camera_Target(Vector3 camera_target)
                => new Camera_Target(camera_target);
        }

        public struct Camera_Position 
        {
            public readonly Vector3 CAMERA_TARGET;

            public Camera_Position(Vector3 camera_target)
            {
                CAMERA_TARGET = camera_target;
            }

            public static implicit operator Vector3(Camera_Position camera_target)
                => camera_target.CAMERA_TARGET;
            public static implicit operator Camera_Position(Vector3 camera_target)
                => new Camera_Position(camera_target);
        }

        private float camera_3d__pitch;
        private float camera_3d__yaw;

        protected float Camera_3D__Field_Of_View__Y { get; set; }

        private Vector3 camera_3d__front = Vector3.UnitZ;
        private Vector3 camera_3d__right = Vector3.UnitX;
        private Vector3 camera_3d__up = Vector3.UnitY;
        protected Vector3 Camera_3D__Front { get => camera_3d__front; set => camera_3d__front = value; }

        protected Vector3 Camera_3D__Right
            => camera_3d__front;

        protected Vector3 Camera_3D__Up
            => camera_3d__up;

        public Camera_3D()
        {
            Camera_3D__Field_Of_View__Y = MathHelper.DegreesToRadians(0.45f);

            Declare__Field<Camera_Target>
            (
                () => Camera_3D__Front, 
                (camera_target) => Camera_3D__Front = camera_target
            );
            Declare__Field<Camera_Position>
            (
                () => Camera__Position,
                (camera_position) => Camera__Position = camera_position
            );
        }

        private void Private_Update__Vectors__Camera_3D()
        {
            camera_3d__front.X = (float)(Math.Cos(camera_3d__pitch) * Math.Cos(camera_3d__yaw));
            camera_3d__front.Y = (float)Math.Sin(camera_3d__pitch);
            camera_3d__front.Z = (float)(Math.Cos(camera_3d__pitch) * Math.Sin(camera_3d__yaw));

            camera_3d__front = Vector3.Normalize(camera_3d__front);

            camera_3d__right = 
                Vector3
                .Normalize
                (
                    Vector3
                    .Cross
                    (
                        camera_3d__front,
                        Vector3.UnitY
                    )
                );

            camera_3d__up =
                Vector3
                .Normalize
                (
                    Vector3
                    .Cross
                    (
                        camera_3d__right,
                        camera_3d__front
                    )
                );
        }

        protected virtual Matrix4 Get__Look_At__Camera_3D(Vector3 target)
        {
            return
                Matrix4
                .LookAt
                (
                    Camera__Position,
                    target,
                    Camera_3D__Up
                );
        }

        protected virtual float Get__Aspect_Ratio__Camera_3D()
            => Camera__Focal_Width / Camera__Focal_Height;

        protected override Matrix4 Get__Projection__Camera()
        {
            return
                Matrix4
                .CreatePerspectiveFieldOfView
                (
                    Camera_3D__Field_Of_View__Y,
                    Get__Aspect_Ratio__Camera_3D(),
                    Camera__Z_Near,
                    Camera__Z_Far
                );
        }

        protected override Matrix4 Get__View_Space__Camera()
        {
            return Get__Look_At__Camera_3D(Camera__Position + Camera_3D__Front);
        }
    }
}

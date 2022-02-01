
using OpenTK;

namespace Xerxes_Engine.Export_OpenTK.Engine_Objects
{
    public class Camera_2D :
        Camera
    {
        protected readonly Matrix4 Camera_2D__IDENTITY_ORTHOGRAPHIC =
            new Matrix4
            (
                -1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );

        protected override Matrix4 Get__View_Space__Camera()
        {
            return 
                Camera_2D__IDENTITY_ORTHOGRAPHIC
                *
                Matrix4.CreateTranslation(Camera__Position)
                *
                Matrix4.CreateScale(Camera__Zoom);
        }

        protected override Matrix4 Get__Projection__Camera()
        {
            //return Matrix4.CreateTranslation(new Vector3(-Iso_X, -Iso_Y, -1000f)) * Matrix4.CreatePerspectiveFieldOfView(fov, aspect, zNear, zFar);
            //return Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0))
            //    * Matrix4.CreateScale(fov, fov, 1) *
            //    Matrix4.CreateTranslation(Iso_X, Iso_Y, 1f);

            //TODO: Change 0,0 to target x and y.
            return 
                Matrix4
                .CreateOrthographic
                (
                    Camera__Focal_Width, 
                    Camera__Focal_Height, 
                    Camera__Z_Near, 
                    Camera__Z_Far
                );
        }
    }
}

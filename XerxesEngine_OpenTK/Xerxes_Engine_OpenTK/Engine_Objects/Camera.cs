using OpenTK;

namespace Xerxes.Xerxes_OpenTK.Engine_Objects
{
    public abstract class Camera :
        Xerxes_Object<Camera>
    {
        protected Vector3 Camera__Position { get; set; }

        protected float Camera__Z_Near { get; set; }
        protected float Camera__Z_Far { get; set; }
        protected float Camera__Zoom { get; set; }

        protected float Camera__Focal_Width { get; private set; }
        protected float Camera__Focal_Height { get; private set; }

        public Camera()
        {
            Camera__Position = new Vector3(0,0,-2);

            Camera__Z_Near = 0.1f;
            Camera__Z_Far = 100f;
            Camera__Zoom = 1f;

            Declare__Streams()
                .Downstream.Receiving<SA__Game_Window_Resized>
                (
                    Private_Handle_Resize_2D__Camera
                )
                .Downstream.Receiving<SA__Render_Begin>
                (
                    Private_Handle_Render__Camera
                );
        }

        protected virtual void Private_Handle_Render__Camera(SA__Render_Begin e)
        {
            e.Render_Begin__Projection_Matrix = Get__Projection__Camera();
            e.Render_Begin__World_Matrix      = Get__View_Space__Camera();
        }

        private void Private_Handle_Resize_2D__Camera(SA__Game_Window_Resized e)
        {
            Camera__Focal_Width = e.SA__Resize_2D__WIDTH;
            Camera__Focal_Height = e.SA__Resize_2D__HEIGHT;
        }

        protected abstract Matrix4 Get__View_Space__Camera();
        protected abstract Matrix4 Get__Projection__Camera();
    }
}

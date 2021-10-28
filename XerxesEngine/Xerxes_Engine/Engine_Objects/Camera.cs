using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Camera :
        Xerxes_Object<Camera>,
        IXerxes_Descendant_Of<Scene>
    {
        private float zNear = 0.01f, zFar = 10f;
        private float zoom = 0.2f;
        
        public float Zoom { get => zoom; set => zoom = value; }

        private float _Camera__Focal_Width { get; set; }
        private float _Camera__Focal_Height { get; set; }

        //public float Iso_X => Chunk.CartesianToIsometric_X(position.X, position.Y);
        //public float Iso_Y => Chunk.CartesianToIsometric_Y(position.X, position.Y, position.Z);

        public Camera()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Resize_2D>
                (
                    Private_Handle_Resize_2D__Camera
                )
                .Downstream.Receiving<SA__Render_Begin>
                (
                    Private_Handle_Render__Camera
                );
        }

        private void Private_Handle_Render__Camera(SA__Render_Begin e)
        {
            Matrix4 cameraView =
                GetView();

            e.Render_Begin__SCENE_MATRIX__Internal = cameraView;
        }

        private void Private_Handle_Resize_2D__Camera(SA__Resize_2D e)
        {
            _Camera__Focal_Width = e.SA__Resize_2D__WIDTH;
            _Camera__Focal_Height = e.SA__Resize_2D__HEIGHT;
        }

        public Matrix4 GetView()
        {
            //return Matrix4.CreateTranslation(new Vector3(-Iso_X, -Iso_Y, -1000f)) * Matrix4.CreatePerspectiveFieldOfView(fov, aspect, zNear, zFar);
            //return Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0))
            //    * Matrix4.CreateScale(fov, fov, 1) *
            //    Matrix4.CreateTranslation(Iso_X, Iso_Y, 1f);

            //TODO: Change 0,0 to target x and y.
            return Matrix4.CreateTranslation(0,0, -1f) 
                * Matrix4.CreateScale(zoom) 
                * Matrix4.CreateOrthographic
                (
                    _Camera__Focal_Width, 
                    _Camera__Focal_Height, 
                    zNear, 
                    zFar
                );
        }
    }
}

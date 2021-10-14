using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Camera : Xerxes_Descendant<Scene, Camera>
    {
        private float zNear = 0.01f, zFar = 10f;
        private float zoom = 0.2f;
        
        public float Zoom { get => zoom; set => zoom = value; }

        //public float Iso_X => Chunk.CartesianToIsometric_X(position.X, position.Y);
        //public float Iso_Y => Chunk.CartesianToIsometric_Y(position.X, position.Y, position.Z);

        public Camera()
        {
            Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>
                (
                    Private_Handle_Draw__Camera
                );
        }

        private void Private_Handle_Draw__Camera(Streamline_Argument_Draw e)
        {
            Matrix4 cameraView =
                GetView();

            e.Streamline_Argument_Draw__World_Matrix__Internal= cameraView;
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
                    Xerxes_Descendant__Parent__Protected.Scene__Width, 
                    Xerxes_Descendant__Parent__Protected.Scene__Height, 
                    zNear, 
                    zFar
                );
        }
    }
}

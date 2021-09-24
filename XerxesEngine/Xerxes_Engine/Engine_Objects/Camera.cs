using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Camera
    {
        private float zNear = 0.01f, zFar = 10f;
        private float zoom = 0.2f;
        
        private Vector3 position = new Vector3(0,0,0);
        private float velocity = 3;
        private Scene_Layer sceneLayer;

        private Game_Object focusObject;

        public float Velocity { get => velocity; set => velocity = value; }
        public Vector3 Position { get => position; set => position = value; }
        public Game_Object FocusObject { get => focusObject; set => focusObject = value; }

        public Vector3 TargetPosition => (focusObject != null) ? focusObject.Game_Object__Render_Unit_Position__Internal : position;

        public float Zoom { get => zoom; set => zoom = value; }

        //public float Iso_X => Chunk.CartesianToIsometric_X(position.X, position.Y);
        //public float Iso_Y => Chunk.CartesianToIsometric_Y(position.X, position.Y, position.Z);

        public Camera(Scene_Layer sceneLayer)
        {
            this.sceneLayer = sceneLayer;
        }
        
        public void Pan_Linear(float deltaT)
        {
            Vector3 pos = (focusObject != null) ? focusObject.Game_Object__Render_Unit_Position__Internal : Vector3.Zero;
            Vector3 distanceVector = pos - position;

            position += (distanceVector * velocity) * deltaT;
        }

        public void Pan_Pow2(float deltaT)
        {

        }

        public void Pan_Pow3(float deltaT)
        {

        }

        public Matrix4 GetView()
        {
            //return Matrix4.CreateTranslation(new Vector3(-Iso_X, -Iso_Y, -1000f)) * Matrix4.CreatePerspectiveFieldOfView(fov, aspect, zNear, zFar);
            //return Matrix4.LookAt(new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0))
            //    * Matrix4.CreateScale(fov, fov, 1) *
            //    Matrix4.CreateTranslation(Iso_X, Iso_Y, 1f);
            return Matrix4.CreateTranslation(position.X, position.Y, -1f) 
                * Matrix4.CreateScale(zoom) 
                * Matrix4.CreateOrthographic(sceneLayer.Scene_Layer__Game.Width, sceneLayer.Scene_Layer__Game.Height, zNear, zFar);
        }
    }
}

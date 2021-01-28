using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.WorldSpace.ChunkSpace;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine
{
    public class Camera
    {
        private float zNear = 0.01f, zFar = 10f;
        private float zoom = 2f;
        
        private Vector3 position = new Vector3(0,0,0);
        private float velocity = 3;
        private Scene scene;

        private SceneObject focusObject;

        public float Velocity { get => velocity; set => velocity = value; }
        public Vector3 Position { get => position; set => position = value; }
        public SceneObject FocusObject { get => focusObject; set => focusObject = value; }

        public Vector3 TargetPosition => (focusObject != null) ? focusObject.Position : position;

        public float Zoom { get => zoom; set => zoom = value; }

        public float Iso_X => Chunk.CartesianToIsometric_X(position.X, position.Y);
        public float Iso_Y => Chunk.CartesianToIsometric_Y(position.X, position.Y, position.Z);

        public Camera(Scene scene)
        {
            this.scene = scene;
        }
        
        public void Pan_Linear(float deltaT)
        {
            Vector3 pos = (focusObject != null) ? focusObject.Position : Vector3.Zero;
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
            return Matrix4.CreateTranslation(-Iso_X, -Iso_Y, -1f) * Matrix4.CreateScale(zoom) * Matrix4.CreateOrthographic(scene.Game.Width, scene.Game.Height, zNear, zFar);
        }
    }
}

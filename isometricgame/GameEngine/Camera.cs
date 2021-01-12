using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace;
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
        private Vector3 position = new Vector3(0,0,0);
        private float velocity = 3;
        private float zoom = 0.5f;
        private Scene scene;

        public float Velocity { get => velocity; set => velocity = value; }
        public Vector3 Position { get => position; set => position = value; }

        public float Iso_X => Chunk.CartesianToIsometric_X(position.X, position.Y);
        public float Iso_Y => Chunk.CartesianToIsometric_Y(position.X, position.Y, position.Z);

        public Camera(Scene scene)
        {
            this.scene = scene;
        }

        public void Pan_Linear(float deltaT, Vector3 pos)
        {
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
            //return
            //    Matrix4.CreateTranslation(position.X, position.Y + position.Z, 0) *
            //    Matrix4.CreateTranslation(scene.Game.WindowWidth / 4, scene.Game.WindowHeight / 4, 0) *
            //    Matrix4.CreateScale(zoom, zoom, 1);
            return 
                Matrix4.CreateTranslation(-scene.Game.WindowWidth/2, -scene.Game.WindowHeight/2, 0) *
                Matrix4.CreateScale(zoom, zoom, 1) *
                Matrix4.CreateTranslation(Iso_X, Iso_Y, 0);
        }
    }
}

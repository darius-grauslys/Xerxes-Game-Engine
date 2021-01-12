using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Rendering
{
    public struct Sprite
    {
        private Vertex[] verticies;
        private Texture2D texture;

        private bool flippedX, flippedY;

        private float offsetX, offsetY;

        public Vertex[] Vertices => verticies;
        public Texture2D Texture => texture;

        public float OffsetX { get => offsetX; set => offsetX = value; }
        public float OffsetY { get => offsetY; set => offsetY = value; }
        
        public Sprite(Texture2D texture)
        {
            flippedX = false;
            flippedY = false;

            verticies = new Vertex[]
                {
                    new Vertex(new Vector2(0, 0), new Vector2(1, 0)),
                    new Vertex(new Vector2(0, texture.Height), new Vector2(1, 1)),
                    new Vertex(new Vector2(texture.Width, texture.Height), new Vector2(0, 1)),
                    new Vertex(new Vector2(texture.Width, 0), new Vector2(0, 0)),
                };

            this.texture = texture;

            offsetX = 0;
            offsetY = 0;
        }

        public Sprite(Texture2D texture, Vertex[] verticies)
        {
            flippedX = false;
            flippedY = false;

            this.verticies = verticies;

            this.texture = texture;

            offsetX = 0;
            offsetY = 0;
        }

        public void Flip(bool x=true, bool y=true)
        {
            if (x)
            {
                verticies[0] = new Vertex(new Vector2(0, 0), flippedX ? new Vector2(1, 0) : new Vector2(0, 0));
                verticies[2] = new Vertex(new Vector2(texture.Width, texture.Height), flippedX ? new Vector2(0, 1) : new Vector2(1, 1));
                flippedX = !flippedX;
            }
            if (y)
            {
                verticies[1] = new Vertex(new Vector2(0, texture.Height), flippedY ? new Vector2(1, 1) : new Vector2(0, 1));
                verticies[3] = new Vertex(new Vector2(texture.Width, 0), flippedY ? new Vector2(0, 0) : new Vector2(1, 0));
                flippedY = !flippedY;
            }
        }
    }
}

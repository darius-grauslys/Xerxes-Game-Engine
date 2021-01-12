using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Services
{
    public class RenderService : GameService
    {
        private Matrix4 projection;
        private Matrix4 cachedWorldMatrix;

        public RenderService(Game game, int windowWidth, int windowHeight) 
            : base(game)
        {
            AdjustProjection(windowWidth, windowHeight);
            cachedWorldMatrix = Matrix4.CreateTranslation(new Vector3(0,0,0));
        }

        public void AdjustProjection(int width, int height)
        {
            projection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);
        }

        internal void BeginRender()
        {
            GL.ClearColor(Color.FromArgb(5, 5, 25));
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Texture2D);

            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void RenderScene(Scene scene, FrameEventArgs e)
        {
            //Matrix4 view = World.Camera.GetView();

            cachedWorldMatrix = scene.SceneMatrix;
            scene.RenderFrame(this, e);
        }

        internal void EndRender()
        {
            GL.Flush();
        }

        /// <summary>
        /// Draw the given sprite using the given vertices.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="verticies"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void DrawSprite(Sprite s, Vertex[] vertices, float x, float y)
        {
            BindVerticies(s, vertices);
            FastDrawSprite(s, x, y);
        }

        public void DrawSprite(Sprite s, float x, float y)
        {
            DrawSprite(s, s.Vertices, x, y);
        }

        /// <summary>
        /// Draws the given sprite assuming no vertice binding is required.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void FastDrawSprite(Sprite s, float x, float y)
        {
            _drawSprite(s, x, y, cachedWorldMatrix);
        }

        private void _drawSprite(Sprite s, float x, float y, Matrix4 world)
        {
            GL.BindTexture(TextureTarget.Texture2D, s.Texture.ID);

            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(x, y, 0)) * Matrix4.Invert(world);

            GL.LoadMatrix(ref translation);

            GL.BindBuffer(BufferTarget.ArrayBuffer, s.Texture.ID);
            GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, (IntPtr)0);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector2.SizeInBytes));
            GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector2.SizeInBytes * 2));

            GL.DrawArrays(PrimitiveType.Quads, 0, s.Vertices.Length);
        }

        /// <summary>
        /// This is used to relay to GL what vertices and textcoords we are using for the given Sprite's texture ID.
        /// </summary>
        /// <param name="s"></param>
        private void BindVerticies(Sprite s)
        {
            BindVerticies(s, s.Vertices);
        }

        /// <summary>
        /// Bind the Sprite under new vertices instead of its own.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="vertices"></param>
        private void BindVerticies(Sprite s, Vertex[] vertices)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, s.Texture.ID);
            GL.BufferData<Vertex>(
                BufferTarget.ArrayBuffer,
                (IntPtr)(Vertex.SizeInBytes * vertices.Length),
                vertices,
                BufferUsageHint.StaticDraw
                );
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}

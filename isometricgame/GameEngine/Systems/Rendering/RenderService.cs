using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.WorldSpace;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Systems.Rendering
{
    public class RenderService : GameSystem
    {
        private Matrix4 projection;
        private Matrix4 cachedWorldMatrix;

        private Shader shader;

        private string shaderSource_Vert, shaderSource_Frag;

        public RenderService(Game game, int windowWidth, int windowHeight) 
            : base(game)
        {
            AdjustProjection(windowWidth, windowHeight);
            cachedWorldMatrix = Matrix4.CreateTranslation(new Vector3(0,0,0));

            shaderSource_Vert = Path.Combine(game.GAME_DIRECTORY_SHADERS, "shader.vert");
            shaderSource_Frag = Path.Combine(game.GAME_DIRECTORY_SHADERS, "shader.frag");

            shader = new Shader(shaderSource_Vert, shaderSource_Frag);
        }

        public override void Unload()
        {
            shader.Dispose();
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

            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void RenderScene(Scene scene, FrameArgument e)
        {
            //Matrix4 view = World.Camera.GetView();

            cachedWorldMatrix = scene.SceneMatrix;
            scene.RenderFrame(this, e);
        }

        internal void EndRender()
        {
            GL.Flush();
        }

        public void DrawSprite(Sprite s, float x, float y, float z = 0)
        {
            s.Use(s.VBO_Index);
            DrawSprite(x + s.OffsetX, y + s.OffsetY, z);
        }

        public void DrawSprite(float x, float y, float z = 0)
        {
            shader.Use();

            int transform = GL.GetUniformLocation(shader.Handle, "transform");

            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(x, y, z)) * cachedWorldMatrix;

            GL.UniformMatrix4(transform, true, ref translation);
            
            GL.DrawArrays(PrimitiveType.Quads, 0, VertexArray.VERTEXARRAY_INDEX_COUNT);
        }
    }
}

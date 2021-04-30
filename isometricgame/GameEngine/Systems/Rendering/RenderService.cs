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
        public static readonly string EXTENSION_VERT = ".vert", EXTENSION_FRAG = ".frag";
        private SpriteLibrary SpriteLibrary;

        private Matrix4 projection;
        private Matrix4 cachedWorldMatrix;

        private Shader[] Shaders { get; set; }
        public int ShaderCount => Shaders.Length;
        public int GetShader<T>() where T : Shader { for (int i = 0; i < Shaders.Length; i++) if (Shaders[i] is T) return i; return 0; }
        private Shader beginDraw_DefaultShader;

        private string shaderSource_Vert, shaderSource_Frag;

        public RenderService(Game game, int windowWidth, int windowHeight) 
            : base(game, false)
        {
            AdjustProjection(windowWidth, windowHeight);
            cachedWorldMatrix = Matrix4.CreateTranslation(new Vector3(0,0,0));

            shaderSource_Vert = Path.Combine(game.GAME_DIRECTORY_SHADERS, "shader.vert");
            shaderSource_Frag = Path.Combine(game.GAME_DIRECTORY_SHADERS, "shader.frag");

            beginDraw_DefaultShader = new Shader(shaderSource_Vert, shaderSource_Frag);
        }

        internal void LoadShaders(string[] paths)
        {
            Shaders = new Shader[paths.Length];

            for(int i=0;i<paths.Length;i++)
            {
                shaderSource_Vert = Path.Combine(Game.GAME_DIRECTORY_SHADERS, string.Format("{0}{1}", paths[i], EXTENSION_VERT));
                shaderSource_Frag = Path.Combine(Game.GAME_DIRECTORY_SHADERS, string.Format("{0}{1}", paths[i], EXTENSION_FRAG));

                Shaders[i] = new Shader(shaderSource_Vert, shaderSource_Frag);
            }
        }

        public override void Load()
        {
            SpriteLibrary = Game.GetSystem<SpriteLibrary>();
        }

        public override void Unload()
        {
            beginDraw_DefaultShader.Dispose();
        }

        public void AdjustProjection(int width, int height)
        {
            projection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);
        }

        public void SetShader(int shader)
        {
            shader = (shader < 0) ? 0 : ((shader >= Shaders.Length) ? Shaders.Length-1 : shader);
            beginDraw_DefaultShader = Shaders[shader];
            beginDraw_DefaultShader.Use();
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

        internal void RenderScene(Scene scene, FrameArgument e)
        {
            scene.RenderScene(this, e);
        }

        internal void CacheMatrix(Matrix4 mat)
        {
            cachedWorldMatrix = mat;
        }

        internal void EndRender()
        {
            GL.Flush();
            beginDraw_DefaultShader = Shaders[0];
        }

        public void UseSprite(int spriteId, int vaoIndex = 0)
        {
            SpriteLibrary.sprites[spriteId].VAO_Index = vaoIndex;
            SpriteLibrary.sprites[spriteId].Use();
        }

        public void DrawObj(GameObject obj)
        {
            beginDraw_DefaultShader.Use();
            obj._handleDraw(this);
        }

        public void DrawSprite(ref RenderUnit renderUnit, float x, float y, float z = 0)
        {
            UseSprite(renderUnit.Id, renderUnit.VAO_Index);
            DrawSprite_DefaultShader(
                Game.SpriteLibrary.sprites[renderUnit.Id].VertexArrays[renderUnit.VAO_Index].Vertices.  Length,
                x + SpriteLibrary.sprites[renderUnit.Id].OffsetX, y + SpriteLibrary.sprites[renderUnit.Id].OffsetY, z);
        }

        public void DrawSprite(int spriteId, float x, float y, int vaoIndex = 0, float z = 0)
        {
            UseSprite(spriteId, vaoIndex);
            DrawSprite_DefaultShader(Game.SpriteLibrary.sprites[spriteId].VertexArrays[vaoIndex].Vertices.Length, x, y, z);
        }

        public int GetUniformLocation(int shader, string name)
        {
            return GL.GetUniformLocation(Shaders[shader].Handle, name);
        }

        //resolve primitive obsession.
        public void SetUniform1(int uniformLocation, float x)
        {
            GL.Uniform1(uniformLocation, x);
        }

        public void SetUniform4(int uniformLocation, Vector4 vec4)
        {
            GL.Uniform4(uniformLocation, vec4);
        }

        private void DrawSprite_DefaultShader(int vertCount, float x, float y, float z = 0)
        {
            beginDraw_DefaultShader.Use();

            int transform = GL.GetUniformLocation(beginDraw_DefaultShader.Handle, "transform");

            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(x, y, z)) * cachedWorldMatrix;

            GL.UniformMatrix4(transform, true, ref translation);

            GL.DrawArrays(PrimitiveType.Quads, 0, vertCount);
        }
    }
}

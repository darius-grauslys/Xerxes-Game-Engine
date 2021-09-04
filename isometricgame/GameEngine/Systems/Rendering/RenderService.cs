using System.Drawing;
using System.IO;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
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

            shaderSource_Vert = Path.Combine(game.GAME__DIRECTORY__SHADERS, "shader.vert");
            shaderSource_Frag = Path.Combine(game.GAME__DIRECTORY__SHADERS, "shader.frag");

            beginDraw_DefaultShader = new Shader(shaderSource_Vert, shaderSource_Frag);
        }

        internal void Internal_Load__Shaders__Render_Service(string[] shaders)
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__RENDER_SERVICE__LOAD_SHADERS, this);
            Shaders = new Shader[shaders.Length];

            for(int i=0;i<shaders.Length;i++)
            {
                Log.Internal_Write__Verbose__Log(Log.VERBOSE__RENDER_SERVICE__LOAD_SHADER_1, this, 0, shaders[i]);

                shaderSource_Vert = Path.Combine(Game.GAME__DIRECTORY__SHADERS, string.Format("{0}{1}", shaders[i], EXTENSION_VERT));
                shaderSource_Frag = Path.Combine(Game.GAME__DIRECTORY__SHADERS, string.Format("{0}{1}", shaders[i], EXTENSION_FRAG));

                Shaders[i] = new Shader(shaderSource_Vert, shaderSource_Frag);
            }
        }

        public override void Load()
        {
            base.Load();
            SpriteLibrary = Game.Get_System__Game<SpriteLibrary>();
        }

        public override void Unload()
        {
            base.Unload();
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

        internal void RenderScene(Scene scene, Frame_Argument e)
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
            SpriteLibrary.sprites[spriteId].vaoIndex = vaoIndex;
            SpriteLibrary.sprites[spriteId].Use();
        }

        public void DrawObj(GameObject obj)
        {
            beginDraw_DefaultShader.Use();
            obj.Draw(this);
        }

        public void DrawSprite(ref RenderUnit renderUnit, float x, float y, float z = 0)
        {
            UseSprite(renderUnit.id, renderUnit.vaoIndex);
            DrawSprite_DefaultShader(
                Game.Game__Sprite_Library.sprites[renderUnit.id].VertexArrays[renderUnit.VAO_Index].Vertices.  Length,
                x + SpriteLibrary.sprites[renderUnit.id].OffsetX, y + SpriteLibrary.sprites[renderUnit.id].OffsetY, z);
        }

        public void DrawSprite(int spriteId, float x, float y, int vaoIndex = 0, float z = 0)
        {
            UseSprite(spriteId, vaoIndex);
            DrawSprite_DefaultShader(Game.Game__Sprite_Library.sprites[spriteId].VertexArrays[vaoIndex].Vertices.Length, x, y, z);
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

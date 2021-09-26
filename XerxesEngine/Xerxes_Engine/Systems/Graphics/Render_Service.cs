using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Xerxes_Engine.Systems.Graphics.R2;

namespace Xerxes_Engine.Systems.Graphics
{
    public sealed class Render_Service : Game_System
    {
        public const string RENDER_SERVICE__EXTENSION_VERTEX_SHADER = ".vert";
        public const string RENDER_SERVICE__EXTENSION_FRAGMENT_SHADER = ".frag";
        private Vertex_Object_Library SpriteLibrary;

        private Matrix4 projection;
        private Matrix4 cachedWorldMatrix;

        private Shader[] Shaders { get; set; }
        public int ShaderCount => Shaders.Length;
        public int GetShader<T>() where T : Shader { for (int i = 0; i < Shaders.Length; i++) if (Shaders[i] is T) return i; return 0; }
        private Shader beginDraw_DefaultShader;

        private string shaderSource_Vert, shaderSource_Frag;

        internal Render_Service(Game game, int windowWidth, int windowHeight) 
            : base(game, false)
        {
            Establish__Orthographic_Projection__Render_Service(windowWidth, windowHeight);
            cachedWorldMatrix = Matrix4.CreateTranslation(new Vector3(0,0,0));

            shaderSource_Vert = Path.Combine(game.Game__DIRECTORY__SHADERS, "shader.vert");
            shaderSource_Frag = Path.Combine(game.Game__DIRECTORY__SHADERS, "shader.frag");

            beginDraw_DefaultShader = new Shader(shaderSource_Vert, shaderSource_Frag);
        }

        internal void Internal_Load__Shaders__Render_Service(string[] shaders)
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__RENDER_SERVICE__LOAD_SHADERS, this);
            Shaders = new Shader[shaders.Length];

            for(int i=0;i<shaders.Length;i++)
            {
                Log.Internal_Write__Verbose__Log(Log.VERBOSE__RENDER_SERVICE__LOAD_SHADER_1, this, 0, shaders[i]);

                shaderSource_Vert = Path.Combine(Game.Game__DIRECTORY__SHADERS, string.Format("{0}{1}", shaders[i], RENDER_SERVICE__EXTENSION_VERTEX_SHADER));
                shaderSource_Frag = Path.Combine(Game.Game__DIRECTORY__SHADERS, string.Format("{0}{1}", shaders[i], RENDER_SERVICE__EXTENSION_FRAGMENT_SHADER));

                Shaders[i] = new Shader(shaderSource_Vert, shaderSource_Frag);
            }
        }

        protected override void Handle_Load__Game_System()
        {
            base.Handle_Load__Game_System();
            SpriteLibrary = Game.Get_System__Game<Vertex_Object_Library>();
        }

        protected override void Handle_Unload__Game_System()
        {
            base.Handle_Unload__Game_System();
            beginDraw_DefaultShader.Dispose();
        }

        public void Establish__Orthographic_Projection__Render_Service
        (
            int width, 
            int height
        )
        {
            projection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);
        }

        public void Set__Shader__Render_Service(int shader)
        {
            shader = (shader < 0) ? 0 : ((shader >= Shaders.Length) ? Shaders.Length-1 : shader);
            beginDraw_DefaultShader = Shaders[shader];
            beginDraw_DefaultShader.Use();
        }

        internal void Internal_Begin__Render_Service()
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

        internal void Internal_Cache__Matrix__Render_Service(Matrix4 mat)
        {
            cachedWorldMatrix = mat;
        }

        internal void Internal_End__Render_Service()
        {
            GL.Flush();
            beginDraw_DefaultShader = Shaders[0];
        }

        public void Use__Sprite__Render_Service(int spriteId, int vaoIndex = 0)
        {
            SpriteLibrary.Get__Sprite_From_ID__Sprite_Library(spriteId).vaoIndex = vaoIndex;
            SpriteLibrary.Get__Sprite_From_ID__Sprite_Library(spriteId).Use();
        }

        public void Draw__Sprite__Render_Service(ref Render_Unit_R2 renderUnit, float x, float y, float z = 0)
        {
            Use__Sprite__Render_Service(renderUnit.id, renderUnit.vaoIndex);
            Private_Draw__Sprite_With_DefaultShader__Render_Service
            (
                    Game
                    .Game__Sprite_Library
                    .Get__Sprite_From_ID__Sprite_Library(renderUnit.id)
                    .VertexArrays[renderUnit.VAO_Index]
                    .Vertices.Length,
                x + SpriteLibrary.Get__Sprite_From_ID__Sprite_Library(renderUnit.id).OffsetX, 
                y + SpriteLibrary.Get__Sprite_From_ID__Sprite_Library(renderUnit.id).OffsetY, 
                z
            );
        }

        public void Draw__Sprite__Render_Service(int spriteId, float x, float y, int vaoIndex = 0, float z = 0)
        {
            Use__Sprite__Render_Service(spriteId, vaoIndex);
            Private_Draw__Sprite_With_DefaultShader__Render_Service
            (
                Game
                .Game__Sprite_Library
                .Get__Sprite_From_ID__Sprite_Library(spriteId)
                .VertexArrays[vaoIndex]
                .Vertices.Length, 
                x, 
                y, 
                z
            );
        }

        public int Get__Uniform_Location__Render_Service(int shader, string name)
        {
            return GL.GetUniformLocation(Shaders[shader].Handle, name);
        }

        //resolve primitive obsession.
        public void Set__Uniform_1__Render_Service(int uniformLocation, float x)
        {
            GL.Uniform1(uniformLocation, x);
        }

        public void Set__Uniform_4__Render_Service(int uniformLocation, Vector4 vec4)
        {
            GL.Uniform4(uniformLocation, vec4);
        }

        private void Private_Draw__Sprite_With_DefaultShader__Render_Service(int vertCount, float x, float y, float z = 0)
        {
            beginDraw_DefaultShader.Use();

            int transform = GL.GetUniformLocation(beginDraw_DefaultShader.Handle, "transform");

            Matrix4 translation = Matrix4.CreateTranslation(new Vector3(x, y, z)) * cachedWorldMatrix;

            GL.UniformMatrix4(transform, true, ref translation);

            GL.DrawArrays(PrimitiveType.Quads, 0, vertCount);
        }
    }
}

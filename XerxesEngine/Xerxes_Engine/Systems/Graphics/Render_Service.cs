using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Xerxes_Engine.Systems.Graphics
{
    public sealed class Render_Service : Game_System
    {
        public const string RENDER_SERVICE__EXTENSION_VERTEX_SHADER = ".vert";
        public const string RENDER_SERVICE__EXTENSION_FRAGMENT_SHADER = ".frag";

        private Matrix4 _Render_Service__Cached_Projection;
        private Matrix4 _Render_Service__Cached_World_Matrix;

        private Shader[] _Render_Service__Shaders { get; set; }
        public int Get__Shader_Count__Render_Service() 
            => _Render_Service__Shaders.Length;
        public int Get__Shader_Handle__Render_Service<T>() where T : Shader 
        { 
            for (int i = 0; i < _Render_Service__Shaders.Length; i++) 
                if (_Render_Service__Shaders[i] is T) return i; 
            return 0; 
        }
        private Shader _Render_Service_Default_Shader;

        private string 
            _Render_Service__Shader_Source_Vertex, 
            Render_Service__Shader_Source_Fragment;

        internal Render_Service(Game game, int windowWidth, int windowHeight) 
            : base(game, false)
        {
            Establish__Orthographic_Projection__Render_Service(windowWidth, windowHeight);
            _Render_Service__Cached_World_Matrix = Matrix4.CreateTranslation(new Vector3(0,0,0));

            _Render_Service__Shader_Source_Vertex = Path.Combine(game.Game__DIRECTORY__SHADERS, "shader.vert");
            Render_Service__Shader_Source_Fragment = Path.Combine(game.Game__DIRECTORY__SHADERS, "shader.frag");

            _Render_Service_Default_Shader = new Shader(_Render_Service__Shader_Source_Vertex, Render_Service__Shader_Source_Fragment);
        }

        internal void Internal_Load__Shaders__Render_Service(string[] shaders)
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__RENDER_SERVICE__LOAD_SHADERS, this);
            _Render_Service__Shaders = new Shader[shaders.Length];

            for(int i=0;i<shaders.Length;i++)
            {
                Log.Internal_Write__Verbose__Log(Log.VERBOSE__RENDER_SERVICE__LOAD_SHADER_1, this, 0, shaders[i]);

                _Render_Service__Shader_Source_Vertex = 
                    Path
                    .Combine
                    (
                        Game.Game__DIRECTORY__SHADERS, 
                        string
                        .Format
                        (
                            "{0}{1}", 
                            shaders[i], 
                            RENDER_SERVICE__EXTENSION_VERTEX_SHADER
                        )
                    );
                Render_Service__Shader_Source_Fragment = 
                    Path
                    .Combine
                    (
                        Game.Game__DIRECTORY__SHADERS, 
                        string.Format
                        (
                            "{0}{1}", 
                            shaders[i], 
                            RENDER_SERVICE__EXTENSION_FRAGMENT_SHADER
                        )
                    );

                _Render_Service__Shaders[i] = new Shader(_Render_Service__Shader_Source_Vertex, Render_Service__Shader_Source_Fragment);
            }
        }

        protected override void Handle_Load__Game_System()
        {
            base.Handle_Load__Game_System();
        }

        protected override void Handle_Unload__Game_System()
        {
            base.Handle_Unload__Game_System();
            _Render_Service_Default_Shader.Dispose();
        }

        public void Establish__Orthographic_Projection__Render_Service
        (
            int width, 
            int height
        )
        {
            _Render_Service__Cached_Projection = Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);
        }

        public void Set__Shader__Render_Service(int shader)
        {
            shader = (shader < 0) ? 0 : ((shader >= _Render_Service__Shaders.Length) ? _Render_Service__Shaders.Length-1 : shader);
            _Render_Service_Default_Shader = _Render_Service__Shaders[shader];
            _Render_Service_Default_Shader.Use();
        }

        internal void Internal_Begin__Render_Service()
        {
            GL.ClearColor(Color.FromArgb(5, 5, 25));
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref _Render_Service__Cached_Projection);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Texture2D);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        internal void Internal_Cache__Matrix__Render_Service(Matrix4 mat)
        {
            _Render_Service__Cached_World_Matrix = mat;
        }

        internal void Internal_End__Render_Service()
        {
            GL.Flush();
            _Render_Service_Default_Shader = _Render_Service__Shaders[0];
        }

        internal void Draw__Render_Service(Streamline_Argument_Draw e)
        {
            Log.Internal_Write__Verbose__Log("Drawing!", this);
            Vector3 position = e.Streamline_Argument_Draw__Position__Internal;
            Vertex_Object vertex_Object = e.Streamline_Argument_Draw__VERTEX_OBJECT__Internal;
            Matrix4 world_Matrix = e.Streamline_Argument_Draw__World_Matrix__Internal;

            vertex_Object.Internal_Use__Vertex_Object();

            _Render_Service_Default_Shader.Use();

            int transform = GL.GetUniformLocation(_Render_Service_Default_Shader.Handle, "transform");

            Matrix4 translation = Matrix4.CreateTranslation(position) * world_Matrix;

            GL.UniformMatrix4(transform, true, ref translation);

            GL.DrawArrays(PrimitiveType.Quads, 0, vertex_Object.Get__Vertex_Count__Vertex_Object());
        }

        public int Get__Uniform_Location__Render_Service(int shader, string name)
        {
            return GL.GetUniformLocation(_Render_Service__Shaders[shader].Handle, name);
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
    }
}

using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Xerxes_Engine.Export_OpenTK.Exports.Graphics
{
    public sealed class Render_Service : 
        OpenTK_Export
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
            _Render_Service__Shader_Source_Fragment;

        private string
            _Render_Service__Shader_Directory;

        public Render_Service() 
        {

            _Render_Service__Cached_World_Matrix = Matrix4.CreateTranslation(new Vector3(0,0,0));
        }

        protected override void Handle__Rooted__Xerxes_Export()
        {
            Protected_Declare__Catch__Xerxes_Export
                <SA__Render_Begin>
                (
                    Private_Handle__Render__Render_Service
                );
            Protected_Declare__Catch__Xerxes_Export
                <SA__Render_End>
                (
                    Private_Handle__End_Render__Render_Service
                );
            Protected_Declare__Catch__Xerxes_Export
                <SA__Draw>
                (
                    Private_Draw__Render_Service
                );
        }

        protected override void Handle__Associate_Game__Xerxes_Export 
        (
            SA__Associate_Game_OpenTK e
        )
        {
            Private_Establish__Orthographic_Projection__Render_Service
            (
                e.Associate_Root__WIDTH,
                e.Associate_Root__HEIGHT
            );

            _Render_Service__Shader_Directory = 
                e
                .Associate_Root__SHADER_DIRECTORY;
            
            string[] shaders =
                e
                .Associate_Root__SHADERS__Internal;

            Private_Load__Shaders__Render_Service
            (
                shaders
            );
        }

        protected override void Handle__Dissassociate_Game__Xerxes_Export
        (SA__Dissassociate_Game_OpenTK e)
        {
            foreach(Shader shader in _Render_Service__Shaders)
            {
                shader.Dispose();
            }
        }

        private void Private_Load__Shaders__Render_Service
        (
            string[] shaders
        )
        {
            Log.Write__Verbose__Log(Log_Messages__OpenTK.VERBOSE__RENDER_SERVICE__LOAD_SHADERS, this);
            _Render_Service__Shaders = new Shader[shaders.Length];

            for(int i=0;i<shaders.Length;i++)
            {
                Log.Write__Verbose__Log(Log_Messages__OpenTK.VERBOSE__RENDER_SERVICE__LOAD_SHADER_1, this, 0, shaders[i]);

                _Render_Service__Shader_Source_Vertex = 
                    Path
                    .Combine
                    (
                        _Render_Service__Shader_Directory, 
                        string
                        .Format
                        (
                            "{0}{1}", 
                            shaders[i], 
                            RENDER_SERVICE__EXTENSION_VERTEX_SHADER
                        )
                    );
                _Render_Service__Shader_Source_Fragment = 
                    Path
                    .Combine
                    (
                        _Render_Service__Shader_Directory, 
                        string.Format
                        (
                            "{0}{1}", 
                            shaders[i], 
                            RENDER_SERVICE__EXTENSION_FRAGMENT_SHADER
                        )
                    );

                _Render_Service__Shaders[i] = new Shader(_Render_Service__Shader_Source_Vertex, _Render_Service__Shader_Source_Fragment);
            }

            Set__Shader__Render_Service(0);
        }

        private void Private_Establish__Orthographic_Projection__Render_Service
        (
            int width, 
            int height
        )
        {
            _Render_Service__Cached_Projection = 
                Matrix4.CreateOrthographicOffCenter(0, width, height, 0, 0, 1);
        }

        public void Set__Shader__Render_Service(int shader)
        {
            shader = (shader < 0) ? 0 : ((shader >= _Render_Service__Shaders.Length) ? _Render_Service__Shaders.Length-1 : shader);
            _Render_Service_Default_Shader = _Render_Service__Shaders[shader];
            _Render_Service_Default_Shader.Use();
        }

        internal void Private_Handle__Render__Render_Service(SA__Render_Begin e)
        {
            Private_Cache__Matrix__Render_Service(e.Render_Begin__SCENE_MATRIX__Internal);

            GL.ClearColor(Color.FromArgb(5, 5, 25));
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref _Render_Service__Cached_Projection);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Texture2D);

            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void Private_Cache__Matrix__Render_Service(Matrix4 mat)
        {
            _Render_Service__Cached_World_Matrix = mat;
        }

        private void Private_Handle__End_Render__Render_Service(SA__Render_End e)
        {
            GL.Flush();
            _Render_Service_Default_Shader = _Render_Service__Shaders[0];
        }

        private void Private_Draw__Render_Service(SA__Draw e)
        {
            Vector3 position = e.Draw__Position__Internal;
            Vertex_Object_Handle vertex_Object_Handle = e.Draw__VERTEX_OBJECT_HANDLE__Internal;
            Vertex_Object vertex_Object = vertex_Object_Handle.Internal_Get__Vertex_Object__Vertex_Object_Handle();
            Matrix4 world_Matrix = e.Draw__Projection_Matrix__Internal;

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

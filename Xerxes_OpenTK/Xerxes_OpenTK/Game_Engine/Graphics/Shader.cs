
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Xerxes.Game_Engine.Graphics.Xerxes_OpenTK
{
    public class Shader_OpenTK : 
        Shader
    {
        public const string EXTENSION__VERTEX_SHADER = ".vert";
        public const string EXTENSION__FRAGMENT_SHADER = ".frag";

        public override Shader_ID Shader__ID { get; }

        internal Shader_OpenTK(SA__Load_Shader e)
        {
            string path = e.Load_Shader__Resource_Path;
            string name = e.Load_Shader__Shader_Name;
            string vertexPath = 
                Path.Combine
                (
                    path,
                    name + EXTENSION__VERTEX_SHADER
                );
            string fragmentPath =
                Path.Combine
                (
                    path,
                    name + EXTENSION__FRAGMENT_SHADER
                );

            //Console.WriteLine(GL.GetString(StringName.ShadingLanguageVersion));
            int vertexShader, fragmentShader;

            string vertexShaderSource, fragmentShaderSource;

            //read source from shaders
            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            //Compile shaders
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            GL.CompileShader(vertexShader);
            string infoLogVert = GL.GetShaderInfoLog(vertexShader); //log
            if (infoLogVert != string.Empty)
                Log.Write__Info__Log(infoLogVert, this);     

            GL.CompileShader(fragmentShader);
            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader); //log
            if (infoLogFrag != string.Empty)
                Log.Write__Info__Log(infoLogFrag, this);

            Shader__ID = Create__Shader_ID__Shader(GL.CreateProgram());

            GL.AttachShader(Shader__ID, vertexShader);
            GL.AttachShader(Shader__ID, fragmentShader);
                                         
            GL.LinkProgram(Shader__ID);

            GL.DetachShader(Shader__ID, vertexShader);
            GL.DetachShader(Shader__ID, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        protected override bool Handle_Dispose__Shader()
        {
            if (Shader__Is_Disposed)
                return true;
            
            GL.DeleteShader(Shader__ID);

            return true;
        }
    }
}

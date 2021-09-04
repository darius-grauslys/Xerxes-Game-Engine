using System;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Xerxes_Engine.Systems.Graphics
{
    public class Shader : IDisposable
    {
        private int handle;
        private bool disposedValue = false;

        public int Handle { get => handle; private set => handle = value; }

        public Shader(string vertexPath, string fragmentPath)
        {
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
                Console.WriteLine(infoLogVert);     

            GL.CompileShader(fragmentShader);
            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader); //log
            if (infoLogFrag != string.Empty)
                Console.WriteLine(infoLogFrag);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            try
            {
                //GL.DeleteProgram(Handle);
            }
            catch (AccessViolationException) { } //ignore for now
        }
            
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

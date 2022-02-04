
using System;

namespace Xerxes.Game_Engine.Graphics
{
    public abstract class Shader : IDisposable
    {
        public struct Shader_ID
        {
            public readonly int ID;
            
            internal Shader_ID(int id)
            {
                ID = id;
            }

            public static implicit operator int(Shader_ID id)
                => id.ID;
        }

        public abstract Shader_ID Shader__ID { get; }
        public bool Shader__Is_Disposed { get; private set; }

        public void Dispose()
        {
            Shader__Is_Disposed =
                Handle_Dispose__Shader();
        }

        ///<summary>
        /// Returns true if the shader was disposed.
        ///</summary>
        protected abstract bool Handle_Dispose__Shader();

        protected Shader_ID Create__Shader_ID__Shader(int id)
            => new Shader_ID(id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Serializations
{
    public class SerializationService<T> : BaseSerializationService where T : class
    {
        //private int byteSize = Marshal.SizeOf<T>();

        public SerializationService(Game gameRef) 
            : base(gameRef)
        {
        }

        public virtual void SerializeObject(T obj)
        {
            //SerializeObject<T>(obj, byteSize);
        }

        public virtual T DeserializeObject(string filePath)
        {
            return default(T);
            //return DeserializeObject<T>(filePath, byteSize);
        }
    }
}

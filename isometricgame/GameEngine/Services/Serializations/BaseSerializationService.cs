using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Serializations
{
    public class BaseSerializationService
    {
        private Game gameRef;
        
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private MemoryStream memoryStream = new MemoryStream();

        protected Game GameRef { get => gameRef; set => gameRef = value; }
        protected BinaryFormatter BinaryFormatter { get => binaryFormatter; set => binaryFormatter = value; }
        protected MemoryStream MemoryStream { get => memoryStream; set => memoryStream = value; }

        public BaseSerializationService(Game gameRef)
        {
            this.gameRef = gameRef;
        }

        protected void SerializeObject<T>(T o, int byteSize, int index = 0) where T : class
        {
            memoryStream.Flush();
            binaryFormatter.Serialize(memoryStream, o);
            byte[] bytes = memoryStream.ToArray();

            Stream fileStream = File.Open(Path.Combine(GameRef.GAME_DIRECTORY_BASE, o.ToString()), FileMode.OpenOrCreate);
            fileStream.Write(bytes, index * byteSize, byteSize);
            fileStream.Close();
        }

        protected T DeserializeObject<T>(string fileName, int byteSize, int index = 0) where T : class
        {
            byte[] bytes = new byte[byteSize];

            T obj;
            Stream fileStream = File.Open(Path.Combine(GameRef.GAME_DIRECTORY_BASE, fileName), FileMode.OpenOrCreate);

            fileStream.Read(bytes, byteSize * index, byteSize);
            memoryStream = new MemoryStream(bytes);
            obj = binaryFormatter.Deserialize(memoryStream) as T;
            fileStream.Close();
            return obj;
        }
    }
}

using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.Services.Serializations;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services
{
    public class SerializationManager
    {
        private List<BaseSerializationService> services = new List<Serializations.BaseSerializationService>();

        private List<BaseSerializationService> Services { get => services; set => services = value; }

        public void RegisterService<T>(T service) where T : BaseSerializationService 
        {
            Services.Add(service);
        }

        public T GetService<T>() where T : BaseSerializationService
        {
            return Services.Find((s) => s is T) as T;
        }

        /// <summary>
        /// Use if there multiple services of the same type have been registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetIndexedService<T>(int index) where T : BaseSerializationService
        {
            return Services.Find((s) => s is T && (index-- == 0)) as T;
        }
    }
}

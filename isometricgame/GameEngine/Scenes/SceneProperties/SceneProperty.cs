using isometricgame.GameEngine.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Scenes.SceneProperties
{
    /// <summary>
    /// Scene Properties are to Scenes as Game Services are to Games. The one distinict feature is that they opperate on GameAttributes. One example is for Gravity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SceneProperty<T> where T : GameAttribute
    {
        private List<T> attributeSubscribers;
        protected List<T> AttributeSubscribers => attributeSubscribers;

        public SceneProperty(List<T> attributeSubscribers = null)
        {
            this.attributeSubscribers = (attributeSubscribers == null) ? new List<T>() : attributeSubscribers;
        }

        /// <summary>
        /// Runs on each Update Frame. Scene Proeprty Logic goes where.
        /// </summary>
        public virtual void OnUpdate()
        {

        }

        public virtual void AddSubscriber(T attrib)
        {
            if (!attributeSubscribers.Contains(attrib))
                attributeSubscribers.Add(attrib);
        }

        public virtual void RemoveSubscriber(T attrib)
        {
            if (attributeSubscribers.Contains(attrib))
                attributeSubscribers.Remove(attrib);
        }
    }
}

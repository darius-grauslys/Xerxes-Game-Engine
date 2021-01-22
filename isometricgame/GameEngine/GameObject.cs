using isometricgame.GameEngine.Components;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Exceptions.Attributes;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine
{
    public class GameObject
    {
        private Scene scene;
        private Vector3 position;

        private List<GameComponent> attributes = new List<GameComponent>();

        //public Sprite Sprite { get => sprite; set => sprite = value; }
        public Vector3 Position
        {
            get => position;
            set =>
                position = value;
        }

        public float X
        {
            get => position.X;
            set => position.X = value;
        }

        public float Y
        {
            get => position.Y;
            set => position.Y = value;
        }

        public float Z
        {
            get => position.Z;
            set => position.Z = value;
        }

        internal Scene Scene { get => scene; }

        public GameObject(Scene scene, Vector3 position)
        {
            this.scene = scene;
            this.position = position;
            
        }

        public T GetComponent<T>() where T : GameComponent
        {
            return attributes.Find((a) => a is T) as T;
        }

        protected void AddComponent<T>(T attrib) where T : GameComponent
        {
            for (int i = 0; i < attributes.Count; i++)
                if (attributes[i] is T)
                    throw new ExistingAttributeException();
            attributes.Add(attrib);
        }

        public virtual void OnUpdate(FrameArgument args)
        {
            foreach (GameComponent attrib in attributes)
                attrib.Update(args);
        }
    }
}

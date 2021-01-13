using isometricgame.GameEngine.Components;
using isometricgame.GameEngine.Events;
using isometricgame.GameEngine.Exceptions.Attributes;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.WorldSpace.Geometry;
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
        private Orientation orientation;

        private List<GameComponent> attributes = new List<GameComponent>();

        //public Sprite Sprite { get => sprite; set => sprite = value; }
        public Vector3 Position
        {
            get => position;
            protected set
            {
                position = value;
            }
        }
        internal Scene Scene { get => scene; }
        public Orientation Orientation { get => orientation; protected set => orientation = value; }

        public virtual float GetX() { return position.X; }
        public virtual float GetY() { return position.Y; }
        public virtual float GetZ() { return position.Z; }

        public virtual void SetX(float x) { position.X = x; }
        public virtual void SetY(float y) { position.Y = y; }
        public virtual void SetZ(float z) { position.Z = z; }

        public GameObject(Scene scene, Vector3 position)
        {
            this.scene = scene;
            this.position = position;
            
        }

        public T GetAttribute<T>() where T : GameComponent
        {
            return attributes.Find((a) => a is T) as T;
        }

        protected void AddAttribute<T>(T attrib) where T : GameComponent
        {
            for (int i = 0; i < attributes.Count; i++)
                if (attributes[i] is T)
                    throw new ExistingAttributeException();
            attributes.Add(attrib);
        }

        public void OnUpdate(FrameEventArgs args)
        {
            HandleOnUpdate();
            foreach (GameComponent attrib in attributes)
                attrib.OnUpdate(args);
        }

        protected virtual void HandleOnUpdate()
        {

        }
    }
}

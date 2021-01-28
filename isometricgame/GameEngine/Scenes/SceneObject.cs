using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Exceptions.Attributes;
using OpenTK;

namespace isometricgame.GameEngine.Scenes
{
    public class SceneObject
    {
        private Scene scene;
        private Vector3 position;
        private SpriteComponent spriteComponent;

        private List<GameComponent> components = new List<GameComponent>();

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

        public Scene Scene { get => scene; set => scene = value; }
        
        /// <summary>
        /// Setting this changes the recorded component. No call to RemoveComponent/AddComponent is required.
        /// </summary>
        public SpriteComponent SpriteComponent {
            get => spriteComponent;
            protected set
            {
                if (spriteComponent != null)
                {
                    RemoveComponent(spriteComponent);
                }
                spriteComponent = value;
                AddComponent(spriteComponent);
            }
        }

        public SceneObject(Scene scene, Vector3 position, SpriteComponent spriteComponent = null)
        {
            this.scene = scene;
            this.position = position;
            if (spriteComponent != null)
            {
                this.spriteComponent = spriteComponent;
                spriteComponent.ParentObject = this;
                AddComponent(spriteComponent);
            }
        }

        public T GetComponent<T>() where T : GameComponent
        {
            return components.Find((a) => a is T) as T;
        }

        protected void AddComponent<T>(T attrib) where T : GameComponent
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i] is T)
                    throw new ExistingAttributeException();
            components.Add(attrib);
        }

        protected void RemoveComponent<T>(T attrib) where T : GameComponent
        {
            components.Remove(attrib);
        }

        public virtual void OnUpdate(FrameArgument args)
        {
            foreach (GameComponent attrib in components)
                attrib.Update(args);
        }

        public virtual SceneObject Clone()
        {
            SceneObject newObj = new SceneObject(scene, position);

            foreach (GameComponent comp in components)
                newObj.AddComponent(comp.Clone(newObj));

            return newObj;
        }
    }
}

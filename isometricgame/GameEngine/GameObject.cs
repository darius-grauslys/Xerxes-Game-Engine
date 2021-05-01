using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Exceptions.Attributes;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;

namespace isometricgame.GameEngine
{
    public class GameObject
    {
        internal RenderUnit renderUnit;
        private SceneLayer sceneLayer;
        private SpriteComponent spriteComponent;

        private List<GameComponent> components = new List<GameComponent>();

        public Vector3 Position
        {
            get => renderUnit.Position;
            set => renderUnit.Position = value;
        }

        public float X { get => renderUnit.X; set => renderUnit.X = value; }
        public float Y { get => renderUnit.Y; set => renderUnit.Y = value; }
        public float Z { get => renderUnit.Z; set => renderUnit.Z = value; }

        public SceneLayer SceneLayer { get => sceneLayer; set => sceneLayer = value; }
        
        /// <summary>
        /// Setting this changes the recorded component. No call to RemoveComponent/AddComponent is required.
        /// </summary>
        public SpriteComponent SpriteComponent {
            get => GetReservedField(ref spriteComponent);
            protected set => ReplaceComponent(value);
        }

        public GameObject(SceneLayer sceneLayer, Vector3 position)
        {
            this.sceneLayer = sceneLayer;
            Position = position;
        }

        public GameObject(SceneLayer sceneLayer, Vector3 position, string spriteName)
        {
            this.sceneLayer = sceneLayer;
            Position = position;
            SpriteComponent = new SpriteComponent();
            SpriteComponent.SetSprite(spriteName);
        }

        public T GetComponent<T>() where T : GameComponent
        {
            return components.Find((a) => a is T) as T;
        }

        public virtual void ReplaceComponent<T>(T component) where T : GameComponent
        {
            foreach (T c in components.ToList().OfType<T>())
                components.Remove(c);
            AddComponent(component);
        }

        public virtual void AddComponent<T>(T component) where T : GameComponent
        {
            for (int i = 0; i < components.Count; i++)
                if (components[i] is T)
                    throw new ExistingAttributeException();
            components.Add(component);
            component.ParentObject = this;
            component._newParent();
        }

        public virtual void RemoveComponent<T>(T attrib) where T : GameComponent
        {
            components.Remove(attrib);
        }

        public virtual void OnUpdate(FrameArgument args)
        {
            foreach (GameComponent attrib in components)
                attrib.Update(args);
        }

        protected T GetReservedField<T>(ref T field) where T : GameComponent
        {
            if (field != null)
                return field;
            return field = GetComponent<T>();
        }

        internal void _handleDraw(RenderService renderService)
        {
            HandleDraw(renderService);
        }

        protected virtual void HandleDraw(RenderService renderService)
        {
            bool? check = SpriteComponent?.Enabled;
            if (check != null && (bool)check)
                renderService.DrawSprite(ref renderUnit, Position.X, Position.Y, Position.Z);
        }

        public virtual GameObject Clone()
        {
            GameObject newObj = new GameObject(sceneLayer, Position);

            foreach (GameComponent comp in components)
                newObj.AddComponent(comp.Clone());

            return newObj;
        }
    }
}

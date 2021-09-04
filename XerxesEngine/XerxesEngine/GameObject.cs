using System.Linq;
using XerxesEngine.Rendering;
using XerxesEngine.Scenes;
using XerxesEngine.Systems.Rendering;
using OpenTK;

namespace XerxesEngine
{
    public class GameObject
    {
        internal RenderUnit renderUnit;

        private readonly GameObject_Component[] COMPONENTS;

        internal Vector3 Position
        {
            get => renderUnit.Position;
            set => renderUnit.Position = value;
        }

        public Scene_Layer GameObject__Scene_Layer { get; set; }

        internal GameObject
            (
            Scene_Layer sceneLayer,
            Vector3 position, 
            params GameObject_Component[] components
            )
        {
            GameObject__Scene_Layer = sceneLayer;
            
            Position = position;

            COMPONENTS = components?.ToArray() ?? new GameObject_Component[0];
            
            for(int i=0;i<COMPONENTS.Length;i++)
                COMPONENTS[i].Attach_To__GameObject__Component(this);
        }

        public T Get__Component__GameObject<T>() where T : GameObject_Component
        {
            T[] components = COMPONENTS.OfType<T>().ToArray();
            return (components.Length > 0) ? components[0] : null;
        }

        public virtual void OnUpdate(Frame_Argument args)
        {
            foreach (GameObject_Component attrib in COMPONENTS)
                attrib.Update(args);
        }

        internal void Draw(RenderService renderService)
        {
            Handle_Draw__GameObject(renderService);
        }

        protected virtual void Handle_Draw__GameObject(RenderService renderService)
        {
            if (renderUnit.IsInitialized)
                renderService.DrawSprite(ref renderUnit, Position.X, Position.Y, Position.Z);
        }

        public virtual GameObject Clone__GameObject()
        {
            GameObject_Component[] clonedComponents = new GameObject_Component[COMPONENTS.Length];

            for (int i = 0; i < COMPONENTS.Length; i++)
            {
                clonedComponents[i] = COMPONENTS[i].Clone__Component();
            }
            
            GameObject newObj = new GameObject(GameObject__Scene_Layer, Position, clonedComponents);

            return newObj;
        }
    }
}

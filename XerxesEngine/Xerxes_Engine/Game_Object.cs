using System.Linq;
using Xerxes_Engine.Systems.Graphics;
using OpenTK;

namespace Xerxes_Engine
{
    public class Game_Object
    {
        internal Render_Unit_R2 renderUnit;

        private readonly Game_Object_Component[] COMPONENTS;

        internal Vector3 Position
        {
            get => renderUnit.Position;
            set => renderUnit.Position = value;
        }

        public Scene_Layer Game_Object__Scene_Layer { get; set; }

        public Game_Object
            (
            Scene_Layer sceneLayer,
            Vector3 position, 
            params Game_Object_Component[] components
            )
        {
            Game_Object__Scene_Layer = sceneLayer;
            
            Position = position;

            COMPONENTS = components?.ToArray() ?? new Game_Object_Component[0];
            
            for(int i=0;i<COMPONENTS.Length;i++)
                COMPONENTS[i].Attach_To__Game_Object__Component(this);
        }

        public T Get__Component__Game_Object<T>() where T : Game_Object_Component
        {
            T[] components = COMPONENTS.OfType<T>().ToArray();
            return (components.Length > 0) ? components[0] : null;
        }

        public virtual void OnUpdate(Frame_Argument args)
        {
            foreach (Game_Object_Component attrib in COMPONENTS)
                attrib.Update(args);
        }

        internal void Draw(Render_Service renderService)
        {
            Handle_Draw__Game_Object(renderService);
        }

        protected virtual void Handle_Draw__Game_Object(Render_Service renderService)
        {
            if (renderUnit.IsInitialized)
                renderService.DrawSprite(ref renderUnit, Position.X, Position.Y, Position.Z);
        }

        public virtual Game_Object Clone__Game_Object()
        {
            Game_Object_Component[] clonedComponents = new Game_Object_Component[COMPONENTS.Length];

            for (int i = 0; i < COMPONENTS.Length; i++)
            {
                clonedComponents[i] = COMPONENTS[i].Clone__Component();
            }
            
            Game_Object newObj = new Game_Object(Game_Object__Scene_Layer, Position, clonedComponents);

            return newObj;
        }
    }
}

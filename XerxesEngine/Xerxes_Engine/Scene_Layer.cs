using Xerxes_Engine.Systems.Graphics;
using OpenTK;
using System.Collections.Generic;

namespace Xerxes_Engine
{
    public class Scene_Layer
    {
        public int Scene_Layer__LayerLevel { get; internal set; }
        public bool Scene_Layer__Is_Enabled { get; private set; }

        private readonly List<Render_Structure_R2> _Scene_Layer__SCENE_STRUCTURES = new List<Render_Structure_R2>();
        public Render_Structure_R2[] Scene_Layer__Scene_Structures
            => _Scene_Layer__SCENE_STRUCTURES.ToArray();
        protected void Handle_Add__Structure__Scene_Layer(Render_Structure_R2 structure) => _Scene_Layer__SCENE_STRUCTURES.Add(structure);

        private readonly List<Game_Object> _Scene_Layer__SCENE_OBJECTS = new List<Game_Object>();
        public Game_Object[] Scene_Layer__Scene_Objects
            => _Scene_Layer__SCENE_OBJECTS.ToArray();

        protected virtual void Add__Scene_Object__Scene_Layer(Game_Object obj)
        {
            _Scene_Layer__SCENE_OBJECTS.Add(obj);
            obj.Game_Object__Scene_Layer = this;
        }

        internal void Internal_Disable__Scene_Layer()
        {
            Scene_Layer__Is_Enabled = false;
            Handle_Disabled__Scene_Layer();
        }
    
        protected virtual void Handle_Disabled__Scene_Layer() { }

        internal void Internal_Enable__Scene_Layer()
        {
            Scene_Layer__Is_Enabled = true;
            Handle_Enabled__Scene_Layer();
        }

        protected virtual void Handle_Enabled__Scene_Layer() { }

        public Matrix4 Scene_Layer__Layer_Matrix { get; protected set; }
        internal void Internal_Rescale__Scene_Layer()
        {
            Scene_Layer__Layer_Matrix = 
                Matrix4.CreateOrthographic
                    (
                    Scene_Layer__Game.Width, 
                    Scene_Layer__Game.Height, 
                    0.01f, 
                    30000f
                    ) 
                * Matrix4.CreateTranslation(0, 0, 1);
            Handle_Rescaled__Scene_Layer();
        }
        protected virtual void Handle_Rescaled__Scene_Layer() { }
        
        public Scene Scene_Layer__Parent_Scene { get; private set; }
        
        public Game Scene_Layer__Game => Scene_Layer__Parent_Scene?.Game__REFERENCE;
        public Vector2 SceneLayer__Window_Size__Game
            => Scene_Layer__Game?.Get__Window_Size__Game() ?? Vector2.One;
        
        /// <summary>
        /// This is only invoked once.
        /// </summary>
        /// <param name="parent"></param>
        internal void Internal_Set__Parent__Scene_Layer(Scene parent)
        {
            Scene_Layer__Parent_Scene = parent;
            
            Handle_Enter__Scene_Environment(parent);
        }
        /// <summary>
        /// Override this to perform constructor logic that requires a scene presence.
        /// </summary>
        /// <param name="parent"></param>
        protected virtual void Handle_Enter__Scene_Environment(Scene parent) { }

        private bool Scene_Layer__Enabled { get; set; }

        public Scene_Layer(Scene sceneLayerParentScene, int sceneLayerLayerLevel = 0)
        {
            Scene_Layer__LayerLevel = sceneLayerLayerLevel;
            Scene_Layer__Parent_Scene = sceneLayerParentScene;

            Internal_Rescale__Scene_Layer();
        }

        internal void Internal_Gain_Focus__Scene_Layer()
            => Handle_Gained_Focus__Scene_Layer();
        protected virtual void Handle_Gained_Focus__Scene_Layer() { }

        internal void Internal_Update__Scene_Layer(Frame_Argument e)
            => Handle_Update__Scene_Layer(e);
        protected virtual void Handle_Update__Scene_Layer(Frame_Argument e)
        {
            Private_Update__Layer_Objects__Scene_Layer(e);
        }
        
        internal void Begin_Render__Scene_Layer(Render_Service renderService)
            => Handle_Begin_Render__Scene_Layer(renderService);
        /// <summary>
        /// Returns the desired shader ID.
        /// </summary>
        /// <returns></returns>
        protected virtual void Handle_Begin_Render__Scene_Layer(Render_Service renderService) { }

        internal void Render__Scene_Layer(Render_Service renderService, Frame_Argument e)
        {
            if(Scene_Layer__Enabled)
                Handle_Render__Scene_Layer(renderService, e);
        }

        protected virtual void Handle_Render__Scene_Layer(Render_Service renderService, Frame_Argument e)
        {
            foreach (Game_Object so in Scene_Layer__Scene_Objects)
            {
                Handle_Render_Object__Scene_Layer(renderService, so);
            }
        }

        private void Private_Update__Layer_Objects__Scene_Layer(Frame_Argument e)
        {
            if (!Scene_Layer__Enabled)
                return;
            
            foreach (Game_Object obj in Scene_Layer__Scene_Objects)
                obj.OnUpdate(e);

            Handle_Objects_Updated__Scene_Layer(e);
        }
        
        protected virtual void Handle_Objects_Updated__Scene_Layer(Frame_Argument e)
        {
            
        }
        
        protected virtual void Handle_Render_Object__Scene_Layer(Render_Service renderService, Game_Object gameObject)
        {
            gameObject.Draw(renderService);
        }
    }
}

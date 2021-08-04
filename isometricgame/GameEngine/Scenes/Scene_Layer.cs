using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Scenes
{
    public class Scene_Layer
    {
        public int SceneLayer__LayerLevel { get; internal set; }

        private readonly List<RenderStructure> _Scene_Layer__SCENE_STRUCTURES = new List<RenderStructure>();
        public RenderStructure[] Scene_Layer__Scene_Structures
            => _Scene_Layer__SCENE_STRUCTURES.ToArray();
        protected void Handle_Add__Structure__Scene_Layer(RenderStructure structure) => _Scene_Layer__SCENE_STRUCTURES.Add(structure);

        private readonly List<GameObject> _Scene_Layer__SCENE_OBJECTS = new List<GameObject>();
        public GameObject[] Scene_Layer__Scene_Objects
            => _Scene_Layer__SCENE_OBJECTS.ToArray();

        protected virtual void Add__Scene_Object__Scene_Layer(GameObject obj)
        {
            _Scene_Layer__SCENE_OBJECTS.Add(obj);
            obj.GameObject__Scene_Layer = this;
        }

        internal void Internal_Disable__Scene_Layer()
            => Handle_Disabled__Scene_Layer();
        protected virtual void Handle_Disabled__Scene_Layer() { }

        internal void Internal_Enable__Scene_Layer()
            => Handle_Enabled__Scene_Layer();
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
        
        public Game Scene_Layer__Game => Scene_Layer__Parent_Scene?.Game;
        public Vector2 SceneLayer__Window_Size__Game
            => Scene_Layer__Game?.Game__Window_Size ?? Vector2.One;
        
        /// <summary>
        /// This is only invoked once.
        /// </summary>
        /// <param name="parent"></param>
        internal void Internal_Set__Parent__Scene_Layer(Scene parent)
        {
            Scene_Layer__Parent_Scene = parent;
            
            if (Scene_Layer__Enabled)
                parent.sceneLayers.Add(this);
            else
                parent.disabledLayers.Add(this);
            
            Handle_Enter__Scene_Environment(parent);
        }
        /// <summary>
        /// Override this to perform constructor logic that requires a scene presence.
        /// </summary>
        /// <param name="parent"></param>
        protected virtual void Handle_Enter__Scene_Environment(Scene parent) { }

        private bool _scene_layer__Enabled = true;
        public bool Scene_Layer__Enabled 
        { 
            get => _scene_layer__Enabled; 
            set 
            {
                _scene_layer__Enabled = value;
                if (_scene_layer__Enabled) 
                    Scene_Layer__Parent_Scene.EnableLayer(this);
                else 
                    Scene_Layer__Parent_Scene.DisableLayer(this);
            } 
        }

        public Scene_Layer(Scene sceneLayerParentScene, int sceneLayerLayerLevel = 0)
        {
            SceneLayer__LayerLevel = sceneLayerLayerLevel;
            Scene_Layer__Parent_Scene = sceneLayerParentScene;

            Internal_Rescale__Scene_Layer();
        }

        internal void Internal_Gain_Focus__Scene_Layer()
            => Handle_Gained_Focus__Scene_Layer();
        protected virtual void Handle_Gained_Focus__Scene_Layer() { }

        internal void Internal_Update__Scene_Layer(FrameArgument e)
            => Handle_Update__Scene_Layer(e);
        protected virtual void Handle_Update__Scene_Layer(FrameArgument e)
        {
            Private_Update__Layer_Objects__Scene_Layer(e);
        }
        
        internal void Begin_Render__Scene_Layer(RenderService renderService)
            => Handle_Begin_Render__Scene_Layer(renderService);
        /// <summary>
        /// Returns the desired shader ID.
        /// </summary>
        /// <returns></returns>
        protected virtual void Handle_Begin_Render__Scene_Layer(RenderService renderService) { }

        internal void Render__Scene_Layer(RenderService renderService, FrameArgument e)
        {
            if(Scene_Layer__Enabled)
                Handle_Render__Scene_Layer(renderService, e);
        }

        protected virtual void Handle_Render__Scene_Layer(RenderService renderService, FrameArgument e)
        {
            foreach (GameObject so in Scene_Layer__Scene_Objects)
            {
                Handle_Render_Object__Scene_Layer(renderService, so);
            }
        }

        private void Private_Update__Layer_Objects__Scene_Layer(FrameArgument e)
        {
            if (!Scene_Layer__Enabled)
                return;
            
            foreach (GameObject obj in Scene_Layer__Scene_Objects)
                obj.OnUpdate(e);

            Handle_Objects_Updated__Scene_Layer(e);
        }
        
        protected virtual void Handle_Objects_Updated__Scene_Layer(FrameArgument e)
        {
            
        }
        
        protected virtual void Handle_Render_Object__Scene_Layer(RenderService renderService, GameObject gameObject)
        {
            gameObject.Draw(renderService);
        }
    }
}

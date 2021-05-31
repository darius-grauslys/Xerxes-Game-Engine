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
    public class SceneLayer
    {
        public int LayerLevel { get; internal set; }

        private List<RenderStructure> staticSceneStructures = new List<RenderStructure>();
        public List<RenderStructure> StaticSceneStructures
            => staticSceneStructures.ToList();
        protected void Add_StaticStructure(RenderStructure structure) => staticSceneStructures.Add(structure);

        private List<RenderStructure> dynamicSceneStructures = new List<RenderStructure>();
        public List<RenderStructure> DynamicSceneStructures
            => dynamicSceneStructures.ToList();
        protected void Add_DynamicStructure(RenderStructure structure) => dynamicSceneStructures.Add(structure);
        protected void Remove_DynamicStructure(RenderStructure structure) => dynamicSceneStructures.Remove(structure);

        private List<GameObject> staticSceneObjects = new List<GameObject>();
        public List<GameObject> StaticSceneObjects
            => staticSceneObjects.ToList();

        internal void SetDisable()
            => Handle_Disabled();
        protected virtual void Handle_Disabled() { }

        internal void SetEnabled()
            => Handle_Enabled();
        protected virtual void Handle_Enabled() { }

        protected void Add_StaticObject(GameObject obj) => staticSceneObjects.Add(obj);

        private List<GameObject> dynamicSceneObjects = new List<GameObject>();
        public List<GameObject> DynamicSceneObjects
            => dynamicSceneObjects.ToList();
        protected void Add_DynamicObject(GameObject obj) => dynamicSceneObjects.Add(obj);
        protected void Remove_DynamicObject(GameObject obj) => dynamicSceneObjects.Remove(obj);

        public Matrix4 LayerMatrix { get; protected set; }
        public void Rescale()
        {
            LayerMatrix = Matrix4.CreateOrthographic(Game.Width, Game.Height, 0.01f, 30000f) * Matrix4.CreateTranslation(0, 0, 1);
            Handle_Rescale();
        }
        protected virtual void Handle_Rescale() { }

        public Game Game { get; private set; }
        public Scene ParentScene { get; private set; }
        /// <summary>
        /// Not loop friendly.
        /// </summary>
        /// <param name="parent"></param>
        internal void SetParent(Scene parent)
        {
            if (ParentScene != null)
            {
                Handle_LoseParent(ParentScene);
                if (ParentScene.disabledLayers.Contains(this))
                    ParentScene.disabledLayers.Remove(this);
                else
                    ParentScene.sceneLayers.Remove(this);
            }
            ParentScene = parent;
            if (Enabled)
                parent.sceneLayers.Add(this);
            else
                parent.disabledLayers.Add(this);
            Handle_GainParent(parent);
        }
        protected virtual void Handle_LoseParent(Scene parent) { }
        protected virtual void Handle_GainParent(Scene parent) { }

        private bool enabled = true;
        public bool Enabled { get => enabled; set {
                enabled = value;
                if (enabled) ParentScene.EnableLayer(this);
                else ParentScene.DisableLayer(this);
            } }

        public SceneLayer(Scene parentScene, int layerLevel = 0)
        {
            LayerLevel = layerLevel;
            Game = parentScene.Game;
            ParentScene = parentScene;

            Rescale();
        }

        internal void SceneGainFocus()
            => Handle_SceneGainFocus();
        protected virtual void Handle_SceneGainFocus() { }

        internal void UpdateLayer(FrameArgument e)
            => Handle_UpdateLayer(e);
        protected virtual void Handle_UpdateLayer(FrameArgument e)
        {
            UpdateLayer_Objects(e, StaticSceneObjects.ToArray());
            UpdateLayer_Objects(e, DynamicSceneObjects.ToArray());
        }
        
        internal void BeginRender(RenderService renderService)
            => HandleBeginRender(renderService);
        /// <summary>
        /// Returns the desired shader ID.
        /// </summary>
        /// <returns></returns>
        protected virtual void HandleBeginRender(RenderService renderService) { }

        internal void RenderLayer(RenderService renderService, FrameArgument e)
            => Handle_RenderLayer(renderService, e);
        protected virtual void Handle_RenderLayer(RenderService renderService, FrameArgument e)
        {
            RenderLayer_Objects(renderService, e, StaticSceneObjects.ToArray());
            RenderLayer_Objects(renderService, e, DynamicSceneObjects.ToArray());
        }
                
        private void RenderLayer_Objects(RenderService renderService, FrameArgument e, GameObject[] sceneObjects)
            => Handle_RenderLayer_Objects(renderService, e, sceneObjects);
        protected virtual void Handle_RenderLayer_Objects(RenderService renderService, FrameArgument e, GameObject[] sceneObjects)
        {
            foreach (GameObject so in sceneObjects)
            {
                DrawSprite(renderService, so);
            }
        }

        private void UpdateLayer_Objects(FrameArgument e, GameObject[] gameObjects)
            => Handle_UpdateLayer_Objects(e, gameObjects);
        protected virtual void Handle_UpdateLayer_Objects(FrameArgument e, GameObject[] gameObjects)
        {
            foreach (GameObject obj in gameObjects)
                obj.OnUpdate(e);
        }
        
        protected virtual void DrawSprite(RenderService renderService, GameObject gameObject)
        {
            gameObject._handleDraw(renderService);
        }
    }
}

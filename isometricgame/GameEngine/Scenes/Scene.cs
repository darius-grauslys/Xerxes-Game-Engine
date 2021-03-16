using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Scenes
{
    public class Scene
    {
        private Game game;
        private List<RenderStructure> staticSceneStructures = new List<RenderStructure>();
        private List<RenderStructure> dynamicSceneStructures = new List<RenderStructure>();
        private List<GameObject> staticSceneObjects = new List<GameObject>();
        private List<GameObject> dynamicSceneObjects = new List<GameObject>();
        private Matrix4 sceneMatrix;
        
        public Game Game => game;

        public Matrix4 SceneMatrix { get => sceneMatrix; protected set => sceneMatrix = value; }
        public List<RenderStructure> StaticSceneStructures { get => staticSceneStructures; protected set => staticSceneStructures = value; }
        public List<RenderStructure> DynamicSceneStructures { get => dynamicSceneStructures; protected set => dynamicSceneStructures = value; }
        public List<GameObject> StaticSceneObjects { get => staticSceneObjects; protected set => staticSceneObjects = value; }
        public List<GameObject> DynamicSceneObjects { get => dynamicSceneObjects; protected set => dynamicSceneObjects = value; }

        public Scene(Game game)
        {
            this.game = game;
            
            sceneMatrix = Matrix4.CreateOrthographic(1200,900,0.01f,30000f) * Matrix4.CreateTranslation(0,0,1);
        }

        public virtual void RenderFrame(RenderService renderService, FrameArgument e)
        {
            RenderSceneObjects(renderService, e, StaticSceneObjects.ToArray());
            RenderSceneObjects(renderService, e, DynamicSceneObjects.ToArray());
        }

        public virtual void UpdateFrame(FrameArgument e)
        {
            UpdateObjects(e, StaticSceneObjects.ToArray());
            UpdateObjects(e, DynamicSceneObjects.ToArray());
        }

        protected virtual void RenderSceneObjects(RenderService renderService, FrameArgument e, GameObject[] sceneObjects)
        {
            foreach (GameObject so in sceneObjects)
            {
                if (so.renderUnit.IsInitialized)
                    DrawSprite(renderService, ref so.renderUnit);
            }
        }

        protected virtual void UpdateObjects(FrameArgument e, GameObject[] gameObjects)
        {
            foreach (GameObject obj in gameObjects)
                obj.OnUpdate(e);
        }

        protected virtual void DrawSprite(RenderService renderService, ref RenderUnit renderUnit)
        {
            renderService.DrawSprite(ref renderUnit, renderUnit.Position.X, renderUnit.Position.Y, renderUnit.Position.Z);
        }
    }
}

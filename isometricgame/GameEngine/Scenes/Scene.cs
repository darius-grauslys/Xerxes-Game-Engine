using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.Systems.Scenes;
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
        private List<SceneObjectCollection> sceneObjectCollections = new List<SceneObjectCollection>();
        private List<SceneStructure> staticSceneStructures = new List<SceneStructure>();
        private List<SceneStructure> dynamicSceneStructures = new List<SceneStructure>();
        private List<SceneObject> staticSceneObjects = new List<SceneObject>();
        private List<SceneObject> dynamicSceneObjects = new List<SceneObject>();
        private Matrix4 sceneMatrix;
        
        public List<SceneObjectCollection> SceneObjectCollections { get => sceneObjectCollections; }
        public Game Game => game;

        public Matrix4 SceneMatrix { get => sceneMatrix; protected set => sceneMatrix = value; }
        public List<SceneStructure> StaticSceneStructures { get => staticSceneStructures; protected set => staticSceneStructures = value; }
        public List<SceneStructure> DynamicSceneStructures { get => dynamicSceneStructures; protected set => dynamicSceneStructures = value; }
        public List<SceneObject> StaticSceneObjects { get => staticSceneObjects; protected set => staticSceneObjects = value; }
        public List<SceneObject> DynamicSceneObjects { get => dynamicSceneObjects; protected set => dynamicSceneObjects = value; }

        public Scene(Game game)
        {
            this.game = game;
            
            sceneMatrix = Matrix4.CreateOrthographic(1200,900,0.01f,30000f) * Matrix4.CreateTranslation(0,0,1);
        }

        public virtual void RenderFrame(RenderService renderService, FrameArgument e)
        {
            foreach (SceneStructure ss in StaticSceneStructures)
                RenderStructureUnits(renderService, e, ss.StructuralUnits);
            foreach (SceneStructure ss in DynamicSceneStructures)
                RenderStructureUnits(renderService, e, ss.StructuralUnits);

            RenderSceneObjects(renderService, e, StaticSceneObjects.ToArray());
            RenderSceneObjects(renderService, e, DynamicSceneObjects.ToArray());
        }

        public virtual void UpdateFrame(FrameArgument e)
        {
            for(int i=0;i<sceneObjectCollections.Count;i++)
                UpdateObjects(e, SceneObjectCollections[i].Collection);
            UpdateObjects(e, StaticSceneObjects.ToArray());
            UpdateObjects(e, DynamicSceneObjects.ToArray());
        }

        protected virtual void RenderSceneObjects(RenderService renderService, FrameArgument e, SceneObject[] sceneObjects)
        {
            foreach (SceneObject so in sceneObjects)
            {
                Sprite s = so.SpriteComponent.Sprite;
                DrawSprite(renderService, s, so.X + s.OffsetX, so.Y + s.OffsetY, so.Z);
            }
        }

        protected virtual void RenderStructureUnits(RenderService renderService, FrameArgument e, StructureUnit[] structureUnits)
        {
            SceneObject so;
            Sprite s;
            for (int i = 0; i < structureUnits.Length; i++)
            {
                so = SceneObjectCollections[structureUnits[i].Id].Collection[structureUnits[i].DataTag];
                s = so.SpriteComponent.Sprite;
                DrawSprite(renderService, s, structureUnits[i].Position.X + s.OffsetX, structureUnits[i].Position.Y + s.OffsetY, structureUnits[i].Position.Z);
            }
        }

        protected virtual void UpdateObjects(FrameArgument e, SceneObject[] gameObjects)
        {
            foreach (SceneObject obj in gameObjects)
                obj.OnUpdate(e);
        }

        protected virtual void DrawSprite(RenderService renderService, Sprite s, float x, float y, float z = 0)
        {
            renderService.DrawSprite(s, x, y, z);
        }
    }
}

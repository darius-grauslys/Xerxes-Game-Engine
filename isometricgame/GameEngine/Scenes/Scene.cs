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
        private List<GameObject> gameObjects;
        private Matrix4 sceneMatrix;
        
        public List<GameObject> GameObjects { get => gameObjects; }
        public Game Game => game;

        public Matrix4 SceneMatrix { get => sceneMatrix; protected set => sceneMatrix = value; }

        public Scene(Game game)
        {
            this.game = game;
            gameObjects = new List<GameObject>();

            //sceneMatrix = Matrix4.CreateTranslation(new Vector3(0, 0, 0));
            //sceneMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 8, 1200 / 900, 0.01f, 30000f);
            sceneMatrix = Matrix4.CreateOrthographic(1200,900,0.01f,30000f) * Matrix4.CreateTranslation(0,0,1);
        }

        public virtual void RenderFrame(RenderService renderService, FrameArgument e)
        {
            foreach (GameObject obj in GameObjects)
            {
                SpriteComponent sa = obj.GetAttribute<SpriteComponent>();
                if (sa != null)
                {
                    sa.GetSprite().Use();
                    renderService.DrawSprite(obj.X, obj.Y);
                }
            }
        }

        public virtual void UpdateFrame(FrameArgument e)
        {
            foreach (GameObject obj in GameObjects)
                obj.OnUpdate(e);
        }
    }
}

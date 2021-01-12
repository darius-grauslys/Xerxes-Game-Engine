using isometricgame.GameEngine.Attributes.Rendering;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Services;
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

            sceneMatrix = Matrix4.CreateTranslation(new Vector3(0, 0, 0));
        }

        public virtual void RenderFrame(RenderService renderService, FrameEventArgs e)
        {
            foreach (GameObject obj in GameObjects)
            {
                SpriteAttribute sa = obj.GetAttribute<SpriteAttribute>();
                if (sa != null)
                    foreach (Sprite s in sa.GetSprites())
                        renderService.DrawSprite(s, obj.GetX() + s.OffsetX, obj.GetY() + s.OffsetY);
            }
        }

        public virtual void UpdateFrame(FrameEventArgs e)
        {
            foreach (GameObject obj in GameObjects)
                obj.OnUpdate(e);
        }
    }
}

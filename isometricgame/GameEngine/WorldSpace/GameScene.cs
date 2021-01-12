using isometricgame.GameEngine.WorldSpace.Generators;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Services.Serializations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Scenes;
using OpenTK;
using isometricgame.GameEngine.Services;

namespace isometricgame.GameEngine.WorldSpace
{
    /// <summary>
    /// This is the encapsulating scene that holds the worldspace and ui scenes.
    /// </summary>
    public class GameScene : Scene
    {
        private WorldScene world;
        
        /// <summary>
        /// This is a child scene. This child scene is responsible for drawing the tiles.
        /// </summary>
        public WorldScene World { get => world; set => world = value; }
        
        public GameScene(Game gameRef)
            : base(gameRef)
        {
            //chunkService = new ChunkDirectory(4, new List<GameObject>(), this, new WorldGenerator(this, 2242, 4));
            world = new WorldScene(gameRef);

        }

        public override void RenderFrame(RenderService renderService, FrameEventArgs e)
        {
            renderService.RenderScene(World, e);
        }

        public override void UpdateFrame(FrameEventArgs e)
        {
            world.UpdateFrame(e);
        }
    }
}

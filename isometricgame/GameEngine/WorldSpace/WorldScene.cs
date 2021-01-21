using isometricgame.GameEngine;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.Systems;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace.Generators;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.WorldSpace.ChunkSpace;

namespace isometricgame.GameEngine.WorldSpace
{
    public class WorldScene : Scene
    {
        private ChunkDirectory chunkDirectory;
        private Camera clientCamera;

        private int tileRange, renderDistance;
        
        private SpriteLibrary spriteLibrary;

        public Camera ClientCamera { get => clientCamera; private set => clientCamera = value; }
        public ChunkDirectory ChunkDirectory { get => chunkDirectory; set => chunkDirectory = value; }

        public WorldScene(Game game, int renderDistance=2, int seed=12345)
            : base(game)
        {
            this.ChunkDirectory = new ChunkDirectory(renderDistance, new WorldGenerator(seed));
            this.ClientCamera = new Camera(this);
            this.renderDistance = renderDistance;

            spriteLibrary = game.GetSystem<SpriteLibrary>();
        }
        
        public override void UpdateFrame(FrameArgument e)
        {
            ClientCamera.Pan_Linear((float)e.DeltaTime);
            
            SceneMatrix = ClientCamera.GetView();
            ChunkDirectory.ChunkCleanup(ClientCamera.Position.Xy);

            tileRange = (int)((2 / Math.Log(clientCamera.Zoom + 1)) * 16);
            renderDistance = (tileRange / Chunk.CHUNK_TILE_WIDTH) + 2;
            chunkDirectory.RenderDistance = renderDistance;

            base.UpdateFrame(e);
        }
        
        public override void RenderFrame(RenderService renderService, FrameArgument e)
        {
            if (chunkDirectory.RenderDistance != renderDistance)
                return; //prevent race condition

            float minX, maxX, minY, maxY;

            int flooredX = (int)clientCamera.TargetPosition.X;
            int flooredY = (int)clientCamera.TargetPosition.Y;
                        
            minX = flooredX - tileRange;
            minY = flooredY - tileRange;
            maxX = flooredX + tileRange;
            maxY = flooredY + tileRange;

            float width = chunkDirectory.VisibleWidth;
            float height = chunkDirectory.VisibleHeight;

            float rot = (float)(Math.Sqrt(2) / 2);

            for (float y = maxY; y >= minY; y--)
            {
                for (float x = minX; x < maxX; x++)
                {
                    Tile t;
                    
                    try
                    {
                        t = ChunkDirectory.DeliminateTile(new Vector2(x, y));
                    }
                    catch { return; } //write to error log later.
                    
                    float tx = Chunk.CartesianToIsometric_X(x, y), ty = Chunk.CartesianToIsometric_Y(x, y, t.Z);

                    spriteLibrary.GetSprite(t.Data).Use(t.Orientation);
                    renderService.DrawSprite(tx, ty);
                }
            }
            
            SpriteComponent sa;
            foreach (GameObject obj in GameObjects)
            {
                if ((sa = obj.GetAttribute<SpriteComponent>()) != null)
                {
                    try
                    {
                        Tile t = ChunkDirectory.DeliminateTile(obj.Position.Xy);

                        obj.Z = t.Z;
                    }
                    catch { }

                    Sprite s = sa.GetSprite();

                    float x = obj.X, y = obj.Y;
                    float tx = Chunk.CartesianToIsometric_X(x, y), ty = Chunk.CartesianToIsometric_Y(x, y, obj.Z);
                    renderService.DrawSprite(s, tx, ty);
                }
            }
        }
    }
}

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
        private Camera camera;

        private int tileRange, renderDistance;
        float render_minX, render_maxX, render_minY, render_maxY;

        private SpriteLibrary spriteLibrary;

        public Camera Camera { get => camera; private set => camera = value; }
        public ChunkDirectory ChunkDirectory { get => chunkDirectory; set => chunkDirectory = value; }

        public WorldScene(Game game, Generator worldGenerator, int renderDistance=0)
            : base(game)
        {
            this.ChunkDirectory = new ChunkDirectory(renderDistance, worldGenerator);
            this.Camera = new Camera(this);
            this.renderDistance = renderDistance;

            spriteLibrary = game.GetSystem<SpriteLibrary>();

            ChunkDirectory.ChunkLoaded += ChunkDirectory_ChunkLoaded;
            ChunkDirectory.ChunkUnloaded += ChunkDirectory_ChunkUnloaded;
        }

        protected virtual void ChunkDirectory_ChunkUnloaded(Chunk c)
        {
            for (int i = 0; i < c.ChunkStructures.Count; i++)
                StaticSceneStructures.Remove(c.ChunkStructures[i]);
            for (int i = 0; i < c.ChunkObjects.Count; i++)
                StaticSceneObjects.Remove(c.ChunkObjects[i]);
        }

        protected virtual void ChunkDirectory_ChunkLoaded(Chunk c)
        {
            foreach (SceneStructure ss in c.ChunkStructures)
            {
                StaticSceneStructures.Add(ss);
            }
            foreach (SceneObject so in c.ChunkObjects)
            {
                StaticSceneObjects.Add(so);
                so.Scene = this;
            }
        }

        public override void UpdateFrame(FrameArgument e)
        {
            Camera.Pan_Linear((float)e.DeltaTime);
            
            SceneMatrix = Camera.GetView();
            ChunkDirectory.ChunkCleanup(Camera.Position.Xy);

            tileRange = (int)((2 / Math.Log(camera.Zoom + 1)) * 16);
            renderDistance = (tileRange / Chunk.CHUNK_TILE_WIDTH) + 2;
            chunkDirectory.RenderDistance = renderDistance;

            base.UpdateFrame(e);
        }
        
        public override void RenderFrame(RenderService renderService, FrameArgument e)
        {
            if (chunkDirectory.RenderDistance != renderDistance)
                return; //prevent race condition
            
            int flooredX = (int)camera.TargetPosition.X;
            int flooredY = (int)camera.TargetPosition.Y;
                        
            render_minX = flooredX - tileRange;
            render_minY = flooredY - tileRange;
            render_maxX = flooredX + tileRange;
            render_maxY = flooredY + tileRange;

            float width = chunkDirectory.VisibleWidth;
            float height = chunkDirectory.VisibleHeight;

            float rot = (float)(Math.Sqrt(2) / 2);

            for (float y = render_maxY; y >= render_minY; y--)
            {
                for (float x = render_minX; x < render_maxX; x++)
                {
                    Tile t;
                    
                    try
                    {
                        t = ChunkDirectory.DeliminateTile(new Vector2(x, y));
                    }
                    catch { Console.WriteLine("error"); return; } //write to error log later.
                    
                    float tx = Chunk.CartesianToIsometric_X(x, y), ty = Chunk.CartesianToIsometric_Y(x, y, t.Z);

                    spriteLibrary.GetSprite(t.Data).Use(t.Orientation);
                    renderService.DrawSprite(tx, ty);
                }
            }

            base.RenderFrame(renderService, e);
        }

        protected override void DrawSprite(RenderService renderService, Sprite s, float x, float y, float z = 0)
        {
            if (x < render_minX || x > render_maxX || y < render_minY || y > render_maxY)
                return;

            float cx = Chunk.CartesianToIsometric_X(x, y);
            float cy = Chunk.CartesianToIsometric_Y(x, y, z);
            base.DrawSprite(renderService, s, cx, cy);
        }
    }
}

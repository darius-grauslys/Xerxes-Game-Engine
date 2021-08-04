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
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace.Generators;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.WorldSpace.ChunkSpace;

namespace isometricgame.GameEngine.WorldSpace
{
    public class WorldLayer : Scene_Layer
    {
        private ChunkDirectory chunkDirectory;
        private Camera camera;

        public int renderTileRange, renderDistance, tileRange;

        private SpriteLibrary spriteLibrary;

        public Camera Camera { get => camera; private set => camera = value; }
        public ChunkDirectory ChunkDirectory { get => chunkDirectory; set => chunkDirectory = value; }

        public bool test_flop_REMOVE = true;

        public WorldLayer(Scene sceneLayerParentScene, Generator worldGenerator, int renderDistance=0)
            : base(sceneLayerParentScene)
        {
            this.ChunkDirectory = new ChunkDirectory(renderDistance, worldGenerator);
            this.Camera = new Camera(this);
            this.renderDistance = renderDistance;

            spriteLibrary = Scene_Layer__Game.Get_System__Game<SpriteLibrary>();
        }

        protected override void Handle_Update__Scene_Layer(FrameArgument e)
        {
            Camera.Pan_Linear((float)e.DeltaTime);
            
            Scene_Layer__Layer_Matrix = Camera.GetView();
            ChunkDirectory.ChunkCleanup(Camera.Position.Xy);
            
            renderTileRange = (int)((2 / Math.Log(camera.Zoom + 1)) * 16);
            if (test_flop_REMOVE)
                tileRange = (int)((2 / Math.Log(camera.Zoom * 1.5f + 1)) * 16);
            else
                tileRange = (int)((2 / Math.Log(camera.Zoom + 1)) * 16);
            renderDistance = (renderTileRange / Chunk.CHUNK_TILE_WIDTH) + 2;
            chunkDirectory.RenderDistance = renderDistance;

            base.Handle_Update__Scene_Layer(e);
        }
        
        protected override void Handle_Render__Scene_Layer(RenderService renderService, FrameArgument e)
        {
            if (chunkDirectory.RenderDistance != renderDistance)
                return; //prevent race condition
            
            int flooredX = (int)camera.TargetPosition.X;
            int flooredY = (int)camera.TargetPosition.Y;

            IntegerPosition cpos;
            IntegerPosition spos;

            RenderUnit[] renderUnits;

            float tx, ty;
            uint spriteId;
            
            IntegerPosition rowOffset = new IntegerPosition(1, 0);
            IntegerPosition colOffset = new IntegerPosition(-1, -1);
            int flip = 1, flipadd = 0;
            int test_n = tileRange;
            
            int range = (int)(2.2f * test_n * test_n) + ((test_n - 1) * (test_n - 1));
            int test_n_calc = test_n;

            IntegerPosition basePos = new IntegerPosition(flooredX - test_n / 3, flooredY + (4 * test_n / 3)), yPrimeDescent = basePos;
            int flop = test_n_calc;

            for (int range_prime = 0; range_prime < range; range_prime++)
            {
                yPrimeDescent += colOffset;

                cpos = ChunkDirectory.DeliminateChunkIndex(yPrimeDescent);
                spos = ChunkDirectory.Localize(yPrimeDescent, cpos);


                for (int structureIndex = 0; structureIndex < Chunk.CHUNK_MAX_STRUCTURE_COUNT; structureIndex++)
                {
                    if (ChunkDirectory.Chunks[cpos.X, cpos.Y].ChunkStructures[structureIndex].IsValid)
                    {
                        renderUnits = ChunkDirectory.Chunks[cpos.X, cpos.Y].ChunkStructures[structureIndex].StructuralUnits[spos.X];
                        if (renderUnits[spos.Y].IsInitialized)
                        {
                            tx = Chunk.CartesianToIsometric_X(yPrimeDescent.X, yPrimeDescent.Y);
                            ty = Chunk.CartesianToIsometric_Y(yPrimeDescent.X, yPrimeDescent.Y, renderUnits[spos.Y].Position.Z);

                            spriteId = renderUnits[spos.Y].Id;
                            renderService.DrawSprite(renderUnits[spos.Y].id, tx, ty, renderUnits[spos.Y].vaoIndex);
                        }
                    }
                }
                flop--;

                if (flop <= 0)
                {
                    rowOffset += colOffset * flip;
                    basePos += rowOffset;
                    yPrimeDescent = basePos;
                    flip = flip * -1;
                    flipadd += flip;
                    flop = test_n_calc + flipadd;
                }
            }

            base.Handle_Render__Scene_Layer(renderService, e);
        }

        protected override void Handle_Render_Object__Scene_Layer(RenderService renderService, GameObject gameObject)
        {
            float cx = Chunk.CartesianToIsometric_X(gameObject.renderUnit.X, gameObject.renderUnit.Y);
            float cy = Chunk.CartesianToIsometric_Y(gameObject.renderUnit.X, gameObject.renderUnit.Y, gameObject.renderUnit.Z);

            renderService.DrawSprite(ref gameObject.renderUnit, cx, cy);
        }
    }
}

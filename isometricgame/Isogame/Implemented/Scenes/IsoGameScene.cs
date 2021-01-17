using isometricgame.GameEngine;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Services;
using isometricgame.GameEngine.WorldSpace;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.Isogame.Implemented.Scenes
{
    public class IsoGameScene : GameScene
    {
        UI_Scene ui_scene;
        TextWriter writer;
        
        public IsoGameScene(Game gameRef) 
            : base(gameRef)
        {
            writer = gameRef.GetService<TextWriter>();
            ui_scene = new UI_Scene(gameRef);
        }

        public override void RenderFrame(RenderService renderService, FrameEventArgs e)
        {
            //base.RenderFrame(renderService, e);


            if (!ui_scene.set)
            {
                Chunk c = World.ChunkDirectory.DeliminateChunk(new Vector2(0, 0));
                //ui_scene.Test();
            }

            renderService.RenderScene(ui_scene, e);

            //Console.WriteLine(World.GameObjects[0].Position);

            //writer.DrawText(renderService, "hello world!", "font", 0, 0);

            //SpriteSet font = Game.GetService<SpriteLibrary>().GetSpriteSet<SpriteSet>("font");
            //Sprite[] sprites = font.GetSprites();

            //renderService.DrawSprite(sprites[0], 0, 0, 0, World.FrameView);

            //for (int i = 0; i < sprites.Length; i++)
            //    renderService.DrawSprite(sprites[i], i * 9, 0, World.FrameView);
        }

        private class UI_Scene : Scene
        {
            TextWriter writer;
            Sprite player;
            AssetProvider assetProvider;
            SpriteLibrary sl;

            double delta;

            public UI_Scene(Game game) 
                : base(game)
            {
                writer = game.GetService<TextWriter>();
                sl = game.GetService<SpriteLibrary>();
                player = sl.GetSprite("player");
                assetProvider = game.GetService<AssetProvider>();
            }

            private Sprite test;
            private Sprite chunkSprite;
            public bool set = false;

            public void Test()
            {
                test = assetProvider.CopyTest(player);
                set = true;
            }

            public void SetChunk(Chunk c)
            {
                set = true;
                Sprite[,] tileSprites = new Sprite[Chunk.CHUNK_TILE_WIDTH, Chunk.CHUNK_TILE_HEIGHT];

                for (int x = 0; x < Chunk.CHUNK_TILE_WIDTH; x++)
                {
                    for (int y = 0; y < Chunk.CHUNK_TILE_HEIGHT; y++)
                    {
                        Sprite s = sl.GetSpriteSet<TileSpriteSet>(c.Tiles[x, y].Data).GetSprite(c.Tiles[x, y].Orientation);
                        tileSprites[x, y] = s;
                    }
                }

                int w = tileSprites[0,0].Texture.Width, h = tileSprites[0,0].Texture.Height;

                chunkSprite = assetProvider.FabricateSpriteArea
                    (
                    tileSprites,
                    w,
                    h,
                    Chunk.CHUNK_TILE_WIDTH,
                    Chunk.CHUNK_TILE_HEIGHT,
                    w * Chunk.CHUNK_TILE_WIDTH / 2,
                    h * Chunk.CHUNK_TILE_HEIGHT / 2,
                    (x,y) => 
                    {
                        Tile t = c.Tiles[x, y];
                        return new Vector2
                        (
                            Chunk.CartesianToIsometric_X(x,y),
                            Chunk.CartesianToIsometric_Y(x,y,t.Z)
                            );
                    }
                    );
            }

            public override void RenderFrame(RenderService renderService, FrameEventArgs e)
            {
                base.RenderFrame(renderService, e);

                delta += e.Time;
                
                writer.DrawText(renderService, String.Format("FPS: [ {0} ]", Math.Round(1/e.Time)), "font", 0, 0);

                writer.DrawText(renderService, "The quick brown fox jumped over the lazy dog.", "font", 300 + (float)(100 * Math.Cos(delta)), 300 + (float)( 100 * Math.Sin(delta)));

                //renderService.DrawSprite(test, 100, 200);
                //renderService.DrawSprite(chunkSprite, 100, 100);
            }
        }
    }
}

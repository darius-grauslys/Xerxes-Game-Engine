using isometricgame.GameEngine;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Systems.Rendering;
using isometricgame.GameEngine.Systems.Serialization;
using isometricgame.GameEngine.WorldSpace;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.Isogame.Implemented.Scenes
{
    public class IsoGameScene : WorldScene
    {
        UI_Scene ui_scene;
        TextDisplayer writer;
        
        public IsoGameScene(Game gameRef) 
            : base(gameRef)
        {
            writer = gameRef.GetSystem<TextDisplayer>();
            ui_scene = new UI_Scene(gameRef, this);
        }

        public override void RenderFrame(RenderService renderService, FrameArgument e)
        {
            base.RenderFrame(renderService, e);

            //SceneMatrix = World.SceneMatrix;
            //writer.DrawText(renderService, "hello world!", "font", 0, 0);

            renderService.RenderScene(ui_scene, e);

            //Console.WriteLine(World.GameObjects[0].Position);

            //SpriteSet font = Game.GetService<SpriteLibrary>().GetSpriteSet<SpriteSet>("font");
            //Sprite[] sprites = font.GetSprites();

            //renderService.DrawSprite(sprites[0], 0, 0, 0, World.FrameView);

            //for (int i = 0; i < sprites.Length; i++)
            //    renderService.DrawSprite(sprites[i], i * 9, 0, World.FrameView);
        }

        private class UI_Scene : Scene
        {
            TextDisplayer writer;
            Sprite player;
            AssetProvider assetProvider;
            SpriteLibrary sl;
            WorldScene ws;

            double delta;

            public UI_Scene(Game game, WorldScene ws) 
                : base(game)
            {
                writer = game.GetSystem<TextDisplayer>();
                sl = game.GetSystem<SpriteLibrary>();
                player = sl.GetSprite("player");
                assetProvider = game.GetSystem<AssetProvider>();
                this.ws = ws;
            }

            public override void RenderFrame(RenderService renderService, FrameArgument e)
            {
                base.RenderFrame(renderService, e);

                delta += e.Time;
                
                writer.DrawText(renderService, String.Format("FPS: [ {0} ]\nX: {1}\nY: {2}", Math.Round(1/e.DeltaTime), ws.GameObjects[0].X, ws.GameObjects[0].Y), "font", -590, 430);
            }
        }
    }
}

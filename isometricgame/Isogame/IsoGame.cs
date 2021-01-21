using isometricgame.GameEngine;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Systems.Input;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.WorldSpace.ChunkSpace;
using isometricgame.Isogame.Implemented.GameObjects.PlayerControlled;
using isometricgame.Isogame.Implemented.Scenes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.Isogame
{
    public class IsoGame : Game
    {
        public IsoGame(GameWindow gameWindow) 
            : base(gameWindow)
        {
        }
        
        internal sealed override void RegisterSystems()
        {
            base.RegisterSystems();

            RegisterSystem(new InputService(this, gameWindow));
        }

        internal sealed override void LoadContent()
        {
            base.LoadContent();

            SpriteLibrary.RecordSprite(AssetProvider.ExtractSpriteSheet(@"Assets\GrassTiles.png", "Grass", Tile.TILE_WIDTH, Tile.TILE_HEIGHT));
            SpriteLibrary.RecordSprite(AssetProvider.ExtractSpriteSheet(@"Assets\SandTiles.png", "Sand", Tile.TILE_WIDTH, Tile.TILE_HEIGHT));

            SpriteLibrary.RecordSprite(AssetProvider.ExtractSpriteSheet(@"Assets\player2.png", "player", 88, 88));

            SpriteLibrary.RecordSprite(AssetProvider.ExtractSpriteSheet(@"Assets\gamefont.png", "font", 9, 14));

            TextDisplayer.LoadFont("font", SpriteLibrary.GetSprite("font"));

            WorldScene world = new IsoGameScene(this);
            Player p = new Player(world, new Vector3(0, 0, 0));
            world.GameObjects.Add(p);
            world.ClientCamera.FocusObject = p;
            SetScene(world);
        }
    }
}

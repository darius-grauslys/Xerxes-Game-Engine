using isometricgame.GameEngine;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Services;
using isometricgame.GameEngine.Services.Input;
using isometricgame.GameEngine.WorldSpace;
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
        
        internal sealed override void RegisterServices()
        {
            base.RegisterServices();

            RegisterService(new InputService(this, gameWindow));
        }

        internal sealed override void LoadContent()
        {
            base.LoadContent();

            AssetProvider.LoadTileSet(@"Assets\GrassTiles.png", "grass", SpriteLibrary);
            AssetProvider.LoadTileSet(@"Assets\SandTiles.png", "sand", SpriteLibrary);


            //Sprite[] GrassTiles = AssetProvider;


            //ContentPipe.LoadTileSet(@"Assets\WaterTiles.png", "water", SpriteLibrary);
            SpriteLibrary.RecordSprite("player", new Sprite(AssetProvider.LoadTexture(@"Assets\player.png", true)));

            /*
            Sprite player = SpriteLibrary.GetSprite("player");
            Bitmap bmp = TextureLibrary.GetBitmap(player.Texture);

            BitmapData bmpd = bmp.LockBits(new Rectangle(0,0,bmp.Width,bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Texture2D texture = ContentPipe.BindTexture(bmpd, bmp.Width, bmp.Height);
            SpriteLibrary.RecordSprite("player2", new Sprite(texture));
            */


            //SpriteLibrary.RecordSpriteSet("font", ContentPipe.CreateAndBindSpriteSet(ContentPipe.LoadTextureSheet(@"Assets\gamefont.png", 9, 14, TextWriter.CHARS.Length)), (i) => TextWriter.CHARS[i].ToString());

            FixedSpriteSet font = new FixedSpriteSet(AssetProvider.ExtractSpriteSheet(@"Assets\gamefont.png", "font", 9, 14));
            SpriteLibrary.RecordSpriteSet("font", font, (i) => TextWriter.CHARS[i].ToString());

            TextWriter.LoadFont("font", SpriteLibrary.GetSpriteSet<FixedSpriteSet>("font"));

            GameScene world = new IsoGameScene(this);
            Player p = new Player(world, new Vector3(0, 0, 0));
            world.World.GameObjects.Add(p);
            world.World.SetFocus(p);
            SetScene(world);
        }
    }
}

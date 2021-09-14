using System.Drawing;
using System.IO;
using Xerxes_Engine.Systems.Graphics;

namespace Xerxes_Engine.Systems.Serialization
{
    public sealed class Asset_Pipe : Game_System
    {

        internal Asset_Pipe(Game game) 
            : base(game)
        {
        }

        #region Texture2Ds

        public Texture_R2 LoadTexture(string filePath, bool pixelated = true)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            Bitmap bmp = new Bitmap(filePath);

            Texture_R2 texture = new Texture_R2(bmp, pixelated);
                        
            return texture;
        }

        #endregion

        #region Sprites

        public Sprite ExtractSpriteSheet(
            string filePath, 
            string name = null, 
            int width=-1, 
            int height=-1, 
            float offsetX = 0, 
            float offsetY = 0,
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0)
        {
            Texture_R2 texture = LoadTexture(filePath);

            return new Sprite(
                texture, 
                (width > 0) 
                    ? width 
                    : texture.Width, 
                (height > 0) 
                    ? height 
                    : texture.Height, 
                (name == null) 
                    ? filePath 
                    : name, 
                offsetX / texture.Width, 
                offsetY / texture.Height,
                0,
                r,
                g,
                b,
                a);
        }

        #endregion

        #region legacy
        /*
public void LoadTileSet(string filePath, string assetName, SpriteLibrary library)
{
    TileSpriteAtlas tileset = new TileSpriteAtlas(LoadTileTextureSheet(filePath, Tile.TILE_WIDTH, Tile.TILE_HEIGHT));
    library.RecordSpriteSet(assetName, tileset,
        (i) => (i).ToString()
        );
}
*/

        /*
    public Texture2D[] LoadTileTextureSheet(string filePath, int textureWidth, int textureHeight, bool pixelated = false)
    {
        SpriteSheetReader ssr = new SpriteSheetReader(filePath, textureWidth, textureHeight);
        Texture2D[] textures;

        textures = new Texture2D[16];
        BitmapData bmpd;

        for (int i = 0; i < 8; i++)
        {
            bmpd = ssr.Lock(0, i);
            textures[i] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
            ssr.Unlock(bmpd);
        }
        bmpd = ssr.Lock(0, 2);
        textures[8] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);
        bmpd = ssr.Lock(0, 3);
        textures[9] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);
        bmpd = ssr.Lock(0, 8);
        textures[10] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);
        bmpd = ssr.Lock(0, 9);
        textures[11] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);


        /*for (int i = 10; i < 12; i++)
        {
            bmpd = ssr.Lock(0, i-2);
            textures[i+2] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
            ssr.Unlock();
        }
        bmpd = ssr.Lock(0, 6);
        textures[12] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);
        bmpd = ssr.Lock(0, 7);
        textures[13] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);
        bmpd = ssr.Lock(0, 10);
        textures[14] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
        ssr.Unlock(bmpd);
        textures[15] = textures[0];

        ssr.Dispose();
        return textures;
    }
    */
        /*
        public Texture2D[] LoadTextureSheet(string filePath, int textureWidth, int textureHeight, int count = -1, bool pixelated = false)
        {
            BitmapData bmpd;
            SpriteSheetReader ssr = new SpriteSheetReader(filePath, textureWidth, textureHeight);
            Texture2D[] textures = new Texture2D[(count > 0) ? count : (ssr.SheetWidth * ssr.SheetHeight) / (textureWidth * textureHeight)];
            int rows = ssr.SheetHeight / textureHeight, columns = ssr.SheetWidth / textureWidth;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    int index = column + (row * columns);
                    if (count > -1 && index >= count)
                        return textures;

                    bmpd = ssr.Lock(column, row);
                    textures[index] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
                    ssr.Unlock(bmpd);
                }
            }
            bmpd = ssr.Lock(0, 0);
            textures[0] = BindTexture(bmpd, textureWidth, textureHeight, pixelated);
            ssr.Unlock(bmpd);

            ssr.Dispose();
            return textures;
        }

        public Texture2D[] LoadTextureSheet_1(string filePath, int textureWidth, int textureHeight, int count = -1, bool pixelated = false)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            Texture2D[] textures;

            Bitmap bmp = new Bitmap(filePath);

            textures = new Texture2D[(count > 0) ? count : (bmp.Width * bmp.Height) / (textureWidth * textureHeight)];

            int rows = bmp.Height / textureHeight, columns = bmp.Width / textureWidth;

            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    int index = column * rows + row;
                    if (index >= textures.Length)
                        return textures;

                    BitmapData bmpd = bmp.LockBits(new Rectangle(textureWidth * column, textureHeight * row, textureWidth, textureHeight), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    Texture2D texture = BindTexture(bmpd, textureWidth, textureHeight, pixelated);

                    bmp.UnlockBits(bmpd);

                    textures[index] = texture;
                }
            }

            return textures;
        }
        */

        #endregion

    }
}

using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using isometricgame.GameEngine.WorldSpace;

namespace isometricgame.GameEngine.Services
{
    public class ContentPipe : GameService
    {
        public ContentPipe(Game game) 
            : base(game)
        {
        }

        public Texture2D BindTexture(BitmapData bmpd, int textureWidth, int textureHeight, bool pixelated = false)
        {
            
            int id = GL.GenTexture();
            
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, textureWidth, textureHeight, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmpd.Scan0);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, pixelated ? (int)TextureMinFilter.Nearest : (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, pixelated ? (int)TextureMagFilter.Nearest : (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.ClampToEdge, pixelated ? (int)TextureMagFilter.Nearest : (int)TextureMagFilter.Linear);
                    
            Texture2D texture = new Texture2D(id, new Vector2(textureWidth, textureHeight));
            
            return texture;
        }

        public Texture2D LoadTexture(string filePath, bool pixelated = false)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            Bitmap bmp = new Bitmap(filePath);
            BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Texture2D texture = BindTexture(bmpd, bmp.Width, bmp.Height, pixelated);

            bmp.UnlockBits(bmpd);
                        
            return texture;
        }

        public void LoadTileSet(string filePath, string assetName, SpriteLibrary library)
        {
            TileSpriteSet tileset = new TileSpriteSet(LoadTileTextureSheet(filePath, Tile.TILE_WIDTH, Tile.TILE_HEIGHT));
            library.RecordSpriteSet(assetName, tileset,
                (i) => (i).ToString()
                );
        }

        public Sprite[] ExtractSpriteSheet(string filePath, string assetName, int width, int height)
        {
            Texture2D spriteSheet = LoadTexture(filePath);
            Sprite[] sprites = new Sprite[spriteSheet.Area / (width * height)];
            Vertex[] verticies;
            float step = (float)width / (float)spriteSheet.Width;

            for (int i = 0; i < sprites.Length; i++)
            {
                //maybe wrong
                verticies = new Vertex[]
                {
                    /*
                    new Vertex(new Vector2(0              , Tile.TILE_HEIGHT), new Vector2(step * i,     0)),
                    new Vertex(new Vector2(Tile.TILE_WIDTH, Tile.TILE_HEIGHT), new Vector2(step * (i+1), 0)),
                    new Vertex(new Vector2(Tile.TILE_WIDTH, 0               ), new Vector2(step * (i+1), 1)),
                    new Vertex(new Vector2(0              , Tile.TILE_HEIGHT), new Vector2(step * i,     0)),
                    new Vertex(new Vector2(Tile.TILE_WIDTH, 0               ), new Vector2(step * (i+1), 1)),
                    new Vertex(new Vector2(0              , 0               ), new Vector2(step * i,     1))
                    */
                    
                    new Vertex(new Vector2(0, 0), new Vector2(step * (i), 0)),
                    new Vertex(new Vector2(0, height), new Vector2(step * (i), 1)),
                    new Vertex(new Vector2(width, height), new Vector2(step * (i+1), 1)),
                    new Vertex(new Vector2(width, 0), new Vector2(step * (i+1), 0))
                };
                sprites[i] = new Sprite(spriteSheet, verticies);
            }
            return sprites;
        }
        
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
            }*/
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

        //public Bitmap[] SpliceBitmap(string filePath, int width, int height)
        //{
        //    if (!File.Exists(filePath))
        //        throw new FileNotFoundException();

        //    Bitmap[] bmps;

        //    Bitmap bmp = new Bitmap(filePath);

        //    bmps = new Bitmap[(bmp.Width * bmp.Height) / (width * height)];

        //    for (int i = 0; i < bmp.Width / width; i++)
        //    {
        //        for (int j = 0; j < bmp.Height / height; j++)
        //        {
        //            BitmapData bmpd = bmp.LockBits(new Rectangle(width * i, height * j, width, height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

        //            Bitmap section = new Bitmap(width, height, 0, System.Drawing.Imaging.PixelFormat.Alpha, bmpd.Scan0);

        //            bmp.UnlockBits(bmpd);

        //            bmps[i + j] = section;
        //        }
        //    }

        //    return bmps;
        //}
    }
}

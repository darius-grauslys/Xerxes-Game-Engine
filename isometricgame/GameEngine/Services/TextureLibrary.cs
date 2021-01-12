using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace isometricgame.GameEngine.Services
{
    public class TextureLibrary : GameService
    {
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public TextureLibrary(Game game) 
            : base(game)
        {
        }

        public Texture2D GetTexture(string textureName)
        {
            return textures[textureName];
        }

        public void RecordTexture(string textureName, Texture2D texture)
        {
            textures.Add(textureName, texture);
        }

        public Bitmap GetBitmap(Texture2D texture)
        {
            Bitmap bmp = new Bitmap(texture.Width, texture.Height);
            BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindBuffer(BufferTarget.TextureBuffer, texture.ID);
            GL.ReadBuffer(ReadBufferMode.FrontLeft);

            GL.ReadPixels(0, 0, texture.Width, texture.Height, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.Bitmap, bmpd.Scan0);
            bmp.UnlockBits(bmpd);

            return bmp;
        }
    }
}

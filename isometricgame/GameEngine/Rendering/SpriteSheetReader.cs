using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering
{
    public class SpriteSheetReader : IDisposable
    {
        private Bitmap bmp;
        private BitmapData block;
        private int spriteWidth, spriteHeight;
        private bool isLock = false;

        public int SheetWidth => bmp.Width;
        public int SheetHeight => bmp.Height;

        public SpriteSheetReader(string filePath, int spriteWidth, int spriteHeight)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException();

            bmp = new Bitmap(filePath);
            this.spriteHeight = spriteHeight;
            this.spriteWidth = spriteWidth;
        }

        public BitmapData Lock(int x, int y)
        {
            if (isLock)
                throw new InvalidOperationException();
            isLock = true;
            return block = bmp.LockBits(new Rectangle(spriteWidth * x, spriteHeight * y, spriteWidth, spriteHeight), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }

        public void Unlock(BitmapData bmpd)
        {
            if (!isLock)
                return;
            bmp.UnlockBits(bmpd);
            isLock = false;
        }

        public void Dispose()
        {
            bmp.Dispose();
        }
    }
}

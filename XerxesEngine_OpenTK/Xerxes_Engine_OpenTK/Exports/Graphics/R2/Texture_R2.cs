using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Xerxes.Xerxes_OpenTK
{
    public struct Texture_R2
    {
        public const int CHANNEL_COUNT = 4;

        public int ID { get; }
        public Integer_Vector_2 SIZE { get; } 
        public int Width => SIZE.X;
        public int Height => SIZE.Y;

        public int Area => Width * Height;

        private Texture_R2(int width, int height)
        {
            ID = GL.GenTexture();

            SIZE = new Integer_Vector_2(width, height);
        }

        public Texture_R2(Bitmap bitmap, bool pixelated = true)
        : this(bitmap.Width, bitmap.Height)
        {
            BitmapData bmpd = 
                bitmap
                .LockBits
                (
                    new Rectangle
                    (
                        0, 
                        0, 
                        bitmap.Width, 
                        bitmap.Height
                    ), 
                    ImageLockMode.ReadOnly, 
                    System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );


            GL.BindTexture(TextureTarget.Texture2D, ID);

            GL.TexImage2D
            (
                TextureTarget.Texture2D, 
                0,
                PixelInternalFormat.Rgba, 
                bitmap.Width, 
                bitmap.Height, 
                0, 
                global::OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, 
                bmpd.Scan0
            );

            Private_Parameterize__Texture_R2(pixelated);

            bitmap.UnlockBits(bmpd);
        }

        public Texture_R2
        (
            byte[] byte_array,
            int width,
            int height,
            bool pixelated = true
        )
        : this(width, height)
        {
            GL.BindTexture(TextureTarget.Texture2D, ID);

            GL.TexImage2D
            (
                TextureTarget.Texture2D, 
                0,
                PixelInternalFormat.Rgba, 
                width,
                height,
                0, 
                global::OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, 
                byte_array
            );

            Private_Parameterize__Texture_R2(pixelated);
        }

        public Texture_R2
        (
            byte[,,] channel_array,
            int width,
            int height,
            bool pixelated = true
        )
        : this(width, height)
        {
            GL.BindTexture(TextureTarget.Texture2D, ID);

            GL.TexImage2D
            (
                TextureTarget.Texture2D, 
                0,
                PixelInternalFormat.Rgba, 
                width,
                height,
                0, 
                global::OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, 
                channel_array
            );

            Private_Parameterize__Texture_R2(pixelated);
        }

        private void Private_Parameterize__Texture_R2
        (
            bool pixelated
        )
        {
            GL.TexParameter
            (
                TextureTarget.Texture2D, 
                TextureParameterName.TextureMinFilter, 
                pixelated 
                    ? (int)TextureMinFilter.Nearest 
                    : (int)TextureMinFilter.Linear
            );
            GL.TexParameter
            (
                TextureTarget.Texture2D, 
                TextureParameterName.TextureMagFilter, 
                pixelated 
                    ? (int)TextureMagFilter.Nearest 
                    : (int)TextureMagFilter.Linear
            );

            GL.TexParameter
            (
                TextureTarget.Texture2D, 
                TextureParameterName.ClampToEdge, 
                pixelated 
                    ? (int)TextureMagFilter.Nearest 
                    : (int)TextureMagFilter.Linear
            );
        }
    }
}


using System.Linq;

namespace Xerxes_Engine.Export_OpenTK
{
    public class SA__Create_Texture_R2 :
        Streamline_Argument
    {
        internal byte[] Create_Texture_R2__BYTE_ARRAY { get; }
        internal byte[,,] Create_Texture_R2__CHANNEL_ARRAY { get; }

        internal bool Create_Texture_R2__IS_1D_ARRAY
            => Create_Texture_R2__BYTE_ARRAY != null;

        public int Create_Texture_R2__WIDTH { get; }
        public int Create_Texture_R2__HEIGHT { get; }

        public bool Create_Texture_R2__IS_PIXELATED { get; }

        public Texture_R2 Create_Texture_R2__Texture_R2__Returned { get; internal set; }

        public SA__Create_Texture_R2(byte[] bitmap_data, int width, int height, bool pixelated = true)
        {
            Create_Texture_R2__BYTE_ARRAY =
                bitmap_data.ToArray();

            Create_Texture_R2__WIDTH = width;
            Create_Texture_R2__HEIGHT = height;

            Create_Texture_R2__IS_PIXELATED = pixelated;
        }

        public SA__Create_Texture_R2(byte[,,] bitmap_channel_data, int width, int height, bool pixelated = true)
        {
            byte[,,] copy = new byte[width,height,Texture_R2.CHANNEL_COUNT];

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    for(int channel = 0; channel < Texture_R2.CHANNEL_COUNT; channel++)
                    {
                        copy[x,y,channel] = bitmap_channel_data[x,y,channel];
                    }
                }
            }

            Create_Texture_R2__CHANNEL_ARRAY = copy;

            Create_Texture_R2__WIDTH = width;
            Create_Texture_R2__HEIGHT = height;

            Create_Texture_R2__IS_PIXELATED = pixelated;
        }
    }
}

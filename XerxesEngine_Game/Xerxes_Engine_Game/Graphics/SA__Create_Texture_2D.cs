
using System.Linq;

namespace Xerxes.Game_Engine.Graphics
{
    public class SA__Create_Texture_2D :
        Streamline_Argument
    {
        internal byte[] _create_texture_2d__byte_array;
        internal byte[,,] _create_texture_2d__channel_array;

        public bool Create_Texture_2D__Is_Byte_Array_Or_Channel_Array { get; private set; }

        public SA__Create_Texture_2D()
        {

        }

        public void Set__Bytes__Create_Texture_2D
        (byte[] byte_array)
        {
            _create_texture_2d__byte_array =
                byte_array.ToArray();

            Create_Texture_2D__Is_Byte_Array_Or_Channel_Array =
                true;
        }

        public void Set__Channels__Create_Texture_2D
        (byte[,,] channel_array, int width, int height, int channel_count)
        {
            _create_texture_2d__channel_array =
                new byte[width, height, channel_count];

            for(int y=0;y<height;y++)
                for(int x=0;x<width;x++)
                    for (int c=0;c<channel_count;c++)
                        _create_texture_2d__channel_array[x,y,c] = 
                            channel_array[x,y,c];

            Create_Texture_2D__Is_Byte_Array_Or_Channel_Array =
                false;
        }
    }
}

using System.Collections.Generic;
using OpenTK;

namespace Xerxes.Xerxes_OpenTK.Tools 
{
    public static class String_Batcher
    {
        public const int CHAR_PIXEL_WIDTH = 18;
        public const int CHAR_PIXEL_HEIGHT = 28;

        public const float CHAR_WIDTH = ((float)CHAR_PIXEL_WIDTH/CHAR_PIXEL_HEIGHT);
        public const float CHAR_HEIGHT = 1;

        private const string ALPHABET =
            ",gjpqyABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhiklmnorstuvwxz1234567890.?!/-+@#$%^&+()_=[]\\[]|:;\"'<>`~";
  
        public static Vertex_Object Batch
        (
            string s, 
            Texture_R2 font,
            float char_width  = CHAR_WIDTH,
            float char_height = CHAR_HEIGHT
        )
        {
            List<Integer_Vector_2> uvs = new List<Integer_Vector_2>();
            List<Vector2> positions = new List<Vector2>();
  
            int x=0, y=0;
            foreach(char c in s)
            {
                if (c == '\n')
                {
                    x=0; y++;
                    continue;
                }
                uvs.Add(new Integer_Vector_2(ALPHABET.IndexOf(c),0));
                positions.Add(new Vector2(x,y));
                x++;
            }

            Vertex_Object text =
                Vertex_Object
                .Create
                (
                    font,
                    char_width,
                    char_height,
                    CHAR_PIXEL_WIDTH,
                    CHAR_PIXEL_HEIGHT,
                    uvs.ToArray(),
                    positions.ToArray()
                );
  
            return text;
        }
    }
}

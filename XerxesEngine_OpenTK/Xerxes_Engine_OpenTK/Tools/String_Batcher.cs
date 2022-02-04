using OpenTK;

namespace Xerxes.Xerxes_OpenTK.Tools 
{
    public static class String_Batcher
    {
        public const int CHAR_PIXEL_WIDTH = 18;
        public const int CHAR_PIXEL_HEIGHT = 28;
        private const string ALPHABET =
            ",gjpqyABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhiklmnorstuvwxz1234567890.?!/-+@#$%^&+()_=[]\\[]|:;\"'<>`~";
  
        public static Vertex_Object Batch
        (
            string s, 
            Texture_R2 font,
            float char_width=((float)CHAR_PIXEL_WIDTH)/CHAR_PIXEL_HEIGHT,
            float char_height=1
        )
        {
            Batch_Index[] batch_indices = new Batch_Index[s.Length];
  
            int x=0, y=0;
            int i=0;
            foreach(char c in s)
            {
                if (c == '\n')
                {
                    x=0; y++;
                    continue;
                }
                Integer_Vector_2 index =
                    new Integer_Vector_2
                    (
                        ALPHABET.IndexOf(c), 0
                    );
                Vector2 offset =
                    new Vector2(char_width * x, char_height * y);
                batch_indices[i++] = new Batch_Index(index, offset);
                x++;
            }

            Vertex_Object text =
                Vertex_Object
                .Create
                (
                    font,
                    CHAR_PIXEL_WIDTH,
                    CHAR_PIXEL_HEIGHT,
                    char_width,
                    char_height,
                    batch_indices
                );
  
            return text;
        }
    }
}

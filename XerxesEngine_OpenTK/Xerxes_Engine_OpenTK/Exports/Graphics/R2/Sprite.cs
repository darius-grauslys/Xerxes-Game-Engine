using OpenTK;
using Xerxes.Tools;

namespace Xerxes.Xerxes_OpenTK
{
    public struct Sprite
    {
        public struct Sprite_Index
        {
            public int SPRITE_INDEX { get; }
            
            public Sprite_Index(int index)
            {
                SPRITE_INDEX = index;
            }

            public static implicit operator int(Sprite_Index sprite_index)
                => sprite_index.SPRITE_INDEX;
            public static implicit operator Sprite_Index(int sprite_index)
                => new Sprite_Index(sprite_index);
        }

        public Sprite_Index Active_Index { get; set; }
        public Vertex_Object Active_Vertex_Object 
            => _VERTEX_OBJECTS[Active_Index];
        public Vector3 Size                { get; set; }

        private Vertex_Object[] _VERTEX_OBJECTS { get; }

        public Sprite(Vertex_Object[] vertex_Objects)
        {
            _VERTEX_OBJECTS = vertex_Objects;
            Active_Index = 0;
            Size = new Vector3();
        }

        //TODO: wrap index
        public bool Set__Active_Vertex_Object__Sprite
        (int index)
        {
            if 
            (
                Math_Helper
                .Check_If__Obeys_Range_Clamp__Positive_Integer
                (
                    index, 
                    _VERTEX_OBJECTS.Length
                )
            )
            {
                return false;
            }

            Active_Index = index;

            return true;
        }
    }
}

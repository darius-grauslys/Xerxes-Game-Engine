using OpenTK;
using Xerxes_Engine.Tools;

namespace Xerxes_Engine.Export_OpenTK
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

        public Sprite_Index Sprite__Active_Index { get; set; }
        public Vertex_Object Sprite__Active_Object 
            => _Sprite__VERTEX_OBJECTS[Sprite__Active_Index];
        public Vector3 Sprite__Size                { get; set; }

        private Vertex_Object[] _Sprite__VERTEX_OBJECTS { get; }

        internal Sprite(Vertex_Object[] vertex_Objects)
        {
            _Sprite__VERTEX_OBJECTS = vertex_Objects;
            Sprite__Active_Index = 0;
            Sprite__Size = new Vector3();
        }

        //TODO: wrap index
        internal bool Set__Active_Vertex_Object__Sprite
        (int index)
        {
            if 
            (
                Math_Helper
                .Check_If__Obeys_Range_Clamp__Positive_Integer
                (
                    index, 
                    _Sprite__VERTEX_OBJECTS.Length
                )
            )
            {
                return false;
            }

            Sprite__Active_Index = index;

            return true;
        }
    }
}

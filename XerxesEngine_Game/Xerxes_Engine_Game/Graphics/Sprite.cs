
namespace Xerxes.Game_Engine.Graphics
{
    public struct Sprite
    {
        public struct Sprite_Index
        {
            public readonly int Sprite_Index__INDEX;

            public Sprite_Index(uint index)
            {
                Sprite_Index__INDEX = (int)index;
            }

            public static implicit operator Sprite_Index(int index)
                => new Sprite_Index((uint)index);
        }

        private readonly IVertex_Object[] _sprite__VERTEX_OBJECTS;

        private int _sprite__active_index;

        public Sprite_Index Sprite__Active_Index
        {
            get => _sprite__active_index;
            set => Set__Active_Vertex_Object__Sprite(value);
        }
        public IVertex_Object Sprite__Active_Vertex_Object
            => _sprite__VERTEX_OBJECTS[_sprite__active_index];

        public Sprite(IVertex_Object[] vertex_objects)
        {
            _sprite__VERTEX_OBJECTS = new IVertex_Object[vertex_objects.Length];
            for(int i=0;i<_sprite__VERTEX_OBJECTS.Length;i++)
                _sprite__VERTEX_OBJECTS[i] = vertex_objects[i];

            _sprite__active_index = 0;
        }

        public void Set__Active_Vertex_Object__Sprite(Sprite_Index index)
        {
            _sprite__active_index = index.Sprite_Index__INDEX;
        }
    }
}

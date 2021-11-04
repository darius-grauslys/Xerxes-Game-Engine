using OpenTK;

namespace Xerxes_Engine.Export_OpenTK
{
    public class Sprite
    {
        internal Vertex_Object_Handle Sprite__Active_Object__Internal { get; set; }
        internal Vector3 Sprite__Size__Internal                       { get; set; }

        private Vertex_Object_Handle[] _Sprite__VERTEX_OBJECTS        { get; }

        internal Sprite(Vertex_Object_Handle[] vertex_Objects)
        {
            _Sprite__VERTEX_OBJECTS = vertex_Objects;
            Sprite__Active_Object__Internal = vertex_Objects[0];
        }

        internal bool Internal_Set__Active_Vertex_Object__Sprite
        (int index)
        {
            if 
            (
                Tools
                .Math_Helper
                .Check_If__Obeys_Range_Clamp__Positive_Integer
                (
                    index, 
                    _Sprite__VERTEX_OBJECTS.Length
                )
            )
            {
                return false;
            }

            Sprite__Active_Object__Internal =
                _Sprite__VERTEX_OBJECTS[index];

            return true;
        }
    }
}

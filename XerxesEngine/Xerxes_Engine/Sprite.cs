using System;
using OpenTK;

namespace Xerxes_Engine
{
    public class Sprite
    {
        internal Vertex_Object  Sprite__Active_Object__Internal { get; set; }
        private Vertex_Object[] _Sprite__VERTEX_OBJECTS         { get; }

        internal Sprite(Vertex_Object[] vertex_Objects)
        {
            _Sprite__VERTEX_OBJECTS = vertex_Objects;
            Sprite__Active_Object__Internal = vertex_Objects[0];
        }

        internal void Internal_Scale__Sprite(float scale)
        {
            Private_Modify__Vertex_Objects__Sprite
            (
                (v, i) => Private_Set__Scales_Of_Vertex__Sprite(v,i,scale,scale)
            );
        }

        internal void Internal_Resize__Sprite(float width, float height)
        {
            Private_Modify__Vertex_Objects__Sprite
            (
                (v, i) => Private_Set__Sizes_Of_Vertex__Sprite(v, i, width, height)
            );
        }

        private void Private_Modify__Vertex_Objects__Sprite
        (
            Func<Vertex[], int, Vertex> modificationMethod
        )
        {
            foreach(Vertex_Object vertex_Object in _Sprite__VERTEX_OBJECTS)
            {
                vertex_Object
                    .Internal_Modify__Vertex_Array__Vertex_Object
                    (
                        0,
                        -1,
                        modificationMethod 
                    );
            }
        }

        private Vertex Private_Set__Sizes_Of_Vertex__Sprite
        (
            Vertex[] verts, 
            int index, 
            float width, 
            float height 
        )
        {
            Vertex baseline = verts[index];
            Vector2 position =
                Tools
                .Math_Helper
                .Get__Stepped(baseline.Position);
            position = 
                new Vector2
                (
                    position.X * width,
                    position.Y * height
                );
            Vertex vert =
                new Vertex
                (
                    baseline,
                    position
                );

            return vert;
        }

        private Vertex Private_Set__Scales_Of_Vertex__Sprite
        (
            Vertex[] verts, 
            int index, 
            float scaleX, 
            float scaleY
        )
        {
            Vertex baseline = verts[index];
            Vertex vert =
                new Vertex
                (
                    baseline,
                    new Vector2
                    (
                        baseline.Position.X * scaleX,
                        baseline.Position.Y * scaleY
                    )
                );
            return vert;
        }

        internal bool Internal_Set__Active_Vertex_Object__Sprite(int index)
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

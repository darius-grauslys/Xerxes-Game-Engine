
using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using Xerxes.Tools;

namespace Xerxes.Xerxes_OpenTK.Exports.Graphics.R2.Canvas
{
    public struct Draw_Order
    {
        public Shape Draw_Order__SHAPE { get; }
        public Vector4 Draw_Order__COLOR { get; }
        public Draw_Mode Draw_Order__MODE { get; }

        public Integer_Vector_2 Draw_Order__ORIGIN { get; }
        public Vector2 Draw_Order__SCALE { get; }

        private List<Integer_Vector_2> _Draw_Order__POINTS { get; }
        public IEnumerable<Integer_Vector_2> Get__Points__Draw_Order()
        {
            foreach(Integer_Vector_2 point in _Draw_Order__POINTS)
                yield return point;
        }

        public Draw_Order
        (
            Shape shape = Shape.Point,
            Vector4? color = null,
            Draw_Mode mode = Draw_Mode.Fill,
            Integer_Vector_2? origin = null,
            Vector2? scale = null,
            IEnumerable<Integer_Vector_2> points = null
        )
        {
            Draw_Order__SHAPE = shape;
            Draw_Order__COLOR = color ?? Math_Helper.Convert__Color_To_Vec4(Color.Black);
            Draw_Order__MODE = mode;

            Draw_Order__ORIGIN =
                origin
                ??
                new Integer_Vector_2();
            Draw_Order__SCALE =
                scale
                ??
                new Vector2(1,1);

            _Draw_Order__POINTS =
                new List<Integer_Vector_2>
                (
                    points
                    ??
                    new Integer_Vector_2[] {new Integer_Vector_2(0,0)}
                );
        }

        public Integer_Vector_2 this[int i]
            => _Draw_Order__POINTS[i];
    }
}

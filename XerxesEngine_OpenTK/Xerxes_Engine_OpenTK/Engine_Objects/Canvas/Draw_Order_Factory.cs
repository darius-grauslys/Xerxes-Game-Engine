
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using Xerxes.Tools;

namespace Xerxes.Xerxes_OpenTK.Exports.Graphics.R2.Canvas
{
    public class Draw_Order_Factory
    {
        internal Queue<Draw_Order> Draw_Order_Factory__DRAW_ORDERS__Internal { get; }
        
        public Vector4 Draw_Order_Factory__Color { get; private set; }
        public Draw_Mode Draw_Order_Factory__Mode { get; private set; }

        public Integer_Vector_2 Draw_Order_Factory__Origin { get; private set; }
        public Vector2 Draw_Order_Factory__Scale { get; private set; }

        internal Draw_Order_Factory()
        {
            Draw_Order_Factory__Color = Math_Helper.Convert__Color_To_Vec4(Color.White);
            Draw_Order_Factory__DRAW_ORDERS__Internal = new Queue<Draw_Order>();
            Draw_Order_Factory__Scale = new Vector2(1,1);
        }

#region Colors
        public IEnumerable<Draw_Order> Finish()
            => new Queue<Draw_Order>(Draw_Order_Factory__DRAW_ORDERS__Internal);

        public Draw_Order_Factory Clear()
        {
            Draw_Order_Factory__DRAW_ORDERS__Internal.Clear();
            return this;
        }

        public Draw_Order_Factory Set__Color(Vector4 color)
        {
            Draw_Order_Factory__Color = color;
            return this;
        }

        public Draw_Order_Factory Set__Color(float r = 0, float g = 0, float b = 0, float alpha = 1)
        {
            Vector4 color_v4 = new Vector4(r,g,b,alpha);
            return Set__Color(color_v4);
        }

        public Draw_Order_Factory Set__Color(Color color, float alpha = 1)
        {
            Vector4 color_v4 = Math_Helper.Convert__Color_To_Vec4(color);
            return Set__Color(color_v4);
        }

        public Draw_Order_Factory Set__Draw_Mode(Draw_Mode mode)
        {
            Draw_Order_Factory__Mode = mode;
            return this;
        }

        public Draw_Order_Factory Set__Origin(Integer_Vector_2 origin)
        {
            Draw_Order_Factory__Origin = origin;
            return this;
        }

        public Draw_Order_Factory Set__Scale(Vector2 scale)
        {
            Draw_Order_Factory__Scale = scale;
            return this;
        }
#endregion

#region Draws

        private Draw_Order_Factory Draw
        (
            Shape shape,
            Integer_Vector_2[] points
        )
        {
            Draw_Order_Factory__DRAW_ORDERS__Internal
                .Enqueue
                (
                    new Draw_Order
                    (
                        shape, 
                        Draw_Order_Factory__Color, 
                        Draw_Order_Factory__Mode, 
                        Draw_Order_Factory__Origin,
                        Draw_Order_Factory__Scale,
                        points
                    )
                );
            return this;
        }

        public Draw_Order_Factory Draw__Line
        (
            Integer_Vector_2 point_a,
            Integer_Vector_2 point_b
        )
        {
            Draw(Shape.Line, new Integer_Vector_2[] { point_a, point_b });
            return this;
        }

        public Draw_Order_Factory Draw__Triangle
        (
            Integer_Vector_2 point_a,
            Integer_Vector_2 point_b,
            Integer_Vector_2 point_c
        )
        {
            Draw(Shape.Triangle, new Integer_Vector_2[] { point_a, point_b, point_c });
            return this;
        }

        public Draw_Order_Factory Draw__Circle
        (
            Integer_Vector_2 center,
            Integer_Vector_2 point_width,
            Integer_Vector_2 point_height
        )
        {
            Draw(Shape.Circle, new Integer_Vector_2[] { center, point_width, point_height });
            return this;
        }

        public Draw_Order_Factory Draw__Rectangle
        (
            int min_x,
            int min_y,
            int max_x,
            int max_y
        )
        {
            return Draw__Rectangle(new Integer_Vector_2(min_x, min_y), new Integer_Vector_2(max_x, max_y));
        }

        public Draw_Order_Factory Draw__Rectangle
        (
            Integer_Vector_2 min,
            Integer_Vector_2 max
        )
        {
            Draw(Shape.Rectangle, new Integer_Vector_2[] { min, max });
            return this;
        }
#endregion
    }
}

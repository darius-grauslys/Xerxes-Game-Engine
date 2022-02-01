
using OpenTK;

namespace Xerxes_Engine.Export_OpenTK.Exports.Graphics.R2.Canvas
{
    public class Canvas_Context
    {
        internal byte[,,] Canvas_Context__CHANNEL_ARRAY { get; }
        public int Canvas_Context__WIDTH { get; }
        public int Canvas_Context__HEIGHT { get; }

        public Integer_Vector_2 Canvas_Context__Origin { get; internal set; }
        public Vector2 Canvas_Context__Scale { get; internal set; }

        internal Canvas_Context(byte[,,] channel_array, int width, int height)
        {
            Canvas_Context__CHANNEL_ARRAY = channel_array;
            Canvas_Context__WIDTH = width;
            Canvas_Context__HEIGHT = height;

            Canvas_Context__Scale = new Vector2(1,1);
        }

        internal void Update__Canvas_Context(Draw_Order draw_order)
        {
            Canvas_Context__Origin = draw_order.Draw_Order__ORIGIN;
            Canvas_Context__Scale = draw_order.Draw_Order__SCALE;
        }

        public void Apply__Context_To_Point__Canvas_Context
        (
            ref int x,
            ref int y
        )
        {
            Integer_Vector_2 point = new Integer_Vector_2(x,y);
            point = Apply__Context_To_Point__Canvas_Context(point);

            x = point.X;
            y = point.Y;
        }

        public Integer_Vector_2 Apply__Context_To_Point__Canvas_Context
        (
            Integer_Vector_2 point
        )
        {
            Integer_Vector_2 offset_point_by_origin =
                point - Canvas_Context__Origin;

            float scale_x = 
                Canvas_Context__Scale.X;
            float scale_y =
                Canvas_Context__Scale.Y;

            int point_x = offset_point_by_origin.X;
            int point_y = offset_point_by_origin.Y;

            if (scale_x < 0)
            {
                // Scale in respect to Canvas.
                scale_x *= -1;
                point_x = (int)(point_x * (scale_x * Canvas_Context__WIDTH));
            }
            else
            {
                point_x = (int)(point_x * scale_x);
            }

            if (scale_y < 0)
            {
                scale_y *= -1;
                point_y = (int)(point_x * (scale_x * Canvas_Context__HEIGHT));
            }
            else
            {
                point_y = (int)(point_y * scale_y);
            }

            return new Integer_Vector_2(point_x, point_y);
        }
    }
}

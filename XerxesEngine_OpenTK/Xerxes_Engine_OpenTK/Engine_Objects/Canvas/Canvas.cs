
using OpenTK;
using Xerxes_Engine.Tools;

namespace Xerxes_Engine.Export_OpenTK.Exports.Graphics.R2.Canvas
{
    public class Canvas
    {
        private Draw_Order_Factory _Canvas__DRAW_ORDER_FACTORY { get; }

        public Canvas()
        {
            _Canvas__DRAW_ORDER_FACTORY =
                new Draw_Order_Factory();
        }

        public Draw_Order_Factory Draw(bool new_drawing = true)
        {
            if (new_drawing)
                _Canvas__DRAW_ORDER_FACTORY.Clear();
            return _Canvas__DRAW_ORDER_FACTORY;
        }

        public SA__Create_Texture_R2 Create__Texture__Canvas(int width, int height)
        {
            byte[,,] channel_array =
                new byte[width, height, Texture_R2.CHANNEL_COUNT];

            Canvas_Context context = 
                new Canvas_Context(channel_array, width, height);

            foreach(Draw_Order draw_order in _Canvas__DRAW_ORDER_FACTORY.Draw_Order_Factory__DRAW_ORDERS__Internal)
            {
                Log.Write__Info__Log(draw_order.Draw_Order__SHAPE.ToString(), this);
                context.Update__Canvas_Context(draw_order);
                switch(draw_order.Draw_Order__SHAPE)
                {
                    default:
                    case Shape.Point:
                        Handle__Composite_Point__Canvas
                        (
                            context, 
                            draw_order.Draw_Order__COLOR,
                            draw_order.Draw_Order__MODE,
                            draw_order[0]
                        );
                        break;
                    case Shape.Line:
                        Handle__Composite_Line__Canvas
                        (
                            context,
                            draw_order.Draw_Order__COLOR,
                            draw_order.Draw_Order__MODE,
                            draw_order[0],
                            draw_order[1]
                        );
                        break;
                    case Shape.Rectangle:
                        Handle__Composite_Rectangle__Canvas
                        (
                            context,
                            draw_order.Draw_Order__COLOR,
                            draw_order.Draw_Order__MODE,
                            draw_order[0],
                            draw_order[1]
                        );
                        break;
                }
            }

            SA__Create_Texture_R2 e_create =
                new SA__Create_Texture_R2 
                (
                    channel_array, 
                    width,
                    height
                );

            return e_create;
        }

        protected virtual void Handle__Composite_Point__Canvas
        (
            Canvas_Context context,
            Vector4 color,
            Draw_Mode draw_mode,
            Integer_Vector_2 point
        )
        {
            Draw(context, point, color, this);
        }

        protected virtual void Handle__Composite_Line__Canvas
        (
            Canvas_Context context,
            Vector4 color,
            Draw_Mode draw_mode,
            Integer_Vector_2 point_a,
            Integer_Vector_2 point_b
        )
        {
            bool invalid_a =
                Assert__Invalid_Position(context, point_a, this);
            bool invalid_b =
                Assert__Invalid_Position(context, point_b, this);

            // The entire line is out of bounds.
            if (invalid_a && invalid_b)
                return;

            int dx = point_b.X - point_a.X;
            int dy = point_b.Y - point_a.Y;

            int x_smaller;
            int x_larger;

            Math_Helper.Compare__Magnitude(point_a.X, point_b.X, out x_smaller, out x_larger);

            int y_smaller;
            int y_larger;
            
            Math_Helper.Compare__Magnitude(point_a.Y, point_b.Y, out y_smaller, out y_larger);

            float x_over_y = (float)dx / (float)dy;
            float y_over_x = (float)dy / (float)dx;

            float y = y_smaller;
            float x = x_smaller;

            bool initial_succes =
                !Assert__Invalid_Position(context, (int)x, (int)y, this);

            while(y < y_larger && x < x_larger)
            {
                y += y_over_x;
                x += x_over_y;

                bool success = Draw(context, (int)x, (int)y, color, this);

                // We might be starting outside of the canvas and moving in. Keep going.
                if (!initial_succes  && success)
                    initial_succes = true;
                
                // We were in the canvas, and now moving out - stop.
                if (initial_succes && !success)
                    break;
            }
        }

        protected void Handle__Composite_Rectangle__Canvas
        (
            Canvas_Context context,
            Vector4 color,
            Draw_Mode draw_mode,
            Integer_Vector_2 min,
            Integer_Vector_2 max
        )
        {
            bool invalid_min =
                Assert__Invalid_Position(context, min, this);
            bool invalid_max =
                Assert__Invalid_Position(context, max, this);

            if (invalid_min && invalid_max)
                return;

            for(int y = min.Y; y < max.Y; y++)
            {
                for(int x = min.X; x < max.X; x++)
                {
                    Draw(context, x, y, color, this);
                }
            }
        }

        protected static bool Assert__Invalid_Position
        (
            Canvas_Context context, 
            Integer_Vector_2 point,
            Canvas instance
        )
            => Assert__Invalid_Position(context, point.X, point.Y, instance);

        protected static bool Assert__Invalid_Position
        (
            Canvas_Context context, 
            int x, 
            int y,
            Canvas instance
        )
        {
            bool bounded_x =
                x >= 0 && x <= context.Canvas_Context__WIDTH;
            bool bounded_y =
                y >= 0 && y <= context.Canvas_Context__HEIGHT;

            bool bounded =
                bounded_x
                &&
                bounded_y;

            if (bounded)
                return false;

            Private_Log_Error__Invalid_Position(context, x, y, instance);

            return true;
        }

        protected static bool Draw
        (
            Canvas_Context context, 
            Integer_Vector_2 point, 
            Vector4 color,
            Canvas instance
        )
            => Draw(context, point.X, point.Y, color, instance);

        protected static bool Draw
        (
            Canvas_Context context, 
            int x, 
            int y, 
            Vector4 color,
            Canvas instance
        )
        {
            if (Assert__Invalid_Position(context, x, y, instance))
                return false;

            context.Apply__Context_To_Point__Canvas_Context(ref x, ref y);

            //TODO: handle draw modes.
            context.Canvas_Context__CHANNEL_ARRAY[x,y,0] = (byte)(color.X * 255);
            context.Canvas_Context__CHANNEL_ARRAY[x,y,1] = (byte)(color.Y * 255);
            context.Canvas_Context__CHANNEL_ARRAY[x,y,2] = (byte)(color.Z * 255);
            context.Canvas_Context__CHANNEL_ARRAY[x,y,3] = (byte)(color.W * 255);

            return true;
        }

        private static void Private_Log_Error__Invalid_Position
        (
            Canvas_Context context, 
            int x, int y,
            Canvas instance
        )
        {
            Log.Write__Error__Log
            (
                $"Invalid point: ({x},{y}) on canvas: {context.Canvas_Context__WIDTH}x{context.Canvas_Context__HEIGHT}!",
                instance,
                Log_Message_Type.Error__Critical
            );
        }
    }
}

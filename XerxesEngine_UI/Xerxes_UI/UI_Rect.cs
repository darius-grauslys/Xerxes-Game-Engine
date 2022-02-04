using OpenTK;
using Xerxes.Tools;

namespace Xerxes_UI
{
    public struct UI_Rect
    {
        private static Vector3[] UI_Rect__HADAMARD_ANCHORS = 
            new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(0.5f,0,0),
                new Vector3(1,0,0),

                new Vector3(0,0.5f,0),
                new Vector3(0.5f,0.5f,0),
                new Vector3(1,0.5f,0),

                new Vector3(0,1,0),
                new Vector3(0.5f,1,0),
                new Vector3(1,1,0)
            };

        /// <summary>
        /// X - Width, Y - Height, Z - Ignored
        /// </summary>
        public Vector3 UI_Rect__RECT { get; }
        public float UI_Rect__WIDTH  => UI_Rect__RECT.X;
        public float UI_Rect__HEIGHT => UI_Rect__RECT.Y;

        public UI_Rect(float width, float height)
        {
            UI_Rect__RECT = new Vector3(width, height, 0);
        }

        public Vector3 Get__Anchor_Point__UI_Rect
        (
            UI_Horizontal_Anchor_Type horizontal_Anchor_Type,
            UI_Vertical_Anchor_Type   vertical_Anchor_Type
        )
        {
            UI_Anchor_Type anchor_Type = 
                UI_Anchor
                .Internal_Get__Anchor_Type
                (
                    horizontal_Anchor_Type, 
                    vertical_Anchor_Type
                );

            return Private_Get__Anchor_Point__UI_Rect(anchor_Type);
        }

        public Vector3 Get__Anchor_Point__UI_Rect
        (
            UI_Vertical_Anchor_Type   vertical_Anchor_Type,
            UI_Horizontal_Anchor_Type horizontal_Anchor_Type
        )
        {
            return Get__Anchor_Point__UI_Rect(horizontal_Anchor_Type, vertical_Anchor_Type);
        }

        private Vector3 Private_Get__Anchor_Point__UI_Rect
        (
            UI_Anchor_Type anchor_Type
        )
        {
            Vector3 hadamard = 
                Math_Helper
                .Get__Hadamard_Product
                (
                    UI_Rect__RECT,
                    UI_Rect__HADAMARD_ANCHORS[(int)anchor_Type]
                );

            return hadamard;
        }
    }
}

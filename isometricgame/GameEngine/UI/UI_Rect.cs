using System;
using System.Collections.Specialized;
using System.Drawing;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    public class UI_Rect
    {
        #region Anchors
        //Anchors
        private static readonly Vector3[] _UI_Rect__ANCHOR_BASIS = new Vector3[]
        {
            // + + +
            // . . .
            // . . .
            new Vector3(0,1,0),
            new Vector3(0.5f,1,0),
            new Vector3(1,1,0),
            
            // . . .
            // + + +
            // . . .
            new Vector3(0,0.5f,0),
            new Vector3(0.5f,0.5f,0),
            new Vector3(1,0.5f,0),
            
            // . . .
            // . . .
            // + + +
            new Vector3(0,0,0),
            new Vector3(0.5f,0,0),
            new Vector3(1,0,0),
        };
        
        private readonly Vector3[] _UI_Rect__Anchor_Points = new Vector3[9];
        
        private void Private_Scale__Anchor_Points__UI_Panel()
        {
            for (int i = 0; i < _UI_Rect__ANCHOR_BASIS.Length; i++)
            {
                _UI_Rect__Anchor_Points[i] = new Vector3
                (
                    _UI_Rect__ANCHOR_BASIS[i].X * UI_Rect__Size.X,
                    _UI_Rect__ANCHOR_BASIS[i].Y * UI_Rect__Size.Y,
                    0
                );
            }
        }

        internal Vector3 Internal_Get__Local_Anchor_Position__UI_Rect
        (
            UI_Anchor_Position_Type positionType
        )
            => _UI_Rect__Anchor_Points[(int) positionType];

        internal Vector3 Internal_Get__Anchor_Position__UI_Rect
        (
            UI_Anchor_Position_Type positionType
        )
            => _UI_Rect__Anchor_Points[(int) positionType]
               + Get__Position_In_GameSpace__UI_Rect();
        
        #endregion
        
        #region Size
        //Size
        public Vector2 UI_Rect__Size { get; private set; }
        
        public Vector2 UI_Rect__Scaling_Vector { get; private set; }
        internal void Set__Scaling_Vector__UI_Rect(Vector2 newSize)
            => UI_Rect__Scaling_Vector = MathHelper.Get__Safe_Normalized(newSize);
        
        public float Get__Hypotenuse__UI_Rect()
            => MathHelper.Get__Hypotenuse(UI_Rect__Size);
        
        public float UI_Rect__Width
            => UI_Rect__Size.X;
        public float UI_Rect__Height
            => UI_Rect__Size.Y;

        public Vector3 UI_Rect__Width__As_Vector3
            => new Vector3(UI_Rect__Width, 0, 0);

        public Vector3 UI_Rect__Height__As_Vector3
            => new Vector3(0, UI_Rect__Height, 0);

        public Vector3 UI_Rect__Size__As_Vector3
            => new Vector3(UI_Rect__Size);
        
        public float UI_Rect__Area => UI_Rect__Width * UI_Rect__Height;
        
        public readonly Vector2 UI_Rect__INITIAL_SIZE;

        public float Get__Initial_Hypotenuse__UI_Rect()
            => MathHelper.Get__Hypotenuse(UI_Rect__INITIAL_SIZE);
        
        public float UI_Rect__Max_Hypotenuse { get; private set; }
        internal void Set__Max_Hypotenuse__UI_Rect(float newMaxHypotenuse)
        {
            newMaxHypotenuse = MathHelper.Clamp__UFloat(newMaxHypotenuse);
            
            UI_Rect__Max_Hypotenuse = 
                (UI_Rect__Min_Hypotenuse > newMaxHypotenuse) 
                    ? UI_Rect__Min_Hypotenuse
                    : newMaxHypotenuse;

            float currentHypotenuse = Get__Hypotenuse__UI_Rect();
            
            if (currentHypotenuse > newMaxHypotenuse)
                Internal_Rescale__UI_Rect(newMaxHypotenuse);
        }
        
        public float UI_Rect__Min_Hypotenuse { get; private set; }
        internal void Set__Min_Hypotenuse__UI_Rect(float newMinHypotenuse)
        {
            newMinHypotenuse = MathHelper.Clamp__UFloat(newMinHypotenuse);
            
            UI_Rect__Min_Hypotenuse = 
                (UI_Rect__Max_Hypotenuse < newMinHypotenuse)
                    ? UI_Rect__Max_Hypotenuse
                    : newMinHypotenuse;

            float currentHypotenuse = Get__Hypotenuse__UI_Rect();
            
            if (currentHypotenuse < newMinHypotenuse)
                Internal_Rescale__UI_Rect(newMinHypotenuse);
        }
        
        public float UI_Rect__INITIAL_WIDTH => UI_Rect__INITIAL_SIZE.X;
        public float UI_Rect__INITIAL_HEIGHT => UI_Rect__INITIAL_SIZE.Y;

        public float UI_Rect__INITIAL_AREA => UI_Rect__INITIAL_WIDTH * UI_Rect__INITIAL_HEIGHT;
        #endregion
        
        #region Positionals
        //Positionals
        /// <summary>
        /// The scene position of the element. This is determined by it's container among other factors.
        /// The Local Origin can also manipulate the position, this is an offset based on the
        /// element's own anchors.
        /// </summary>
        private Vector3 _UI_Rect__Position { get; set; }

        /// <summary>
        /// Returns the position of the element influenced by local origin.
        /// Use this for proper positioning of UI_GameObjects.
        /// [_UI_Rect__Position]
        /// </summary>
        /// <returns></returns>
        public Vector3 Get__Position_In_UISpace__UI_Rect()
            => _UI_Rect__Position;
        /// <summary>
        /// Returns the position of the element without local origin influence.
        /// Use this for rendering graphics of UI_GameObjects - NOT - for proper positioning.
        /// Don't use this if you don't know what you're doing.
        /// [_UI_Rect__Position - UI_Rect__Local_Origin_Offset]
        /// </summary>
        /// <returns></returns>
        public Vector3 Get__Position_In_GameSpace__UI_Rect()
            => _UI_Rect__Position - UI_Rect__Local_Origin_Offset;

        private Vector3 UI_Rect__Bottom_Left_Bound
            => 
                Get__Position_In_GameSpace__UI_Rect()
                ;
        private Vector3 Get__Bottom_Left_Bound__With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Bottom_Left_Bound + offset;

        /// <summary>
        /// The corner vertically opposed to the origin point.
        /// </summary>
        private Vector3 UI_Rect__Top_Left_Bound
            =>
                Get__Position_In_GameSpace__UI_Rect()
                + UI_Rect__Height__As_Vector3;
        private Vector3 Get__Top_Left_Bound__With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Top_Left_Bound + offset;

        /// <summary>
        /// The corner horizontally opposed to the origin point.
        /// </summary>
        private Vector3 UI_Rect__Bottom_Right_Bound
            => 
                Get__Position_In_GameSpace__UI_Rect() 
                + UI_Rect__Width__As_Vector3;
        private Vector3 Get__Bottom_Right_Bound__With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Bottom_Right_Bound + offset;
        
        /// <summary>
        /// The corner that is both vertically, and horizontally opposed to the origin point.
        /// </summary>
        private Vector3 UI_Rect__Top_Right_Bound
            => 
                Get__Position_In_GameSpace__UI_Rect()
                + UI_Rect__Size__As_Vector3;
        private Vector3 Get__Top_Right_Bound__With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Top_Right_Bound + offset;
        
        /// <summary>
        /// The anchor index which is used to offset the local origin of the UI_Rect.
        /// </summary>
        public UI_Anchor_Position_Type UI_Rect__Local_Origin_Type { get; internal set; }

        /// <summary>
        /// The vector quantity which offsets the UI_Rect position post scale/reposition calculations.
        /// </summary>
        public Vector3 UI_Rect__Local_Origin_Offset
            => _UI_Rect__Anchor_Points[(int) UI_Rect__Local_Origin_Type];

        /// <summary>
        /// Sets the position of the Rect while enforcing local origin offsets.
        /// </summary>
        /// <param name="position"></param>
        internal void Internal_Set__Position__UI_Rect(Vector3 position)
        {
            _UI_Rect__Position = position;
        }
        #endregion

        public UI_Rect
            (
            Vector2? size = null,
            UI_Anchor_Position_Type originOffset = UI_Anchor_Position_Type.Top_Left
            )
            :this
                (
                size?.X ?? 1, 
                size?.Y ?? 1,
                originOffset
                )
        {
            
        }

        public UI_Rect
            (
            float width, 
            float height,
            UI_Anchor_Position_Type originOffset = UI_Anchor_Position_Type.Top_Left
            )
        {
            UI_Rect__Local_Origin_Type = originOffset;
            _UI_Rect__Position = Vector3.Zero;

            Vector2 usedSize = MathHelper.Clamp__Vector2_UFloat(new Vector2(width, height));

            UI_Rect__INITIAL_SIZE = usedSize;

            UI_Rect__Max_Hypotenuse = float.MaxValue;

            Internal_Resize__UI_Rect(usedSize);
            Internal_Set__Position__UI_Rect(Vector3.Zero);
        }
        
        internal void Internal_Rescale__UI_Rect
        (
            float hypotenuse
        )
        {
            float clampedHypotenuse = MathHelper.Clamp__Float
            (
                hypotenuse,
                UI_Rect__Min_Hypotenuse,
                UI_Rect__Max_Hypotenuse
            );
            
            UI_Rect__Size = UI_Rect__Scaling_Vector * clampedHypotenuse;
            
            Private_Scale__Anchor_Points__UI_Panel();
        }

        internal void Internal_Resize__UI_Rect
        (
            Vector2 newSize
        )
        {
            newSize = MathHelper.Clamp__Vector2_UFloat(newSize);

            UI_Rect__Scaling_Vector = newSize.Normalized();
            
            Internal_Rescale__UI_Rect(MathHelper.Get__Hypotenuse(newSize));
        }

        public override string ToString()
        {
            return String.Format
            (
                "Rect: [P:{0} S:{1}]",
                _UI_Rect__Position,
                UI_Rect__Size
            );
        }

        #region Static Anaylsis

        private struct UI_Rect__Offset_Bounds_Struct
        {
            public readonly Vector3
                Origin_Bound,
                Vertical_Bound,
                Furthest_Bound,
                Horizontal_Bound;

            public UI_Rect__Offset_Bounds_Struct(Vector3? nullable_Offset, UI_Rect rect)
            {
                Vector3 offset = nullable_Offset ?? Vector3.Zero;
                
                Origin_Bound = rect.Get__Bottom_Left_Bound__With_Offset__UI_Rect(offset);
                Vertical_Bound = rect.Get__Top_Left_Bound__With_Offset__UI_Rect(offset);
                Furthest_Bound = rect.Get__Top_Right_Bound__With_Offset__UI_Rect(offset);
                Horizontal_Bound = rect.Get__Bottom_Right_Bound__With_Offset__UI_Rect(offset);
            }
        }

        public static bool CheckIf__Position_Is_Within_Rect__XY0(UI_Rect rect, Vector3 position)
        {
            return 
                (
                    MathHelper.CheckIf__Bounded_XY0_Exclusive
                        (
                        position,
                        rect.UI_Rect__Bottom_Left_Bound,
                        rect.UI_Rect__Top_Right_Bound
                        )
                );
        }
        
        public static bool CheckIf__Rects_Overlap
            (
            UI_Rect rect1, UI_Rect rect2,
            Vector3? nullable_Offset_1 = null,
            Vector3? nullable_Offset_2 = null
            )
        {
            UI_Rect__Offset_Bounds_Struct rect1_Offset = new UI_Rect__Offset_Bounds_Struct(nullable_Offset_1, rect1);
            UI_Rect__Offset_Bounds_Struct rect2_Offset = new UI_Rect__Offset_Bounds_Struct(nullable_Offset_2, rect2);
            
            return 
                (
                    MathHelper.CheckIf__Bounded_XYZ_Exclusive
                    (
                        rect1_Offset.Origin_Bound,
                        rect2_Offset.Origin_Bound,
                        rect2_Offset.Furthest_Bound
                    )
                    ||
                    MathHelper.CheckIf__Bounded_XYZ_Exclusive
                    (
                        rect1_Offset.Vertical_Bound,
                        rect2_Offset.Origin_Bound,
                        rect2_Offset.Furthest_Bound
                    )
                    ||
                    MathHelper.CheckIf__Bounded_XYZ_Exclusive
                    (
                        rect1_Offset.Furthest_Bound,
                        rect2_Offset.Origin_Bound,
                        rect2_Offset.Furthest_Bound
                    )
                    ||
                    MathHelper.CheckIf__Bounded_XYZ_Exclusive
                    (
                        rect1_Offset.Horizontal_Bound,
                        rect2_Offset.Origin_Bound,
                        rect2_Offset.Furthest_Bound
                    )
                )
                ||
                (
                    rect1_Offset.Origin_Bound == rect2_Offset.Origin_Bound
                    &&
                    rect1_Offset.Vertical_Bound == rect2_Offset.Vertical_Bound
                    &&
                    rect1_Offset.Furthest_Bound == rect2_Offset.Furthest_Bound
                    &&
                    rect1_Offset.Horizontal_Bound == rect2_Offset.Horizontal_Bound
                )
                ;
        }

        public static bool CheckIf__Rect_Is_Bound_By_Rect
            (
            UI_Rect isThisBounded, 
            UI_Rect byThis,
            Vector3? nullable_Offset_1 = null,
            Vector3? nullable_Offset_2 = null
            )
        {
            UI_Rect__Offset_Bounds_Struct rect1_Offset = new UI_Rect__Offset_Bounds_Struct(nullable_Offset_1, isThisBounded);
            UI_Rect__Offset_Bounds_Struct rect2_Offset = new UI_Rect__Offset_Bounds_Struct(nullable_Offset_2, byThis);
            
            return 
            (
                MathHelper.CheckIf__Bounded_XYZ_Inclusive
                (
                    rect1_Offset.Origin_Bound,
                    rect2_Offset.Origin_Bound,
                    rect2_Offset.Furthest_Bound
                )
                &&
                MathHelper.CheckIf__Bounded_XYZ_Inclusive
                (
                    rect1_Offset.Vertical_Bound,
                    rect2_Offset.Origin_Bound,
                    rect2_Offset.Furthest_Bound
                )
                &&
                MathHelper.CheckIf__Bounded_XYZ_Inclusive
                (
                    rect1_Offset.Furthest_Bound,
                    rect2_Offset.Origin_Bound,
                    rect2_Offset.Furthest_Bound
                )
                &&
                MathHelper.CheckIf__Bounded_XYZ_Inclusive
                (
                    rect1_Offset.Horizontal_Bound,
                    rect2_Offset.Origin_Bound,
                    rect2_Offset.Furthest_Bound
                )
            );
        }
        #endregion
    }
}
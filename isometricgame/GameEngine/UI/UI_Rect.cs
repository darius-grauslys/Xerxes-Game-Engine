using System;
using System.Collections.Specialized;
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

        internal Vector3 Internal_Get__Anchor_Position__UI_Rect
        (
            UI_Anchor_Position_Type position
        )
        {
            return _UI_Rect__Anchor_Points[(int) position];
        }
        
        #endregion
        
        #region Size
        //Size
        public Vector2 UI_Rect__Size { get; private set; }

        public float UI_Rect__Width
            => UI_Rect__Size.X;
        public float UI_Rect__Height
            => UI_Rect__Size.Y;

        public Vector3 UI_Rect__Width__As_Vector3
            => new Vector3(UI_Rect__Width, 0, 0);

        public Vector3 UI_Rect__Height__As_Vector3
            => new Vector3(0, UI_Rect__Height, 0);
        
        public float UI_Rect__Area => UI_Rect__Width * UI_Rect__Height;
        
        public readonly Vector2 UI_Rect__INITIAL_SIZE;
        public Vector2 UI_Rect__Max_Size { get; private set; }
        public void Set__Max_Size__UI_Rect(Vector2 newSize)
        {
            newSize = MathHelper.Clamp_Vec_UFloat(newSize);
            
            UI_Rect__Max_Size = 
                (MathHelper.IsGreaterArea(UI_Rect__Min_Size, UI_Rect__Max_Size))
                ? UI_Rect__Min_Size
                : newSize;
            
            if (MathHelper.IsGreaterArea(UI_Rect__Size, UI_Rect__Max_Size))
                Internal_Rescale__UI_Rect(UI_Rect__Max_Size);
        }
        
        public Vector2 UI_Rect__Min_Size { get; private set; }
        public void Set__Min_Size__UI_Rect(Vector2 newSize)
        {
            newSize = MathHelper.Clamp_Vec_UFloat(newSize);
            
            UI_Rect__Min_Size = 
                (MathHelper.IsGreaterArea(UI_Rect__Min_Size, UI_Rect__Max_Size))
                    ? UI_Rect__Max_Size
                    : newSize;
            
            if (MathHelper.IsGreaterArea(UI_Rect__Min_Size, UI_Rect__Size))
                Internal_Rescale__UI_Rect(UI_Rect__Min_Size);
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
        public Vector3 UI_Rect__Position { get; private set; }
        public Vector3 Get__Position_With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Position + offset;
        
        /// <summary>
        /// The corner vertically opposed to the origin point.
        /// </summary>
        public Vector3 UI_Rect__Vertical_Bound
            => UI_Rect__Position + new Vector3(0, UI_Rect__Height, 0);
        public Vector3 Get__Vertical_Bound_With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Vertical_Bound + offset;
        
        /// <summary>
        /// The corner horizontally opposed to the origin point.
        /// </summary>
        public Vector3 UI_Rect__Horizontal_Bound
            => UI_Rect__Position + new Vector3(UI_Rect__Width, 0, 0);
        public Vector3 Get__Horizontal_Bound_With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Horizontal_Bound + offset;
        
        /// <summary>
        /// The corner that is both vertically, and horizontally opposed to the origin point.
        /// </summary>
        public Vector3 UI_Rect__Furthest_Bound
            => UI_Rect__Position + new Vector3(UI_Rect__Width, UI_Rect__Height, 0);
        public Vector3 Get__Furthest_Bound_With_Offset__UI_Rect(Vector3 offset)
            => UI_Rect__Furthest_Bound + offset;
        
        /// <summary>
        /// The anchor index which is used to offset the local origin of the UI_Rect.
        /// </summary>
        internal int UI_Rect__Local_Origin__Internal { get; private set; }
        
        /// <summary>
        /// The vector quantity which offsets the UI_Rect position post scale/reposition calculations.
        /// </summary>
        public Vector3 UI_Rect__Local_Origin_Offset__Internal { get; private set; }

        /// <summary>
        /// Changes the local origin using an anchor within this UI_Rect.
        /// Not implemented yet.
        /// </summary>
        /// <param name="localOriginPosition"></param>
        internal void Internal_Set__Local_Origin__UI_Rect
        (
            UI_Anchor_Position_Type localOriginPosition
        )
        {
            UI_Rect__Local_Origin__Internal =
                (int) localOriginPosition;

            UI_Rect__Local_Origin_Offset__Internal = _UI_Rect__Anchor_Points[UI_Rect__Local_Origin__Internal];
        }

        /// <summary>
        /// Sets the position of the Rect.
        /// </summary>
        /// <param name="position"></param>
        internal void Internal_Set__Position__UI_Rect(Vector3 position)
        {
            UI_Rect__Position = position - UI_Rect__Local_Origin_Offset__Internal;
        }
        #endregion

        public UI_Rect
            (
            Vector2? size = null,
            UI_Anchor_Position_Type originOffset = UI_Anchor_Position_Type.Bottom_Left
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
            UI_Anchor_Position_Type originOffset = UI_Anchor_Position_Type.Bottom_Left
            )
        {
            UI_Rect__Position = new Vector3();
            Internal_Set__Local_Origin__UI_Rect(originOffset);

            Vector2 usedSize = new Vector2(width, height);

            UI_Rect__INITIAL_SIZE = MathHelper.Clamp_Vec_UFloat(usedSize);

            UI_Rect__Max_Size = MathHelper.MAX_VECTOR2_SQUARED;

            Internal_Rescale__UI_Rect(usedSize);
        }
        
        internal void Internal_Rescale__UI_Rect
        (
            Vector2 newSize
        )
        {
            Vector2 clampedSize = MathHelper.Clamp_Vec_UFloat(newSize);

            if (MathHelper.IsGreaterArea(clampedSize, UI_Rect__Max_Size))
                clampedSize = UI_Rect__Max_Size;
            if (MathHelper.IsGreaterArea(UI_Rect__Min_Size, clampedSize))
                clampedSize = UI_Rect__Min_Size;
            
            UI_Rect__Size = clampedSize;
            
            Private_Scale__Anchor_Points__UI_Panel();
        }

        #region Static Anaylsis

        private struct UI_Rect__Offset_Bounds_Struct
        {
            public readonly Vector3
                Position,
                Vertical_Bound,
                Furthest_Bound,
                Horizontal_Bound;

            public UI_Rect__Offset_Bounds_Struct(Vector3? nullable_Offset, UI_Rect rect)
            {
                Vector3 offset = nullable_Offset ?? Vector3.Zero;
                
                Position = rect.Get__Position_With_Offset__UI_Rect(offset);
                Vertical_Bound = rect.Get__Vertical_Bound_With_Offset__UI_Rect(offset);
                Furthest_Bound = rect.Get__Furthest_Bound_With_Offset__UI_Rect(offset);
                Horizontal_Bound = rect.Get__Horizontal_Bound_With_Offset__UI_Rect(offset);
            }
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
                    MathHelper.IsBounded_XYZ_Exclusive
                    (
                        rect1_Offset.Position,
                        rect2_Offset.Position,
                        rect2_Offset.Furthest_Bound
                    )
                    ||
                    MathHelper.IsBounded_XYZ_Exclusive
                    (
                        rect1_Offset.Vertical_Bound,
                        rect2_Offset.Position,
                        rect2_Offset.Furthest_Bound
                    )
                    ||
                    MathHelper.IsBounded_XYZ_Exclusive
                    (
                        rect1_Offset.Furthest_Bound,
                        rect2_Offset.Position,
                        rect2_Offset.Furthest_Bound
                    )
                    ||
                    MathHelper.IsBounded_XYZ_Exclusive
                    (
                        rect1_Offset.Horizontal_Bound,
                        rect2_Offset.Position,
                        rect2_Offset.Furthest_Bound
                    )
                )
                ||
                (
                    rect1_Offset.Position == rect2_Offset.Position
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
                MathHelper.IsBounded_XYZ_Inclusive
                (
                    rect1_Offset.Position,
                    rect2_Offset.Position,
                    rect2_Offset.Furthest_Bound
                )
                &&
                MathHelper.IsBounded_XYZ_Inclusive
                (
                    rect1_Offset.Vertical_Bound,
                    rect2_Offset.Position,
                    rect2_Offset.Furthest_Bound
                )
                &&
                MathHelper.IsBounded_XYZ_Inclusive
                (
                    rect1_Offset.Furthest_Bound,
                    rect2_Offset.Position,
                    rect2_Offset.Furthest_Bound
                )
                &&
                MathHelper.IsBounded_XYZ_Inclusive
                (
                    rect1_Offset.Horizontal_Bound,
                    rect2_Offset.Position,
                    rect2_Offset.Furthest_Bound
                )
            );
        }

        public static bool CheckIf__Any_Bounding_Points_Exceed__UI_Rect
            (
            UI_Rect subjectRect,
            Vector3 axes,
            Vector3 subjectPoint,
            bool checkGreaterThan
            )
        {
            bool ret = false;
            
            For_Each__Bounding_Point__UI_Rect
                (
                subjectRect,
                (p) =>
                {
                    Vector3 comparingValues = MathHelper.Hadamard_Product
                    (
                        p, axes
                    );
                    
                    ret = ret || 
                          (
                              Private_Compare__Bounding_Point_Axis__UI_Rect(comparingValues.X, subjectPoint.X, checkGreaterThan)
                              ||
                              Private_Compare__Bounding_Point_Axis__UI_Rect(comparingValues.Y, subjectPoint.Y, checkGreaterThan)
                              ||
                              Private_Compare__Bounding_Point_Axis__UI_Rect(comparingValues.Z, subjectPoint.Z, checkGreaterThan)
                          );
                }
            );

            return ret;
        }

        private static bool Private_Compare__Bounding_Point_Axis__UI_Rect(float comparedAxis, float subjectPoint,
            bool checkGreaterThan)
        {
            return
                (comparedAxis != 0) &&
                (
                    checkGreaterThan
                        ? comparedAxis < subjectPoint
                        : comparedAxis > subjectPoint
                );
        }
        
        public static void For_Each__Bounding_Point__UI_Rect(UI_Rect subjectRect, Action<Vector3> operation)
        {
            operation?.Invoke(subjectRect.UI_Rect__Position);
            operation?.Invoke(subjectRect.UI_Rect__Vertical_Bound);
            operation?.Invoke(subjectRect.UI_Rect__Furthest_Bound);
            operation?.Invoke(subjectRect.UI_Rect__Horizontal_Bound);
        }
        
        #endregion
    }
}
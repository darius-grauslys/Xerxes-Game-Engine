using System;
using OpenTK;
using MathHelper = XerxesEngine.Tools.MathHelper;

namespace XerxesEngine.UI
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

        private static readonly UI_Anchor_Position_Type[] _UI_Rect__EXTERIOR_POSITION_TYPES = new UI_Anchor_Position_Type[]
        {
            UI_Anchor_Position_Type.Top_Left,    UI_Anchor_Position_Type.Top_Middle,    UI_Anchor_Position_Type.Top_Right,
            
            UI_Anchor_Position_Type.Middle_Left,                                        UI_Anchor_Position_Type.Middle_Right,
            
            UI_Anchor_Position_Type.Bottom_Left, UI_Anchor_Position_Type.Bottom_Middle, UI_Anchor_Position_Type.Bottom_Right
        };

        private static readonly UI_Anchor_Position_Type[] _UI_Rect__CORNER_POSITION_TYPES = new UI_Anchor_Position_Type[]
        {
            UI_Anchor_Position_Type.Top_Left,                                           UI_Anchor_Position_Type.Top_Right,
            
            
            
            UI_Anchor_Position_Type.Bottom_Left,                                        UI_Anchor_Position_Type.Bottom_Right
        };
        
        private readonly Vector3[] _UI_Rect__ANCHOR_POINTS = new Vector3[9];

        private readonly Vector3[] _UI_Rect__LOGICAL_NORMALIZED_CORNERS = new Vector3[4];
        
        private void Private_Scale__Anchor_Points__UI_Panel()
        {
            for (int i = 0; i < _UI_Rect__ANCHOR_BASIS.Length; i++)
            {
                _UI_Rect__ANCHOR_POINTS[i] = new Vector3
                (
                    _UI_Rect__ANCHOR_BASIS[i].X * UI_Rect__Size.X,
                    _UI_Rect__ANCHOR_BASIS[i].Y * UI_Rect__Size.Y,
                    0
                );
            }

            Vector3 middle = Internal_Get__Anchor_Point__UI_Rect(UI_Anchor_Position_Type.Middle);

            for (int i =0; i < _UI_Rect__CORNER_POSITION_TYPES.Length; i++)
            {
                UI_Anchor_Position_Type positionType = _UI_Rect__CORNER_POSITION_TYPES[i];
                
                Vector3 vec = Internal_Get__Anchor_Point__UI_Rect(positionType);
                vec -= middle;

                _UI_Rect__LOGICAL_NORMALIZED_CORNERS[i] = vec.Normalized() / 1000f;
            }
        }

        internal Vector3 Internal_Get__Anchor_Point__UI_Rect
        (
            UI_Anchor_Position_Type positionType
        )
            => _UI_Rect__ANCHOR_POINTS[(int) positionType];

        internal Vector3 Internal_Get__Anchor_Position__UI_Rect
        (
            UI_Anchor_Position_Type positionType
        )
            => Private_Get__Anchor_Position__UI_Rect((int) positionType);
        
        private Vector3 Private_Get__Anchor_Position__UI_Rect
        (
            int index
        )
            => _UI_Rect__ANCHOR_POINTS[index]
               + Get__Position_In_GameSpace__UI_Rect();

        internal Vector3[] Internal_Get__Anchor_Positions__UI_Rect()
        {
            Vector3[] anchorPositions = new Vector3[_UI_Rect__ANCHOR_POINTS.Length];

            for (int i = 0; i < _UI_Rect__ANCHOR_POINTS.Length; i++)
                anchorPositions[i] = Private_Get__Anchor_Position__UI_Rect(i);

            return anchorPositions;
        }

        internal Vector3[] Internal_Get__Exterior_Anchor_Positions__UI_Rect()
        {
            Vector3[] vecs = new Vector3[8];

            int index = 0;

            for (int i = 0; i < _UI_Rect__ANCHOR_POINTS.Length; i++)
            {
                if (i == (int) UI_Anchor_Position_Type.Middle)
                    continue;

                vecs[index] = Internal_Get__Anchor_Position__UI_Rect
                (
                    (UI_Anchor_Position_Type) i
                );
                
                index++;
            }

            return vecs;
        }

        internal Vector3[] Internal_Get__Bounding_Corners__UI_Rect(Vector3? nullableOffset)
        {
            Vector3 offset = nullableOffset ?? Vector3.Zero;
            
            Vector3[] vecs = new Vector3[]
            {
                Internal_Get__Anchor_Position__UI_Rect(UI_Anchor_Position_Type.Top_Left) + offset,
                Internal_Get__Anchor_Position__UI_Rect(UI_Anchor_Position_Type.Bottom_Right) + offset
            };

            return vecs;
        }

        internal Vector3[] Internal_Get__Logical_Bounding_Corners__UI_Rect(Vector3? nullableOffset)
        {
            Vector3[] vecs = Internal_Get__Bounding_Corners__UI_Rect(nullableOffset);

            vecs[0] -= _UI_Rect__LOGICAL_NORMALIZED_CORNERS[0];
            vecs[1] -= _UI_Rect__LOGICAL_NORMALIZED_CORNERS[3];

            return vecs;
        }
        
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

        public bool Is__Any_Length_Zero__UI_Rect
            => UI_Rect__Width == 0 || UI_Rect__Height == 0;
        
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

        private Vector3[] Get__Bounds__UI_Rect()
            => new Vector3[]
            {
                UI_Rect__Bottom_Left_Bound,
                UI_Rect__Top_Left_Bound,
                UI_Rect__Top_Right_Bound,
                UI_Rect__Bottom_Right_Bound
            };
        
        /// <summary>
        /// The anchor index which is used to offset the local origin of the UI_Rect.
        /// </summary>
        public UI_Anchor_Position_Type UI_Rect__Local_Origin_Type { get; internal set; }

        /// <summary>
        /// The vector quantity which offsets the UI_Rect position post scale/reposition calculations.
        /// </summary>
        public Vector3 UI_Rect__Local_Origin_Offset
            => _UI_Rect__ANCHOR_POINTS[(int) UI_Rect__Local_Origin_Type];

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
        
        public static bool CheckIf__Rect_Is_Within_Rect
            (
            UI_Rect isThis, 
            UI_Rect withinThis,
            Vector3? nullable_SubjectOffset = null,
            Vector3? nullable_TargetOffset = null,
            bool inclusive = true
            )
        {
            Vector3 subjectOffset = nullable_SubjectOffset ?? Vector3.Zero;
            Vector3 targetOffset = nullable_TargetOffset ?? Vector3.Zero;
            
            foreach (UI_Anchor_Position_Type positionType in _UI_Rect__CORNER_POSITION_TYPES)
            {
                Vector3 subjectVector = isThis.Internal_Get__Anchor_Position__UI_Rect(positionType);

                bool isBounded = CheckIf__Position_Is_Bounded_By_Rect
                (
                    subjectVector,
                    subjectOffset,
                    withinThis,
                    targetOffset,
                    inclusive
                );

                if (!isBounded)
                    return false;
            }

            return true;
        }
        
        public static bool CheckIf__Rect_Overlaps_Rect
            (
            UI_Rect isThis, 
            UI_Rect withinThis,
            Vector3? nullableOffset = null
        )
        {
            Vector3 offset = nullableOffset ?? Vector3.Zero;

            bool isSubjectWithin = Private_CheckIf__Rect_Bounds_Overlaps_Rect
            (
                isThis,
                withinThis,
                offset,
                Vector3.Zero
            );

            bool isTargetWithin = Private_CheckIf__Rect_Bounds_Overlaps_Rect
            (
                withinThis,
                isThis,
                Vector3.Zero,
                offset
            );

            return isSubjectWithin || isTargetWithin;
        }
        
        private static bool Private_CheckIf__Rect_Bounds_Overlaps_Rect
        (
            UI_Rect subject,
            UI_Rect target,
            Vector3 subjectOffset,
            Vector3 targetOffset
        )
        {
            Vector3 targetMiddle = target.Internal_Get__Anchor_Position__UI_Rect(UI_Anchor_Position_Type.Middle) +
                                   targetOffset;

            for (int i = 0; i < _UI_Rect__CORNER_POSITION_TYPES.Length; i++)
            {
                bool isOverlapping = Private_CheckIf__Bounds_Overlap
                (
                    subject,
                    target,
                    subjectOffset,
                    targetOffset,
                    targetMiddle,
                    i
                );

                if (isOverlapping)
                    return true;
            }

            return false;
        }

        private static bool Private_CheckIf__Bounds_Overlap
        (
            UI_Rect subject,
            UI_Rect target,
            Vector3 subjectOffset,
            Vector3 targetOffset,
            Vector3 targetMiddle,
            int cornerIndex
        )
        {
            UI_Anchor_Position_Type complimentaryPosition = _UI_Rect__CORNER_POSITION_TYPES[cornerIndex];
            
            Vector3 inscribed_S =
                subject.Internal_Get__Anchor_Position__UI_Rect(complimentaryPosition) 
                + subjectOffset
                - subject._UI_Rect__LOGICAL_NORMALIZED_CORNERS[cornerIndex];
            
            Vector3 dist_Inscribed = inscribed_S - targetMiddle;

            Vector3 w_delta_outscribed = new Vector3
            (
                target.UI_Rect__Width/2,
                dist_Inscribed.Y,
                0
            );
            
            Vector3 h_delta_outscribed = new Vector3
            (
                dist_Inscribed.X,
                target.UI_Rect__Height/2,
                0
            );
            
            float hyp_w = MathHelper.Get__Hypotenuse(w_delta_outscribed);
            float hyp_h = MathHelper.Get__Hypotenuse(h_delta_outscribed);

            float hyp_inscribed = MathHelper.Get__Hypotenuse(dist_Inscribed);

            bool bound_w = MathHelper.Tolerable__LessThan__Float(hyp_inscribed, hyp_w);
            bool bound_h = MathHelper.Tolerable__LessThan__Float(hyp_inscribed, hyp_h);

            return bound_w && bound_h;
        }
        
        public static bool CheckIf__Position_Is_Bounded_By_Rect
        (
            Vector3 position,
            Vector3 positionOffset,
            UI_Rect rect,
            Vector3 rectOffset,
            bool inclusive = true
        )
        {
            Vector3[] logicalBounds = (inclusive) 
                ? rect.Internal_Get__Bounding_Corners__UI_Rect(rectOffset)
                : rect.Internal_Get__Logical_Bounding_Corners__UI_Rect(rectOffset);

            Vector3 offsetPosition = position + positionOffset;

            bool isBounded = MathHelper.CheckIf__Vec_Is_Bounded__Forth_Quadrant
            (
                offsetPosition,
                logicalBounds[0],
                logicalBounds[1]
            );
            
            return isBounded;
        }
        
        #endregion
    }
}

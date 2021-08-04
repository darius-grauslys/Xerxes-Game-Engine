using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    public class UI_Anchor_Padding
    {
        public readonly UI_Anchor_Padding_Type UI_Anchor_Padding__TYPE;
        public bool UI_Anchor_Padding__Is_Constrained => ((int) UI_Anchor_Padding__TYPE) > 0;
        public bool UI_Anchor_Padding__Is_Percentage => ((int) UI_Anchor_Padding__TYPE) % 2 == 0;
        
        public readonly UI_Anchor_Sort_Type UI_Anchor_Padding__SORT_TYPE;
        public bool UI_Anchor_Padding__Is_Padding_On_Middle_Anchor
            => (int) UI_Anchor_Padding__SORT_TYPE > 4;
        
        /// <summary>
        /// The scaling buffer value this buffer was constructed with.
        /// </summary>
        public readonly float UI_Anchor_Padding__INITIAL_SCALING_VALUE;
        
        public float UI_Anchor_Padding__Padding_Scale { get; private set; }

        internal void Internal_Set__Padding_Scale__UI_Anchor_Padding(float scale)
        {
            float valueToBeScaled = scale;
            
            switch (UI_Anchor_Padding__TYPE)
            {
                case UI_Anchor_Padding_Type.Constrained__Pixel:
                case UI_Anchor_Padding_Type.Constrained__Percentage:
                    valueToBeScaled = MathHelper.Clamp_UFloat(valueToBeScaled,
                        (UI_Anchor_Padding__Is_Percentage) ? 1 : float.MaxValue);
                    break;
            }

            UI_Anchor_Padding__Padding_Scale = valueToBeScaled;
        }

        public float UI_Anchor_Padding__Leading_Padding { get; internal set; }
        public Vector3 Get__Leading_Padding__As_Vector3__UI_Anchor_Padding()
            => Private_Get__Padding__As_Vector3(UI_Anchor_Padding__Leading_Padding);
        
        public float UI_Anchor_Padding__Trailing_Padding { get; internal set; }
        public Vector3 Get__Trailing_Padding__As_Vector3__UI_Anchor_Padding()
            => Private_Get__Padding__As_Vector3(UI_Anchor_Padding__Trailing_Padding);
        
        private Vector3 Private_Get__Padding__As_Vector3(float padding)
        {
            switch (UI_Anchor_Padding__SORT_TYPE)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Right:
                    return new Vector3(padding, 0, 0);
                default:
                    return new Vector3(0, padding, 0);
            }
        }
        
        /// <summary>
        /// Extend the anchor padding in response to a change in the container panel's size.
        /// </summary>
        /// <param name="panelSize"></param>
        internal void Internal_Scale__Float_Buffer__UI_Anchor_Padding(Vector2 panelSize)
        {
            switch (UI_Anchor_Padding__SORT_TYPE)
            {
                case UI_Anchor_Sort_Type.Left:
                case UI_Anchor_Sort_Type.Right:
                    Private_Scale__Float_Buffer_Based_On_Scaling_Type__UI_Anchor_Padding(panelSize.X);
                    break;
                default:
                    Private_Scale__Float_Buffer_Based_On_Scaling_Type__UI_Anchor_Padding(panelSize.Y);
                    break;
            }
        }

        private void Private_Scale__Float_Buffer_Based_On_Scaling_Type__UI_Anchor_Padding(float axisSize)
        {
            switch (UI_Anchor_Padding__TYPE)
            {
                case UI_Anchor_Padding_Type.Constrained__Pixel:
                case UI_Anchor_Padding_Type.Unbound__Pixel:
                    UI_Anchor_Padding__Leading_Padding =
                        UI_Anchor_Padding__Padding_Scale;
                    UI_Anchor_Padding__Trailing_Padding = axisSize - UI_Anchor_Padding__Leading_Padding;
                    break;
                case UI_Anchor_Padding_Type.Constrained__Percentage:
                case UI_Anchor_Padding_Type.Unbound__Percentage:
                    float axisIfMiddle = (UI_Anchor_Padding__Is_Padding_On_Middle_Anchor)
                        ? axisSize * 0.5f
                        : axisSize;
                    UI_Anchor_Padding__Leading_Padding =
                        UI_Anchor_Padding__Padding_Scale * axisIfMiddle;
                    UI_Anchor_Padding__Trailing_Padding = axisSize - UI_Anchor_Padding__Leading_Padding;
                    break;
            }
            
            if (UI_Anchor_Padding__Is_Constrained)
            {
                UI_Anchor_Padding__Leading_Padding = MathHelper.Clamp_UFloat(UI_Anchor_Padding__Leading_Padding);
                UI_Anchor_Padding__Trailing_Padding = MathHelper.Clamp_UFloat(UI_Anchor_Padding__Trailing_Padding);
            }
        }
        
        /// <summary>
        /// Constructs a Buffer value, and constrains the float value based on the UI_Buffer_Type.
        /// </summary>
        /// <param name="paddingScale">The value which the buffer scales off of.</param>
        /// <param name="uiAnchorPaddingType">If Constrained, the float value will be UFloat clamped.</param>
        /// <param name="uiAnchorPaddingSortType"></param>
        internal UI_Anchor_Padding(float paddingScale, UI_Anchor_Padding_Type uiAnchorPaddingType, UI_Anchor_Sort_Type uiAnchorPaddingSortType)
        {
            UI_Anchor_Padding__TYPE = uiAnchorPaddingType;

            Internal_Set__Padding_Scale__UI_Anchor_Padding(paddingScale);
            
            UI_Anchor_Padding__INITIAL_SCALING_VALUE = UI_Anchor_Padding__Padding_Scale;
            UI_Anchor_Padding__SORT_TYPE = uiAnchorPaddingSortType;
        }
    }
}
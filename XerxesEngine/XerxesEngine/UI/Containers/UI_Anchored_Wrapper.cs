using System;
using OpenTK;
using MathHelper = XerxesEngine.Tools.MathHelper;

namespace XerxesEngine.UI
{
    /// <summary>
    /// Wraps UI_Element and provides localized position logic.
    /// </summary>
    public class UI_Anchored_Wrapper : UI_Wrapper
    {
        #region Anchor
        /// <summary>
        /// Used for determining sorted positions.
        /// </summary>
        public UI_Anchor UI_Indexed_Element__Anchor { get; private set; }

        public UI_Anchor_Position_Type UI_Indexed_Element__Anchor_Position_Type
            => UI_Indexed_Element__Anchor.UI_Anchor__Target_Anchor_Point;

        public UI_Anchor_Sort_Type Get__Major_Sort_Type__UI_Indexed_Element
            => UI_Indexed_Element__Anchor.Get__Major_Sort_Type__UI_Anchor();

        public UI_Anchor_Sort_Type Get__Minor_Sort_Type__UI_Indexed_Element
            => UI_Indexed_Element__Anchor.Get__Minor_Sort_Type__UI_Anchor();
        
        /// <summary>
        /// The base offset of the wrapped element to the anchor. This is used for scaling.
        /// This is typically set once for static panels, and multiple times for dynamic ones - UI_Glide_Panel.
        /// This position typically represents the sorting end result.
        /// </summary>
        public Vector3 UI_Indexed_Element__Position_From_Anchor { get; private set; }
        /// <summary>
        /// The hypotenuse that is derived with the position relative to the anchor.
        /// This is mainly used with a ratio to the parent's UI_Rect hypotenuse. This ratio
        /// helps us convert the Parent's UI_Rect hypotenuse to this wrapped element's new UI_Rect
        /// Hypotenuse. The end result is a scaling that preserves size and area coverage.
        /// </summary>
        public float UI_Indexed_Element__Hypotenuse_To_Anchor { get; private set; }
        /// <summary>
        /// When this quotient is multiplied with the Parent's Hypotenuse, the end result is a
        /// properly scaled Child Hypotenuse. This is true for square scaling - that is, no
        /// breakage of initial ratio between width and height of the parent element.
        /// </summary>
        public float UI_Indexed_Element__Ratio_Of__Hypotenuse_To_Anchor__And__Parent_Hypotenuse { get; private set; }
        /// <summary>
        /// Normalized Vector for the anchor offset.
        /// </summary>
        /// <returns></returns>
        public Vector3 Get_Normalized__Position_From_Anchor__UI_Indexed_Element()
            => MathHelper.Get__Safe_Normalized(UI_Indexed_Element__Position_From_Anchor);
        public Vector3 Get__Current_Position_From_Anchor__UI_Indexed_Element()
            =>
                Get_Normalized__Position_From_Anchor__UI_Indexed_Element()
                * UI_Indexed_Element__Ratio_Of__Hypotenuse_To_Anchor__And__Parent_Hypotenuse
                * UI_Wrapper__CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element();

        internal void Set__Relative_Position_From_Anchor__UI_Indexed_Element(Vector3 offset)
        {
            UI_Indexed_Element__Position_From_Anchor = offset;
            UI_Indexed_Element__Hypotenuse_To_Anchor = MathHelper.Get__Hypotenuse(offset.Xy);
            UI_Indexed_Element__Ratio_Of__Hypotenuse_To_Anchor__And__Parent_Hypotenuse =
                UI_Indexed_Element__Hypotenuse_To_Anchor /
                UI_Wrapper__CONTAINER.Get__Hypotenuse_Of_Rect__UI_Element();
        }
        
        #endregion

        public UI_Anchored_Wrapper
        (
            UI_Element wrappedElement,
            UI_Container parentContainer,
            UI_Anchor bindingAnchor
        )
        : base (wrappedElement, parentContainer)
        {
            UI_Anchor_Position_Type localOrigin = wrappedElement.Get__Local_Origin_Position_Type__UI_Element();
            
            UI_Indexed_Element__Anchor = bindingAnchor;
        }

        public override string ToString()
        {
            return String.Format("Indexed_Element: [E: {0} A: {1}]", UI_Wrapper__WRAPPED_ELEMENT, UI_Indexed_Element__Anchor);
        }
    }
}
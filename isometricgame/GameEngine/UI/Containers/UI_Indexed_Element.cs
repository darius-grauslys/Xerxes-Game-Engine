using System;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// Wraps UI_Element and provides localized position logic.
    /// </summary>
    public class UI_Indexed_Element
    {
        /// <summary>
        /// The UI_Element that indexes this element.
        /// </summary>
        protected UI_Container UI_Indexed_Element__PARENT_CONTAINER { get; }

        public UI_Element UI_Indexed_Element__ELEMENT { get; }

        public UI_GameObject UI_Indexed_Element__Associated_UI_GameObject
            => UI_Indexed_Element__ELEMENT.UI_Element__Associated_UI_GameObject;
        
        #region Scaling
        private float UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT { get; }
        
        internal void Internal_Scale__Element__UI_Indexed_Element(float? newHypotenuse = null)
            => UI_Indexed_Element__ELEMENT.Internal_Scale__UI_Element
            (
                newHypotenuse 
                ??
                UI_Indexed_Element__PARENT_CONTAINER.UI_Element__Hypotenuse
                *
                UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT
            );
        
        #endregion
        
        #region Anchor
        /// <summary>
        /// Used for determining sorted positions.
        /// </summary>
        public UI_Anchor UI_Indexed_Element__Anchor { get; private set; }

        public UI_Anchor_Position_Type UI_Indexed_Element__Anchor_Position_Type
            => UI_Indexed_Element__Anchor.UI_Anchor__POSITION_TYPE;

        public UI_Anchor_Sort_Type UI_Indexed_Element__Major_Sort
            => UI_Indexed_Element__Anchor.UI_Anchor__MAJOR_SORT_TYPE;

        public UI_Anchor_Sort_Type UI_Indexed_Element__Minor_Sort
            => UI_Indexed_Element__Anchor.UI_Anchor__MINOR_SORT_TYPE;
        
        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        public Vector3 UI_Indexed_Element__Position_From_Anchor { get; private set; }
        public float UI_Indexed_Element__Hypotenuse_Of_Position_From_Anchor { get; private set; }
        public float UI_Indexed_Element__Ratio_Of_Hypotenuse_From_Anchor_To_Parent { get; private set; }
        public Vector3 Get_Normalized__Position_From_Anchor__UI_Indexed_Element()
            => MathHelper.Get__Safe_Normalized(UI_Indexed_Element__Position_From_Anchor);
        public Vector3 Get__Current_Position_From_Anchor__UI_Indexed_Element()
            =>
                Get_Normalized__Position_From_Anchor__UI_Indexed_Element()
                * UI_Indexed_Element__Ratio_Of_Hypotenuse_From_Anchor_To_Parent
                * UI_Indexed_Element__PARENT_CONTAINER.UI_Element__Hypotenuse;

        internal void Set__Relative_Position_From_Anchor__UI_Indexed_Element(Vector3 offset)
        {
            UI_Indexed_Element__Position_From_Anchor = offset;
            UI_Indexed_Element__Hypotenuse_Of_Position_From_Anchor = MathHelper.Get__Hypotenuse(offset.Xy);
            UI_Indexed_Element__Ratio_Of_Hypotenuse_From_Anchor_To_Parent =
                UI_Indexed_Element__Hypotenuse_Of_Position_From_Anchor /
                UI_Indexed_Element__PARENT_CONTAINER.UI_Element__Hypotenuse;
        }

        public UI_Indexed_Element
        (
            UI_Element indexedElement,
            UI_Anchor bindingAnchor,
            UI_Container parentContainer
        )
        {
            UI_Indexed_Element__ELEMENT = indexedElement;

            UI_Indexed_Element__Anchor = bindingAnchor ?? new UI_Anchor();
            
            UI_Indexed_Element__PARENT_CONTAINER = parentContainer;

            UI_Indexed_Element__RATIO_OF_HYPOTENUSE_TO_PARENT =
                UI_Indexed_Element__ELEMENT.UI_Element__Hypotenuse /
                UI_Indexed_Element__PARENT_CONTAINER.UI_Element__Hypotenuse;
        }
    }
}
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
        protected UI_Container UI_Indexed_Element__Parent_Container { get; private set; }

        public readonly UI_Element UI_Indexed_Element__ELEMENT;

        public UI_GameObject UI_Indexed_Element__Associated_UI_GameObject
            => UI_Indexed_Element__ELEMENT.UI_Element__Associated_UI_GameObject;
        
        #region Scaling
        private float UI_Indexed_Element__Ratio_Of_Hypotenuse_To_Parent { get; set; }
        
        internal void Internal_Scale__Element__UI_Indexed_Element(float? newHypotenuse = null)
            => UI_Indexed_Element__ELEMENT.Internal_Scale__UI_Element
            (
                newHypotenuse 
                ??
                UI_Indexed_Element__Parent_Container.UI_Element__Hypotenuse
                *
                UI_Indexed_Element__Ratio_Of_Hypotenuse_To_Parent
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
        public Vector3 UI_Indexed_Element__Relative_Position_From_Anchor { get; private set; }

        internal void Set__Relative_Position_From_Anchor__UI_Indexed_Element(Vector3 offset)
            => UI_Indexed_Element__Relative_Position_From_Anchor = offset;

        public UI_Indexed_Element
        (
            UI_Element indexedElement,
            UI_Anchor bindingAnchor,
            UI_Container parentContainer
        )
        {
            UI_Indexed_Element__ELEMENT = indexedElement;

            UI_Indexed_Element__Anchor = bindingAnchor ?? new UI_Anchor();
            
            UI_Indexed_Element__Parent_Container = parentContainer;

            UI_Indexed_Element__Ratio_Of_Hypotenuse_To_Parent =
                UI_Indexed_Element__ELEMENT.UI_Element__Hypotenuse /
                UI_Indexed_Element__Parent_Container.UI_Element__Hypotenuse;
        }
    }
}
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
        public readonly UI_Element UI_Indexed_Element__PARENT_CONTAINER;
        private Vector2 UI_Indexed_Element__Initial_Size_Of__PARENT_CONTAINER
            => UI_Indexed_Element__PARENT_CONTAINER.UI_Element__BOUNDING_RECT.UI_Rect__INITIAL_SIZE;
        public readonly UI_Element UI_Indexed_Element__WRAPPED_ELEMENT;

        public UI_GameObject UI_Indexed_Element__Associated_UI_GameObject_Of__WRAPPED_ELEMENT
            => UI_Indexed_Element__WRAPPED_ELEMENT.UI_Element__Associated_UI_GameObject;
        
        public Vector2 UI_Indexed_Element__Initial_Size_Of__WRAPPED_ELEMENT
            => UI_Indexed_Element__WRAPPED_ELEMENT.UI_Element__BOUNDING_RECT.UI_Rect__INITIAL_SIZE;
        
        public readonly float UI_Indexed_Element__INITIAL_AREA_PERCENTAGE_OF_CONTAINER;

        private readonly float UI_Indexed_Element__X_TO_Y__RATIO;

        public UI_Anchor_Position_Type Get__Anchor_Position_Type__UI_Indexed_Element()
            => UI_Anchor.Get__Internal_Position__From__Two_Internal_Sorts
            (
                UI_Indexed_Element__WRAPPED_ELEMENT.UI_Element__MAJOR_ANCHOR.UI_Anchor__Sort_Type__Internal,
                UI_Indexed_Element__WRAPPED_ELEMENT.UI_Element__LESSER_ANCHOR.UI_Anchor__Sort_Type__Internal
            );
        
        public Vector3 UI_Indexed_Element__Initial_Relative_Position_To_Anchor { get; private set; }
        internal void Internal_Bind__Initial_Position_Relative_To_Anchor(Vector3 relativePosition)
            => UI_Indexed_Element__Initial_Relative_Position_To_Anchor = relativePosition;

        internal void Internal_Scale__UI_Indexed_Element(Vector2? nonDefaultScaledSize = null)
        {
            UI_Indexed_Element__WRAPPED_ELEMENT.Internal_Scale__UI_Element(nonDefaultScaledSize ?? Internal_Get__Appropriated_Size__UI_Indexed_Element());
        }

        internal Vector2 Internal_Get__Appropriated_Size__UI_Indexed_Element()
        {
            float area = MathHelper.Area(UI_Indexed_Element__PARENT_CONTAINER.UI_Element__Size);
            float ratioArea = area
                              * UI_Indexed_Element__INITIAL_AREA_PERCENTAGE_OF_CONTAINER
                              * UI_Indexed_Element__X_TO_Y__RATIO;

            float width = (float) Math.Sqrt(ratioArea);

            return new Vector2(width, width * UI_Indexed_Element__X_TO_Y__RATIO);
        }

        public UI_Indexed_Element(UI_Element parentElement, UI_Element wrappedElement)
        {
            UI_Indexed_Element__PARENT_CONTAINER = parentElement;
            UI_Indexed_Element__WRAPPED_ELEMENT = wrappedElement;
            
            UI_Indexed_Element__INITIAL_AREA_PERCENTAGE_OF_CONTAINER =
                (float)Math.Sqrt
                (
                    MathHelper.Area_Ratio_Safe
                    (
                        UI_Indexed_Element__Initial_Size_Of__WRAPPED_ELEMENT,
                        UI_Indexed_Element__Initial_Size_Of__PARENT_CONTAINER
                    )
                );

            UI_Indexed_Element__X_TO_Y__RATIO =
                UI_Indexed_Element__Initial_Size_Of__WRAPPED_ELEMENT.Y
                /
                UI_Indexed_Element__Initial_Size_Of__WRAPPED_ELEMENT.X;
        }

        public override string ToString()
        {
            return String.Format("Wrapped: " + UI_Indexed_Element__WRAPPED_ELEMENT.ToString());
        }
    }
}
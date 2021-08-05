using System;
using OpenTK;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// Represents a UI element.
    /// </summary>
    public class UI_Element
    {
        public event Action<UI_Element> Event__Repositioned__UI_Element;
        public event Action<UI_Element> Event__Scaled__UI_Element;
        
        public UI_GameObject UI_Element__Associated_UI_GameObject { get; private set; }
        internal void Internal_Set__Associated_UI_GameObject__UI_Element(UI_GameObject uiGameObject)
            => UI_Element__Associated_UI_GameObject = uiGameObject;
        
        public bool UI_Element__Scales { get; set; }

        internal readonly UI_Rect UI_Element__BOUNDING_RECT;

        //Width
        public float UI_Element__Width => UI_Element__BOUNDING_RECT.UI_Rect__Width;
        public Vector3 UI_Element__Width__As_Vector3 => UI_Element__BOUNDING_RECT.UI_Rect__Width__As_Vector3;

        //Height
        public float UI_Element__Height => UI_Element__BOUNDING_RECT.UI_Rect__Height;
        public Vector3 UI_Element__Height__As_Vector3 => UI_Element__BOUNDING_RECT.UI_Rect__Height__As_Vector3;

        //Size
        public Vector2 UI_Element__Size => UI_Element__BOUNDING_RECT.UI_Rect__Size;

        public float UI_Element__Hypotenuse => UI_Element__BOUNDING_RECT.Get__Hypotenuse__UI_Rect();
        
        public Vector3 UI_Element__Position
            => UI_Element__BOUNDING_RECT.UI_Rect__Position;
        public Vector3 UI_Element__Position_Without_Local_Origin_Offset
            => UI_Element__BOUNDING_RECT.UI_Rect__Position__Without_Local_Origin_Offset;
        
        public Vector3 Get__Anchor_Position__UI_Element(UI_Anchor_Position_Type position)
            => UI_Element__BOUNDING_RECT.Internal_Get__Anchor_Position__UI_Rect(position);
        
        internal virtual void Internal_Set__Position__UI_Element(Vector3 position)
        {
            Internal_Set__Position_Silently__UI_Element(position);
            Event__Repositioned__UI_Element?.Invoke(this);
        }

        internal void Internal_Set__Position_Silently__UI_Element(Vector3 position)
        {
            UI_Element__BOUNDING_RECT.Internal_Set__Position__UI_Rect(position);
        }

        internal UI_Element(Vector2? size = null) 
            : this
                ( 
                new UI_Rect(size)
                )
        {
        }
        
        internal UI_Element
        (
            UI_Rect boundingRect,
            
            UI_GameObject associated_UIGameObject = null
        )
        {
            UI_Element__Scales = true;

            UI_Element__BOUNDING_RECT = boundingRect;
            
            Internal_Set__Associated_UI_GameObject__UI_Element(associated_UIGameObject);
        }

        internal void Internal_Resize__UI_Element(Vector2 newSize)
        {
            UI_Element__BOUNDING_RECT.Internal_Resize__UI_Rect(newSize);
            
            Internal_Scale__UI_Element(UI_Element__Hypotenuse);
        }
        
        internal void Internal_Scale__UI_Element(float hypotenuse)
        {
            if (UI_Element__Scales)
            {
                Internal_Handle_Scale__UI_Element(hypotenuse);
            
                Handle_Scale__UI_Element();
            
                Event__Scaled__UI_Element?.Invoke(this);
            }
        }

        internal virtual void Internal_Handle_Scale__UI_Element(float newHypotenuse)
        {
            UI_Element__BOUNDING_RECT.Internal_Rescale__UI_Rect(newHypotenuse);
        }

        protected virtual void Handle_Scale__UI_Element()
        {
            
        }

        public override string ToString()
        {
            return String.Format
            (
                "UI_Element ({0})", 
                UI_Element__Position
            );
        }
    }
}
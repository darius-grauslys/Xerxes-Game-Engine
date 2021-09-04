using System;
using OpenTK;
using Math_Helper = Xerxes_Engine.Tools.Math_Helper;

namespace Xerxes_Engine.UI
{
    /// <summary>
    /// Represents a UI element.
    /// </summary>
    public class UI_Element
    {
        public event Action<UI_Element> Event__Repositioned__UI_Element;
        public event Action<UI_Element> Event__Scaled__UI_Element;
        
        public UI_Game_Object UI_Element__Associated_UI_Game_Object { get; private set; }
        internal void Internal_Set__Associated_UI_Game_Object__UI_Element(UI_Game_Object uiGame_Object)
            => UI_Element__Associated_UI_Game_Object = uiGame_Object;
        
        public bool UI_Element__Scales { get; set; }

        internal UI_Rect UI_Element__BOUNDING_RECT { get; }

        //Width
        public float Get__Width__UI_Element() 
            => UI_Element__BOUNDING_RECT.UI_Rect__Width;
        public Vector3 Get__Width__As_Vector3__UI_Element() 
            => UI_Element__BOUNDING_RECT.UI_Rect__Width__As_Vector3;

        //Height
        public float Get__Height__UI_Element() 
            => UI_Element__BOUNDING_RECT.UI_Rect__Height;
        public Vector3 Get__Height__As_Vector3__UI_Element() 
            => UI_Element__BOUNDING_RECT.UI_Rect__Height__As_Vector3;

        //Size
        public Vector2 Get__Size__UI_Element() 
            => UI_Element__BOUNDING_RECT.UI_Rect__Size;
        public float Get__Hypotenuse_Of_Rect__UI_Element() 
            => UI_Element__BOUNDING_RECT.Get__Hypotenuse__UI_Rect();

        internal void Internal_Set__Local_Origin_Position_Type__UI_Element(UI_Anchor_Position_Type positionType)
            => UI_Element__BOUNDING_RECT.UI_Rect__Local_Origin_Type = positionType;

        public UI_Anchor_Position_Type Get__Local_Origin_Position_Type__UI_Element()
            => UI_Element__BOUNDING_RECT.UI_Rect__Local_Origin_Type;
        
        /// <summary>
        /// This position is used for proper UI positioning.
        /// [_UI_Rect__Position]
        /// </summary>
        /// <returns></returns>
        public Vector3 Get__Position_In_UISpace__UI_Element()
            => UI_Element__BOUNDING_RECT.Get__Position_In_UISpace__UI_Rect();

        /// <summary>
        /// This position is used for proper graphical rendering.
        /// [_UI_Rect__Position - UI_Rect__Local_Origin_Offset]
        /// </summary>
        /// <returns></returns>
        public Vector3 Get__Position_In_GameSpace__UI_Element()
            => UI_Element__BOUNDING_RECT.Get__Position_In_GameSpace__UI_Rect();
        public Vector3 Get__Local_Origin_Offset__UI_Element()
            => UI_Element__BOUNDING_RECT.UI_Rect__Local_Origin_Offset;
        
        /// <summary>
        /// Returns the position of an anchor point without the element's position offsetting it.
        /// </summary>
        /// <param name="positionType"></param>
        /// <returns></returns>
        public Vector3 Get__Local_Anchor_Position__UI_Element(UI_Anchor_Position_Type positionType)
            => UI_Element__BOUNDING_RECT.Internal_Get__Anchor_Point__UI_Rect(positionType);

        public Vector3 Get__Anchor_Position__UI_Element(UI_Anchor_Position_Type positionType)
            => UI_Element__BOUNDING_RECT.Internal_Get__Anchor_Position__UI_Rect(positionType);
        
        internal virtual void Internal_Set__Position__UI_Element(Vector3 position)
        {
            Internal_Set__Position_Silently__UI_Element(position);
            Event__Repositioned__UI_Element?.Invoke(this);
        }
        
        internal void Internal_Set__Position_Silently__UI_Element(Vector3 position)
        {
            UI_Element__BOUNDING_RECT.Internal_Set__Position__UI_Rect(position);
            Handle_Reposition__UI_Element();
        }

        protected virtual void Handle_Reposition__UI_Element()
        {
            
        }

        public UI_Element(Vector2? size = null) 
            : this
                ( 
                new UI_Rect(size)
                )
        {
        }
        
        public UI_Element
        (
            UI_Rect boundingRect
        )
        {
            UI_Element__Scales = true;
            UI_Element__BOUNDING_RECT = boundingRect;
        }

        internal void Internal_Resize__UI_Element(Vector2 newSize)
        {
            UI_Element__BOUNDING_RECT.Set__Scaling_Vector__UI_Rect(newSize);
            
            Internal_Scale__UI_Element(Math_Helper.Get__Hypotenuse(newSize));
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
                "UI_Element [{0}]", 
                UI_Element__BOUNDING_RECT
            );
        }
    }
}

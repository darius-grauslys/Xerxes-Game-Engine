using OpenTK;

namespace isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers
{
    public class UI_Element_Buffer : UI_Container
    {
        public UI_Element UI_Element_Buffer__ELEMENT { get; }
        
        public UI_Element_Buffer(UI_Element elementToBuffer, Vector4 axisOffsets) 
            : base
                (
                new UI_Rect
                    (
                    elementToBuffer.Get__Width__UI_Element() 
                    + axisOffsets.X + axisOffsets.Z, 
                    elementToBuffer.Get__Height__UI_Element()
                    + axisOffsets.Y + axisOffsets.W
                    )
                )
        {
            Vector3 anchorOffset = new Vector3
            (
                axisOffsets.X - axisOffsets.Z,
                axisOffsets.W - axisOffsets.Y,
                0
            );

            Add__UI_Element__UI_Container
            (
                elementToBuffer,
                new UI_Anchor
                    (
                    UI_Anchor_Position_Type.Middle,
                    UI_Anchor_Offset_Type.Pixel,
                    anchorOffset
                )
            );

            if (UI_Container__Element_Addition_Success_State)
                UI_Element_Buffer__ELEMENT = elementToBuffer;
        }
    }
}
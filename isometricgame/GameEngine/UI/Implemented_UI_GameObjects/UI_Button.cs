using System;
using System.Linq;
using isometricgame.GameEngine.UI.Components;
using isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers;
using isometricgame.GameEngine.UI.Frame_Arguments;
using OpenTK;

namespace isometricgame.GameEngine.UI.Implemented_UI_GameObjects
{
    public class UI_Button : UI_GameObject
    {
        private UI_Strict_Container UiButtonStrictContainer { get; }
        public UI_Text UI_Button__Text { get; }
        
        public UI_Button
            (
            UI_Scene_Layer sceneLayer, 
            string spriteAlias, 
            UI_Rect uiRect,
            Action<UI_MouseButton_Pulse_FrameArgument> clickHandler,
            string defaultText = "",
            string defaultFont = "font",
            string textFieldSprite = null,
            Vector2? textSize = null,
            params GameObject_Component[] components
            ) 
            : base
                (
                sceneLayer, 
                spriteAlias, 
                new UI_Strict_Container(uiRect), 
                Enumerable.Concat
                    (
                    new GameObject_Component[] { new UI_Clickable_Component(clickHandler) },
                    components
                    ).ToArray()
                )
        {
            UiButtonStrictContainer = Get__UI_Element__UI_GameObject() as UI_Strict_Container;
            UI_Button__Text = new UI_Text
            (
                sceneLayer,
                defaultText,
                defaultFont,
                textFieldSprite,
                UI_Anchor_Position_Type.Middle_Right,
                textSize
            );

            UiButtonStrictContainer.Add__UI_GameObject__UI_Strict_Panel
                (
                UI_Button__Text,
                new UI_Anchor
                (
                    UI_Anchor_Position_Type.Middle
                )
                );
        }
    }
}

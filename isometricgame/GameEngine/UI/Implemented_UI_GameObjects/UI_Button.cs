using System;
using System.Linq;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.UI.Components;
using isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers;
using OpenTK;

namespace isometricgame.GameEngine.UI.Implemented_UI_GameObjects
{
    public class UI_Button : UI_GameObject
    {
        private UI_Strict_Panel _UI_Button__STRICT_PANEL { get; }
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
                new UI_Strict_Panel(uiRect), 
                Enumerable.Concat
                    (
                    new GameObject_Component[] { new UI_Clickable_Component(clickHandler) },
                    components
                    ).ToArray()
                )
        {
            _UI_Button__STRICT_PANEL = Internal_UI_GameObject__UI_Element as UI_Strict_Panel;
            UI_Button__Text = new UI_Text
            (
                sceneLayer.Scene_Layer__Game.Game__Text_Displayer,
                sceneLayer,
                defaultText,
                defaultFont,
                textFieldSprite,
                null,
                textSize
            );

            _UI_Button__STRICT_PANEL.Add__UI_GameObject__UI_Strict_Panel
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
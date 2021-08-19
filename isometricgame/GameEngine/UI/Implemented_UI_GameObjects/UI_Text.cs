using System;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI.Implemented_UI_GameObjects
{
    public class UI_Text : UI_GameObject
    {
        private static readonly Vector2 DEFAULT_TEXT_SIZE = new Vector2(18, 28);
        private TextDisplayer UI_Text__TEXTDISPLAYER__Reference { get; }
        
        public string UI_Text__String { get; set; }
        public string UiTextDefaultFont { get; set; }
        
        public UI_Text
            (
            UI_Scene_Layer sceneLayer, 
            string defaultText = "",
            string defaultFont = "font",
            string spriteAlias = null, 
            UI_Anchor_Position_Type localOrigin = UI_Anchor_Position_Type.Bottom_Left,
            Vector2? textSize = null,
            params GameObject_Component[] components
            ) 
            : base
                (
                sceneLayer, 
                spriteAlias, 
                new UI_Element
                    (
                    new UI_Rect
                        (
                        textSize 
                        ?? 
                        MathHelper.Get__Hadamard_Product
                            (
                            DEFAULT_TEXT_SIZE,
                            new Vector2(defaultText.Length, 1)
                            ),
                        localOrigin
                        )
                    ), 
                components
                )
        {
            UI_Text__TEXTDISPLAYER__Reference = sceneLayer.Scene_Layer__Game.Game__Text_Displayer;
            UI_Text__String = defaultText;
            UiTextDefaultFont = defaultFont;
        }

        protected override void Handle_Draw__GameObject(RenderService renderService)
        {
            if (UI_Text__String == String.Empty)
                return;
            
            base.Handle_Draw__GameObject(renderService);

            Vector3 gameSpacePosition = Get__Game_Space_Position__UI_GameObject();
            
            UI_Text__TEXTDISPLAYER__Reference.DrawText
                (
                renderService,
                UI_Text__String,
                UiTextDefaultFont,
                gameSpacePosition.X,
                gameSpacePosition.Y
                );
        }
    }
}
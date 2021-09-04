using System;
using Xerxes_Engine.Systems.Graphics;
using OpenTK;
using Math_Helper = Xerxes_Engine.Tools.Math_Helper;
using Xerxes_Engine.Systems.Graphics.R2;

namespace Xerxes_Engine.UI.Implemented_UI_Game_Objects
{
    public class UI_Text : UI_Game_Object
    {
        private static readonly Vector2 DEFAULT_TEXT_SIZE = new Vector2(18, 28);
        private Text_Displayer UI_Text__TEXTDISPLAYER__Reference { get; }
        
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
            params Game_Object_Component[] components
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
                        Math_Helper.Get__Hadamard_Product
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

        protected override void Handle_Draw__Game_Object(Render_Service renderService)
        {
            if (UI_Text__String == String.Empty)
                return;
            
            base.Handle_Draw__Game_Object(renderService);

            Vector3 gameSpacePosition = Get__Game_Space_Position__UI_Game_Object();
            
            UI_Text__TEXTDISPLAYER__Reference.Draw__Text__Text_Displayer
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

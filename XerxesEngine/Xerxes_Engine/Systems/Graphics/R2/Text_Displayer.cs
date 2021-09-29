using System.Collections.Generic;

namespace Xerxes_Engine.Systems.Graphics.R2
{
    public sealed class Text_Displayer : Game_System
    {
        /*
        public const string Text_Displayer__CHARS = ",gjpqyABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhiklmnorstuvwxz1234567890.?!/-+@#$%^&*()_=[]\\{}|:;\"'<>`~";

        private Vertex_Object_Library _Game__Sprite_Library__REFERENCE;

        private Dictionary<string, int> _Text_Displayer__FONTS { get; }
        
        */

        internal Text_Displayer(Game game) 
            : base(game)
        {
            //_Text_Displayer__FONTS = new Dictionary<string, int>();
        }

        /*

        protected override void Handle_Load__Game_System()
        {
            _Game__Sprite_Library__REFERENCE = Game.Get_System__Game<Vertex_Object_Library>();
        }

        public void Load__Font__Text_Displayer(string fontName, int fontSpriteId)
        {
            _Text_Displayer__FONTS.Add(fontName, fontSpriteId);
        }

        public void Draw__Text__Text_Displayer(Render_Service renderService, string text, string fontName, float x, float y)
        {
            float fontWidth = _Game__Sprite_Library__REFERENCE.Get__Sprite_From_ID__Sprite_Library(_Text_Displayer__FONTS[fontName]).SubWidth;
            float fontHeight = _Game__Sprite_Library__REFERENCE.Get__Sprite_From_ID__Sprite_Library(_Text_Displayer__FONTS[fontName]).SubHeight;
            float descentGap = fontHeight / 2 + 1;
            float commaDescent = -3;
            float descentCharacter = -5;

            int font = _Text_Displayer__FONTS[fontName];

            float yWrite = y, xWrite = x, yOffset = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    xWrite += fontWidth;
                    continue;
                }
                if (text[i] == '\n')
                {
                    yWrite-= fontHeight + descentGap;
                    xWrite = x;
                    continue;
                }

                int index = Text_Displayer__CHARS.IndexOf(text[i]);

                yOffset = (index == 0) ? commaDescent : (index < 6) ? descentCharacter : 0;

                renderService.Draw__Sprite__Render_Service(font, xWrite, yWrite + yOffset, index);
                xWrite += fontWidth;
            }
        }
        */
    }
}

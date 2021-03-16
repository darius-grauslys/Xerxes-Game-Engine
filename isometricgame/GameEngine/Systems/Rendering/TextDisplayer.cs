using isometricgame.GameEngine.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using isometricgame.GameEngine.Systems.Rendering;

namespace isometricgame.GameEngine.Systems.Rendering
{
    public class TextDisplayer : GameSystem
    {
        public static readonly string CHARS = ",gjpqyABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhiklmnorstuvwxz1234567890.?!/-+@#$%^&*()_=[]\\{}|:;\"'<>`~";

        private SpriteLibrary SpriteLibrary;

        private Dictionary<string, int> fonts = new Dictionary<string, int>();

        public TextDisplayer(Game game) 
            : base(game)
        {

        }

        public override void Load()
        {
            SpriteLibrary = Game.GetSystem<SpriteLibrary>();
        }

        public void LoadFont(string fontName, int fontSpriteId)
        {
            fonts.Add(fontName, fontSpriteId);
        }

        public void DrawText(RenderService renderService, string text, string fontName, float x, float y)
        {
            float fontWidth = SpriteLibrary.sprites[fonts[fontName]].SubWidth;
            float fontHeight = SpriteLibrary.sprites[fonts[fontName]].SubHeight;
            float descentGap = fontHeight / 2 + 1;
            float commaDescent = -3;
            float descentCharacter = -5;

            int font = fonts[fontName];

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

                int index = CHARS.IndexOf(text[i]);

                yOffset = (index == 0) ? commaDescent : (index < 6) ? descentCharacter : 0;

                renderService.DrawSprite(font, xWrite, yWrite + yOffset, index);
                xWrite += fontWidth;
            }
        }
    }
}

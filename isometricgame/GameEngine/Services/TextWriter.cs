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

namespace isometricgame.GameEngine.Services
{
    public class TextWriter : GameService
    {
        public readonly string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890.,?!/-+@#$%^&*()_ =[]{}|:;\"'<>`~";

        private Dictionary<string, FixedSpriteSet> fonts = new Dictionary<string, FixedSpriteSet>();

        public TextWriter(Game game) 
            : base(game)
        {

        }

        public void LoadFont(string fontName, FixedSpriteSet set)
        {
            fonts.Add(fontName, set);
        }

        public void DrawText(RenderService renderService, string text, string fontName, float x, float y)
        {
            float fontWidth = fonts[fontName].TextureWidth;
            float fontHeight = fonts[fontName].TextureHeight;
            float yWrite = y, xWrite = x;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    xWrite += fontWidth;
                    continue;
                }
                if (text[i] == '\n')
                {
                    yWrite+= fontHeight;
                    xWrite = x;
                    continue;
                }
                renderService.DrawSprite(fonts[fontName].GetSprite(CHARS.IndexOf(text[i])), xWrite, yWrite);
                xWrite += fontWidth;
            }
        }
    }
}

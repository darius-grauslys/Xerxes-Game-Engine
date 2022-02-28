
using Xerxes.Xerxes_OpenTK.Exports.Graphics;
using Xerxes.Xerxes_OpenTK.Exports.Serialization;

namespace Xerxes.Xerxes_OpenTK.Templates
{
    public class Game_R2 :
        OpenTK_Game
    {
        public Game_R2()
        {
            Declare__Endpoint<Render_Service>();
            Declare__Endpoint<Texture_R2_Generator>();

            Declare__Streams()
                .Upstream.Extending<SA__Load_Texture_R2>();
        }

        protected Texture_R2 Protected_Load__Texture_R2__Game_R2(string file)
        {
            SA__Load_Texture_R2 e_load_texture =
                new SA__Load_Texture_R2(file);

            Invoke__Ascending(e_load_texture);

            return e_load_texture.Load_Texture_R2__Texture;
        }
    }
}

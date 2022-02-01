
using Xerxes_Engine.Export_OpenTK.Exports.Graphics;
using Xerxes_Engine.Export_OpenTK.Exports.Serialization;

namespace Xerxes_Engine.Export_OpenTK.Templates
{
    public class Game_R2 :
        Game
    {
        public Game_R2()
        {
            Declare__Export<Render_Service>();
            Declare__Export<Texture_R2_Generator>();

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

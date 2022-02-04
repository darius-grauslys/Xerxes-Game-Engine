
namespace Xerxes.Game_Engine.Graphics
{
    public class SA__Load_Texture_2D :
        Streamline_Argument
    {
        public string Load_Texture_2D__Resource_Path { get; set; }
        public ITexture_2D Load_Texture_2D__Texture__Returned { get; set; }

        public SA__Load_Texture_2D(string path)
        {
            Load_Texture_2D__Resource_Path = path;
        }
    }
}

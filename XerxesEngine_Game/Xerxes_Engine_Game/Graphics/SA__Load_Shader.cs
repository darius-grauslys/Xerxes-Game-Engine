
namespace Xerxes.Game_Engine.Graphics
{
    public sealed class SA__Load_Shader :
        Streamline_Argument
    {
        public string Load_Shader__Resource_Path { get; set; }
        public string Load_Shader__Shader_Name { get; set; }
        public Shader.Shader_ID Load_Shader__Shader_ID__Returned { get; internal set; }

        public SA__Load_Shader()
        {}
    }
}

namespace Xerxes_Engine
{
    public class SA__Associate_Root : Streamline_Argument
    {
        public string Associate_Root__BASE_DIRECTORY { get; }
        public string Associate_Root__ASSET_DIRECTORY { get; }
        public string Associate_Root__SHADER_DIRECTORY { get; }

        public int Associate_Root__WIDTH { get; }
        public int Associate_Root__HEIGHT { get; }

        internal string[] Associate_Root__SHADERS__Internal { get; }

        internal SA__Associate_Root
        (
            double elapsedTime,
            double deltaTime,
            
            string base_Directory,
            string asset_Directory,
            string shader_Directory,

            int width,
            int height,

            params string[] shaders
        )
        : base
        (
            elapsedTime,
            deltaTime
        )
        {
            Associate_Root__BASE_DIRECTORY = base_Directory;
            Associate_Root__ASSET_DIRECTORY = asset_Directory;
            Associate_Root__SHADER_DIRECTORY = shader_Directory;

            Associate_Root__WIDTH = width;
            Associate_Root__HEIGHT = height;

            Associate_Root__SHADERS__Internal = shaders;
        }
    }
}

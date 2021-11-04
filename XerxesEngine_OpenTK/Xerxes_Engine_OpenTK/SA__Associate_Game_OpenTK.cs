namespace Xerxes_Engine.Export_OpenTK
{
    public sealed class SA__Associate_Game_OpenTK :
        SA__Associate_Root
    {
        public string Associate_Root__BASE_DIRECTORY { get; }
        public string Associate_Root__ASSET_DIRECTORY { get; }
        public string Associate_Root__SHADER_DIRECTORY { get; }

        public int Associate_Root__WIDTH { get; }
        public int Associate_Root__HEIGHT { get; }

        internal string[] Associate_Root__SHADERS__Internal { get; }

        internal SA__Associate_Game_OpenTK 
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

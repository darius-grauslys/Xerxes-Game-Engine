namespace Xerxes_Engine.Export_OpenTK
{
    public sealed class SA__Load_Texture_R2 :
        Streamline_Argument
    {
        public string Load_Texture_R2__FILE_PATH            { get; }
        public bool Load_Texture_R2__IS_PIXELATED           { get; }
        public Texture_R2 Load_Texture_R2__Texture          { get; private set; }
        private bool _Load_Texture_R2__Is_Internally_Set    { get; set; }

        public SA__Load_Texture_R2
        (
            string filePath,
            bool pixelated = false
        )
        {
            Load_Texture_R2__FILE_PATH =
                filePath;
            Load_Texture_R2__IS_PIXELATED =
                pixelated;
        }

        internal void Internal_Set__Texture_R2_Alias__SA
        (Texture_R2 texture_R2)
        {
            _Load_Texture_R2__Is_Internally_Set = true;
            Load_Texture_R2__Texture = texture_R2;
        }
    }
}

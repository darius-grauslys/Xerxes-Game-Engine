using System.Drawing;
using System.IO;

namespace Xerxes_Engine.Systems.Serialization
{
    public sealed class Asset_Pipe : Game_System
    {
        private string _Asset_Pipe__ASSET_DIRECTORY { get; }

        internal Asset_Pipe(Game game) 
            : base(game)
        {
            _Asset_Pipe__ASSET_DIRECTORY =
                game.Game__DIRECTORY__ASSETS;
        }

        #region Texture2Ds

        public Texture_R2? Load__Texture_R2__Asset_Pipe(string localPath, bool pixelated = true)
        {
            string realizedPath = Path.Combine(_Asset_Pipe__ASSET_DIRECTORY, localPath);

            if (!File.Exists(realizedPath))
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log.ERROR__ASSET_PIPE__FILE_NOT_FOUND_1,
                    this,
                    realizedPath
                );
                return null; 
            }

            Bitmap bmp = new Bitmap(realizedPath);

            Texture_R2 texture = new Texture_R2(bmp, pixelated);
                        
            return texture;
        }

        #endregion
    }
}

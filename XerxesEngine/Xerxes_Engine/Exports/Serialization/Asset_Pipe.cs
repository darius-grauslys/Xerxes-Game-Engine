using System.Drawing;
using System.IO;

namespace Xerxes_Engine.Systems.Serialization
{
    public sealed class Asset_Pipe : 
        Xerxes_Export
    {
        private string _Asset_Pipe__Asset_Directory { get; set; }

        public Asset_Pipe() 
        {
        }

        protected override void Handle__Rooted__Xerxes_Export()
        {
            Protected_Declare__Catch__Xerxes_Export
                <SA__Load_Texture_R2>
                (
                    Private_Handle__Load_Texture_R2__Asset_Pipe
                );
        }

        protected override void Handle__Associate_Game__Xerxes_Export
        (SA__Associate_Game e)
        {
            _Asset_Pipe__Asset_Directory =
                e
                .SA__Associate_Game__GAME
                .Game__DIRECTORY__ASSETS;
        }

        #region Texture2Ds

        private void Private_Handle__Load_Texture_R2__Asset_Pipe
        (
            SA__Load_Texture_R2 e
        )
        {
            string realizedPath = 
                Path.Combine
                (
                    _Asset_Pipe__Asset_Directory, 
                    e.Load_Texture_R2__FILE_PATH
                );

            if (!File.Exists(realizedPath))
            {
                Log.Internal_Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log.ERROR__ASSET_PIPE__FILE_NOT_FOUND_1,
                    this,
                    realizedPath
                );
            }

            Bitmap bmp = new Bitmap(realizedPath);

            Texture_R2 texture = new Texture_R2
            (
                bmp, 
                e.Load_Texture_R2__IS_PIXELATED
            );
                        
            e
            .Internal_Set__Texture_R2_Alias__SA
            (texture);
        }

        #endregion
    }
}

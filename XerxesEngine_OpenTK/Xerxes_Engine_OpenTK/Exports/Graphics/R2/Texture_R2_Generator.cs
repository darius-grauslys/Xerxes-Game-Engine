using System.Drawing;
using System.IO;

namespace Xerxes_Engine.Export_OpenTK.Exports.Serialization
{
    public sealed class Texture_R2_Generator : 
        OpenTK_Export
    {
        private string _Texture_R2_Generator__Asset_Directory { get; set; }

        public Texture_R2_Generator() {}

        protected override void Handle__Rooted__Xerxes_Export()
        {
            Declare__Receiving
                <SA__Load_Texture_R2>
                (
                    Private_Handle__Load_Texture_R2__Texture_R2_Generator
                );
            Declare__Receiving
                <SA__Create_Texture_R2>
                (
                    Private_Handle__Create_Texture_R2__Texture_R2_Generator
                );
            Declare__Receiving
                <SA__Sealed_Under_Game>
                ((e) => {});
        }

        protected override void Handle__Associate_Root__Xerxes_Export
        (SA__Associate_Game_OpenTK e)
        {
            _Texture_R2_Generator__Asset_Directory =
                e
                .Associate_Root__ASSET_DIRECTORY;
        }

        private void Private_Handle__Load_Texture_R2__Texture_R2_Generator
        (
            SA__Load_Texture_R2 e
        )
        {
            string realizedPath = 
                Path.Combine
                (
                    _Texture_R2_Generator__Asset_Directory, 
                    e.Load_Texture_R2__FILE_PATH
                );

            if (!File.Exists(realizedPath))
            {
                Log.Write__Log
                (
                    Log_Message_Type.Error__IO,
                    Log_Messages__OpenTK.ERROR__ASSET_PIPE__FILE_NOT_FOUND_1,
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

        private void Private_Handle__Create_Texture_R2__Texture_R2_Generator
        (
            SA__Create_Texture_R2 e
        )
        {
            if (e.Create_Texture_R2__IS_1D_ARRAY)
            {
                e.Create_Texture_R2__Texture_R2__Returned =
                    new Texture_R2
                    (
                        e.Create_Texture_R2__BYTE_ARRAY,
                        e.Create_Texture_R2__WIDTH,
                        e.Create_Texture_R2__HEIGHT,
                        e.Create_Texture_R2__IS_PIXELATED
                    );

                return;
            }

            e.Create_Texture_R2__Texture_R2__Returned =
                new Texture_R2
                (
                    e.Create_Texture_R2__CHANNEL_ARRAY,
                    e.Create_Texture_R2__WIDTH,
                    e.Create_Texture_R2__HEIGHT,
                    e.Create_Texture_R2__IS_PIXELATED
                );
        }
    }
}

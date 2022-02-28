using System.Drawing;
using System.IO;
using Xerxes.Game_Engine;

namespace Xerxes.Xerxes_OpenTK.Exports.Serialization
{
    public sealed class Texture_R2_Generator : 
        Xerxes_Endpoint
    {
        private string _Texture_R2_Generator__Asset_Directory { get; set; }

        public Texture_R2_Generator()
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
            Declare__Receiving
                <SA__Configure_Root>
                (Private_Configure__Root__Texture_R2_Generator);
        }

        private void Private_Configure__Root__Texture_R2_Generator
        (SA__Configure_Root e)
        {
            _Texture_R2_Generator__Asset_Directory =
                OpenTK_Game
                .Game__Directory_Assets;
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

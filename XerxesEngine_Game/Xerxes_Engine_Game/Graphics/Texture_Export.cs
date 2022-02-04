
namespace Xerxes.Game_Engine.Graphics
{
    public abstract class Texture_Export<Args> :
        Game_Export<Args>
        where Args : SA__Configure_Root
    {
        protected override void Handle__Rooted__Xerxes_Export()
        {
            Declare__Receiving<SA__Load_Texture_2D>(Handle_Load__Texture__Texture_Export);
            Declare__Receiving<SA__Create_Texture_2D>(Handle_Create__Texture__Texture_Export);
        }

        protected abstract void Handle_Load__Texture__Texture_Export
        (SA__Load_Texture_2D e);

        protected abstract void Handle_Create__Texture__Texture_Export
        (SA__Create_Texture_2D e);
    }
}

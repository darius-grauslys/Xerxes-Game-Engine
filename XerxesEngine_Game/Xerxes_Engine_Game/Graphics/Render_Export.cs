
namespace Xerxes.Game_Engine.Graphics
{
    public abstract class Render_Export<Args> :
        Game_Export<Args>
        where Args : SA__Configure_Root
    {
        protected override void Handle__Rooted__Xerxes_Export()
        {
            Declare__Receiving<SA__Begin_Render>(Handle_Begin__Render_Export);
            Declare__Receiving<SA__Draw>(Handle_Draw__Render_Export);
            Declare__Receiving<SA__End_Render>(Handle_End__Render_Export);
            Declare__Receiving<SA__Load_Shader>(Internal_Handle_Load__Shader__Render_Export);
        }

        protected abstract void Handle_Begin__Render_Export
        (SA__Begin_Render e);

        protected abstract void Handle_Draw__Render_Export
        (SA__Draw e);

        protected abstract void Handle_End__Render_Export
        (SA__End_Render e);

        internal void Internal_Handle_Load__Shader__Render_Export
        (SA__Load_Shader e)
        {
            Shader.Shader_ID id =
                Handle_Load__Shader__Render_Export(e);

            e.Load_Shader__Shader_ID__Returned = id;
        }

        protected abstract Shader.Shader_ID Handle_Load__Shader__Render_Export
        (SA__Load_Shader e);
    }
}

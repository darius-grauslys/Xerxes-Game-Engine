
namespace Xerxes.Game_Engine.Graphics
{
    public abstract class Vertex_Object_Export<Args> :
        Game_Export<Args>
        where Args : SA__Configure_Root
    {
        protected override void Handle__Rooted__Xerxes_Export()
        {
            Declare__Receiving<SA__Declare_Vertex_Object>
            (Handle_Create__Vertex_Object__Vertex_Object_Export);
        }

        protected abstract void Handle_Create__Vertex_Object__Vertex_Object_Export
        (SA__Declare_Vertex_Object e);
    }
}

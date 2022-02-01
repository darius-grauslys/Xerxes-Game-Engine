using OpenTK;

namespace Xerxes_Engine.Export_OpenTK
{
    public sealed class SA__Render_Begin :
        SA__Chronical
    {
        internal Matrix4 Render_Begin__Projection_Matrix { get; set; }
        internal Matrix4 Render_Begin__World_Matrix { get; set; }

        internal SA__Render_Begin(SA__Chronical e)
            : base(e)
        {

        }
    }
}


using OpenTK;
using Xerxes.Game_Engine;

namespace Xerxes.Xerxes_OpenTK
{
    public class Entity_Render_System :
        Xerxes_System<IFeature__Render_Target, SA__Operate_Frame_Feature<IFeature__Render_Target>>
    {
        private SA__Draw _Entity_Render_System__DRAW { get; set; }

        public Entity_Render_System()
        {
            _Entity_Render_System__DRAW =
                new SA__Draw(null);

            Declare__Streams()
                .Upstream.Extending<SA__Draw>();
        }

        protected override void Handle_Operate__Feature__System(SA__Operate_Frame_Feature<IFeature__Render_Target> e)
        {
            IFeature__Render_Target feature = e.Operate_Feature__Feature;
            
            _Entity_Render_System__DRAW
                .Draw__Position =
                new Vector3(feature.X, feature.Y, feature.Z);
            _Entity_Render_System__DRAW
                .Draw__Vertex_Object =
                feature.Vertex_Object;

            Invoke__Ascending(_Entity_Render_System__DRAW);
        }
    }
}

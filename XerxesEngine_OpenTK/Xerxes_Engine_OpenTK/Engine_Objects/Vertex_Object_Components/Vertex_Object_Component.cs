

namespace Xerxes.Xerxes_OpenTK.Engine_Objects.Vertex_Object_Components
{
    public class Vertex_Object_Component :
        Xerxes_Object<Vertex_Object_Component>
    {
        protected Vertex_Object Vertex_Object_Component__Vertex_Object { get; set; }

        public Vertex_Object_Component()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Draw>(Handle__Draw__Vertex_Object_Component);

            Declare__Field<Vertex_Object>
            (
                () => Vertex_Object_Component__Vertex_Object,
                (vertex_object) => Vertex_Object_Component__Vertex_Object = vertex_object
            );
        }

        protected void Handle__Set_VOH__Vertex_Object_Component
        (
            SA__Set_Vertex_Object e
        )
        {
            Vertex_Object_Component__Vertex_Object =
                e.Set_Vertex_Object__VERTEX_OBJECT;
        }

        protected void Handle__Draw__Vertex_Object_Component
        (
            SA__Draw e
        )
        {
            e.Draw__Vertex_Object =
                Vertex_Object_Component__Vertex_Object;
        }
    }
}

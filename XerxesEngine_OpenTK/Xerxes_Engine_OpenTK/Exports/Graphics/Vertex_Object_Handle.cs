namespace Xerxes_Engine.Export_OpenTK
{
    public class Vertex_Object_Handle : Distinct_Handle
    {
        internal Vertex_Object_Dictionary Vertex_Object_Handle__Source__Internal { get; } 

        internal Vertex_Object Internal_Get__Vertex_Object__Vertex_Object_Handle()
            => 
            Vertex_Object_Handle__Source__Internal
            .Internal_Get__Vertex_Object__Vertex_Object_Dictionary(this);

        internal Vertex_Object_Handle(string internalHandle, Vertex_Object_Dictionary source)
            : base(internalHandle, source)
        {
            Vertex_Object_Handle__Source__Internal = source;
        }
    }
}

namespace Xerxes_Engine
{
    internal class Vertex_Object_Dictionary : Distinct_Handle_Dictionary<Vertex_Object_Handle, Vertex_Object>
    {
        private const string VERTEX_OBJECT_DICTIONARY__BASE_FORMAT = "Vertex_Object";

        internal Vertex_Object Internal_Get__Vertex_Object__Vertex_Object_Dictionary(Vertex_Object_Handle internalHandle)
            => Protected_Get__Element__Distinct_Handle_Dictionary(internalHandle);
        internal Vertex_Object[] Internal_Get__Vertex_Objects__Vertex_Object_Dictionary(Vertex_Object_Handle[] internalHandles)
            => Protected_Get__Elements__Distinct_Handle_Dictionary(internalHandles);

        internal Vertex_Object_Dictionary() 
        { }

        internal Vertex_Object_Handle Internal_Declare__Vertex_Object__Vertex_Object_Dictionary
        (
            Vertex_Object vertex_Object    
        )
            => 
            Protected_Declare__Element__Distinct_Handle_Dictionary
            (
                VERTEX_OBJECT_DICTIONARY__BASE_FORMAT,
                vertex_Object
            );
        internal Vertex_Object_Handle[] Internal_Declare__Vertex_Objects__Vertex_Object_Dictionary
        (
            Vertex_Object[] vertex_Objects
        )
        {
            Vertex_Object_Handle[] vertex_Object_Handles = new Vertex_Object_Handle[vertex_Objects.Length];
            for(int i=0;i<vertex_Objects.Length;i++)
                vertex_Object_Handles[i] =
                    Protected_Declare__Element__Distinct_Handle_Dictionary
                    (
                        VERTEX_OBJECT_DICTIONARY__BASE_FORMAT,
                        vertex_Objects[i]
                    );
            return vertex_Object_Handles;
        }

        internal void Internal_Dispose__Vertex_Objects__Vertex_Object_Dictionary()
        {
            Vertex_Object_Handle[] vertex_Object_Handles = Protected_Get__Keys__Distinct_Handle_Dictionary();
            foreach(Vertex_Object_Handle vertex_Object_Handle in vertex_Object_Handles)
            {
                Vertex_Object vertex_Object = Protected_Get__Element__Distinct_Handle_Dictionary(vertex_Object_Handle); 
                Protected_Remove__Element__Distinct_Handle_Dictionary(vertex_Object_Handle);
                vertex_Object.Dispose();
            }
        }

        protected override Vertex_Object_Handle Handle_Get__New_Handle__Distinct_Handle_Dictionary
        (
            string internalStringHandle
        )
        {
            return new Vertex_Object_Handle(internalStringHandle, this);
        }
    }
}

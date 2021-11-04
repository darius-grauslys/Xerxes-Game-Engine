using System;
using System.Linq;
using OpenTK.Graphics.OpenGL;

namespace Xerxes_Engine.Export_OpenTK
{
    /// <summary>
    /// Represents a collection of verticies with the intent
    /// of representing something during render.
    /// </summary>
    public class Vertex_Object : IDisposable
    {
        public const int VERTEX_OBJECT__BASE_VERTEX_COUNT = 4;

        public Vertex_Object_Handle Vertex_Object__HANDLE { get; internal set; }

        private Vertex[] _Vertex_Object__Base_Vertex_Array { get; set; }
        private Vertex[] _Vertex_Object__VERTICES { get; }
        public int Get__Vertex_Count__Vertex_Object()
            => _Vertex_Object__VERTICES.Length;
        public Texture_R2 Vertex_Object__TEXTURE_R2 { get; }

        public int Vertex_Object__GL_BUFFER_ID { get; private set; }
        public int Vertex_Object__GL_VERTEX_ARRAY_ID { get; private set; }

        internal Vertex_Object(Vertex[] vertices, Texture_R2 texture_R2)
            : this(vertices.Length, texture_R2)
        {
            _Vertex_Object__VERTICES = vertices;
            Internal_Set__Base_Array();
            Internal_Set__Buffer_Data__Vertex_Object();
        }

        internal Vertex_Object(int count, Texture_R2 texture_R2)
        { 
            // Generate the buffer for vertice info on the gpu.
            Vertex_Object__GL_BUFFER_ID = GL.GenBuffer();
            // Generate the id for this object on the gpu.
            Vertex_Object__GL_VERTEX_ARRAY_ID = GL.GenVertexArray();
            Vertex_Object__TEXTURE_R2 = texture_R2;
            _Vertex_Object__VERTICES = new Vertex[count];

            GL.BindVertexArray(Vertex_Object__GL_VERTEX_ARRAY_ID);
            Internal_Bind__Buffer__Vertex_Object();
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 8 * sizeof(float), 4 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            GL.BindVertexArray(0);
        }

        internal void Internal_Set__Base_Array()
        {
            if (_Vertex_Object__Base_Vertex_Array != null)
                return;

            _Vertex_Object__Base_Vertex_Array =
                _Vertex_Object__VERTICES.ToArray();
        }

        private void Private_Modify__Vertex_Array__Vertex_Object
        (
            Vertex[] referenceArray,
            int modificationIndex,
            int modificationRange,
            Func<Vertex[], int, Vertex> vertexModification
        )
        {
            bool isValidIndex, isValidRange, isValidMethod;

            isValidIndex = 
                Private_Check_For__Valid_Modification_Index__Vertex_Object
                (
                    referenceArray,
                    modificationIndex
                );
            isValidRange =
                Private_Check_For__Valid_Modification_Range__Vertex_Object
                (
                    referenceArray,
                    modificationIndex,
                    modificationRange
                );
            isValidMethod =
                Private_Check_For__Valid_Modification_Method__Vertex_Object
                (
                    vertexModification
                );

            if (!isValidIndex || !isValidRange || !isValidMethod)
                return;

            for(int i=modificationIndex;i<modificationRange;i++)
            {
                _Vertex_Object__VERTICES[i] = vertexModification.Invoke(referenceArray, i);
            }
        }

        internal void Internal_Modify__Vertex_Array__Vertex_Object
        (
            int modificationIndex,
            Vertex[] modificaiton
        )
        {
            Private_Modify__Vertex_Array__Vertex_Object
            (
                _Vertex_Object__VERTICES,
                modificationIndex,
                modificaiton.Length,
                (va, i) => modificaiton[i - modificationIndex]
            );
        }

        /// <summary>
        /// <paramref name="pullFromCurrent_OpposedToBase">
        /// If true, will feed the current array as vertex reference
        /// otherwise the base array. For scaling it is good to set
        /// this to false.
        /// </paramref>
        /// </summary>
        internal void Internal_Modify__Vertex_Array__Vertex_Object
        (
            int modificationIndex,
            int modificationRange,
            Func<Vertex[], int, Vertex> modificationMethod,
            bool pullFromCurrent_OpposedToBase = true
        )
        {
            Vertex[] referenceArray = 
                (pullFromCurrent_OpposedToBase)
                ? _Vertex_Object__VERTICES
                : _Vertex_Object__Base_Vertex_Array
                ;

            Private_Modify__Vertex_Array__Vertex_Object
            (
                referenceArray,
                modificationIndex,
                modificationRange,
                modificationMethod
            );
        }
    
        private bool Private_Check_For__Valid_Modification_Method__Vertex_Object
        (
            Func<Vertex[], int, Vertex> method
        )
        {
            if (method != null)
                return true;

            Private_Log_Error_Error__Invalid_Modification_Method
            (
                this,
                method
            );

            return false;
        }

        private bool Private_Check_For__Valid_Modification_Index__Vertex_Object
        (
            Vertex[] referenceArray,
            int modificationIndex
        )
        {
            bool isValid =
                Tools
                .Math_Helper
                .Check_If__Obeys_Range_Clamp__Positive_Integer
                (
                    modificationIndex,
                    referenceArray.Length
                );

            if (isValid)
                return true;

            Private_Log_Error_Error__Invalid_Modification_Index
            (
                this,
                modificationIndex,
                referenceArray.Length
            );

            return false;
        }

        private bool Private_Check_For__Valid_Modification_Range__Vertex_Object
        (
            Vertex[] referenceArray,
            int modificationIndex,
            int modificationRange
        )
        {
            bool isValidRange =
                Tools
                .Math_Helper
                .Check_If__Obeys_Range_Clamp__Positive_Integer
                (
                    modificationIndex + modificationRange,
                    referenceArray.Length
                );
            
            if (isValidRange)
                return true;

            Private_Log_Error_Error__Invalid_Modification_Range
            (
                this,
                modificationIndex,
                modificationRange,
                referenceArray.Length
            );

            return false;
        }

#region Internal GL Initalizations
        internal void Internal_Set__Buffer_Data__Vertex_Object()
        {
            GL.BindVertexArray(Vertex_Object__GL_VERTEX_ARRAY_ID);
            Internal_Bind__Buffer__Vertex_Object();
            GL.BufferData
            (
                BufferTarget.ArrayBuffer, 
                _Vertex_Object__VERTICES.Length * Vertex.SizeInBytes, 
                _Vertex_Object__VERTICES, 
                BufferUsageHint.StaticDraw
            );
            GL.BindVertexArray(0);
        }
#endregion

#region Internal GL Utilizations
        internal void Internal_Use__Vertex_Object()
        {
            GL.BindTexture(TextureTarget.Texture2D, Vertex_Object__TEXTURE_R2.ID);
            GL.BindVertexArray(Vertex_Object__GL_VERTEX_ARRAY_ID);
        }

        internal void Internal_Bind__Buffer__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Vertex_Object__GL_BUFFER_ID);
        }

        /// <summary>
        /// This should be moved to Render_Service.
        /// </summary>
        [Obsolete]
        public void Internal_Unbind__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
#endregion

#region Native Overloads
        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(Vertex_Object__GL_BUFFER_ID);
        }

        public override string ToString()
        {
            string str = "";
            foreach (Vertex vertex in _Vertex_Object__VERTICES)
            {
                str += vertex.ToString();
            }
            return str;
        }
#endregion

#region Static Logging
        private static void Private_Log_Error_Error__Invalid_Modification_Index
        (
            Vertex_Object source,
            int index,
            int length
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Rendering_Setup,
                Log_Messages__OpenTK.ERROR__VERTEX_OBJECT__INVALID_MODIFICATION_INDEX_2,
                source,
                index,
                length
            );
        }

        private static void Private_Log_Error_Error__Invalid_Modification_Range
        (
            Vertex_Object source,
            int index,
            int range,
            int length
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Rendering_Setup,
                Log_Messages__OpenTK.ERROR__VERTEX_OBJECT__MODIFICATION_OUT_OF_BOUNDS_3,
                source,
                index,
                range,
                length
            );
        }

        private static void Private_Log_Error_Error__Invalid_Modification_Method
        (
            Vertex_Object source,
            Func<Vertex[], int, Vertex> method
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Rendering_Setup,
                Log_Messages__OpenTK.ERROR__VERTEX_OBJECT__MODIFICATION_METHOD_IS_INVALID_1,
                source,
                method
            );
        }
#endregion
    }
}

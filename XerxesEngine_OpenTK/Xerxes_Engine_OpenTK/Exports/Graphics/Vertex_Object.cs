using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Xerxes_Engine.Tools;

namespace Xerxes_Engine.Export_OpenTK
{
    /// <summary>
    /// Represents a collection of verticies with the intent
    /// of representing something during render.
    /// </summary>
    public struct Vertex_Object : IDisposable
    {
        public const int VERTEX_OBJECT__BASE_VERTEX_COUNT = 4;

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
            _Vertex_Object__Base_Vertex_Array = null;

            GL.BindVertexArray(Vertex_Object__GL_VERTEX_ARRAY_ID);
            Internal_Bind__Buffer__Vertex_Object();
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, 5 * sizeof(float));
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
                Math_Helper
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
                Math_Helper
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
            GL.BindTexture(TextureTarget.Texture2D, Vertex_Object__TEXTURE_R2.Texture_R2__ID);
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

#region Static Utility 
        /// <summary>
        /// Creates the vertex data for a 2D texture.
        /// It uses the entire texture size.
        /// This is good for non-animated sprites.
        /// </summary>
        public static Vertex_Object Create
        (
            Texture_R2 texture,
            float vo_width = 1,
            float vo_height = 1,
            float? nullable_sub_width = null,
            float? nullable_sub_height = null,
            Integer_Vector_2[] nullable_indicies = null,
            Vector2[] nullable_offsets  = null
        )
        {

            Integer_Vector_2[] batchIndices = 
                nullable_indicies
                ?? 
                new Integer_Vector_2[] { new Integer_Vector_2() };

            Vector2[] batchPositions = 
                nullable_offsets
                ??
                new Vector2[] { new Vector2() };

            float sub_width = 
                nullable_sub_width
                ??
                texture.Texture_R2__Width;
            float sub_height =
                nullable_sub_height
                ??
                texture.Texture_R2__Height;

            Vertex[] batch = new Vertex[VERTEX_OBJECT__BASE_VERTEX_COUNT * batchIndices.Length];

            for(int i=0;i<batchIndices.Length;i++)
            {
                Integer_Vector_2 ivec = batchIndices[i];
                Vector2 position = new Vector2(vo_width * batchPositions[i].X, vo_height * batchPositions[i].Y);
                Vertex[] vertices = 
                    Private_Extract__Splice
                    (
                        texture.Texture_R2__Width,
                        texture.Texture_R2__Height,
                        vo_width,
                        vo_height,
                        sub_width,
                        sub_height,
                        ivec.Y, ivec.X,
                        position.X, position.Y
                    );
                for(int j=0;j<vertices.Length;j++)
                {
                    batch[i*VERTEX_OBJECT__BASE_VERTEX_COUNT + j]
                        = vertices[j];
                }
            }

            Vertex_Object vertex_object = new Vertex_Object(batch, texture);

            return vertex_object;
        }

        /// <summary>
        /// Creates an array of vertices which associate to
        /// a (offsetX,offsetY) + (0|subWidth,0|subHeight) position
        /// and associated texture coord.
        /// The associated texture coord is determined by
        /// (subWidth,subHeight) and the row/col indices.
        /// </summary>
        private static Vertex[] Private_Extract__Splice
        (
            float texture_R2_Width,
            float texture_R2_Height,
            float vo_width,
            float vo_height,
            float subWidth,
            float subHeight,
            int row,
            int col,
            float offsetX = 0,
            float offsetY = 0,
            float r = 0,
            float g = 0,
            float b = 0,
            float a = 0
        )
        {
            float textCoord_X = subWidth / texture_R2_Width;
            float textCoord_Y = subHeight / texture_R2_Height;

            float textCoord_X_A = textCoord_X * col;
            float textCoord_X_B = textCoord_X * (col + 1);
            float textCoord_Y_A = textCoord_Y * row;
            float textCoord_Y_B = textCoord_Y * (row + 1);

            float x_a = -offsetX, 
                  y_a = offsetY;
            float x_b = -offsetX, 
                  y_b = offsetY + vo_height;
            float x_c = -offsetX + vo_width, 
                  y_c = y_b;
            float x_d = x_c, 
                  y_d = y_a;

            Vertex[] vertices = new Vertex[]
            {
                new Vertex(x_d, y_d, 0,  textCoord_X_A, textCoord_Y_B,   r,g,b,a),
                new Vertex(x_c, y_c, 0,  textCoord_X_A, textCoord_Y_A,   r,g,b,a),
                new Vertex(x_b, y_b, 0,  textCoord_X_B, textCoord_Y_A,   r,g,b,a),
                new Vertex(x_a, y_a, 0,  textCoord_X_B, textCoord_Y_B,   r,g,b,a)
            };

            return vertices;
        }
#endregion

#region Logging
        private static void Private_Log_Error_Error__Invalid_Sub_Length
        (
            string subLengthString,
            float invalidSubLength
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Rendering_Setup,
                Log_Messages__OpenTK.ERROR__VERTEX_OBJECT__INVALID_SUB_LENGTH_2,
                typeof(Vertex_Object),
                subLengthString,
                invalidSubLength
            );
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

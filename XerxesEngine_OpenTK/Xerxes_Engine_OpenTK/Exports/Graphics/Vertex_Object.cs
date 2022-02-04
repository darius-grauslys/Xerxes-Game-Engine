using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Xerxes.Tools;

namespace Xerxes.Xerxes_OpenTK
{
    /// <summary>
    /// Represents a collection of verticies with the intent
    /// of representing something during render.
    /// </summary>
    public struct Vertex_Object : IDisposable
    {
        public const int BASE_VERTEX_COUNT = 4;

        private Vertex[] _Base_Vertex_Array { get; set; }
        private Vertex[] _VERTICES { get; }
        public int Get__Vertex_Count()
            => _VERTICES.Length;
        public Texture_R2 TEXTURE_R2 { get; }

        public int GL_BUFFER_ID { get; private set; }
        public int VERTEX_ARRAY_ID { get; private set; }

        public bool IS_PROPER { get; }

        internal Vertex_Object(Vertex[] vertices, Texture_R2 texture_R2)
            : this(vertices.Length, texture_R2)
        {
            _VERTICES = vertices;
            Internal_Set__Base_Array();
            Internal_Set__Buffer_Data__Vertex_Object();
        }

        internal Vertex_Object(int count, Texture_R2 texture_R2)
        { 
            IS_PROPER = true;
            // Generate the buffer for vertice info on the gpu.
            GL_BUFFER_ID = GL.GenBuffer();
            // Generate the id for this object on the gpu.
            VERTEX_ARRAY_ID = GL.GenVertexArray();
            TEXTURE_R2 = texture_R2;
            _VERTICES = new Vertex[count];
            _Base_Vertex_Array = null;

            GL.BindVertexArray(VERTEX_ARRAY_ID);
            Internal_Bind__Buffer__Vertex_Object();
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vertex.Size_In_Bytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vertex.Size_In_Bytes, 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Vertex.Size_In_Bytes, 5 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            GL.BindVertexArray(0);
        }

        internal void Internal_Set__Base_Array()
        {
            if (_Base_Vertex_Array != null)
                return;

            _Base_Vertex_Array =
                _VERTICES.ToArray();
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
                _VERTICES[i] = vertexModification.Invoke(referenceArray, i);
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
                _VERTICES,
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
                ? _VERTICES
                : _Base_Vertex_Array
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
            GL.BindVertexArray(VERTEX_ARRAY_ID);
            Internal_Bind__Buffer__Vertex_Object();
            GL.BufferData
            (
                BufferTarget.ArrayBuffer, 
                _VERTICES.Length * Vertex.Size_In_Bytes, 
                _VERTICES, 
                BufferUsageHint.StaticDraw
            );
            GL.BindVertexArray(0);
        }
#endregion

#region Internal GL Utilizations
        internal void Internal_Use__Vertex_Object()
        {
            GL.BindTexture(TextureTarget.Texture2D, TEXTURE_R2.ID);
            GL.BindVertexArray(VERTEX_ARRAY_ID);
        }

        internal void Internal_Bind__Buffer__Vertex_Object()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, GL_BUFFER_ID);
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
            GL.DeleteBuffer(GL_BUFFER_ID);
        }

        public override string ToString()
        {
            string str = "";
            foreach (Vertex vertex in _VERTICES)
            {
                str += vertex.ToString();
            }
            return str;
        }
#endregion

#region Static Utility 
        /// <summary>
        /// Creates the vertex data for a 2D texture.
        /// Use batch_indices to grab specific splices
        /// or even batch splices together by using
        /// multiple Batch_Indices.
        /// </summary>
        public static Vertex_Object Create
        (
            Texture_R2 texture,
            float optional_sub_width = -1,
            float optional_sub_height = -1,
            float optional_vo_width = -1,
            float optional_vo_height = -1,
            params Batch_Index[] batch_indices
        )
        {
            if (batch_indices == null)
                return new Vertex_Object(); //TODO: log

            Batch_Index[] utilized_batch_indices =
                batch_indices.Length > 0
                ? batch_indices
                : new Batch_Index[] { new Batch_Index() };

            float vo_width =
                optional_vo_width >= 0
                ? optional_vo_width
                : 1;
            float vo_height =
                optional_vo_height >= 0
                ? optional_vo_height
                : 1;

            float sub_width = 
                optional_sub_width >= 0
                ? optional_sub_width
                : texture.Width;
            float sub_height =
                optional_sub_height >= 0
                ? optional_sub_height
                : texture.Height;

            Vertex[] batch = new Vertex[BASE_VERTEX_COUNT * utilized_batch_indices.Length];

            for(int i=0;i<utilized_batch_indices.Length;i++)
            {
                Integer_Vector_2 ivec = utilized_batch_indices[i].Batch_Index__INDEX;
                Vector2 position = utilized_batch_indices[i].Batch_Index__OFFSET;
                Vertex[] vertices = 
                    Private_Extract__Splice
                    (
                        texture.Width,
                        texture.Height,
                        vo_width,
                        vo_height,
                        sub_width,
                        sub_height,
                        ivec.Y, ivec.X,
                        position.X, position.Y
                    );
                for(int j=0;j<vertices.Length;j++)
                {
                    batch[i*BASE_VERTEX_COUNT + j]
                        = vertices[j];
                }
            }

            Vertex_Object vertex_object = new Vertex_Object(batch, texture);

            return vertex_object;
        }

        /// <summary>
        /// Split a texture up into multple Vertex_Objects.
        /// Each Batch_Index constitutes to one Vertex_Object.
        /// </summary>
        public static Vertex_Object[] Splice
        (
            Texture_R2 texture,
            float sub_width,
            float sub_height,
            float optional_vo_width = -1,
            float optional_vo_height = -1,
            params Batch_Index[] batch_indices
        )
        {
            //TODO: Log
            sub_width = 
                sub_width < 0
                ? 1
                : sub_width
                ;
            sub_height = 
                sub_height < 0
                ? 1
                : sub_height 
                ;

            float vo_width = 
                optional_vo_width < 0
                ? 1
                : optional_vo_width
                ;
            float vo_height =
                optional_vo_height < 0
                ? 1
                : optional_vo_height
                ;

            Batch_Index[] utilized_batch_indices;

            if (batch_indices == null || batch_indices.Length == 0)
            {
                int cols = (int)(texture.Width  / sub_width);
                int rows = (int)(texture.Height / sub_height);

                utilized_batch_indices = new Batch_Index[rows * cols];
                for(int y=0;y<rows;y++)
                    for(int x=0;x<cols;x++)
                        utilized_batch_indices[y * rows + x] =
                            new Batch_Index(x,y);
            }
            else
                utilized_batch_indices = batch_indices;

            Vertex_Object[] vo_s = new Vertex_Object[utilized_batch_indices.Length];

            for(int i=0;i<utilized_batch_indices.Length;i++)
                vo_s[i] =
                    Create(texture, sub_width, sub_height, vo_width, vo_height, utilized_batch_indices[i]);

            return vo_s;
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

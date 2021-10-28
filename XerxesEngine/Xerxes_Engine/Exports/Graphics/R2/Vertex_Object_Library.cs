using OpenTK;

namespace Xerxes_Engine.Exports.Graphics.R2
{
    /// <summary>
    /// Creates and records Vertex_Objects. From here,
    /// sprites are able to bind to them. Eventually meshes too.
    /// </summary>
    public sealed class Vertex_Object_Library : 
        Xerxes_Export 
    {
        private const string VERTEX_OBJECT_LIBRARY__SUB_LENGTH_STRING__WIDTH  = "Sub-Width" ;
        private const string VERTEX_OBJECT_LIBRARY__SUB_LENGTH_STRING__HEIGHT = "Sub-Height";

        private const int VERTEX_OBJECT_LIBRARY__BASE_VERTEX_COUNT = 4;

        private Vertex_Object_Dictionary _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY { get; }

        public Vertex_Object_Library() 
        {
            _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY = new Vertex_Object_Dictionary();
        }

        protected override void Handle__Rooted__Xerxes_Export()
        {
            Protected_Declare__Catch__Xerxes_Export
                <SA__Declare_Vertex_Object>
                (
                    Private_Catch_Declare__Vertex_Object__Vertex_Object_Library
                );
        }

        protected override void Handle__Dissassociate_Game__Xerxes_Export
        (SA__Dissassociate_Game e)
        {
            _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY
                .Internal_Dispose__Vertex_Objects__Vertex_Object_Dictionary();
        }

        private void Private_Catch_Declare__Vertex_Object__Vertex_Object_Library
        (SA__Declare_Vertex_Object e)
        {
            Vertex_Object_Handle voh =
                Private_Declare__Vertex_Object__Vertex_Object_Library
                (
                    e
                );

            e.Declare_Vertex_Object__Vertex_Object_Handle__Internal = voh;
        }

        /// <summary>
        /// Creates the vertex data for a 2D texture.
        /// It uses the entire texture size.
        /// This is good for non-animated sprites.
        /// </summary>
        private Vertex_Object_Handle Private_Declare__Vertex_Object__Vertex_Object_Library
        (
            SA__Declare_Vertex_Object e
        )
        {
            Texture_R2 texture_R2 =
                e.Declare_Vertex_Object__TEXTURE_R2__Internal;

            Integer_Vector_2[] batchIndices = 
                e
                .Declare_Vertex_Object__BATCH_INDEX__Internal 
                ?? 
                new Integer_Vector_2[] { new Integer_Vector_2() };
            Integer_Vector_2[] batchPositions = 
                e
                .Declare_Vertex_Object__BATCH_POSITIONS__Internal
                ??
                new Integer_Vector_2[] { new Integer_Vector_2() };

            float subWidth = e.Declare_Vertex_Object__SPLICE_WIDTH__Internal;
            float subHeight = e.Delcare_Vertex_Object__SPLICE_HEIGHT__Internal;

            Vertex[] batch = new Vertex[VERTEX_OBJECT_LIBRARY__BASE_VERTEX_COUNT * batchIndices.Length];

            for(int i=0;i<batchIndices.Length;i++)
            {
                Integer_Vector_2 ivec = batchIndices[i];
                Vector2 ipos = new Vector2(subWidth * batchPositions[i].X, subHeight * batchPositions[i].Y);
                Vertex[] vertices = 
                    Private_Extract__Splice
                    (
                        texture_R2.Width,
                        texture_R2.Height,
                        subWidth,
                        subHeight,
                        ivec.Y, ivec.X,
                        ipos.X, ipos.Y
                    );
                for(int j=0;j<vertices.Length;j++)
                {
                    batch[i*VERTEX_OBJECT_LIBRARY__BASE_VERTEX_COUNT + j]
                        = vertices[j];
                }
            }

            Vertex_Object vertex_Object = new Vertex_Object(batch, texture_R2);

            Vertex_Object_Handle vertex_Object_Handle =
                _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY
                .Internal_Declare__Vertex_Object__Vertex_Object_Dictionary
                (
                    vertex_Object
                );

            return vertex_Object_Handle;
        }

#region Static Extraction
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

            float x_a = offsetX, 
                  y_a = offsetY;
            float x_b = offsetX, 
                  y_b = offsetY + subHeight;
            float x_c = offsetX + subWidth, 
                  y_c = y_b;
            float x_d = x_c, 
                  y_d = y_a;

            Vertex[] vertices = new Vertex[]
            {
                Vertex.Create(x_a, y_a,  textCoord_X_A, textCoord_Y_B,   r,g,b,a),
                Vertex.Create(x_b, y_b,  textCoord_X_A, textCoord_Y_A,   r,g,b,a),
                Vertex.Create(x_c, y_c,  textCoord_X_B, textCoord_Y_A,   r,g,b,a),
                Vertex.Create(x_d, y_d,  textCoord_X_B, textCoord_Y_B,   r,g,b,a)
            };

            return vertices;
        }
#endregion

#region Logging
        private static void Private_Log_Error__Invalid_Sub_Length
        (
            Vertex_Object_Library library,
            string subLengthString,
            float invalidSubLength
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Rendering_Setup,
                Log.ERROR__VERTEX_OBJECT__INVALID_SUB_LENGTH_2,
                library,
                subLengthString,
                invalidSubLength
            );
        }
#endregion
    }
}

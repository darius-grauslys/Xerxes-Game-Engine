namespace Xerxes_Engine.Systems.Graphics.R2
{
    /// <summary>
    /// Creates and records Vertex_Objects. From here,
    /// sprites are able to bind to them. Eventually meshes too.
    /// </summary>
    public sealed class Vertex_Object_Library : Game_System
    {
        private const string VERTEX_OBJECT_LIBRARY__SUB_LENGTH_STRING__WIDTH  = "Sub-Width" ;
        private const string VERTEX_OBJECT_LIBRARY__SUB_LENGTH_STRING__HEIGHT = "Sub-Height";

        private const int VERTEX_OBJECT_LIBRARY__BASE_VERTEX_COUNT = 4;

        private Vertex_Object_Dictionary _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY { get; }

        internal Vertex_Object_Library(Game gameRef) 
            : base(gameRef)
        {
            _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY = new Vertex_Object_Dictionary();
        }

        protected override void Handle_Unload__Game_System()
        {
            _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY
                .Internal_Dispose__Vertex_Objects__Vertex_Object_Dictionary();
        }

        /// <summary>
        /// Creates the vertex data for a 2D texture.
        /// It uses the entire texture size.
        /// This is good for non-animated sprites.
        /// </summary>
        public Vertex_Object_Handle Create__Vertex_Object__Vertex_Object_Library
        (
            Texture_R2 texture_R2
        )
        {
            Vertex[] vertices = 
                Private_Extract__Splice
                (
                    texture_R2.Width,
                    texture_R2.Height,
                    texture_R2.Width,
                    texture_R2.Height,
                    0, 0
                );

            Vertex_Object vertex_Object = new Vertex_Object(vertices, texture_R2);

            Vertex_Object_Handle vertex_Object_Handle =
                _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY
                .Internal_Declare__Vertex_Object__Vertex_Object_Dictionary
                (
                    vertex_Object
                );

            return vertex_Object_Handle;
        }

#region Sprite Sheeting
        public Vertex_Object_Handle[] Splice__Into_New_Vertex_Objects__Vertext_Object_Library
        (
            Texture_R2 texture_R2,
            float subWidth,
            float subHeight,
            int? nullabledCountConstraint = null
        )
        {
            //constraint subWidth, and subHeight to not be 0.
            bool isInvalidSubLengths =
                Private_Check_If__Sub_Lengths_Are_Invalid__Vertex_Object_Library
                (
                    texture_R2.Width,
                    texture_R2.Height,
                    subWidth,
                    subHeight
                );

            if (isInvalidSubLengths)
                return null; //TODO: Return default.

            int rowLength = (int)(texture_R2.Width / subWidth); 
            int count = 
                Private_Validate__Vertex_Count__Vertex_Object_Library
                (
                    texture_R2.Width,
                    texture_R2.Height,
                    subWidth,
                    subHeight,
                    nullabledCountConstraint
                );

            Vertex_Object[] vertex_Objects =
                Private_Splice__Into_New_Vertex_Objects
                (
                    texture_R2,
                    subWidth,
                    subHeight,
                    count,
                    rowLength
                );

            Vertex_Object_Handle[] vertex_Object_Handles =
                _Vertex_Object_Library__VERTEX_OBJECT_DICTIONARY
                .Internal_Declare__Vertex_Objects__Vertex_Object_Dictionary
                (
                    vertex_Objects
                );

            return vertex_Object_Handles;
        }

        private bool Private_Check_If__Sub_Lengths_Are_Invalid__Vertex_Object_Library
        (
            float texture_R2_Width,
            float texture_R2_Height,
            float subWidth,
            float subHeight
        )
        {
            bool isInvalidWidth = 
                Tools.Math_Helper
                .Check_If__Obeys_Clamp__Positive_Float
                (
                    subWidth,
                    texture_R2_Width
                );
            bool isInvalidHeight =
                Tools.Math_Helper
                .Check_If__Obeys_Clamp__Positive_Float
                (
                    subHeight,
                    texture_R2_Height
                );
            
            if (isInvalidWidth)
                Private_Log_Error__Invalid_Sub_Length
                (
                    this, 
                    VERTEX_OBJECT_LIBRARY__SUB_LENGTH_STRING__WIDTH, 
                    subWidth
                );
            if (isInvalidHeight)
                Private_Log_Error__Invalid_Sub_Length
                (
                    this,
                    VERTEX_OBJECT_LIBRARY__SUB_LENGTH_STRING__HEIGHT,
                    subHeight
                );

            return isInvalidWidth || isInvalidHeight;
        }

        private int Private_Validate__Vertex_Count__Vertex_Object_Library
        (
            float texture_R2_Width,
            float texture_R2_Height,
            float subWidth,
            float subHeight,
            int? nullabledCountConstraint
        )
        {
            // Divide area of texture_R2, with area of sub-lengths. Cast to int.
            int count = 
                (int)Tools.Math_Helper
                .Calculate__Safe_Area_Ratio
                (
                    texture_R2_Width, texture_R2_Height,
                    subWidth, subHeight
                );

            // Is true if countConstraint was not null but still invalid.
            bool isInvalidCountConstraint;
            // Constrain the nullabledCountConstraint.
            count = 
                Tools.Math_Helper
                .Clamp__Positive_Integer
                (
                    nullabledCountConstraint ?? count, 
                    out isInvalidCountConstraint,
                    count
                );

            if (isInvalidCountConstraint)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__VERTEX_OBJECT_LIBRARY__COUNT_CONSTRAINT_INVALID_2,
                    this,
                    nullabledCountConstraint,
                    count
                );
            }

            return count;
        }

        private static Vertex_Object[] Private_Splice__Into_New_Vertex_Objects
        (
            Texture_R2 texture_R2,
            float subWidth,
            float subHeight,
            int count,
            int rowLength
        )
        {
            Vertex_Object[] vertex_Objects = new Vertex_Object[count];

            Vertex[] vertices;
            int row, col;
            for(int i=0;i<count;i++)
            {
                row = count / rowLength; 
                col = count % rowLength;

                vertices =
                    Private_Extract__Splice
                    (
                        texture_R2.Width,
                        texture_R2.Height,
                        subWidth,
                        subHeight,
                        row, col
                    );

                vertex_Objects[i] = new Vertex_Object(vertices, texture_R2);
            }

            return vertex_Objects;
        }
#endregion

#region Sprite Batching
        private Vertex_Object_Handle Create__Batched_Vertex_Object__Vertext_Object_Library
        (
        )
        { return null; } //TODO: Implement
#endregion

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
                  y_d = y_b;

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

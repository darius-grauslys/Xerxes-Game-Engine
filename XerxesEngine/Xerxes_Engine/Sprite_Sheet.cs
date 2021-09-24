using System;

namespace Xerxes_Engine
{
    /// <summary>
    /// Represents a
    /// </summary>
    public class Sprite_Sheet
    {
        private const string SPRITE_SHEET__PARAMETER__SUB_WIDTH  = "subWidth";
        private const string SPRITE_SHEET__PARAMETER__SUB_HEIGHT = "subHeight";

        private Vertex_Object[] _Sprite_Sheet__VERTEX_OBJECTS   { get; }
        public float Sprite_Sheet__BASE_SPRITE_WIDTH            { get; }
        public float Sprite_Sheet__BASE_SPRITE_HEIGHT           { get; }
        public Texture_R2 Sprite_Sheet__TEXTURE_R2              { get; }
        public int Sprite_Sheet__ROW_COUNT                      { get; }
        public int Sprite_Sheet__COLUMN_COUNT                   { get; }
        public int Sprite_Sheet__SPRITE_COUNT                   { get; }

        /// <summary>
        /// <param name="nullableCountConstraint">
        /// Optional constraint on available sprite count.
        /// Cannot exceed available count = row*col.
        /// </param>
        /// </summary>
        public Sprite_Sheet
        (
            Texture_R2 texture_R2,
            float subWidth,
            float subHeight,
            int? nullableCountConstraint = null
        )
        {
            Sprite_Sheet__TEXTURE_R2 = texture_R2;

            bool doesNotDivideHeight = !Tools.Math_Helper.Divides(subHeight, texture_R2.Height);

            // In case the sub lengths do not divide,
            // we will floor the values as to not have
            // out of bounds texture coords.
            Sprite_Sheet__COLUMN_COUNT = 
                Private_Validate__Row_Column_Count__Sprite_Sheet
                (
                    subHeight,
                    texture_R2.Height,
                    SPRITE_SHEET__PARAMETER__SUB_HEIGHT
                ); 
            Sprite_Sheet__ROW_COUNT    = 
                Private_Validate__Row_Column_Count__Sprite_Sheet
                (
                    subHeight,
                    texture_R2.Height,
                    SPRITE_SHEET__PARAMETER__SUB_HEIGHT
                ); 

            int count = Sprite_Sheet__ROW_COUNT * Sprite_Sheet__COLUMN_COUNT;
            int countConstraint = nullableCountConstraint ?? -1;
            Sprite_Sheet__SPRITE_COUNT = 
                Private_Get__Validated_Count_Constraint(this, count, countConstraint);

            Private_Splice__Sprite_Sheet();
        }

        private void Private_Splice__Sprite_Sheet()
        {
            int row,col;
            for(int i=0;i<Sprite_Sheet__SPRITE_COUNT;i++)
            {
                row = i / Sprite_Sheet__ROW_COUNT;
                col = i % Sprite_Sheet__COLUMN_COUNT;

                Vertex[] vertices = 
                    Vertex_Object.Extract__Splice
                    (
                        Sprite_Sheet__TEXTURE_R2.Width,
                        Sprite_Sheet__TEXTURE_R2.Height,
                        Sprite_Sheet__BASE_SPRITE_WIDTH,
                        Sprite_Sheet__BASE_SPRITE_HEIGHT,
                        row,
                        col
                    );

                _Sprite_Sheet__VERTEX_OBJECTS[i] = new Vertex_Object(vertices);
            }
        }

        private int Private_Validate__Row_Column_Count__Sprite_Sheet
        (
            float subLength,
            float fullLength,
            string paramName
        )
        {
            bool doesNotDivideWidth  = !Tools.Math_Helper.Divides(subLength, fullLength);

            if (doesNotDivideWidth)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__SPRITE_SHEET__SUB_DIMENSION_DOES_NOT_DIVIDE_3,
                    this,
                    subLength,
                    paramName,
                    fullLength 
                );
            }

            int validatedRowColumnCount = (int)Math.Floor(fullLength / subLength);

            return validatedRowColumnCount;
        }

        private static int Private_Get__Validated_Count_Constraint
        (
            Sprite_Sheet spriteSheet,
            int count,
            int countConstraint
        )
        {
            if (0 > countConstraint)
                return count;

            if (countConstraint < count)
            {
                Log.Internal_Write__Verbose__Log
                (
                    Log.VERBOSE__SPRITE_SHEET__USING_COUNT_CONSTRAINT_2,
                    spriteSheet,
                    countConstraint,
                    count
                );
                return countConstraint;
            }

            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__SPRITE_SHEET__COUNT_CONSTRAINT_INVALID_2,
                spriteSheet,
                countConstraint,
                count
            );

            return count;
        }
    }
}


namespace Xerxes.Game_Engine.Graphics
{
    public struct Batch_Index
    {
        public readonly int Batch_Index__COLUMN;
        public readonly int Batch_Index__ROW;

        public readonly float Batch_Index__OFFSET_X;
        public readonly float Batch_Index__OFFSET_Y;
        public readonly float Batch_Index__OFFSET_Z;

        public Batch_Index
        (
            int row,
            int column,

            float offset_x,
            float offset_y,
            float offset_z
        )
        {
            Batch_Index__ROW = row;
            Batch_Index__COLUMN = column;

            Batch_Index__OFFSET_X = offset_x;
            Batch_Index__OFFSET_Y = offset_y;
            Batch_Index__OFFSET_Z = offset_z;
        }
    }
}

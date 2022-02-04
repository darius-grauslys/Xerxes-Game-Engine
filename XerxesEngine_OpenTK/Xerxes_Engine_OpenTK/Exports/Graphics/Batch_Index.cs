
using OpenTK;

namespace Xerxes.Xerxes_OpenTK
{
    public struct Batch_Index
    {
        public Integer_Vector_2 Batch_Index__INDEX { get; }
        public Vector2 Batch_Index__OFFSET { get; }

        public Batch_Index(int x, int y, float offset_x=0, float offset_y=0)
        {
            Batch_Index__INDEX = new Integer_Vector_2(x,y);
            Batch_Index__OFFSET = new Vector2(x,y);
        }
        
        public Batch_Index(Integer_Vector_2 index, Vector2 offset)
        {
            Batch_Index__INDEX = index;
            Batch_Index__OFFSET = offset;
        }
    }
}

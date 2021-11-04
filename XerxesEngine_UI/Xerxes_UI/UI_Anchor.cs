using OpenTK;

namespace Xerxes_UI
{
    public class UI_Anchor
    {
        /// <summary>
        /// The percentage of scale between base size and
        /// the ancestor base size.
        /// </summary>
        public Vector3 UI_Anchor__SCALE_TO_ANCESTOR { get; }
        public Vector3 UI_Anchor__OFFSET_FROM_ANCHOR_POINT { get; }
        
        public UI_Anchor_Type UI_Anchor__ANCHOR_TYPE { get; }
        public UI_Anchor_Type UI_Anchor__ANCHOR_MAJOR { get; }
        public UI_Anchor_Type UI_Anchor__ANCHOR_MINOR { get; }

        internal UI_Anchor
        (
            UI_Anchor_Type anchor_Major,
            UI_Anchor_Type anchor_Minor
        )
        {
            UI_Anchor__ANCHOR_MAJOR = anchor_Major;
            UI_Anchor__ANCHOR_MINOR = anchor_Minor;

            UI_Anchor__ANCHOR_TYPE = 
                Internal_Merge__Anchor_Type
                (
                    UI_Anchor__ANCHOR_MAJOR,
                    UI_Anchor__ANCHOR_MINOR
                );
        }

#region Static Anchor Typing
        internal static UI_Anchor_Type Internal_Get__Anchor_Type
        (
            UI_Horizontal_Anchor_Type horizontal_Anchor_Type,
            UI_Vertical_Anchor_Type   vertical_Anchor_Type
        )
        {
            int sum = ((int)horizontal_Anchor_Type) + ((int)vertical_Anchor_Type);

            return Private_Get__Anchor_Type(sum);
        }

        internal static UI_Anchor_Type Internal_Get__Anchor_Type
        (
            UI_Vertical_Anchor_Type   vertical_Anchor_Type,
            UI_Horizontal_Anchor_Type horizontal_Anchor_Type
        )
        {
            return Internal_Get__Anchor_Type(horizontal_Anchor_Type, vertical_Anchor_Type);
        }

        internal static UI_Anchor_Type Internal_Merge__Anchor_Type
        (
            UI_Anchor_Type anchor_Type1,
            UI_Anchor_Type anchor_Type2
        )
        {
            int sum = ((int)anchor_Type1) + ((int)anchor_Type2);

            if (sum % 2 == 0)
                return (UI_Anchor_Type)(sum / 2);
            return (UI_Anchor_Type)(sum - 4);
        }

        private static UI_Anchor_Type Private_Get__Anchor_Type(int sum)
            => (UI_Anchor_Type)sum;
#endregion
    }
}

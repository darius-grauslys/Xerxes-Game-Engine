using Xerxes;
using Xerxes.Engine_Objects;

namespace Xerxes_UI
{
    public class UI_Game_Object :
        Game_Object,
        IXerxes_Ancestor_Of<UI_Game_Object>,
        IXerxes_Descendant_Of<UI_Game_Object>
    {
        private UI_Anchor _UI_Game_Object__ANCHOR { get; }
        private UI_Rect   _UI_Game_Object__RECT   { get; }

        public UI_Game_Object()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Resize_2D>
                (
                    Private_Translate__Resized_2D
                )
                .Downstream.Extending<SA__Resize_2D>();
        }

        private void Private_Translate__Resized_2D
        (SA__Resize_2D e)
        {
        }
    }
}

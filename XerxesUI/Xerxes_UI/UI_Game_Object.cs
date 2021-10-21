using Xerxes_Engine;
using Xerxes_Engine.Engine_Objects;
using Xerxes_Engine.Engine_Objects.R2;

namespace Xerxes_UI
{
    public class UI_Game_Object :
        Game_Object,
        IXerxes_Descendant_Of<UI_Game_Object>,
        IXerxes_Descendant_Of<Scene_Layer>,
        IXerxes_Ancestor_Of<UI_Game_Object>,
        IXerxes_Ancestor_Of<Game_Object_Component>
    {
        public UI_Game_Object()
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <SA__UI_Transformed>();

            Associate__Xerxes_Engine_Object(new Sprite_Render_Component());
            Associate__Xerxes_Engine_Object(new UI_Transform_Component());
        }
    }
}

using OpenTK;
using Xerxes_Engine;
using Xerxes_Engine.Engine_Objects;
using Xerxes_Engine.Tools;

namespace Xerxes_UI
{
    public class UI_Transform_Component :
        Game_Object_Component,
        IXerxes_Descendant_Of<UI_Game_Object> 
    {
        internal Vector3 _UI_Transform_Component__Scale { get; private set; }
        internal Vector3 _UI_Transform_Component__Position { get; private set; }

        public UI_Transform_Component()
        {
            Protected_Declare__Downstream_Catch__Xerxes_Engine_Object
                <SA__Draw>
                (
                    Private_Handle__Draw__Transform_Component
                );

            Protected_Declare__Downstream_Catch__Xerxes_Engine_Object
                <SA__UI_Transformed>
                (
                    Private_Handle__Ancestor_Transformed__Transform_Component
                );
        }

        private void Private_Handle__Draw__Transform_Component
        (
            SA__Draw e
        )
        {
            e.SA__Draw__Scale__Internal =
                _UI_Transform_Component__Scale;
            e.SA__Draw__Position__Internal =
                _UI_Transform_Component__Position;
        }

        private void Private_Handle__Ancestor_Transformed__Transform_Component
        (
            SA__UI_Transformed e
        )
        {
            Vector3 newScale = 
                Math_Helper.Get__Hadamard_Product
                (
                    _UI_Transform_Component__Scale,
                    e.SA_UI_Transformed__Delta_Scale__Internal
                );
            Vector3 newPosition =
                _UI_Transform_Component__Position
                +
                e.SA_UI_Transformed__Delta_Position__Internal;

            e.Internal_Assume__Argument__SA__UI_Transformed
            (
                _UI_Transform_Component__Scale,
                newScale,
                _UI_Transform_Component__Position,
                newPosition
            );

            _UI_Transform_Component__Scale = newScale;
            _UI_Transform_Component__Position = newPosition;
        }
    }
}

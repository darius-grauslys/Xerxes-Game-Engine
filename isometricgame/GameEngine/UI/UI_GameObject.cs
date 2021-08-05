using System.Linq;
using isometricgame.GameEngine.UI.Components;
using OpenTK;

namespace isometricgame.GameEngine.UI
{
    public class UI_GameObject : GameObject
    {
        internal readonly UI_Render_Component UI_GameObject__UI_RENDER__Internal;
        protected UI_Render_Component UI_GameObject__UI_Render => UI_GameObject__UI_RENDER__Internal;
        internal UI_Element UI_GameObject__UI_Element__Internal
            => UI_GameObject__UI_RENDER__Internal.UI_Render__ELEMENT;

        public UI_GameObject
            (
            UI_Scene_Layer sceneLayer,
            string spriteAlias, 
            
            UI_Rect boundingRect,
            
            params GameObject_Component[] components
            ) 
            : base
                (
                sceneLayer,
                Vector3.Zero, 
                Enumerable
                    .Concat<GameObject_Component>
                        (
                        new GameObject_Component[]
                        {
                            new UI_Render_Component
                                (
                                spriteAlias,
                                
                                boundingRect
                            )
                        },
                        components
                        )
                    .ToArray()
                )
        {
            UI_GameObject__UI_RENDER__Internal = Get__Component__GameObject<UI_Render_Component>();
        }
    }
}
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

            UI_Vertical_Anchor majorAnchor,
            UI_Horizontal_Anchor lesserAnchor,

            params GameObject_Component[] components
        )
            : this
            (
                sceneLayer,
                spriteAlias,

                boundingRect,

                majorAnchor as UI_Anchor,
                lesserAnchor as UI_Anchor,

                components
            )
        {
            
        }
        
        public UI_GameObject
        (
            UI_Scene_Layer sceneLayer,
            string spriteAlias,

            UI_Rect boundingRect,

            UI_Horizontal_Anchor majorAnchor,
            UI_Vertical_Anchor lesserAnchor,

            params GameObject_Component[] components
        )
            : this
            (
                sceneLayer,
                spriteAlias,

                boundingRect,

                majorAnchor as UI_Anchor,
                lesserAnchor as UI_Anchor,

                components
            )
        {
            
        }
        
        internal UI_GameObject
            (
            UI_Scene_Layer sceneLayer,
            string spriteAlias, 
            
            UI_Rect boundingRect,
            
            UI_Anchor majorAnchor,
            UI_Anchor lesserAnchor,
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
                                
                                boundingRect,
                                
                                majorAnchor,
                                lesserAnchor
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
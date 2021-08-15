using System;
using System.Linq;
using isometricgame.GameEngine.UI.Components;
using OpenTK;

namespace isometricgame.GameEngine.UI
{
    public class UI_GameObject : GameObject
    {
        internal UI_Render_Component Internal_UI_GameObject__UI_RENDER { get; }
        protected UI_Render_Component UI_GameObject__UI_Render => Internal_UI_GameObject__UI_RENDER;
        public UI_Element Get__UI_Element__UI_GameObject()
            => Internal_UI_GameObject__UI_RENDER.UI_Render__Element;

        protected Vector3 Get__Game_Space_Position__UI_GameObject()
            => Get__UI_Element__UI_GameObject().Get__Position_In_GameSpace__UI_Element();

        protected Vector3 Get__UI_Space_Position__UI_GameObject()
            => Get__UI_Element__UI_GameObject().Get__Position_In_UISpace__UI_Element();
        
        public UI_GameObject
            (
            UI_Scene_Layer sceneLayer,
            string spriteAlias, 
            
            UI_Element uiElement,
            
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
                                
                                uiElement
                            )
                        },
                        components
                        )
                    .ToArray()
                )
        {
            Internal_UI_GameObject__UI_RENDER = Get__Component__GameObject<UI_Render_Component>();
        }

        public override string ToString()
        {
            return String.Format("[UI_GameObject] {1} : {0}", base.ToString(), Get__UI_Element__UI_GameObject());
        }
    }
}
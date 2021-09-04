using System;
using System.Linq;
using Xerxes_Engine.UI.Implemented_UI_Components;
using OpenTK;

namespace Xerxes_Engine.UI
{
    public class UI_Game_Object : Game_Object
    {
        internal UI_Render_Component Internal_UI_Game_Object__UI_RENDER { get; }
        protected UI_Render_Component UI_Game_Object__UI_Render => Internal_UI_Game_Object__UI_RENDER;
        public UI_Element Get__UI_Element__UI_Game_Object()
            => Internal_UI_Game_Object__UI_RENDER.UI_Render__Element;

        protected Vector3 Get__Game_Space_Position__UI_Game_Object()
            => Get__UI_Element__UI_Game_Object().Get__Position_In_GameSpace__UI_Element();

        protected Vector3 Get__UI_Space_Position__UI_Game_Object()
            => Get__UI_Element__UI_Game_Object().Get__Position_In_UISpace__UI_Element();
        
        public UI_Game_Object
            (
            UI_Scene_Layer sceneLayer,
            string spriteAlias, 
            
            UI_Element uiElement,
            
            params Game_Object_Component[] components
            ) 
            : base
                (
                sceneLayer,
                Vector3.Zero, 
                Enumerable
                    .Concat<Game_Object_Component>
                        (
                        new Game_Object_Component[]
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
            Internal_UI_Game_Object__UI_RENDER = Get__Component__Game_Object<UI_Render_Component>();
        }

        public override string ToString()
        {
            return String.Format("[UI_Game_Object] {1} : {0}", base.ToString(), Get__UI_Element__UI_Game_Object());
        }
    }
}

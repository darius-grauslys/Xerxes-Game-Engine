using System;
using System.Drawing;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Systems.Input;
using OpenTK;
using OpenTK.Input;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI.Components
{
    public class UI_Clickable_Component : GameObject_Component
    {
        private Action<UI_MouseButton_Pulse_FrameArgument> UI_Clickable__Clicked_Handler { get; set; }
        public void Set__Clicked_Handler__Clickable_Component(Action<UI_MouseButton_Pulse_FrameArgument> handlerClicked)
            => UI_Clickable__Clicked_Handler = handlerClicked;
        
        private UI_Render_Component UI_Clickable__UI_Render_Of_Attached_UI_GameObject { get; set; }

        private Vector3? UI_Clickable__Position
            => UI_Clickable__UI_Render_Of_Attached_UI_GameObject?.UI_Render__Position;

        private UI_Element UI_Clickable__Render_Element
            => UI_Clickable__UI_Render_Of_Attached_UI_GameObject?.UI_Render__Element;

        private UI_Rect UI_Clickable__Bounding_Rect
            => UI_Clickable__Render_Element?.UI_Element__BOUNDING_RECT;

        public UI_Clickable_Component(Action<UI_MouseButton_Pulse_FrameArgument> clickedHandler = null)
        {
            UI_Clickable__Clicked_Handler = clickedHandler;
        }
        
        protected override void Handle_Attach_To__GameObject__Component()
        {
            UI_Scene_Layer uiSceneLayer = Component__Attached_GameObject.GameObject__Scene_Layer as UI_Scene_Layer;
            UI_Clickable__UI_Render_Of_Attached_UI_GameObject =
                Component__Attached_GameObject.Get__Component__GameObject<UI_Render_Component>();
            
            Set__Hardlocked_Status__Component(uiSceneLayer == null || UI_Clickable__UI_Render_Of_Attached_UI_GameObject == null);

            if (Component__Hardlocked)
                return;

            uiSceneLayer.Event__Evaluate_Mouse_Button__UI_Scene_Layer += Private_Resolve__Click__Clickable_Component;
        }

        private void Private_Resolve__Click__Clickable_Component(UI_MouseButton_Pulse_FrameArgument args)
        {
            Vector3 clickedPosition = args.UI_MouseButton_Pulse__MOUSE_POSITION;
            
            if 
            (
                UI_Rect.CheckIf__Position_Is_Bounded_By_Rect
                    (
                    clickedPosition, 
                    Vector3.Zero,
                    UI_Clickable__Bounding_Rect,
                    Vector3.Zero
                    )
            )
            {
                args.Consume__UI_Pulse__UI_Pulse_FrameArgument();
                UI_Clickable__Clicked_Handler?.Invoke(args);
            }
        }
    }
}
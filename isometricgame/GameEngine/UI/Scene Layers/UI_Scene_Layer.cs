﻿using System;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems.Input;
using OpenTK;
using OpenTK.Input;

namespace isometricgame.GameEngine.UI
{
    public class UI_Scene_Layer : Scene_Layer
    {
        public event Action<UI_MouseButton_Pulse_FrameArgument> Event__Evaluate_Mouse_Button__UI_Scene_Layer;
        
        private readonly UI_Strict_Panel UI_Scene_Layer__Strict_Panel;
        public UI_Indexed_Element[] temp_test__get__elements() => UI_Scene_Layer__Strict_Panel?.Get__Child_Elements__UI_Strict_Panel() ?? new UI_Indexed_Element[0];
        private readonly InputHandler UI_Scene_Layer__InputHandler__Internal;
        
        public UI_Scene_Layer(Scene sceneLayerParentScene, int sceneLayerLayerLevel = 0) 
            : base(sceneLayerParentScene, sceneLayerLayerLevel)
        {
            UI_Scene_Layer__Strict_Panel = new UI_Strict_Panel
                (
                new UI_Rect(SceneLayer__Window_Size__Game)
                );

            UI_Scene_Layer__InputHandler__Internal = 
                Scene_Layer__Game.Game__Input_System.RegisterHandler
                    (
                    InputType.Mouse_Button
                    |
                    InputType.Keyboard_UpDown
                    );
            UI_Scene_Layer__InputHandler__Internal.DeclarePulse(MouseButton.Left.ToString());
        }

        protected override void Handle_Update__Scene_Layer(FrameArgument e)
        {
            Private_Evaluate__Mouse_Button_From_Input_Handler(e);
            
            base.Handle_Update__Scene_Layer(e);
        }

        private void Private_Evaluate__Mouse_Button_From_Input_Handler(FrameArgument args)
        {
            //See if a pulse is ready to be evaluated.
            if 
                (
                UI_Scene_Layer__InputHandler__Internal
                    .EvaluatePulseState
                        (
                        MouseButton.Left.ToString(),
                        true
                        )
                )
            {
                MouseButtonEventArgs margs = UI_Scene_Layer__InputHandler__Internal.Mouse_Button;
                Vector3 mousePosition = new Vector3(margs.X - SceneLayer__Window_Size__Game.X / 2,
                    -margs.Y + SceneLayer__Window_Size__Game.Y/2, 0);
                
                UI_MouseButton_Pulse_FrameArgument uiPulseArg = 
                    new UI_MouseButton_Pulse_FrameArgument(args, mousePosition, margs.Button);
                Event__Evaluate_Mouse_Button__UI_Scene_Layer?.Invoke(uiPulseArg);

                if (uiPulseArg.UI_Pulse_FrameArgument__Frame_Evaluates_Pulse)
                {
                    UI_Scene_Layer__InputHandler__Internal
                        .EvaluatePulseState
                        (
                            MouseButton.Left.ToString()
                        );
                }
            }
        }

        protected virtual void Add__UI_Object__UI_Scene_Layer(UI_GameObject uiGameObject, UI_Anchor bindingAnchor)
        {
            bool success = Add__UI_Element__UI_Scene_Layer
            (
                uiGameObject.UI_GameObject__UI_Element__Internal,
                bindingAnchor
            );
            if(success)
                Add__Scene_Object__Scene_Layer(uiGameObject);
        }
        
        protected virtual bool Add__UI_Element__UI_Scene_Layer
        (
            UI_Element element,
            UI_Anchor bindingAnchor
        )
        {
            bool success = UI_Scene_Layer__Strict_Panel.Add__Element__UI_Strict_Panel(element, bindingAnchor);

            if (success && element is UI_Container)
            {
                UI_Container container = element as UI_Container;

                foreach (UI_Indexed_Element indexedElement in container.Internal_Get__CHILD_ELEMENTS__UI_Container())
                {
                    UI_GameObject uiGameObject =
                        indexedElement.UI_Indexed_Element__Associated_UI_GameObject;
                    
                    if(uiGameObject != null)
                        base.Add__Scene_Object__Scene_Layer(uiGameObject);
                }
            }

            return success;
        }

        protected override void Handle_Rescaled__Scene_Layer()
        {
            UI_Scene_Layer__Strict_Panel?.Internal_Resize__UI_Element(Scene_Layer__Game.Get__Window_Size__Game());
            UI_Scene_Layer__Strict_Panel?.Internal_Set__Position__UI_Element(-0.5f * new Vector3(SceneLayer__Window_Size__Game));
        }
    }
}
﻿using System;
using System.Collections.Generic;
using Xerxes_Engine.Systems.Input;
using Xerxes_Engine.UI.UI_Event_Argument_Frames;
using OpenTK;
using OpenTK.Input;

namespace Xerxes_Engine.UI
{
    public class UI_Scene_Layer : Scene_Layer
    {
        public event Action<UI_MouseButton_Pulse_FrameArgument> Event__Evaluate_Mouse_Button__UI_Scene_Layer;
        public event Action<UI_Keyboard_Pulse_Frame_Arguement> Event__Evaluate_Keyboard_Button__UI_Scene_Layer;
        
        private readonly UI_Inclusive_Container UI_Scene_Layer__Strict_Panel;
        private readonly Input_Handler UI_Scene_Layer__InputHandler__Internal;
        
        public UI_Scene_Layer
        (
            UI_Inclusive_Container container = null, 
            int sceneLayerLayerLevel = 0
        ) 
            : base(sceneLayerLayerLevel)
        {
            container?.Internal_Set__Local_Origin_Position_Type__UI_Element(UI_Anchor_Position_Type.Bottom_Left);
            container?.Internal_Resize__UI_Element(SceneLayer__Window_Size__Game);

            UI_Scene_Layer__Strict_Panel = 
                container 
                ?? 
                new UI_Inclusive_Container
                (
                new UI_Rect
                    (
                    SceneLayer__Window_Size__Game, 
                    UI_Anchor_Position_Type.Bottom_Left
                    )
                );

            UI_Scene_Layer__InputHandler__Internal = 
                Scene_Layer__Game.Game__Input_System.RegisterHandler
                    (
                    InputType.Mouse_Button
                    |
                    InputType.Keyboard_UpDown
                    );
        }

        protected void Register__Input_Pulse__UI_Scene_Layer(MouseButton mousePulse)
        {
            UI_Scene_Layer__InputHandler__Internal.DeclarePulse(mousePulse.ToString());
        }

        protected void Register__Input_Pulse__UI_Scene_Layer(Key keyPulse)
        {
            UI_Scene_Layer__InputHandler__Internal.DeclarePulse(keyPulse.ToString());
        }

        protected void Handle_Update_Evaluate__Mouse_Button__UI_Scene_Layer(Event_Argument_Frame args, MouseButton mouseButton)
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

                UI_Scene_Layer__InputHandler__Internal
                    .EvaluatePulseState
                    (
                        MouseButton.Left.ToString()
                    );
            }
        }

        protected void Handle_Update_Evaluate__Keyboard_Button__UI_Scene_Layer(Event_Argument_Frame args, Key keyButton)
        {
            if
            (
                UI_Scene_Layer__InputHandler__Internal
                    .EvaluatePulseState
                    (
                        keyButton.ToString(),
                        true
                    )
            )
            {
                UI_Keyboard_Pulse_Frame_Arguement uiPulseArg =
                    new UI_Keyboard_Pulse_Frame_Arguement(args, keyButton);
                
                Event__Evaluate_Keyboard_Button__UI_Scene_Layer?.Invoke(uiPulseArg);

                UI_Scene_Layer__InputHandler__Internal
                    .EvaluatePulseState
                    (
                        keyButton.ToString()
                    );
            }
        }
        
        protected virtual bool Add__UI_Object__UI_Scene_Layer
        (
            UI_Game_Object uiGame_Object, 
            UI_Anchor bindingAnchor = null
        )
        {
            return Add__UI_Element__UI_Scene_Layer
            (
                uiGame_Object.Get__UI_Element__UI_Game_Object(),
                bindingAnchor
            );
        }
        
        protected virtual bool Add__UI_Element__UI_Scene_Layer
        (
            UI_Element element,
            UI_Anchor bindingAnchor = null
        )
        {
            bool success = UI_Scene_Layer__Strict_Panel.Add__UI_Element__UI_Inclusive_Container(element, bindingAnchor);

            if (success)
            {
                Private_Check__For_Special_Conditions_Of_Child__UI_Scene_Layer(element);
            }

            return success;
        }

        private void Private_Check__For_Special_Conditions_Of_Child__UI_Scene_Layer
        (
            UI_Element element
        )
        {
            if(element.UI_Element__Associated_UI_Game_Object != null)
                Add__Scene_Object__Scene_Layer(element.UI_Element__Associated_UI_Game_Object);
            
            if (element is UI_Container)
                Handle_Add__Children_Of_UI_Container__UI_Scene_Layer(element as UI_Container);
        }
        
        protected virtual void Handle_Add__Children_Of_UI_Container__UI_Scene_Layer
        (
            UI_Container container
        )
        {
            List<UI_Wrapper> indexedElements = container.Internal_Get__CHILD_ELEMENTS__UI_Container();
            
            foreach (UI_Wrapper indexedElement in indexedElements)
            {
                UI_Element element = indexedElement.UI_Wrapper__WRAPPED_ELEMENT;

                Private_Check__For_Special_Conditions_Of_Child__UI_Scene_Layer(element);
            }
        }

        protected override void Handle_Rescaled__Scene_Layer()
        {
            UI_Scene_Layer__Strict_Panel?.Internal_Resize__UI_Element(Scene_Layer__Game.Get__Window_Size__Game());
            UI_Scene_Layer__Strict_Panel?.Internal_Set__Position__UI_Element(-0.5f * new Vector3(SceneLayer__Window_Size__Game));
        }
    }
}
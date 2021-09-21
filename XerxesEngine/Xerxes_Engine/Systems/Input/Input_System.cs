using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;

namespace Xerxes_Engine.Systems.Input
{
    public sealed class Input_System : Game_System
    {
        private Dictionary<InputType, List<Input_Handler>> inputDirectory = new Dictionary<InputType, List<Input_Handler>>()
        {
            { InputType.Keyboard_Press, new List<Input_Handler>() },
            { InputType.Keyboard_UpDown, new List<Input_Handler>() },
            { InputType.Mouse_Button, new List<Input_Handler>() },
            { InputType.Mouse_Move, new List<Input_Handler>() },
            { InputType.Mouse_Wheel, new List<Input_Handler>() }
        };

        private Dictionary<int, List<Input_Handler>> handlerGroups = new Dictionary<int, List<Input_Handler>>();
        
        internal Input_System(Game game) 
            : base(game)
        {
            game.Game__GAME_WINDOW__Internal.KeyDown += Private_Handle__Key_Down__Input_System;
            game.Game__GAME_WINDOW__Internal.KeyPress += Private_Handle__Key_Press__Input_System;
            game.Game__GAME_WINDOW__Internal.KeyUp += Private_Handle__Key_Up__Input_System;

            game.Game__GAME_WINDOW__Internal.MouseDown += Private_Handle__Mouse_Down__Input_System;
            game.Game__GAME_WINDOW__Internal.MouseUp += Private_Handle__Mouse_Up__Input_System;
            game.Game__GAME_WINDOW__Internal.MouseWheel += Private_Handle__Mouse_Wheel__Input_System;
            game.Game__GAME_WINDOW__Internal.MouseMove += Private_Handle__Mouse_Move__Input_System;
        }

        public Input_Handler RegisterHandler(InputType inputType, bool enabled = true, int handlerID = -1)
        {
            int newID =  (handlerID > 0) ? handlerID : handlerGroups.Keys.Count;
            Input_Handler handler = new Input_Handler(newID, inputType, enabled);

            if (handlerGroups.ContainsKey(newID))
                handlerGroups[newID].Add(handler);
            else
            {
                handlerGroups.Add(newID, new List<Input_Handler> { handler });
            }
            
            for(int key = 1; key <= 16; key*=2)
                if (0 < ((int)inputType & key))
                    inputDirectory[(InputType)key].Add(handler);

            return handler;
        }

        private void Private_Handle__Mouse_Move__Input_System(object sender, MouseMoveEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Mouse_Move])
                if (handle.Enabled)
                    handle.Handle_Mouse_Move(sender, e);
        }

        private void Private_Handle__Mouse_Wheel__Input_System(object sender, MouseWheelEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Mouse_Wheel])
                if (handle.Enabled)
                    handle.Handle_Mouse_Wheel(sender, e);
        }

        private void Private_Handle__Mouse_Up__Input_System(object sender, MouseButtonEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Mouse_Button])
                if (handle.Enabled)
                    handle.Handle_Mouse_Button(sender, e);
        }

        private void Private_Handle__Mouse_Down__Input_System(object sender, MouseButtonEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Mouse_Button])
                if (handle.Enabled)
                    handle.Handle_Mouse_Button(sender, e);
        }

        private void Private_Handle__Key_Up__Input_System(object sender, KeyboardKeyEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Keyboard_UpDown])
                if (handle.Enabled)
                    handle.Handle_Keyboard_UpDown(sender, e);
        }

        private void Private_Handle__Key_Press__Input_System(object sender, KeyPressEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Keyboard_Press])
                if (handle.Enabled)
                    handle.Handle_Keyboard_Press(sender, e);
        }

        private void Private_Handle__Key_Down__Input_System(object sender, KeyboardKeyEventArgs e)
        {
            foreach (Input_Handler handle in inputDirectory[InputType.Keyboard_UpDown])
                if (handle.Enabled)
                    handle.Handle_Keyboard_UpDown(sender, e);
        }
    }
}

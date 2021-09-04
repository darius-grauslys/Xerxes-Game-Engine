using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XerxesEngine.Systems.Input
{
    public class InputSystem : GameSystem
    {
        private Dictionary<InputType, List<InputHandler>> inputDirectory = new Dictionary<InputType, List<InputHandler>>()
        {
            { InputType.Keyboard_Press, new List<InputHandler>() },
            { InputType.Keyboard_UpDown, new List<InputHandler>() },
            { InputType.Mouse_Button, new List<InputHandler>() },
            { InputType.Mouse_Move, new List<InputHandler>() },
            { InputType.Mouse_Wheel, new List<InputHandler>() }
        };

        private Dictionary<int, List<InputHandler>> handlerGroups = new Dictionary<int, List<InputHandler>>();
        
        public InputSystem(Game game) 
            : base(game)
        {
            game.KeyDown += GameWindow_KeyDown;
            game.KeyPress += GameWindow_KeyPress;
            game.KeyUp += GameWindow_KeyUp;

            game.MouseDown += GameWindow_MouseDown;
            game.MouseUp += GameWindow_MouseUp;
            game.MouseWheel += GameWindow_MouseWheel;
            game.MouseMove += GameWindow_MouseMove;
        }

        public InputHandler RegisterHandler(InputType inputType, bool enabled = true, int handlerID = -1)
        {
            int newID =  (handlerID > 0) ? handlerID : handlerGroups.Keys.Count;
            InputHandler handler = new InputHandler(newID, inputType, enabled);

            if (handlerGroups.ContainsKey(newID))
                handlerGroups[newID].Add(handler);
            else
            {
                handlerGroups.Add(newID, new List<InputHandler> { handler });
            }
            
            for(int key = 1; key <= 16; key*=2)
                if (0 < ((int)inputType & key))
                    inputDirectory[(InputType)key].Add(handler);

            return handler;
        }

        private void GameWindow_MouseMove(object sender, MouseMoveEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Mouse_Move])
                if (handle.Enabled)
                    handle.Handle_Mouse_Move(sender, e);
        }

        private void GameWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Mouse_Wheel])
                if (handle.Enabled)
                    handle.Handle_Mouse_Wheel(sender, e);
        }

        private void GameWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Mouse_Button])
                if (handle.Enabled)
                    handle.Handle_Mouse_Button(sender, e);
        }

        private void GameWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Mouse_Button])
                if (handle.Enabled)
                    handle.Handle_Mouse_Button(sender, e);
        }

        private void GameWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Keyboard_UpDown])
                if (handle.Enabled)
                    handle.Handle_Keyboard_UpDown(sender, e);
        }

        private void GameWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Keyboard_Press])
                if (handle.Enabled)
                    handle.Handle_Keyboard_Press(sender, e);
        }

        private void GameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            foreach (InputHandler handle in inputDirectory[InputType.Keyboard_UpDown])
                if (handle.Enabled)
                    handle.Handle_Keyboard_UpDown(sender, e);
        }
    }
}

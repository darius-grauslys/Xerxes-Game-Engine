using isometricgame.GameEngine.Events;
using isometricgame.GameEngine.Services.Input.Keyboard;
using isometricgame.GameEngine.Services.Input.Mouse;
using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Services.Input
{
    public class InputService : GameSystem
    {
        //private BroadcastNetwork inputBroadcastNetwork = new BroadcastNetwork();
        //private MouseBroadcast mouseBroadcast = new MouseBroadcast();
        //private KeyboardBroadcast keyboardBroadcast = new KeyboardBroadcast();

        //private MouseMoveEventChannel mouseMoveEventChannel;
        //private MouseButtonEventChannel mouseButtonEventChannel;
        //private KeyboardPressEventChannel keyboardPressEventChannel;
        //private KeyboardKeystateEventChannel keyboardKeystateEventChannel;


        private KeyboardKeyEventArgs keyUpDown = null;
        private KeyPressEventArgs keyPress = null;

        private MouseButtonEventArgs mouseUpDown = null;
        private MouseMoveEventArgs mouseMove = null;
        private MouseWheelEventArgs mouseWheel = null;

        public KeyboardKeyEventArgs KeyUpDown => keyUpDown;
        public KeyPressEventArgs KeyPress => keyPress;

        public MouseButtonEventArgs MouseUpDown => mouseUpDown;
        public MouseMoveEventArgs MouseMove => mouseMove;
        public MouseWheelEventArgs MouseWheel => MouseWheel;

        //public BroadcastNetwork InputBroadcastNetwork { get => inputBroadcastNetwork; private set => inputBroadcastNetwork = value; }

        public InputService(Game game, GameWindow gameWindow) 
            : base(game)
        {
            gameWindow.KeyDown += GameWindow_KeyDown;
            gameWindow.KeyPress += GameWindow_KeyPress;
            gameWindow.KeyUp += GameWindow_KeyUp;

            gameWindow.MouseDown += GameWindow_MouseDown;
            gameWindow.MouseUp += GameWindow_MouseUp;
            gameWindow.MouseWheel += GameWindow_MouseWheel;
            gameWindow.MouseMove += GameWindow_MouseMove;

            //InputBroadcastNetwork.RegisterBroadcast(mouseBroadcast);
            //InputBroadcastNetwork.RegisterBroadcast(keyboardBroadcast);

            //mouseMoveEventChannel = mouseBroadcast.GetChannel<MouseMoveEventChannel>();
            //mouseButtonEventChannel = mouseBroadcast.GetChannel<MouseButtonEventChannel>();

            //keyboardPressEventChannel = keyboardBroadcast.GetChannel<KeyboardPressEventChannel>();
            //keyboardKeystateEventChannel = keyboardBroadcast.GetChannel<KeyboardKeystateEventChannel>();

            //mouseBroadcast.ToggleLock(false);
            //keyboardBroadcast.ToggleLock(false);
        }

        private void GameWindow_MouseMove(object sender, MouseMoveEventArgs e)
        {
            mouseMove = e;
        }

        private void GameWindow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mouseWheel = e;
        }

        private void GameWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseUpDown = e;
        }

        private void GameWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseUpDown = e;
        }

        private void GameWindow_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            keyUpDown = e;
        }

        private void GameWindow_KeyPress(object sender, KeyPressEventArgs e)
        {
            keyPress = e;
        }

        private void GameWindow_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keyUpDown = e;
        }
    }
}

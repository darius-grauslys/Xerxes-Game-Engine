#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Events;
using isometricgame.GameEngine.Services.Input;
using OpenTK;
using OpenTK.Input;

namespace isometricgame.GameEngine.Attributes.Physics
{
    public class MovementControllerAttribute : GameAttribute
    {
        private InputService inputService;

        public MovementControllerAttribute(GameObject parentObject) 
            : base(parentObject)
        {
            inputService = ParentObject.Scene.Game.GetService<InputService>();
#if DEBUG
            if (inputService == null)
                Console.WriteLine("[MovementController.construct] Input service not registered.");
#endif
        }

        protected override void HandleOnUpdate(FrameEventArgs args)
        {
            if (inputService == null)
            {
                Toggle(false);
#if DEBUG
                Console.WriteLine("[MovementController.handle] Input service not registered.");
#endif
                return;
            }

            if (inputService.KeyUpDown == null)
                return;

            if (inputService.KeyUpDown.Keyboard.IsKeyDown(Key.W))
            {
                ParentObject.SetY(ParentObject.GetY() - 0.2f);
            }
            if (inputService.KeyUpDown.Keyboard.IsKeyDown(Key.A))
            {
                ParentObject.SetX(ParentObject.GetX() - 0.2f);
            }
            if (inputService.KeyUpDown.Keyboard.IsKeyDown(Key.S))
            {
                ParentObject.SetY(ParentObject.GetY() + 0.2f);
            }
            if (inputService.KeyUpDown.Keyboard.IsKeyDown(Key.D))
            {
                ParentObject.SetX(ParentObject.GetX() + 0.2f);
            }
        }
    }
}

#define DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Systems.Input;
using OpenTK;
using OpenTK.Input;

namespace isometricgame.GameEngine.Components.Physics
{
    public enum MovementDirection
    {
        None = 0,
        East = 1,
        West = 2,
        //skip 3
        South = 4,
        SouthEast = 5,
        SouthWest = 6,
        //skip 7
        North = 8,
        NorthEast = 9,
        NorthWest = 10
    };

    /// <summary>
    /// Remove this
    /// </summary>
    public class MovementControllerComponent : GameComponent
    {
        public static int MovementDirection_ToAnim(MovementDirection dir)
        {
            int val = (int)dir;
            int offset = (int)(val / 3.5);
            return val - offset;
        }

        private readonly byte DIRECTION_UPDOWN = 12;
        private readonly byte DIRECITON_LEFRIGHT = 3;
        private readonly byte DIRECTION_UP = 8;
        private readonly byte DIRECTION_DOWN = 4;
        private readonly byte DIRECTION_LEFT = 2;
        private readonly byte DIRECTION_RIGHT = 1;

        private InputHandler inputHandler;
        private byte direction = 0, dir_ew = 3, dir_ns = 12;

        /// <summary>
        /// 1: Right, 2: Left, 4: Down, 8: Up.
        /// </summary>
        private byte ByteDirection => direction;
        public MovementDirection Direction => (MovementDirection)ByteDirection;

        private float speed = 0.05f;

        public MovementControllerComponent(GameObject parentObject) 
            : base(parentObject)
        {
            inputHandler = ParentObject.Scene.Game.GetSystem<InputService>().Register(InputType.Keyboard_UpDown);
        }

        protected override void OnUpdate(FrameArgument args)
        {
            dir_ew = DIRECITON_LEFRIGHT;
            dir_ns = DIRECTION_UPDOWN;

            if (inputHandler.Keyboard_UpDown == null)
                return;

            if (inputHandler.Keyboard_UpDown.Keyboard.IsKeyDown(Key.Space))
            {
                ParentObject.Y = 200;
                ParentObject.X = 200;
            }

            if (inputHandler.Keyboard_UpDown.Keyboard.IsKeyDown(Key.W))
            {
                ParentObject.Y += speed;
                dir_ns = (byte)(dir_ns & DIRECTION_DOWN);
            }
            if (inputHandler.Keyboard_UpDown.Keyboard.IsKeyDown(Key.A))
            {
                ParentObject.X -= speed;
                dir_ew = (byte)(dir_ew & DIRECTION_RIGHT);
            }
            if (inputHandler.Keyboard_UpDown.Keyboard.IsKeyDown(Key.S))
            {
                ParentObject.Y -= speed;
                dir_ns = (byte)(dir_ns & DIRECTION_UP);
            }
            if (inputHandler.Keyboard_UpDown.Keyboard.IsKeyDown(Key.D))
            {
                ParentObject.X += speed;
                dir_ew = (byte)(dir_ew & DIRECTION_LEFT);
            }

            direction = (byte)((dir_ew ^ DIRECITON_LEFRIGHT) | (dir_ns ^ DIRECTION_UPDOWN));
            if (direction == 15) direction = 0;
        }
    }
}

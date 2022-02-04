
namespace Xerxes.Game_Engine.Input
{
    public abstract class SA__Input_Mouse_Move
    {
        public abstract int Input_Mouse_Move__Mouse_X { get; }
        public abstract int Input_Mouse_Move__Mouse_Y { get; }

        protected internal abstract void Handle_Update__Mouse_X__Input_Mouse_Move
        (int mouse_x);

        protected internal abstract void Handle_Update__Mouse_Y__Input_Mouse_Move
        (int mouse_y);
    }
}

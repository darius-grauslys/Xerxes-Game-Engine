
namespace Xerxes.Game_Engine.Input
{
    public abstract class SA__Input_Key_Up :
        Streamline_Argument
    {
        public Key Input_Key_Up__Key_Up { get; }

        protected internal abstract void Handle_Update__Input_Key_Up
        (Key key_up);
    }
}

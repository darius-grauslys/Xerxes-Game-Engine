
namespace Xerxes.Game_Engine.Input
{
    public abstract class SA__Input_Key_Down :
        Streamline_Argument
    {
        public Key Input_Key_Down__Key_Down { get; }

        protected internal abstract void Handle_Update__Input_Key_Down
        (Key key_down);
    }
}


namespace Xerxes.Game_Engine.Input
{
    public abstract class SA__Input_Mouse_Button :
        Streamline_Argument
    {
        public abstract Mouse_Button Input_Mouse_Button__Mouse_Button { get; }

        protected internal abstract void Handle_Update__Input_Mouse_Button
        (Mouse_Button mouse_button);
    }
}

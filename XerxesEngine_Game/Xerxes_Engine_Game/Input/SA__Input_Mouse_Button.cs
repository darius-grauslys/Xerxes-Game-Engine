
namespace Xerxes.Game_Engine.Input
{
    public abstract class SA__Input_Mouse_Button :
        Streamline_Argument
    {
        public abstract Mouse_Button Input_Mouse_Button__Mouse_Button { get; protected set; }

        public abstract bool Check_If__Mouse_Button_Down__Input_Mouse_Button(Mouse_Button button);
    }
}

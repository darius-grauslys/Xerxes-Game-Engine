
namespace Xerxes.Game_Engine.Input
{
    public abstract class SA__Input_Key :
        Streamline_Argument
    {
        public abstract Key Input_Key__Event_Key { get; protected set; }

        public abstract bool Check_If__Key_Down__Input_Key(Key key);

        internal SA__Input_Key(){}
    }
}

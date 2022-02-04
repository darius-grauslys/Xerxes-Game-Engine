
namespace Xerxes.Game_Engine
{
    public class SA__Chronical :
        Streamline_Argument
    {
        public float Chronical__Delta_Time { get; internal set; }

        public SA__Chronical(SA__Chronical e)
        {
            Chronical__Delta_Time = e.Chronical__Delta_Time;
        }

        internal SA__Chronical()
        {
        }
    }
}

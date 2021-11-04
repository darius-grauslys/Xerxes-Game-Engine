namespace Xerxes_Engine.Export_OpenTK
{
    public class SA__Chronical :
        Streamline_Argument
    {
        public double Chronical__DELTA_TIME { get; }
        public double Chronical__ELAPSED_TIME { get; }

        internal SA__Chronical
        (
            double deltaTime,
            double elapsedTime
        )
        {
            Chronical__DELTA_TIME = deltaTime;
            Chronical__ELAPSED_TIME = elapsedTime;
        }

        internal SA__Chronical
        (SA__Chronical e)
        {
            Chronical__DELTA_TIME = e.Chronical__DELTA_TIME;
            Chronical__ELAPSED_TIME = e.Chronical__ELAPSED_TIME;
        }
    }
}

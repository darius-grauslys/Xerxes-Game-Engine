namespace Xerxes_Engine.Export_OpenTK
{
    public class SA__Update :
        SA__Chronical
    {
        internal static readonly SA__Update TIMELESS = new SA__Update(-1,-1);

        internal SA__Update
        (
            double deltaTime, 
            double elapsedTime
        ) 
        : base
        (
            deltaTime, 
            elapsedTime
        )
        {
        }

        internal SA__Update
        (
            SA__Update e
        )
        : base
        (
            e.Chronical__DELTA_TIME,
            e.Chronical__ELAPSED_TIME
        )
        {}
    }
}

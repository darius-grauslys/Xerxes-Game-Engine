
namespace Xerxes.Game_Engine
{
    public class SA__Operate_Feature_Chronical<TFeature> :
        SA__Operate_Feature<TFeature>
        where TFeature : IFeature
    {
        public float Operate_Feature_Chronical__DELTA_TIME { get; }

        public SA__Operate_Feature_Chronical
        (
            SA__Chronical e,
            TFeature feature
        ) 
        : base(feature)
        {
            Operate_Feature_Chronical__DELTA_TIME = e.Chronical__Delta_Time;
        }
    }
}

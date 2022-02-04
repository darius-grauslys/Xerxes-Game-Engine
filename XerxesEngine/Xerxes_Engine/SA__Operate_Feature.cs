
namespace Xerxes
{
    public class SA__Operate_Feature<TFeature> :
        Streamline_Argument
        where TFeature : IFeature
    {
        public TFeature Operate_Feature__Feature { get; set; }

        public SA__Operate_Feature(TFeature feature)
        {
            Operate_Feature__Feature = feature;
        }
    }
}

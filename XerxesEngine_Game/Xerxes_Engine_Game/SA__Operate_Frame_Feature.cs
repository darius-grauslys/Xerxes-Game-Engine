
namespace Xerxes.Game_Engine
{
    public class SA__Operate_Frame_Feature<TFeature> :
    SA__Operate_Feature<TFeature>
    where TFeature : IFeature
    {
        public double Operate_Frame_Feature__Delta_Time { get; internal set; }
        public double Operate_Frame_Feature__Elapsed_Time { get; internal set; }

        public SA__Operate_Frame_Feature(){}

        public SA__Operate_Frame_Feature(SA__Update e)
        {
            Operate_Frame_Feature__Delta_Time = e.Frame__Delta_Time;
            Operate_Frame_Feature__Elapsed_Time = e.Frame__Elapsed_Time;
        }

        public SA__Operate_Frame_Feature(SA__Render e)
        {
            Operate_Frame_Feature__Delta_Time = e.Frame__Delta_Time;
            Operate_Frame_Feature__Elapsed_Time = e.Frame__Elapsed_Time;
        }
    }
}

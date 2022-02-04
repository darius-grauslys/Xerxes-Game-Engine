
namespace Xerxes
{
    public abstract class System<SA, TFeature> :
        Xerxes_Object<System<SA, TFeature>>
        where SA       : SA__Operate_Feature<TFeature>
        where TFeature : IFeature
    {
        public System()
        {
            Declare__Streams()
                .Downstream.Receiving<SA>
                (Handle_Operate__Feature__System);
        }

        protected abstract void Handle_Operate__Feature__System
        (SA e);
    }
}

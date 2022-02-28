
namespace Xerxes
{
    public abstract class Xerxes_System<TFeature, TOperation> :
        Xerxes_Object<Xerxes_System<TFeature, TOperation>>
        where TFeature : IFeature
        where TOperation : SA__Operate_Feature<TFeature>
    {
        public Xerxes_System()
        {
            Declare__Streams()
                .Downstream.Receiving<TOperation>
                (Handle_Operate__Feature__System);
        }

        protected abstract void Handle_Operate__Feature__System
        (TOperation e);
    }
}

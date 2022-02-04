namespace Xerxes
{
    //internal interface IRoot {}
    public abstract class Root<Args,A,D> :
        Xerxes_Object<Root<Args,A,D>>//,
        //IRoot
        where Args : SA__Configure_Root 
        where A : SA__Associate_Root
        where D : SA__Dissassociate_Root
    {
        internal Export_Dictionary Internal_ROOT__EXPORTS { get; }

        protected Root()
        {
            Internal_ROOT__EXPORTS = new Export_Dictionary();

            Declare__Streams()
                .Upstream  .Extending<Args>()
                .Downstream.Extending<Args>()
                .Downstream.Extending<A>()
                .Downstream.Extending<D>()
                .Upstream  .Extending<A>()
                .Upstream  .Extending<D>();
        }

        protected internal abstract A Configure(Args configuration_args);

        protected internal abstract void Execute();

        protected bool Declare__Export<E>()
        where E : Xerxes_Export_Base, new()
        {
            E export = new E();
            Log.Write__Verbose__Log
            (
                Log.VERBOSE__ROOT__DECLARING_EXPORT_1,
                this,
                export
            );
            bool success =
                Internal_ROOT__EXPORTS
                .Internal_Declare__Export__Export_Dictionary(export);

            return success;
        }
    }
}

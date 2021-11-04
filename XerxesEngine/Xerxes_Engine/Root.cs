namespace Xerxes_Engine
{
    internal interface IRoot {}
    public class Root<A,D> :
        Xerxes_Object<Root<A,D>>,
        IRoot
        where A : SA__Associate_Root
        where D : SA__Dissassociate_Root
    {
        private Export_Dictionary _ROOT__EXPORTS { get; }

        public Root()
        {
            _ROOT__EXPORTS = new Export_Dictionary();

            Declare__Streams()
                .Downstream.Extending<A>()
                .Downstream.Extending<D>()
                .Upstream  .Extending<A>()
                .Upstream  .Extending<D>();
        }

        protected bool Seal(A e)
        {
            bool isSealed =
                Xerxes_Linker
                .Internal_Seal__Root(this, _ROOT__EXPORTS);

            if (!isSealed)
                return false;

            Invoke__Ascending
                (e);

            return true;
        }

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
                _ROOT__EXPORTS
                .Internal_Declare__Export__Export_Dictionary(export);

            return success;
        }
    }
}

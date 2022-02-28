
using System;

namespace Xerxes
{
    public abstract class Root<A_Event,D_Event> :
        Root_Base, IDisposable
        where A_Event : Root_Association_Event, new()
        where D_Event : Root_Dissassociation_Event, new()
    {
        protected Root()
        {
            Declare__Streams()
                .Upstream  .Extending<SA__Configure_Root>()
                .Downstream.Extending<SA__Configure_Root>()
                .Downstream.Extending<SA__Associate_Root>()
                .Upstream  .Extending<SA__Associate_Root>()
                .Downstream.Extending<SA__Dissassociate_Root>()
                .Upstream  .Extending<SA__Dissassociate_Root>();
        }

        public virtual void Dispose()
        {
            Invoke__Descending(new SA__Dissassociate_Root(new D_Event()));
        }

        protected bool Declare__Endpoint<E>()
        where E : Xerxes_Endpoint, new()
        {
            E import = new E();
            Log.Write__Verbose__Log
            (
                Log.VERBOSE__ROOT__DECLARING_IMPORT_1,
                this,
                import
            );
            bool success =
                Internal_ROOT__ENDPOINTS
                .Internal_Declare__Endpoint__Endpoint_Dictionary(import);

            return success;
        }
    }
}

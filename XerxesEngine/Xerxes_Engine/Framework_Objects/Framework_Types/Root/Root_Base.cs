
namespace Xerxes
{
    public abstract class Root_Base :
        Xerxes_Object_Base
    {
        internal Endpoint_Dictionary Internal_ROOT__ENDPOINTS { get; }

        internal Root_Base()
        {
            Internal_ROOT__ENDPOINTS = new Endpoint_Dictionary();
        }

        internal virtual void Internal_Configure__Root_Base(SA__Configure_Root e)
        {
            Handle__Configure__Root_Base(e);

            Invoke__Ascending(e);
            Invoke__Descending(e);
        }

        protected virtual void Handle__Configure__Root_Base(SA__Configure_Root e){}

        protected internal abstract void Execute();
    }

}

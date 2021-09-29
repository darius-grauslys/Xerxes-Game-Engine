namespace Xerxes_Engine
{
    public sealed class Xerxes_Childless<T> 
        : Xerxes_Descendant<T, Xerxes_Childless<T>>
          where T : Xerxes_Object
    {
        private Xerxes_Childless(){}
    }
}

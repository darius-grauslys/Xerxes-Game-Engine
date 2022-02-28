
namespace Xerxes
{
    public class Xerxes_Test_Instance<X_Object, X_Base> :
        Xerxes_Object<Xerxes_Test_Instance<X_Object, X_Base>>
        where X_Object : X_Base, new()
        where X_Base : Xerxes_Object<X_Base> 
    {
        public Xerxes_Test_Instance()
        {
            X_Object target = new X_Object();
            Declare__Hierarchy()
                .Internal_Associate__Descendant__Xerxes_Ancestry<X_Base>(target);
        }
    }
}

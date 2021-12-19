
namespace Xerxes_Engine
{
    internal class Xerxes_Test_Suite<Target> :
        Xerxes_Object<Xerxes_Test_Suite<Target>>
        where Target : Xerxes_Object<Target>, new()
    {
        public Xerxes_Test_Suite()
        {
            Target target = new Target();

            target
                .Declare__Ancestor<Xerxes_Test_Suite<Target>>();

            Declare__Descendant<Target>();

            Declare__Hierarchy()
                .Internal_Associate__Descendant__Xerxes_Ancestry<Target>(target);
        }
    }
}

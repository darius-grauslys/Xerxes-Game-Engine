
namespace Xerxes_Engine
{
    public class Xerxes_Test_Suite<Target>
    where Target : Xerxes_Object<Target>
    {
        public void Test<X_Object, SA>(SA streamline_argument)
        where X_Object : Target, new()
        where SA : Streamline_Argument
        {
            Xerxes.Test<X_Object, Target, SA>(streamline_argument);
        }
    }
}

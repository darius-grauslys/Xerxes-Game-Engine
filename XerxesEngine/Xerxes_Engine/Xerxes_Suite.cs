
namespace Xerxes
{
    public static class Xerxes_Suite
    {
        public static void Run<Xerxes_Root, A, D>
        (
            SA__Configure_Root xerxes_arguments
        )
        where Xerxes_Root : Root<A,D>, new()
        where A : Root_Association_Event, new()
        where D : Root_Dissassociation_Event, new()
        {
            Xerxes_Root instance = new Xerxes_Root();
            instance.Internal_Configure__Root_Base(xerxes_arguments);
            A associate_args = new A();

            Xerxes_Linker.Internal_Seal(instance, instance.Internal_ROOT__ENDPOINTS);

            instance.Invoke__Ascending(xerxes_arguments);
            instance.Invoke__Descending(xerxes_arguments);

            instance.Invoke__Association(associate_args);

            instance.Execute();
        }

        public static void Test<X_Object, X_Base, SA>
        (
            SA streamline_argument
        )
        where X_Object : X_Base, new()
        where X_Base : Xerxes_Object<X_Base>
        where SA : Streamline_Argument
        {
            Xerxes_Test_Instance<X_Object, X_Base> test_suite =
                new Xerxes_Test_Instance<X_Object, X_Base>();

            test_suite
                .Declare__Streams()
                .Downstream.Extending<SA__Configure_Root>()
                .Downstream.Extending<SA>();

            Xerxes_Linker.Internal_Seal(test_suite);

            test_suite.Invoke__Descending(streamline_argument);
        }

        public static void Test<X_Object, SA>
        (
            SA streamline_argument
        )
        where X_Object : Xerxes_Object<X_Object>, new()
        where SA : Streamline_Argument
        {
            Test<X_Object, X_Object, SA>(streamline_argument);
        }
    }
}

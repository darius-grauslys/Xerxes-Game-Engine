
namespace Xerxes_Engine
{
    public static class Xerxes
    {
        public static void Run<Xerxes_Root, Args, A, D>
        (
            Args xerxes_arguments
        )
        where Xerxes_Root : Root<Args,A,D>, new()
        where Args : SA__Configure_Root
        where A : SA__Associate_Root
        where D : SA__Dissassociate_Root
        {
            Xerxes_Root instance = new Xerxes_Root();
            A associate_args = instance.Configure(xerxes_arguments);

            Xerxes_Linker.Internal_Seal(instance, instance.Internal_ROOT__EXPORTS);

            instance.Invoke__Ascending(xerxes_arguments);
            instance.Invoke__Descending(xerxes_arguments);

            instance.Invoke__Ascending(associate_args);
            instance.Invoke__Descending(associate_args);

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

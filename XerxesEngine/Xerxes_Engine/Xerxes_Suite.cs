
namespace Xerxes
{
    public static class Xerxes_Suite
    {
        public static void Run<Xerxes_Root>
        (
            SA__Configure_Root xerxes_arguments
        )
        where Xerxes_Root : Root, new()
        {
            Xerxes_Root instance = new Xerxes_Root();
            instance.Internal_Configure__Root_Base(xerxes_arguments);

            Xerxes_Linker.Internal_Seal(instance);

            instance.Invoke__Ascending(xerxes_arguments);
            instance.Invoke__Descending(xerxes_arguments);

            instance.Execute();
        }

        public static void Test<XObject, XBase, TGenology, SA>
        (
            SA streamline_argument,
            SA__Configure_Root configure = null
        )
        where XObject : XBase, new()
        where XBase : Xerxes_Object<TGenology>, new()
        where TGenology : Xerxes_Genology, new()
        where SA : Streamline_Argument
        {
            Xerxes_Object test_suite = new Xerxes_Object();

            test_suite
                .Genology
                .Declare__Streamlines
                    .With__Descendants
                        .Extending<SA__Configure_Root>()
                        .Extending<SA>();

            Xerxes_Linker.Internal_Seal(test_suite);

            if (configure != null)
                test_suite.Invoke__Descending(configure);
            test_suite.Invoke__Descending(streamline_argument);
        }

        public static void Test<X_Object, SA>
        (
            SA streamline_argument
        )
        where X_Object : Xerxes_Object, new()
        where SA : Streamline_Argument
        {
            Test<X_Object, Xerxes_Object, Xerxes_Genology__Standard, SA>(streamline_argument);
        }
    }
}


namespace Xerxes
{
    public static class Xerxes_Suite
    {
        public static void Run<Xerxes_Root>
        (
            string[] args,
            Log_Arguments log_args = null,
            bool logging = true
        )
        where Xerxes_Root : Root, new()
            => Run<Xerxes_Root>(new SA__Configure_Root(args), log_args, logging);

        public static void Run<Xerxes_Root>
        (
            SA__Configure_Root xerxes_arguments,
            Log_Arguments log_args = null,
            bool logging = true
        )
        where Xerxes_Root : Root, new()
        {
            if(logging)
                Log.Initalize__Log(log_args != null ? log_args : new Log_Arguments());

            Xerxes_Root instance = new Xerxes_Root();

            Xerxes_Linker.Internal_Seal(instance);

            instance.Internal_Configure__Root_Base(xerxes_arguments);

            instance.Execute();
        }

        public static void Test<XObject, XBase, TGenealogy, SA>
        (
            SA streamline_argument,
            SA__Configure_Root configure = null
        )
        where XObject : XBase, new()
        where XBase : Xerxes_Object<TGenealogy>, new()
        where TGenealogy : Xerxes_Genealogy, new()
        where SA : Streamline_Argument
        {
            Xerxes_Object test_suite = new Xerxes_Object();

            test_suite
                .Genealogy
                .Genealogy__Streamlines__Protected
                    .Streamlines__Primary_Stream__Protected
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
            Test<X_Object, Xerxes_Object, Xerxes_Genealogy__Standard, SA>(streamline_argument);
        }
    }
}

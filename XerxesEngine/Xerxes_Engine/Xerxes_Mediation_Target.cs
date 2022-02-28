
namespace Xerxes
{
    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA
    > :
    Xerxes_Object
    <
        Xerxes_Mediation_Target
        <
            TTarget,
            SA
        >
    >
    where TTarget : Xerxes_Object_Base, new()
    where SA      : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA>>
                (Handle_Mediation__Xerxes_Mediation_Target)
                .Downstream.Extending<SA>();

            Declare__Hierarchy()
                .Associate<TTarget>();
        }

        protected virtual void Handle_Mediation__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }

    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA1,
        SA2
    > :
    Xerxes_Object
    <
        Xerxes_Mediation_Target
        <
            TTarget,
            SA1,
            SA2
        >
    >
    where TTarget : Xerxes_Object_Base, new()
    where SA1     : Streamline_Argument
    where SA2     : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA1>>
                (Handle_Mediation__SA1__Xerxes_Mediation_Target)
                .Downstream.Extending<SA1>()
                .Downstream.Receiving<SA__Mediate<TTarget, SA2>>
                (Handle_Mediation__SA2__Xerxes_Mediation_Target)
                .Downstream.Extending<SA2>();

            Declare__Hierarchy()
                .Associate<TTarget>();
        }

        protected virtual void Handle_Mediation__SA1__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA1> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }

        protected virtual void Handle_Mediation__SA2__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA2> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }

    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA1,
        SA2,
        SA3
    > :
    Xerxes_Object
    <
        Xerxes_Mediation_Target
        <
            TTarget,
            SA1,
            SA2,
            SA3
        >
    >
    where TTarget : Xerxes_Object_Base, new()
    where SA1     : Streamline_Argument
    where SA2     : Streamline_Argument
    where SA3     : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA1>>
                (Handle_Mediation__SA1__Xerxes_Mediation_Target)
                .Downstream.Extending<SA1>()
                .Downstream.Receiving<SA__Mediate<TTarget, SA2>>
                (Handle_Mediation__SA2__Xerxes_Mediation_Target)
                .Downstream.Extending<SA2>()
                .Downstream.Receiving<SA__Mediate<TTarget, SA3>>
                (Handle_Mediation__SA3__Xerxes_Mediation_Target)
                .Downstream.Extending<SA3>();

            Declare__Hierarchy()
                .Associate<TTarget>();
        }

        protected virtual void Handle_Mediation__SA1__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA1> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }

        protected virtual void Handle_Mediation__SA2__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA2> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }

        protected virtual void Handle_Mediation__SA3__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA3> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }

    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA1,
        SA2,
        SA3,
        SA4
    > :
    Xerxes_Mediation_Target<TTarget, SA1, SA2, SA3>
    where TTarget : Xerxes_Object_Base, new()
    where SA1     : Streamline_Argument
    where SA2     : Streamline_Argument
    where SA3     : Streamline_Argument
    where SA4     : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA4>>
                (Handle_Mediation__SA4__Xerxes_Mediation_Target)
                .Downstream.Extending<SA4>();
        }

        protected virtual void Handle_Mediation__SA4__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA4> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }

    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA1,
        SA2,
        SA3,
        SA4,
        SA5
    > :
    Xerxes_Mediation_Target<TTarget, SA1, SA2, SA3, SA4>
    where TTarget : Xerxes_Object_Base, new()
    where SA1     : Streamline_Argument
    where SA2     : Streamline_Argument
    where SA3     : Streamline_Argument
    where SA4     : Streamline_Argument
    where SA5     : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA5>>
                (Handle_Mediation__SA5__Xerxes_Mediation_Target)
                .Downstream.Extending<SA5>();
        }

        protected virtual void Handle_Mediation__SA5__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA5> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }

    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA1,
        SA2,
        SA3,
        SA4,
        SA5,
        SA6
    > :
    Xerxes_Mediation_Target<TTarget, SA1, SA2, SA3, SA4, SA5>
    where TTarget : Xerxes_Object_Base, new()
    where SA1     : Streamline_Argument
    where SA2     : Streamline_Argument
    where SA3     : Streamline_Argument
    where SA4     : Streamline_Argument
    where SA5     : Streamline_Argument
    where SA6     : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA6>>
                (Handle_Mediation__SA6__Xerxes_Mediation_Target)
                .Downstream.Extending<SA6>();
        }

        protected virtual void Handle_Mediation__SA6__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA6> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }

    public class Xerxes_Mediation_Target
    <
        TTarget, 
        SA1,
        SA2,
        SA3,
        SA4,
        SA5,
        SA6,
        SA7
    > :
    Xerxes_Mediation_Target<TTarget, SA1, SA2, SA3, SA4, SA5, SA6>
    where TTarget : Xerxes_Object_Base, new()
    where SA1     : Streamline_Argument
    where SA2     : Streamline_Argument
    where SA3     : Streamline_Argument
    where SA4     : Streamline_Argument
    where SA5     : Streamline_Argument
    where SA6     : Streamline_Argument
    where SA7     : Streamline_Argument
    {
        public Xerxes_Mediation_Target()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Mediate<TTarget, SA7>>
                (Handle_Mediation__SA7__Xerxes_Mediation_Target)
                .Downstream.Extending<SA7>();
        }

        protected virtual void Handle_Mediation__SA7__Xerxes_Mediation_Target
        (SA__Mediate<TTarget, SA7> mediation)
        {
            Invoke__Descending(mediation.Mediate__Streamline_Argument);
        }
    }
}

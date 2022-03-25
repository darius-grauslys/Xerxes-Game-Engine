
namespace Xerxes
{
    public class Xerxes_Object__Switch :
    Xerxes_Object__Switch<Xerxes_Genealogy__Standard_Switch>
    {}

    public class Xerxes_Object__Switch<GSwitch> :
    Xerxes_Object<GSwitch>
    where GSwitch : Xerxes_Genealogy__Standard_Switch, new()
    {
        protected internal void Switch__Descending<SA, XTarget>()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Genealogy
                .Genealogy_Switch__Switcher__Protected
                    .Switch_Table__Primary_Table__Protected
                        .Switcher__Switch_Descending__Internal
                            .Internal_Set__Active_Object__Switch<SA, XTarget>();
        }

        protected internal void Switch__All__Descending<XTarget>()
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Genealogy
                .Genealogy_Switch__Switcher__Protected
                    .Switch_Table__Primary_Table__Protected
                        .Switcher__Switch_Descending__Internal
                            .Internal_Set__All__Switch<XTarget>();
        }

        protected void Mediate__Switch__Descending<SA>(SA e)
        where SA : Streamline_Argument
        {
            Genealogy
                .Genealogy_Switch__Switcher__Protected
                    .Switch_Table__Primary_Table__Protected
                        .Internal_Mediate__Descending__Switcher(e);
        }

        protected void Handle__Specific_Transition
        <
            SA__Transition,
            SA,
            XTarget
        >(SA__Transition transition)
        where SA__Transition :
        SA__Transition_Switch<SA, XTarget>
        where SA :
        Streamline_Argument
        where XTarget : 
        Xerxes_Object_Base, new()
        {
            transition
                .Internal_Handle__Transition__Transition_Switch_Base
                <
                    Xerxes_Object__Switch
                    <
                        GSwitch
                    >, 
                    GSwitch
                >(this);
        }

        protected void Handle__Complete_Transition
        <
            SA__Transition,
            XTarget
        >(SA__Transition transition)
        where SA__Transition :
        SA__Transition_Switch<XTarget>
        where XTarget :
        Xerxes_Object_Base, new()
        {
            transition
                .Internal_Handle__Transition__Transition_Switch_Base
                <
                    Xerxes_Object__Switch
                    <
                        GSwitch
                    >,
                    GSwitch
                >(this);
        }
    }

    public class Xerxes_Object__Mediated :
    Xerxes_Object<Xerxes_Genealogy__Standard_Mediator>
    {
        protected void Mediate__Descending<SA, XTarget>(SA e)
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Invoke__Descending(new SA__Mediate<XTarget, SA>() { Mediate__Mediated_Streamline_Argument = e });
        }
    }

    public class Xerxes_Object :
    Xerxes_Object<Xerxes_Genealogy__Standard>
    {
    }

    /// <summary>
    /// Represents a type in Xerxes_Engine that depends on the
    /// Update/Render control flow. All internalized logging messages
    /// are related to such objects - or systems.
    ///
    /// Xerxes_Object requires a self reference to T. Any other type
    /// will cause a critical error.
    ///
    /// Calls to Update and Render are internalized. Exposure to
    /// handling these calls are given via protected virtual definitions.
    /// </summary>
    public class Xerxes_Object<TGenealogy> : 
    Xerxes_Object_Base 
    where TGenealogy : Xerxes_Genealogy, new()
    {
        protected internal TGenealogy Genealogy { get; }

        public Xerxes_Object()
        {
            Genealogy =
                new TGenealogy();

            Internal_Set__Genealogy__Xerxes_Engine_Object(Genealogy);
        }
    }
}

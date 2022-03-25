
namespace Xerxes 
{
    public class SA__Transition_Switch<SA, XTarget> :
    SA__Transition_Switch_Base
    where SA : Streamline_Argument
    where XTarget : Xerxes_Object_Base, new()
    {
        internal override void Internal_Handle__Transition__Transition_Switch_Base
        <
            XSwitch, 
            TGenealogy
        >(XSwitch xswitch)
        {
            xswitch
                .Switch__Descending<SA, XTarget>();
        }
    }

    public class SA__Transition_Switch<XTarget> :
    SA__Transition_Switch_Base
    where XTarget :
    Xerxes_Object_Base, new()
    {
        internal override void Internal_Handle__Transition__Transition_Switch_Base
        <
            XSwitch, 
            TGenealogy
        >(XSwitch xswitch)
        {
            xswitch
                .Switch__All__Descending<XTarget>();
        }
    }

    internal delegate void Switch_Delegate<SA, XTarget>()
        where SA : Streamline_Argument
        where XTarget : Xerxes_Object_Base, new();

    public abstract class SA__Transition_Switch_Base :
    Streamline_Argument
    {
        internal SA__Transition_Switch_Base(){}

        internal abstract void Internal_Handle__Transition__Transition_Switch_Base
        <
            XSwitch,
            TGenealogy
        >(XSwitch xswitch)
        where XSwitch :
        Xerxes_Object__Switch<TGenealogy>
        where TGenealogy :
        Xerxes_Genealogy__Standard_Switch, new();
    }
}

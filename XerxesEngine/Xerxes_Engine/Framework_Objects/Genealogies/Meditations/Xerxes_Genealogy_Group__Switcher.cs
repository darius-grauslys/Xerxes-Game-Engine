
namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Switcher
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Child
    <
        TGenealogy,
        TParent
    >
    where TGenealogy :
    Xerxes_Genealogy
    where TParent :
    Xerxes_Genealogy_Group__Switch_Table
    <
        TGenealogy
    >
    {
        internal Switch Switcher__Switch_Descending__Internal { get; private set; }

        public Xerxes_Genealogy_Group__Switcher()
        {
            Switcher__Switch_Descending__Internal =
                new Switch();
        }

        protected internal void Protected_Declare__Descendant_Switch_Target__Switcher
        <SA, XTarget>()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Switcher__Switch_Descending__Internal
                .Internal_Add__Switch_Table_Entry__Switch<SA, XTarget>();
        }

        internal void Internal_Mediate__Descending__Switcher<SA>(SA e)
        where SA :
        Streamline_Argument
        {
            Switcher__Switch_Descending__Internal
                .Internal_Mediate__Descending__Switch(e, Genealogy_Group__Enclosing_Object__Internal);
        }
    }
}

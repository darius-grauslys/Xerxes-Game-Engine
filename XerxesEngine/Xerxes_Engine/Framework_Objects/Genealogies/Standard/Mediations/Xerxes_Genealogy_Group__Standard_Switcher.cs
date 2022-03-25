
namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Switcher__Descendants
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Switcher
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
        public 
            Xerxes_Genealogy_Group__Standard_Switcher__Descendants
            <
                TGenealogy,
                TParent
            >
            Declare__Switch<SA, XTarget>()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Declare__Descendant_Switch_Target__Switcher
            <
                SA,
                XTarget
            >();

            return this;
        }



        public TParent Finish__With_Switching
            => Genealogy_Group_Child__Enclosing_Parent;

        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }
    }

    /*
    public class Xerxes_Genealogy_Group__Standard_Switcher__Ancestors
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Switcher
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
        public 
            Xerxes_Genealogy_Group__Standard_Switcher__Ancestors
            <
                TGenealogy,
                TParent
            >
            Declare__Switch<SA, XTarget>()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Declare__Ancestor_Switch_Target__Switcher
            <
                SA,
                XTarget
            >();

            return this;
        }




        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }
    }
    */
}


namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Switch_Table__Descendants
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Switch_Table
    <
        Xerxes_Genealogy_Group__Standard_Switch_Table__Descendants
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Switcher__Descendants
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Switch_Table__Descendants
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Switcher__Descendants
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Switch_Table__Descendants
                <
                    TGenealogy
                >
            >
            Switch__On_Descendants
            => Switch_Table__Primary_Table__Protected;




        public TGenealogy Finish__With_Switches
            => Genealogy_Group__Enclosing_Genealogy;
    }

    /*
    public class Xerxes_Genealogy_Group__Standard_Switch_Table__Ancestors
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Switch_Table
    <
        Xerxes_Genealogy_Group__Standard_Switch_Table__Ancestors
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Switcher__Ancestors
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Switch_Table__Ancestors
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Switcher__Ancestors
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Switch_Table__Ancestors
                <
                    TGenealogy
                >
            >
            Switch__On_Ancestors
            => Switch_Table__Primary_Table__Protected;




        public TGenealogy Finish__With_Switches
            => Genealogy_Group__Enclosing_Genealogy;
    }

    public class Xerxes_Genealogy_Group__Standard_Switch_Table__Intermediate
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Switch_Table
    <
        Xerxes_Genealogy_Group__Standard_Switch_Table__Intermediate
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Switcher__Descendants
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Switch_Table__Intermediate
            <
                TGenealogy
            >
        >,
        Xerxes_Genealogy_Group__Standard_Switcher__Ancestors
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Switch_Table__Intermediate
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Switcher__Descendants
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Switch_Table__Intermediate
                <
                    TGenealogy
                >
            >
            Switch__On_Descendants
            => Switch_Table__Primary_Table__Protected;
        public 
            Xerxes_Genealogy_Group__Standard_Switcher__Ancestors
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Switch_Table__Intermediate
                <
                    TGenealogy
                >
            >
            Switch__On_Ancestors
            => Switch_Table__Secondary_Table__Protected;




        public TGenealogy Finish__With_Switches
            => Genealogy_Group__Enclosing_Genealogy;
    }
    */
}

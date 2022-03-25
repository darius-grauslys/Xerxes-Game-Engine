
namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Switch_Table
    <
        TThis,
        TGenealogy,
        TPrimary_Table
    > :
    Xerxes_Genealogy_Group__Switch_Table
    <
        TGenealogy
    >
    where TThis :
    Xerxes_Genealogy_Group__Switch_Table
    <
        TThis,
        TGenealogy,
        TPrimary_Table
    >
    where TGenealogy :
    Xerxes_Genealogy
    where TPrimary_Table :
    Xerxes_Genealogy_Group__Switcher
    <
        TGenealogy,
        TThis
    >, new()
    {
        protected internal TPrimary_Table Switch_Table__Primary_Table__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy_Group()
        {
            Switch_Table__Primary_Table__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TPrimary_Table
                >();
        }
    }

    public abstract class Xerxes_Genealogy_Group__Switch_Table 
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    where TGenealogy :
    Xerxes_Genealogy
    {

    }
}

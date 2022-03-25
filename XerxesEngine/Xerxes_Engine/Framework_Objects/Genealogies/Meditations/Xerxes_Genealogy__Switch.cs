
namespace Xerxes
{
    public abstract class Xerxes_Genealogy__Switch
    <
        TThis,
        TSwitcher,
        TStreamlines
    > : 
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines
    >
    where TThis :
    Xerxes_Genealogy__Switch
    <
        TThis,
        TSwitcher,
        TStreamlines
    >, new()
    where TSwitcher : 
    Xerxes_Genealogy_Group__Switch_Table
    <
        TThis
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    {
        protected internal TSwitcher Genealogy_Switch__Switcher__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy()
        {
            base.Handle_Linking__Genealogy();

            Genealogy_Switch__Switcher__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TSwitcher
                >();
        }
    }

    public abstract class Xerxes_Genealogy__Switch
    <
        TThis,
        TSwitcher,
        TStreamlines,
        TAssociations
    > : 
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations
    >
    where TThis :
    Xerxes_Genealogy__Switch
    <
        TThis,
        TSwitcher,
        TStreamlines,
        TAssociations
    >, new()
    where TSwitcher :
    Xerxes_Genealogy_Group__Switch_Table
    <
        TThis
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TThis
    >, new()
    {
        protected internal TSwitcher Genealogy_Switch__Switcher__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy()
        {
            base.Handle_Linking__Genealogy();

            Genealogy_Switch__Switcher__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TSwitcher
                >();
        }
    }

    public abstract class Xerxes_Genealogy__Switch
    <
        TThis,
        TSwitcher,
        TStreamlines,
        TAssociations,
        TEndpoints
    > : 
    Xerxes_Genealogy
    <
        TThis,
        TStreamlines,
        TAssociations,
        TEndpoints
    >
    where TThis :
    Xerxes_Genealogy__Switch
    <
        TThis,
        TSwitcher,
        TStreamlines,
        TAssociations,
        TEndpoints
    >, new()
    where TSwitcher :
    Xerxes_Genealogy_Group__Switch_Table
    <
        TThis
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TThis
    >, new()
    where TAssociations :
    Xerxes_Genealogy_Group__Associations
    <
        TThis
    >, new()
    where TEndpoints :
    Xerxes_Genealogy_Group__Endpoints
    <
        TThis
    >, new()
    {
        protected internal TSwitcher Genealogy_Switch__Switcher__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy()
        {
            base.Handle_Linking__Genealogy();

            Genealogy_Switch__Switcher__Protected =
                Protected_Link__Genealogy_Group__Genealogy
                <
                    TThis,
                    TSwitcher
                >();
        }
    }
}

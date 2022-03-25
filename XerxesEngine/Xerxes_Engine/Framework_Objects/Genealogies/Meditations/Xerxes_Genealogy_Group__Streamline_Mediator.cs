
namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Streamline_Mediator
    <
        TThis,
        TGenealogy,
        TPrimary_Extender
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >
    where TThis :
    Xerxes_Genealogy_Group__Streamline_Mediator
    <
        TThis,
        TGenealogy,
        TPrimary_Extender
    >, new()
    where TGenealogy :
    Xerxes_Genealogy
    where TPrimary_Extender :
    Xerxes_Genealogy_Group__Stream_Mediator
    <
        TGenealogy,
        TThis
    >, new()
    {
        protected TPrimary_Extender Streamline_Mediator__Primary_Extender__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy_Group()
        {
            Streamline_Mediator__Primary_Extender__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TPrimary_Extender
                >();
        }
    }

    public abstract class Xerxes_Genealogy_Group__Streamline_Mediator
    <
        TThis,
        TGenealogy,
        TPrimary_Extender,
        TSecondary_Extender
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >
    where TThis :
    Xerxes_Genealogy_Group__Streamline_Mediator
    <
        TThis,
        TGenealogy,
        TPrimary_Extender,
        TSecondary_Extender
    >, new()
    where TGenealogy :
    Xerxes_Genealogy
    where TPrimary_Extender :
    Xerxes_Genealogy_Group__Stream_Mediator
    <
        TGenealogy,
        TThis
    >, new()
    where TSecondary_Extender :
    Xerxes_Genealogy_Group__Stream_Mediator
    <
        TGenealogy,
        TThis
    >, new()
    {
        protected TPrimary_Extender Streamline_Mediator__Primary_Extender__Protected { get; private set; }
        protected TSecondary_Extender Streamline_Mediator__Secondary_Extender__Protected { get; private set; }

        protected internal override void Handle_Linking__Genealogy_Group()
        {
            Streamline_Mediator__Primary_Extender__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TPrimary_Extender
                >();

            Streamline_Mediator__Secondary_Extender__Protected =
                Protected_Link__Child_Group__Genealogy_Group
                <
                    TThis,
                    TSecondary_Extender
                >();
        }
    }
}

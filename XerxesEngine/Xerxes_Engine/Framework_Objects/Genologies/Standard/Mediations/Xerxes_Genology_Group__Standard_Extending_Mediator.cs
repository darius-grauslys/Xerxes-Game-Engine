
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Extending_Mediator__Descendants
    <
        TGenology,
        TParent,
        XTarget
    > :
    Xerxes_Genology_Group__Extending_Mediator
    <
        TGenology,
        TParent,
        XTarget
    >
    where TGenology :
    Xerxes_Genology
    where TParent :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where XTarget :
    Xerxes_Object_Base, new()
    {
        public 
            Xerxes_Genology_Group__Standard_Extending_Mediator__Descendants
            <
                TGenology,
                TParent,
                XTarget
            >
            Mediate__Extending
            <
                SA
            >()
        where SA :
        Streamline_Argument
        {
            Protected_Mediate__Descendants__Mediator<SA>();

            return this;
        }

        protected internal override void Handle_Linking__Genology_Group()
        {
        }
    }

    public class Xerxes_Genology_Group__Standard_Extending_Mediator__Ancestors
    <
        TGenology,
        TParent,
        XTarget
    > :
    Xerxes_Genology_Group__Extending_Mediator
    <
        TGenology,
        TParent,
        XTarget
    >
    where TGenology :
    Xerxes_Genology
    where TParent :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where XTarget :
    Xerxes_Object_Base, new()
    {
        public 
            Xerxes_Genology_Group__Standard_Extending_Mediator__Ancestors
            <
                TGenology,
                TParent,
                XTarget
            >
            Mediate__Extending
            <
                SA
            >()
        where SA :
        Streamline_Argument
        {
            Protected_Mediate__Ancestors__Mediator<SA>();

            return this;
        }

        protected internal override void Handle_Linking__Genology_Group()
        {
        }
    }
}

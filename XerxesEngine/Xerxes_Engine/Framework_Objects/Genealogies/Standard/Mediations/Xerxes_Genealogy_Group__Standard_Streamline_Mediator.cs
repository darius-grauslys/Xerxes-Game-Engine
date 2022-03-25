
namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Descendants 
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Descendants
        <
            TGenealogy
        >, 
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Descendants
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Descendants
                <
                    TGenealogy
                >
            >
            With__Mediated_Descendants
            => Streamlines__Primary_Stream__Protected;




        public TGenealogy Finish__With_Streamlines
            => Genealogy_Group__Enclosing_Genealogy;
    }

    public class Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Ancestors
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Ancestors
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Ascending_Streams
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Ancestors
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Ascending_Streams
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Ancestors
                <
                    TGenealogy
                >
            >
            With__Mediated_Ancestors
            => Streamlines__Primary_Stream__Protected;




        public TGenealogy Finish__With_Streamlines
            => Genealogy_Group__Enclosing_Genealogy;
    }

    public class Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
            <
                TGenealogy
            >
        >,
        Xerxes_Genealogy_Group__Standard_Ascending_Streams
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
                <
                    TGenealogy
                >
            >
            With__Mediated_Descendants
            => Streamlines__Primary_Stream__Protected;

        public 
            Xerxes_Genealogy_Group__Standard_Ascending_Streams
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamline_Mediator__Intermediate
                <
                    TGenealogy
                >
            >
            With__Mediated_Ancestors
            => Streamlines__Secondary_Stream__Protected;




        public TGenealogy Finish__With_Streamlines
            => Genealogy_Group__Enclosing_Genealogy;
    }
}

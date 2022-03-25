
namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Streamlines_Descending
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        Xerxes_Genealogy_Group__Standard_Streamlines_Descending
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Descending_Streams
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamlines_Descending
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Descending_Streams
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamlines_Descending
                <
                    TGenealogy
                >
            >
            With__Descendants
            => Streamlines__Primary_Stream__Protected;
    }

    public class Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Streamlines
    <
        Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
        <
            TGenealogy
        >,
        TGenealogy,
        Xerxes_Genealogy_Group__Standard_Descending_Streams
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
            <
                TGenealogy
            >
        >,
        Xerxes_Genealogy_Group__Standard_Ascending_Streams
        <
            TGenealogy,
            Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
            <
                TGenealogy
            >
        >
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        public 
            Xerxes_Genealogy_Group__Standard_Descending_Streams
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
                <
                    TGenealogy
                >
            >
            With__Descendants
            => Streamlines__Primary_Stream__Protected;

        public 
            Xerxes_Genealogy_Group__Standard_Ascending_Streams
            <
                TGenealogy,
                Xerxes_Genealogy_Group__Standard_Streamlines_Intermediate
                <
                    TGenealogy
                >
            >
            With__Ancestors
            => Streamlines__Secondary_Stream__Protected;
    }
}

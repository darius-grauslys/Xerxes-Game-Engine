
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Endpoints
    <
        TGenology
    > :
    Xerxes_Genology_Group__Endpoints
    <
        TGenology
    >
    where TGenology :
    Xerxes_Genology
    {
        protected internal override void Handle_Linking__Genology_Group()
        {
        }

        public Xerxes_Genology_Group__Standard_Endpoints<TGenology> Endpoint
        <
            XEndpoint
        >()
        where XEndpoint :
        Xerxes_Object<Xerxes_Genology__Standard_Endpoint>, new()
        {
            Protected_Declare__Endpoint__Endpoints
            <
                XEndpoint,
                Xerxes_Genology__Standard_Endpoint,
                Xerxes_Genology_Group__Standard_Streamlines_Descending
                <
                    Xerxes_Genology__Standard_Endpoint
                >,
                Xerxes_Genology_Group__Standard_Descending_Streams
                <
                    Xerxes_Genology__Standard_Endpoint,
                    Xerxes_Genology_Group__Standard_Streamlines_Descending
                    <
                        Xerxes_Genology__Standard_Endpoint
                    >
                >
            >();

            return this;
        }

        public Xerxes_Genology_Group__Standard_Endpoints<TGenology> Endpoint
        <
            XEndpoint,
            XGenology,
            XStreamlines,
            XDescending_Streams
        >()
        where XEndpoint :
        Xerxes_Object<XGenology>, new()
        where XGenology:
        Xerxes_Genology
        <
            XGenology,
            XStreamlines
        >, new()
        where XStreamlines :
        Xerxes_Genology_Group__Streamlines
        <
            XStreamlines,
            XGenology,
            XDescending_Streams
        >, new()
        where XDescending_Streams :
        Xerxes_Genology_Group__Descending_Streams
        <
            XDescending_Streams,
            XGenology,
            XStreamlines
        >, new()
        {
            Protected_Declare__Endpoint__Endpoints
            <
                XEndpoint,
                XGenology,
                XStreamlines,
                XDescending_Streams
            >();

            return this;
        }



        public TGenology Finish__With_Endpoints
            => Genology_Group__Enclosing_Genology;
    }
}

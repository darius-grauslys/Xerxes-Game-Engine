
namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Endpoints
    <
        TGenealogy
    > :
    Xerxes_Genealogy_Group__Endpoints
    <
        TGenealogy
    >
    where TGenealogy :
    Xerxes_Genealogy
    {
        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }

        public Xerxes_Genealogy_Group__Standard_Endpoints<TGenealogy> Endpoint
        <
            XEndpoint
        >()
        where XEndpoint :
        Xerxes_Object<Xerxes_Genealogy__Standard_Endpoint>, new()
        {
            Protected_Declare__Endpoint__Endpoints
            <
                XEndpoint,
                Xerxes_Genealogy__Standard_Endpoint,
                Xerxes_Genealogy_Group__Standard_Streamlines_Descending
                <
                    Xerxes_Genealogy__Standard_Endpoint
                >,
                Xerxes_Genealogy_Group__Standard_Descending_Streams
                <
                    Xerxes_Genealogy__Standard_Endpoint,
                    Xerxes_Genealogy_Group__Standard_Streamlines_Descending
                    <
                        Xerxes_Genealogy__Standard_Endpoint
                    >
                >
            >();

            return this;
        }

        public Xerxes_Genealogy_Group__Standard_Endpoints<TGenealogy> Endpoint
        <
            XEndpoint,
            XGenealogy,
            XStreamlines,
            XDescending_Streams
        >()
        where XEndpoint :
        Xerxes_Object<XGenealogy>, new()
        where XGenealogy:
        Xerxes_Genealogy
        <
            XGenealogy,
            XStreamlines
        >, new()
        where XStreamlines :
        Xerxes_Genealogy_Group__Streamlines
        <
            XStreamlines,
            XGenealogy,
            XDescending_Streams
        >, new()
        where XDescending_Streams :
        Xerxes_Genealogy_Group__Streams
        <
            XGenealogy,
            XStreamlines
        >, new()
        {
            Protected_Declare__Endpoint__Endpoints
            <
                XEndpoint,
                XGenealogy,
                XStreamlines,
                XDescending_Streams
            >();

            return this;
        }



        public TGenealogy Finish__With_Endpoints
            => Genealogy_Group__Enclosing_Genealogy;
    }
}

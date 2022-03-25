
namespace Xerxes
{
    public class Xerxes_Endpoint
    <
        TGenealogy
    > :
    Xerxes_Object<TGenealogy>
    where TGenealogy :
    Xerxes_Genealogy__Standard_Endpoint, new()
    {}

    public class Xerxes_Endpoint
    <
        TGenealogy,
        EStreamline,
        EDescending_Streams
    > :
    Xerxes_Object<TGenealogy>
    where TGenealogy : 
    Xerxes_Genealogy
    <
        TGenealogy,
        EStreamline
    >, new()
    where EStreamline :
    Xerxes_Genealogy_Group__Streamlines
    <
        EStreamline,
        TGenealogy,
        EDescending_Streams
    >, new()
    where EDescending_Streams :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        EStreamline
    >, new()
    {
        protected Xerxes_Endpoint()
        {
        }
    }
}


namespace Xerxes
{
    public abstract class Xerxes_System
    <
        TFeature, 
        TOperation,
        TGenealogy, 
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream
    > :
    Xerxes_Object
    <
        TGenealogy
    >
    where TFeature : 
    IFeature
    where TOperation : 
    SA__Operate_Feature
    <
        TFeature
    >
    where TGenealogy : 
    Xerxes_Genealogy
    <
        TGenealogy,
        TStreamlines
    >, new()
    where TStreamlines :
    Xerxes_Genealogy_Group__Streamlines
    <
        TStreamlines,
        TGenealogy,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TStreamlines
    >, new()
    {
        public Xerxes_System()
        {
        }

        protected abstract void Handle_Operate__Feature__System
        (TOperation e);
    }
}

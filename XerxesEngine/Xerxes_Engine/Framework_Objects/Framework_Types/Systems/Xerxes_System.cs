
namespace Xerxes
{
    public abstract class Xerxes_System
    <
        TFeature, 
        TOperation,
        TGenology, 
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream
    > :
    Xerxes_Object
    <
        TGenology
    >
    where TFeature : 
    IFeature
    where TOperation : 
    SA__Operate_Feature
    <
        TFeature
    >
    where TGenology : 
    Xerxes_Genology
    <
        TGenology,
        TStreamlines,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TStreamlines :
    Xerxes_Genology_Group__Streamlines
    <
        TStreamlines,
        TGenology,
        TDescending_Stream,
        TAscending_Stream
    >, new()
    where TDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        TDescending_Stream,
        TGenology,
        TStreamlines
    >, new()
    where TAscending_Stream :
    Xerxes_Genology_Group__Ascending_Streams
    <
        TAscending_Stream,
        TGenology,
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

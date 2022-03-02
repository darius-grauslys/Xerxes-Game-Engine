
namespace Xerxes
{
    public abstract class Xerxes_System
    <
        TGenology, 
            GStreamlines_Descending,
            GDescending_Stream,
        TFeature, 
        TOperation
    > :
    Xerxes_Object<TGenology>
    where TGenology : 
    Xerxes_Genology__Descending
    <
        TGenology,
        GStreamlines_Descending,
        GDescending_Stream
    >, new()
    where GStreamlines_Descending :
    Xerxes_Genology_Group__Streamlines_Descending
    <
        GStreamlines_Descending,
        TGenology,
        GDescending_Stream
    >, new()
    where GDescending_Stream :
    Xerxes_Genology_Group__Descending_Streams
    <
        GDescending_Stream,
        TGenology,
        GStreamlines_Descending
    >, new()
    where TFeature : IFeature
    where TOperation : SA__Operate_Feature<TFeature>
    {
        public Xerxes_System()
        {
        }

        protected abstract void Handle_Operate__Feature__System
        (TOperation e);
    }
}

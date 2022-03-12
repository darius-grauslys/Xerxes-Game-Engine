
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Child<TGenology, TParent> :
    Xerxes_Genology_Group<TGenology>
    where TGenology : Xerxes_Genology
    where TParent   : Xerxes_Genology_Group<TGenology>
    {
        protected internal TParent Genology_Group_Child__Enclosing_Parent { get; internal set; }
    }
}

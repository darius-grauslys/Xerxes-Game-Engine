
namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Child<TGenealogy, TParent> :
    Xerxes_Genealogy_Group<TGenealogy>
    where TGenealogy : Xerxes_Genealogy
    where TParent   : Xerxes_Genealogy_Group<TGenealogy>
    {
        protected internal TParent Genealogy_Group_Child__Enclosing_Parent { get; internal set; }
    }
}

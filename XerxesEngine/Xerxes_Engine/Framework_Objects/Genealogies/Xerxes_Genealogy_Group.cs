
namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    where TGenealogy : 
    Xerxes_Genealogy
    {
        internal TGenealogy Genealogy_Group__Enclosing_Genealogy__Internal { get; set; }
        protected TGenealogy Genealogy_Group__Enclosing_Genealogy => Genealogy_Group__Enclosing_Genealogy__Internal;
        internal Xerxes_Object_Base Genealogy_Group__Enclosing_Object__Internal { get; set; }



        protected TGroup Protected_Link__Child_Group__Genealogy_Group<TThis, TGroup>()
        where TGroup : Xerxes_Genealogy_Group__Child<TGenealogy, TThis>, new()
        where TThis  : Xerxes_Genealogy_Group<TGenealogy>
        {
            if (!(this is TThis))
            {
                //TODO: const log
                Log.Write__Error__Log($"The Genealogy Group {typeof(TGroup)} cannot be a child of, {GetType()}!", this);
                return null;
            }
    
            TGroup group = new TGroup();

            group.Genealogy_Group__Enclosing_Genealogy__Internal =
                Genealogy_Group__Enclosing_Genealogy;

            group
                .Genealogy_Group_Child__Enclosing_Parent =
                this as TThis;

            group
                .Genealogy_Group__Enclosing_Object__Internal =
                Genealogy_Group__Enclosing_Object__Internal;

            return group;
        }

        protected internal abstract void Handle_Linking__Genealogy_Group();
    }
}

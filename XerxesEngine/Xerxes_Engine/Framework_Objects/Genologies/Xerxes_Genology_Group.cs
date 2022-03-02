
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group<TGenology>
    where TGenology : Xerxes_Genology
    {
        internal TGenology Genology_Group__Enclosing_Genology__Internal { get; set; }
        protected TGenology Genology_Group__Enclosing_Genology => Genology_Group__Enclosing_Genology__Internal;
        internal Xerxes_Object_Base Genology_Group__Enclosing_Object__Internal
            => Genology_Group__Enclosing_Genology__Internal
                .Genology__Enclosing_Object__Internal;




        protected TGroup Protected_Link__Child_Group__Genology_Group<TThis, TGroup>()
        where TGroup : Xerxes_Genology_Group__Child<TGenology, TThis>, new()
        where TThis  : Xerxes_Genology_Group<TGenology>
        {
            if (!(this is TThis))
            {
                //TODO: const log
                Log.Write__Error__Log($"The Genology Group {typeof(TGroup)} cannot be a child of, {GetType()}!", this);
                return null;
            }
    
            TGroup group = new TGroup();

            group.Genology_Group__Enclosing_Genology__Internal =
                Genology_Group__Enclosing_Genology;

            group
                .Genology_Group_Child__Enclosing_Parent =
                this as TThis;

            return group;
        }
    }
}


namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Associations
    <   
        TGenealogy
    > :
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    where TGenealogy : 
    Xerxes_Genealogy
    {
        protected internal void Protected_Associate__Associations<XObject>()
        where XObject : Xerxes_Object_Base, new()
        {
            XObject descendant = 
                new XObject();

            Genealogy_Group__Enclosing_Genealogy
                .Genealogy__DESCENDANT_GENOLOGIES__Internal
                .Add(descendant.Xerxes_Object_Base__Genealogy__Internal);
        }

        internal void Internal_Associate__Associations<XObject>
        (
            XObject instance
        )
        where XObject : Xerxes_Object_Base
        {
            Genealogy_Group__Enclosing_Genealogy
                .Genealogy__DESCENDANT_GENOLOGIES__Internal
                .Add(instance.Xerxes_Object_Base__Genealogy__Internal);
        }
    }
}

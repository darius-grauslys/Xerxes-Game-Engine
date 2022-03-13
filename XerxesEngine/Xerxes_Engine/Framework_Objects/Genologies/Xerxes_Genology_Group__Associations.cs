
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Associations
    <   
        TGenology
    > :
    Xerxes_Genology_Group
    <
        TGenology
    >
    where TGenology : 
    Xerxes_Genology
    {
        protected internal void Protected_Associate__Associations<XObject>()
        where XObject : Xerxes_Object_Base, new()
        {
            XObject descendant = 
                new XObject();

            Genology_Group__Enclosing_Genology
                .Genology__DESCENDANT_GENOLOGIES__Internal
                .Add(descendant.Xerxes_Object_Base__Genology__Internal);
        }

        internal void Internal_Associate__Associations<XObject>
        (
            XObject instance
        )
        where XObject : Xerxes_Object_Base
        {
            Genology_Group__Enclosing_Genology
                .Genology__DESCENDANT_GENOLOGIES__Internal
                .Add(instance.Xerxes_Object_Base__Genology__Internal);
        }
    }
}

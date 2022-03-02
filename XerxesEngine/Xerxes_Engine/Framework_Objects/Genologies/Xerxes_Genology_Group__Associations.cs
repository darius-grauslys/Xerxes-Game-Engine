
namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Associations
    <   
        TThis,
        TGenology
    > :
    Xerxes_Genology_Group<TGenology>
    where TThis : Xerxes_Genology_Group__Associations<TThis, TGenology>
    where TGenology : Xerxes_Genology
    {
        protected TThis Protected_Associate__Associations<XObject>()
        where XObject : Xerxes_Object_Base, new()
        {
            XObject descendant = 
                new XObject();

            Genology_Group__Enclosing_Genology
                .Genology__DESCENDANT_GENOLOGIES__Internal
                .Add(descendant.Xerxes_Object_Base__Genology__Internal);

            return this as TThis;
        }
    }
}

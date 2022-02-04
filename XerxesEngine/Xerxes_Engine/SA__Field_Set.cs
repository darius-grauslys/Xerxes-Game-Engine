
namespace Xerxes
{
    public class SA__Field_Set<Target, TType> :
        SA__Field_Get<Target, TType>
        where Target : Xerxes_Object_Base
    {
        public TType Field__SET_VALUE { get; }

        public SA__Field_Set(TType value)
        {
            Field__SET_VALUE = value;
        }
    }
}

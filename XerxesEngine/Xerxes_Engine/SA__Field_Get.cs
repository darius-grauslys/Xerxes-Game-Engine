
namespace Xerxes
{
    public class SA__Field_Get<Target, TType> : 
        Streamline_Argument
        where Target : Xerxes_Object_Base
    {
        public TType Field_Get__Returning_Value { get; internal set; }

        public SA__Field_Get()
        {}
    }
}

namespace Xerxes
{
    public class Streamline_Argument_Mediated<S,X> :
        Streamline_Argument
        where S : Streamline_Argument
        where X : Xerxes_Object_Base 
    {
        public S Streamline_Argument_Mediated__ARGUMENT { get; }

        public Streamline_Argument_Mediated
        (
            S streamline_Argument
        ) 
        {
            Streamline_Argument_Mediated__ARGUMENT =
                streamline_Argument;
        }
    }
}

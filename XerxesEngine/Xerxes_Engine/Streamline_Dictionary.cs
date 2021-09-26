namespace Xerxes_Engine
{
    internal class Streamline_Dictionary : Distinct_Type_Dictionary<Streamline_Argument, Streamline_Base>
    {
        internal void Internal_Declare__Streamline__Streamline_Dictionary<T>
        (
            Streamline<T> streamline
        ) where T : Streamline_Argument 
        {
            Protected_Define__Element__Distinct_Type_Dictionary<T>(streamline);
        }

        internal Streamline<T> Internal_Get__Streamline__Streamline_Dictionary<T>
        ( ) where T : Streamline_Argument
        {
            Streamline<T> streamline = 
                Protected_Get__Element__Distinct_Type_Dictionary<T>() as Streamline<T>;

            return streamline;
        }
    }
}

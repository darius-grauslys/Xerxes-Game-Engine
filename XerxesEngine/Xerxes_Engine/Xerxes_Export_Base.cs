namespace Xerxes
{
    public class Xerxes_Export_Base
    {
        internal Streamline_Dictionary 
            Xerxes_Export__Game_Exportline_Dictionary__Internal_REFERENCE { get; set; } 

        internal Xerxes_Export_Base(){}

        internal void Internal_Root__Exportline_Dictionary__Xerxes_Export
        (
            Streamline_Dictionary exportline_Dictionary
        )
        {
            Xerxes_Export__Game_Exportline_Dictionary__Internal_REFERENCE =
                exportline_Dictionary;

            Internal_Handle_Root__Exportline_Dictionary__Xerxes_Export_Base();

            Handle__Rooted__Xerxes_Export();
        }

        internal virtual void Internal_Handle_Root__Exportline_Dictionary__Xerxes_Export_Base
        (){}

        protected virtual void Handle__Rooted__Xerxes_Export
        ()
        {

        }
    }
}

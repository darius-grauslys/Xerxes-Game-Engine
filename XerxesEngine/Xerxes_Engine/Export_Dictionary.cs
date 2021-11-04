namespace Xerxes_Engine
{
    internal sealed class Export_Dictionary :
        Distinct_Type_Dictionary<Xerxes_Export_Base, Xerxes_Export_Base>
    {
        internal Streamline_Dictionary
            Export_Dictionary__EXPORTLINES__Internal { get; }
            
        internal Export_Dictionary()
        {
            Export_Dictionary__EXPORTLINES__Internal =
                new Streamline_Dictionary();
        }

        internal bool Internal_Declare__Export__Export_Dictionary<T>
        (
            T export
        ) where T : Xerxes_Export_Base 
        {
            bool success =
                Protected_Define__Element__Distinct_Type_Dictionary<T>
                (
                    export
                );

            if (!success)
            {
                Private_Log_Error__Duplicate_Export_Declared
                (
                    this,
                    export
                );
                return false;
            }

            export
                .Internal_Root__Exportline_Dictionary__Xerxes_Export
                (
                    Export_Dictionary__EXPORTLINES__Internal
                );

            return success;
        }

#region Static Logging
        private static void Private_Log_Error__Duplicate_Export_Declared
        (
            Export_Dictionary dictionary,
            Xerxes_Export_Base export
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__EXPORT_DICTIONARY__DUPLICATE_DECLARATION_1,
                dictionary,
                export
            );
        }
#endregion
    }
}

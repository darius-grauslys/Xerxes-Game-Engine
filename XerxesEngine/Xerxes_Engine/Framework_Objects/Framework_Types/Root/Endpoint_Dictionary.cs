
using System;
using System.Collections.Generic;

namespace Xerxes
{
    internal sealed class Endpoint_Dictionary :
        Distinct_Type_Dictionary<Xerxes_Object_Base, Xerxes_Object_Base>
    {
        internal bool Internal_Declare__Endpoint__Endpoint_Dictionary<T>
        (
            T export
        ) where T : Xerxes_Object_Base 
        {
            bool success =
                Protected_Define__Element__Distinct_Type_Dictionary<T>
                (
                    export
                );

            if (!success)
            {
                Private_Log_Error__Duplicate_Endpoint_Declared
                (
                    this,
                    export
                );
                return false;
            }
            return success;
        }

        internal IEnumerable<KeyValuePair<Type, Streamline_Base>>
            Internal_Get__Endpoint_Streamlines__Endpoint_Dictionary()
        {
            foreach
            (
                Xerxes_Object_Base endpoint 
                in 
                Protected_Get__Elements__Distinct_Typed_Dictionary()
            )
            {
                foreach
                (
                    KeyValuePair<Type, Streamline_Base> streamline_entry 
                    in 
                    endpoint
                    .Xerxes_Object_Base__DESCENDING_EXTENDING_STREAMLINES__Internal
                    .Internal_Get__Entries__Streamline_Dictionary()
                )
                    yield return streamline_entry;
            }
        }
#region Static Logging
        private static void Private_Log_Error__Duplicate_Endpoint_Declared
        (
            Endpoint_Dictionary dictionary,
            Xerxes_Object_Base export
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__ENDPOINT_DICTIONARY__DUPLICATE_DECLARATION_1,
                dictionary,
                export
            );
        }
#endregion
    }
}

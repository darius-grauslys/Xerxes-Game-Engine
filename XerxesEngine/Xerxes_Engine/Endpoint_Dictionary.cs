
using System;
using System.Collections.Generic;

namespace Xerxes
{
    internal sealed class Endpoint_Dictionary :
        Distinct_Type_Dictionary<Xerxes_Endpoint, Xerxes_Endpoint>
    {
        internal bool Internal_Declare__Endpoint__Endpoint_Dictionary<T>
        (
            T export
        ) where T : Xerxes_Endpoint 
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
                Xerxes_Endpoint endpoint 
                in 
                Protected_Get__Elements__Distinct_Typed_Dictionary()
            )
            {
                foreach
                (
                    KeyValuePair<Type, Streamline_Base> streamline_entry 
                    in endpoint.Internal_Get__Ancestral_Streamlines__Xerxes_Endpoint()
                )
                    yield return streamline_entry;
            }
        }
#region Static Logging
        private static void Private_Log_Error__Duplicate_Endpoint_Declared
        (
            Endpoint_Dictionary dictionary,
            Xerxes_Endpoint export
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

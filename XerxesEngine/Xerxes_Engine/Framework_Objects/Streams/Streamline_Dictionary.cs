using System;
using System.Collections.Generic;

namespace Xerxes
{
    internal class Streamline_Dictionary : 
    Distinct_Type_Dictionary<Streamline_Argument, Streamline_Base>
    {
        internal IEnumerable<KeyValuePair<Type, Streamline_Base>> Internal_Get__Entries__Streamline_Dictionary()
            => Protected_Get__Entries__Distinct_Typed_Dictionary();

        internal bool Internal_Declare__Streamline__Streamline_Dictionary<T>
        (
            Streamline<T> streamline
        ) where T : Streamline_Argument 
        {
            return Protected_Define__Element__Distinct_Type_Dictionary<T>(streamline);
        }

        internal bool Internal_Check_If__Type_Exists__Streamline_Dictionary<T>
        () where T : Streamline_Argument
        {
            bool typeExists =
                Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary<T>();

            return typeExists;
        }

        internal bool Internal_Check_If__Type_Exists__Streamline_Dictionary
        (
            Type t
        )
        {
            bool typeExists =
                Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary
                (
                    t
                );

            return typeExists;
        }

        internal Streamline<T> Internal_Get__Streamline__Streamline_Dictionary<T>
        ( ) where T : Streamline_Argument
        {
            Streamline<T> streamline = 
                Protected_Get__Element__Distinct_Type_Dictionary<T>() as Streamline<T>;

            return streamline;
        }

        public override string ToString()
        {
            string ret = "";
            foreach(KeyValuePair<Type,Streamline_Base> pair in Protected_Get__Entries__Distinct_Typed_Dictionary())
            {
                ret += String.Format("{0}-{1}\n", pair.Key, pair.Value);
            }

            return ret;
        }
    }
}

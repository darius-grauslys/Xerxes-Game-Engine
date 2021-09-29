using System;
using System.Collections.Generic;

namespace Xerxes_Engine
{
    internal class Streamline_Dictionary : Distinct_Type_Dictionary<Streamline_Argument, Streamline_Base>
    {
        internal bool Internal_Declare__Streamline__Streamline_Dictionary<T>
        (
            Streamline<T> streamline
        ) where T : Streamline_Argument 
        {
            return Protected_Define__Element__Distinct_Type_Dictionary<T>(streamline);
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
            foreach(KeyValuePair<Type,Streamline_Base> pair in Internal_Get__Entries__Distinct_Typed_Dictionary())
            {
                ret += String.Format("{0}-{1}\n", pair.Key, pair.Value);
            }

            return ret;
        }
    }
}

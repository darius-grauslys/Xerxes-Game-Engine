using System;
using System.Collections.Generic;

namespace Xerxes
{
    public class Xerxes_Endpoint
    {
        internal Streamline_Dictionary Xerxes_Endpoint__ANCESTOR_RECEIVERS__Internal { get; }
        internal Streamline_Dictionary Xerxes_Endpoint__ANCESTOR_EXTENDERS__Internal { get; }

        protected Xerxes_Endpoint()
        {
            Xerxes_Endpoint__ANCESTOR_RECEIVERS__Internal =
                new Streamline_Dictionary();
            Xerxes_Endpoint__ANCESTOR_EXTENDERS__Internal =
                new Streamline_Dictionary();
        }

        internal IEnumerable<KeyValuePair<Type, Streamline_Base>> Internal_Get__Ancestral_Streamlines__Xerxes_Endpoint()
        {
            foreach
            (
                KeyValuePair<Type, Streamline_Base> pair 
                in
                Xerxes_Endpoint__ANCESTOR_RECEIVERS__Internal
                .Internal_Get__Entries__Streamline_Dictionary()
            )
                yield return pair;

            foreach
            (
                KeyValuePair<Type, Streamline_Base> pair
                in
                Xerxes_Endpoint__ANCESTOR_EXTENDERS__Internal
                .Internal_Get__Entries__Streamline_Dictionary()
            )
                yield return pair;
        }


        protected void Declare__Receiving<S>
        (Action<S> listener) where S : Streamline_Argument
        {
            Streamline<S> streamline =
                new Streamline<S>
                (
                    listener,
                    isExtending: false
                );

            Xerxes_Endpoint__ANCESTOR_RECEIVERS__Internal
                .Internal_Declare__Streamline__Streamline_Dictionary<S>(streamline);
        }

        protected void Declare__Extending<S>
        () where S : Streamline_Argument
        {
            Streamline<S> streamline =
                new Streamline<S>(isReceiving: false);

            Xerxes_Endpoint__ANCESTOR_EXTENDERS__Internal
                .Internal_Declare__Streamline__Streamline_Dictionary<S>(streamline);
        }

        protected void Invoke__Descending<S>
        (S e) where S : Streamline_Argument
        {
            Xerxes_Endpoint__ANCESTOR_EXTENDERS__Internal
                .Internal_Get__Streamline__Streamline_Dictionary<S>()
                .Internal_Stream__Streamline(e);
        }
    }
}

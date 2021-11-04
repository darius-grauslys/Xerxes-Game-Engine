using System;
using System.Collections.Generic;
using System.Linq;

namespace Xerxes_Engine
{
    /// <summary>
    /// A wrapper class for a Type-key dictionary.
    /// Handles repeating type-keys by logging them
    /// as errors. T: Type-Key, V: Value
    /// </summary>
    public class Distinct_Type_Dictionary<T, Y> where T : class where Y : class
    {
        private Dictionary<Type,Y> _Distinct_Type_Dictionary__DICTIONARY { get; }

        protected bool Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary<H>
        () where H : T
        {
            Type t = typeof(H);

            bool typeExists =
                Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary
                (
                    t
                );

            return typeExists;
        }

        protected bool Protected_Check_If__Type_Exists__Distinct_Typed_Dictionary
        (
            Type t
        )
        {
            bool typeExists = 
                _Distinct_Type_Dictionary__DICTIONARY
                .ContainsKey(t);

            return typeExists;
        }

        internal IEnumerable<KeyValuePair<Type,Y>> Internal_Get__Entries__Distinct_Typed_Dictionary()
            => _Distinct_Type_Dictionary__DICTIONARY.ToArray();

        internal IEnumerable<Type> Internal_Get__Types__Distinct_Typed_Dictionary()
            => _Distinct_Type_Dictionary__DICTIONARY.Keys.ToArray();

        protected Distinct_Type_Dictionary()
        {
            _Distinct_Type_Dictionary__DICTIONARY = new Dictionary<Type,Y>();
        }

        protected virtual bool Protected_Define__Element__Distinct_Type_Dictionary<H>
        (
            Y element
        ) where H :T
        {
            Type typeH = typeof(H);
            bool isInvalidType =
                Private_Check_If__Type_Key_Is_Present__Distinct_Type_Dictionary
                (
                    typeH
                );

            if (isInvalidType)
                return false;

            _Distinct_Type_Dictionary__DICTIONARY
                .Add(typeH, element);
            return true;
        }

        protected virtual Y Protected_Get__Element__Distinct_Type_Dictionary<H>()
            where H : T
        {
            Type typeH = typeof(H);
            bool isInvalidType =
                !Private_Check_If__Type_Key_Is_Present__Distinct_Type_Dictionary
                (
                    typeH
                );

            if (isInvalidType)
                return null; 

            Y returningElement =
                _Distinct_Type_Dictionary__DICTIONARY[typeH] as Y;
            return returningElement;
        }

        protected virtual dynamic Protected_Get__Element__Distinct_Type_Dictionary
        (
            Type typeH 
        )
        {
            bool isInvalidKey =
                !Private_Check_If__Type_Key_Is_Present__Distinct_Type_Dictionary
                (
                    typeH
                );
            if (isInvalidKey)
                return null;

            dynamic elementY = _Distinct_Type_Dictionary__DICTIONARY[typeH];
            return elementY;
        }

        private bool Private_Check_If__Type_Key_Is_Present__Distinct_Type_Dictionary
        (
            Type typeY
        )
        {
            bool isNotPresentKey =
                 _Distinct_Type_Dictionary__DICTIONARY
                .ContainsKey(typeY);

            return isNotPresentKey;
        }

        /// <summary>
        /// Checks to see if the key Y exists in both dictionarys
        /// and if it does, invokes the given action.
        /// Returns true if the action was invoked, and false
        /// otherwise.
        internal static bool Internal_On__Matching_Key<H>
        (
            Distinct_Type_Dictionary<T,Y> distinct_Type_Dictionary_1,
            Distinct_Type_Dictionary<T,Y> distinct_Type_Dictionary_2,
            Action<Y, Y> action
        ) where H : T
        {
            Y element_1 = 
                distinct_Type_Dictionary_1?
                .Protected_Get__Element__Distinct_Type_Dictionary<H>();
            Y element_2 =
                distinct_Type_Dictionary_2?
                .Protected_Get__Element__Distinct_Type_Dictionary<H>();

            if (element_1 == null || element_2 == null)
                return false;
            
            action?.Invoke(element_1, element_2);
            return true;
        }

        internal static void Internal_On_All__Matching_Keys
        (
            Distinct_Type_Dictionary<T,Y> key_Sourced_Dictionary,
            Distinct_Type_Dictionary<T,Y> target_Compare_Dictionary,
            Action<Y, Y> dynamic_Pair_Found_Action,
            Action<Y> dynamic_Pair_Not_Found_Action
        )
        {
            IEnumerable<Type> keys = 
                key_Sourced_Dictionary
                ._Distinct_Type_Dictionary__DICTIONARY.Keys;

            foreach(Type key in keys)
            {
                Y element_1 = 
                    key_Sourced_Dictionary
                    .Protected_Get__Element__Distinct_Type_Dictionary(key);
                Y element_2 =
                    target_Compare_Dictionary
                    .Protected_Get__Element__Distinct_Type_Dictionary(key);

                if(element_1 == null || element_2 == null)
                {
                    dynamic_Pair_Not_Found_Action?
                        .Invoke(element_1);
                    continue;
                }

                dynamic_Pair_Found_Action?
                    .Invoke(element_1, element_2);
            }
        }
    }
}

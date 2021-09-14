using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Xerxes_Engine
{
    internal abstract class Distinct_Dictionary<H,T> where H : Distinct_Handle
    {
        private const string _Distinct_Dictionary__DEFAULT_HANDLE_FORMAT = "{0}_{1}";

        private const string _Distinct_Dictionary__REGEX_ARG_0 = "(?!(\\\\|{))*{(0)}(?!(\\\\|}))*";
        private const string _Distinct_Dictionary__REGEX_ARG_1 = "(?!(\\\\|{))*{(1)}(?!(\\\\|}))*";


        private Dictionary<H,T> _Distinct_Dictionary__DICTIONARY { get; }

        private Dictionary<string,int> _Distinct_Dictionary__REPETATIVE_ENTRIES { get; }


        private string _Distinct_Dictionary__HANDLE_FORMAT { get; }


        protected T Protected_Get__Element__Distinct_Dictionary(H distinctHandle)
            => _Distinct_Dictionary__DICTIONARY[distinctHandle];

        protected T Protected_Get__Element__Distinct_Dictionary(string lousyHandle)
        {
            H lousy = Handle_Get__New_Handle__Distinct_Dictionary(lousyHandle);
            if(_Distinct_Dictionary__DICTIONARY.ContainsKey(lousy))
                return _Distinct_Dictionary__DICTIONARY[lousy];
            return default(T);
        }

        protected H Protected_Declare__Element__Distinct_Dictionary(string name, T element)
            => Private_Add__Distinct_Dictionary(name, element);
            
        public Distinct_Dictionary(string format=null)
        {
            _Distinct_Dictionary__HANDLE_FORMAT = Private_Validate__Handle_Format__Distinct_Dictionary(format);

            _Distinct_Dictionary__DICTIONARY = new Dictionary<H, T>();
            _Distinct_Dictionary__REPETATIVE_ENTRIES = new Dictionary<string, int>();
        }

        protected bool Protected_CheckIf__Format_String_Is_Valid__Distinct_Dictionary
        (
            string format
        )
        {
            if (format == null)
                return false;

            Regex regex_0 = new Regex(_Distinct_Dictionary__REGEX_ARG_0);
            Regex regex_1 = new Regex(_Distinct_Dictionary__REGEX_ARG_1);
            Match match_0 = regex_0.Match(format);
            Match match_1 = regex_1.Match(format);

            return match_0.Success && match_1.Success;
        }

        private string Private_Validate__Handle_Format__Distinct_Dictionary(string format)
        {
            bool formatIsValid =
                Protected_CheckIf__Format_String_Is_Valid__Distinct_Dictionary(format);

            if(formatIsValid)
                return format;

            return _Distinct_Dictionary__DEFAULT_HANDLE_FORMAT;
        }

        private H Private_Add__Distinct_Dictionary(string entry, T element)
        {
            H handle = 
                Private_Get__Validated_Handle__Distinct_Dictionary
                (
                    entry
                );

            _Distinct_Dictionary__DICTIONARY.Add(handle, element);

            return handle;
        }

        private H Private_Get__Validated_Handle__Distinct_Dictionary
        (
            string internalStringHandle
        )
        {
            H handle;

            if(_Distinct_Dictionary__REPETATIVE_ENTRIES.ContainsKey(internalStringHandle))
            {
                handle = 
                    Private_Get__Incremented_Repetative_Handle__Distinct_Dictionary
                    (
                        internalStringHandle
                    );
                
                return handle;
            }

            handle =
                Private_Get__New_Record_Handle__Distinct_Dictionary
                (
                    internalStringHandle
                );

            return handle;
        }

        private H Private_Get__Incremented_Repetative_Handle__Distinct_Dictionary
        (
            string internalStringHandle
        )
        {
            int entry = _Distinct_Dictionary__REPETATIVE_ENTRIES[internalStringHandle]++;
            return Private_Get__New_Handle__Distinct_Dictionary(internalStringHandle, entry);
        }

        private H Private_Get__New_Record_Handle__Distinct_Dictionary
        (
            string internalStringHandle
        )
        {
            _Distinct_Dictionary__REPETATIVE_ENTRIES.Add(internalStringHandle, 0);
            return Private_Get__New_Handle__Distinct_Dictionary(internalStringHandle, 0);
        }

        private H Private_Get__New_Handle__Distinct_Dictionary
        (
            string internalStringHandle, 
            int entry
        )
        {
            string handleString = 
                String.Format
                (
                    _Distinct_Dictionary__HANDLE_FORMAT, 
                    internalStringHandle, 
                    entry
                );

            return Handle_Get__New_Handle__Distinct_Dictionary(handleString);
        }

        protected abstract H Handle_Get__New_Handle__Distinct_Dictionary
        (
            string internalStringHandle
        );
    }
}

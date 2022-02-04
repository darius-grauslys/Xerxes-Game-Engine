namespace Xerxes
{
    /// <summary>
    /// Represents a internal handle of a Xerxes Object.
    /// In many cases it is not always essential to look up
    /// if a key exists in a Dictionary when utilizing these
    /// internal handles.
    /// <summary/>
    public class Distinct_Handle
    {
        private string _Distinct_Handle__FORMATTED_STRING_HANDLE { get; }
        /// <summary>
        /// Reference to the object which created this handle.
        /// <summary/>
        private object _Distinct_Handle__FACTORY_SOURCE { get; }

        protected Distinct_Handle(string internalStringHandle, object source)
        {
            _Distinct_Handle__FORMATTED_STRING_HANDLE = internalStringHandle;
            _Distinct_Handle__FACTORY_SOURCE = source;
        }

        public bool Equals(Distinct_Handle handle)
            => _Distinct_Handle__FORMATTED_STRING_HANDLE == handle._Distinct_Handle__FORMATTED_STRING_HANDLE;

        public override string ToString()
            => _Distinct_Handle__FORMATTED_STRING_HANDLE;

        internal bool Internal__Is_From_Source__Distinct_Handle(object isFromThis)
            => _Distinct_Handle__FACTORY_SOURCE == isFromThis;
    }
}

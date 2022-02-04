namespace Xerxes
{
    public sealed class Xerxes_Stream_Context
    {
        private Stream _Xerxes_Stream_Context__UP_STREAM { get; }
        private Stream _Xerxes_Stream_Context__DOWN_STREAM { get; }

        internal Xerxes_Stream_Context
        (
            Xerxes_Object_Base xerxes_Object_Base
        )
        {
            _Xerxes_Stream_Context__UP_STREAM =
                xerxes_Object_Base
                .Xerxes_Object_Base__UPSTREAM__Internal;
            _Xerxes_Stream_Context__DOWN_STREAM =
                xerxes_Object_Base
                .Xerxes_Object_Base__DOWNSTREAM__Internal;
        }

        public Xerxes_Streamline_Context Upstream
        {
            get 
            {
                Xerxes_Streamline_Context xerxes_Streamline_Context =
                    new Xerxes_Streamline_Context
                    (
                        this,
                        _Xerxes_Stream_Context__UP_STREAM
                    );

                return xerxes_Streamline_Context;
            }
        }

        public Xerxes_Streamline_Context Downstream
        {
            get 
            {
                Xerxes_Streamline_Context xerxes_Streamline_Context =
                    new Xerxes_Streamline_Context
                    (
                        this,
                        _Xerxes_Stream_Context__DOWN_STREAM
                    );

                return xerxes_Streamline_Context;
            }
        }

        public void Seal(Xerxes_Sealing_Context sealing_Context)
        {
            
        }
    }
}

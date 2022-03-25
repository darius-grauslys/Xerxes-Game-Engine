using System;

namespace Xerxes
{
    public class Xerxes_Object_Base
    {
        public object Xerxes_Object_Base__IDENTIFIER { get; }

        internal Xerxes_Genealogy Xerxes_Object_Base__Genealogy__Internal { get; private set; }

        internal void Internal_Set__Genealogy__Xerxes_Engine_Object(Xerxes_Genealogy genealogy)
        {
            genealogy
                .Genealogy__Enclosing_Object__Internal =
                this;

            Xerxes_Object_Base__Genealogy__Internal =
                genealogy;

            genealogy
                .Handle_Linking__Genealogy();
        }

        internal Endpoint_Dictionary Xerxes_Object_Base__ENDPOINTS__Internal { get; set; }

        internal Stream Xerxes_Object_Base__UPSTREAM__Internal { get; }
        internal Streamline_Dictionary 
            Xerxes_Object_Base__ASCENDING_RECEIVING_STREAMLINES__Internal
            => Xerxes_Object_Base__UPSTREAM__Internal.Stream__RECEIVING_STREAMLINES__Internal;
        internal Streamline_Dictionary 
            Xerxes_Object_Base__ASCENDING_EXTENDING_STREAMLINES__Internal
            => Xerxes_Object_Base__UPSTREAM__Internal.Stream__EXTENDING_STREAMLINES__Internal;
        
        internal Stream Xerxes_Object_Base__DOWNSTREAM__Internal { get; }
        internal Streamline_Dictionary 
            Xerxes_Object_Base__DESCENDING_RECEIVING_STREAMLINES__Internal
            => Xerxes_Object_Base__DOWNSTREAM__Internal.Stream__RECEIVING_STREAMLINES__Internal;
        internal Streamline_Dictionary 
            Xerxes_Object_Base__DESCENDING_EXTENDING_STREAMLINES__Internal
            => Xerxes_Object_Base__DOWNSTREAM__Internal.Stream__EXTENDING_STREAMLINES__Internal;

        protected bool Xerxes_Object_Base__Is_Rooted__Protected { get; private set; }

        internal Xerxes_Object_Base()
        {
            Xerxes_Object_Base__IDENTIFIER = new object();

            Xerxes_Object_Base__ENDPOINTS__Internal =
                new Endpoint_Dictionary();

            Xerxes_Object_Base__UPSTREAM__Internal = new Stream();
            Xerxes_Object_Base__DOWNSTREAM__Internal = new Stream();
        }

        public override string ToString()
        {
            string str = base.ToString();
            str = str.Substring(str.LastIndexOf('.')+1);
            return str;
        }

#region Streamline Management
        protected internal bool Invoke__Descending<S>
        (
            S streamline_Argument
        ) where S : Streamline_Argument
        {
            return Private_Invoke__Streamline__Xerxes_Engine_Object
            (
                Xerxes_Object_Base__DOWNSTREAM__Internal,
                streamline_Argument
            );
        }

        protected internal bool Invoke__Ascending<S>
        (
            S streamline_Argument
        ) where S : Streamline_Argument
        {
            return Private_Invoke__Streamline__Xerxes_Engine_Object
            (
                Xerxes_Object_Base__UPSTREAM__Internal,
                streamline_Argument
            );
        }

        private bool Private_Invoke__Streamline__Xerxes_Engine_Object<S>
        (
            Stream stream,
            S streamline_Argument
        ) where S : Streamline_Argument
        {
            if (streamline_Argument == null)
                return false;

            if (streamline_Argument.Streamline_Argument__Origin_Identifier == null)
                streamline_Argument.Streamline_Argument__Origin_Identifier =
                    Xerxes_Object_Base__IDENTIFIER;

            bool success =
                stream
                .Internal_Invoke__Streamline__Stream<S>
                (
                    streamline_Argument
                );

            if (!success)
            {
                Log.Write__Log
                (
                    Log_Message_Type.Error__Engine_Object,
                    Log.ERROR__XERXES_ENGINE_OBJECT__STREAMLINE_NOT_FOUND_1,
                    this,
                    typeof(S).ToString()
                );
            }

            return success;
        }

        protected bool Subscribe__Descending<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            return Private_Subscribe__Streamline__Xerxes_Engine_Object
                (
                    Xerxes_Object_Base__DOWNSTREAM__Internal,
                    listener,
                    Log.Context__Stream.Downstream
                );
        }

        protected bool Subscribe__Ascending<S>
        (
            Action<S> listener
        ) where S : Streamline_Argument
        {
            return Private_Subscribe__Streamline__Xerxes_Engine_Object
                (
                    Xerxes_Object_Base__UPSTREAM__Internal,
                    listener,
                    Log.Context__Stream.Upstream
                );
        }

        private bool Private_Subscribe__Streamline__Xerxes_Engine_Object<S>
        (
            Stream stream,
            Action<S> listener,
            Log.Context__Stream context
        ) where S : Streamline_Argument
        {
            bool success =
                stream
                .Internal_Subscribe__Streamline__Stream
                (
                    listener
                );

            if (!success)
            {
                Private_Log_Error__Failed_To_Subscribe
                (
                    this,
                    typeof(S),
                    context
                );
            }

            return success;
        }
#endregion

#region Sealing
        internal void Internal_Root__Xerxes_Engine_Object()
        {
            if (Xerxes_Object_Base__Is_Rooted__Protected)
            {
                Log.Write__Warning__Log
                (
                    Log.WARNING__XERXES_ENGINE_OBJECT__REDUNDANT_SEALING,
                    this
                );
            }

            Xerxes_Object_Base__Is_Rooted__Protected = true;
        }
#endregion

#region Static Logging
        private static void Private_Log_Error__Is_Sealed
        (
            Xerxes_Object_Base obj
        ) 
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__SEALED_ASSOCIATION,
                obj
            );
        }

        private static void Private_Log_Error__Failed_To_Declare_Streamline_2C
        (
            Xerxes_Object_Base obj,
            Type streamlineType,
            Log.Context__Stream stream_context,
            Log.Context__Declare_Streamline type_context
        ) 
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_ENGINE_OBJECT__FAILED_TO_DECLARE_STREAMLINE_2C,
                obj,
                streamlineType,
                stream_context,
                type_context
            );
        }

        private static void Private_Log_Error__Failed_To_Subscribe
        (
            Xerxes_Object_Base obj,
            Type streamlineType,
            Log.Context__Stream context
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_OBJECT_BASE__FAILED_TO_SUBSCRIBE_STREAMLINE_2C,
                obj,
                streamlineType,
                context
            );
        }

        private static void Private_Log_Error__Argument_Consumed_2
        (
            Xerxes_Object_Base obj,
            Type streamlineType
        )
        {
            Log.Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__XERXES_OBJECT_BASE__ARGUMENT_CONSUMED_2C,
                obj,
                streamlineType
            );
        }
#endregion
    }
}

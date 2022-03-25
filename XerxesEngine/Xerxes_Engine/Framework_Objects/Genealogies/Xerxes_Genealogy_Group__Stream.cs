
using System;

namespace Xerxes
{
    public abstract class Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TParent
    >:
    Xerxes_Genealogy_Group__Child
    <
        TGenealogy, 
        TParent
    >
    where TGenealogy : 
    Xerxes_Genealogy
    where TParent : 
    Xerxes_Genealogy_Group
    <
        TGenealogy
    >
    {
        protected internal void Protected_Extend__To_Ancestors__Streams<SA>
        (
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genealogy_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__UPSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>
            (
                isRecieving : false,
                declaration_Failure_Receiving : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams,
                declaration_Failure_Extending : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams
            );
        }

        protected internal void Protected_Extend__To_Descendants__Streams<SA>
        (
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genealogy_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__DOWNSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>
            (
                isRecieving: false,
                declaration_Failure_Receiving : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams,
                declaration_Failure_Extending : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams
            );
        }




        protected internal void Protected_Recieve__From_Ancestors__Streams<SA>
        (
            Action<SA> streamline_reciever,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genealogy_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__DOWNSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>
            (
                streamline_reciever, 
                isExtending : false,
                declaration_Failure_Receiving : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams,
                declaration_Failure_Extending : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams
            );
        }

        protected internal void Protected_Recieve__From_Descendants__Streams<SA>
        (
            Action<SA> streamline_reciever,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genealogy_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__UPSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>
            (
                streamline_reciever, 
                isExtending : false,
                declaration_Failure_Receiving : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams,
                declaration_Failure_Extending : 
                (fail_recieving_callback != null)
                    ? fail_recieving_callback
                    : Private_Handle__Failure_To_Declare__Genealogy_Streams
            );
        }

        private void Private_Handle__Failure_To_Declare__Genealogy_Streams
        (
            Streamline_Base e,
            Log.Context__Declare_Streamline context
        )
        {
            //TODO: log const
            Log.Write__Error__Log
            (
                $"Failure to declare stream: {e} with context: {context} for object: {Genealogy_Group__Enclosing_Object__Internal}",
                this
            );
        }
    }
}

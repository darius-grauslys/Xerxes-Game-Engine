
using System;

namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Streams
    <
        TThis,
        TGenology,
        TParent
    >:
    Xerxes_Genology_Group__Child<TGenology, TParent>
    where TThis : Xerxes_Genology_Group__Streams<TThis, TGenology, TParent>
    where TGenology : Xerxes_Genology
    where TParent   : Xerxes_Genology_Group<TGenology>
    {
        protected void From_Ancestors__Extend<SA>
        (
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genology_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__UPSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>(isRecieving : false);
        }

        protected void From_Descendants__Extend<SA>
        (
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genology_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__DOWNSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>(isRecieving: false);
        }




        protected void From_Ancestors__Recieve<SA>
        (
            Action<SA> streamline_reciever,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genology_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__UPSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>(streamline_reciever, isExtending : false);
        }

        protected void From_Descendants__Recieve<SA>
        (
            Action<SA> streamline_reciever,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_recieving_callback = null,
            Action<Streamline_Base, Log.Context__Declare_Streamline>
                fail_extending_callback = null
        )
        where SA : Streamline_Argument
        {
            Genology_Group__Enclosing_Object__Internal
                .Xerxes_Object_Base__DOWNSTREAM__Internal
                .Internal_Declare__Streamline__Stream<SA>(streamline_reciever, isExtending : false);
        }
    }
}

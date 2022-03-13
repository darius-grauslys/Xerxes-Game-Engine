
using System;

namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Recieving_Mediator
    <
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Streams
    <
        TGenology,
        TParent
    >
    where TGenology :
    Xerxes_Genology
    where TParent :
    Xerxes_Genology_Group
    <
        TGenology
    >
    {
        protected internal void Protected_Mediate_Ancestors__Recieving__Mediator
        <
            SA,
            XTarget
        >
        (
            Action<SA> handler
        )
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Recieve__From_Ancestors__Streams
            <SA__Mediate<XTarget, SA>>((e) => handler?.Invoke(e));
        }

        protected internal void Protected_Mediate_Descendants__Recieving__Mediator
        <
            SA,
            XTarget
        >
        (
            Action<SA> handler
        )
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Recieve__From_Descendants__Streams
            <SA__Mediate<XTarget, SA>>((e) => handler?.Invoke(e));
        }
    }
}

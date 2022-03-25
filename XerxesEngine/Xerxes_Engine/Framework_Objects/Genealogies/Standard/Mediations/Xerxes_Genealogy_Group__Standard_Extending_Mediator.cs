
using System;

namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Stream_Mediator
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
        public
            Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
            <
                TGenealogy,
                TParent
            >
            Extending
            <
                SA
            >()
        where SA :
        Streamline_Argument
        {
            Protected_Extend__To_Descendants__Streams<SA>();

            return this;
        }

        public
            Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
            <
                TGenealogy,
                TParent
            >
            Recieving
            <
                SA
            >
            (
                Action<SA> handler
            )
        where SA :
        Streamline_Argument
        {
            Protected_Recieve__From_Descendants__Streams<SA>(handler);

            return this;
        }

        public 
            Xerxes_Genealogy_Group__Standard_Extending_Mediator__Descendants
            <
                TGenealogy,
                TParent
            >
            Mediate__Extending
            <
                SA,
                XTarget            
            >()
        where SA :
        Streamline_Argument
        where XTarget :
        Xerxes_Object_Base, new()
        {
            Protected_Extend__Mediation_To_Descendants__Stream_Mediator<SA, XTarget>();

            return this;
        }



        public TParent Finish__With_Mediations
            => Genealogy_Group_Child__Enclosing_Parent;

        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }
    }
}

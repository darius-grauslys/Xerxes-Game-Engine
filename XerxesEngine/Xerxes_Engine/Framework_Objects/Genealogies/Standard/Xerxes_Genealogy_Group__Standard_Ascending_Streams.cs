
using System;

namespace Xerxes
{
    public class Xerxes_Genealogy_Group__Standard_Ascending_Streams
    <
        TGenealogy,
        TParent
    > :
    Xerxes_Genealogy_Group__Streams
    <
        TGenealogy,
        TParent
    >
    where TGenealogy :
    Xerxes_Genealogy
    where TParent :
    Xerxes_Genealogy_Group__Streamlines
    <
        TGenealogy
    >
    {
        public 
            Xerxes_Genealogy_Group__Standard_Ascending_Streams
            <
                TGenealogy,
                TParent
            >
            Extending
            <SA>()
        where SA :
        Streamline_Argument
        {
            Protected_Extend__To_Ancestors__Streams<SA>();

            return this;
        }

        public 
            Xerxes_Genealogy_Group__Standard_Ascending_Streams
            <
                TGenealogy,
                TParent
            >
            Recieving
            <SA>
            (Action<SA> handler)
        where SA :
        Streamline_Argument
        {
            Protected_Recieve__From_Ancestors__Streams<SA>(handler);

            return this;
        }




        public TParent Finish__With_Ancestors
            => Genealogy_Group_Child__Enclosing_Parent;
        protected internal override void Handle_Linking__Genealogy_Group()
        {
        }
    }
}

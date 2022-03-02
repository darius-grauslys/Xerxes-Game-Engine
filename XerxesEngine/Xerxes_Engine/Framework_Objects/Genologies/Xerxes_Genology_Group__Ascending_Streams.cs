
using System;

namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Ascending_Streams
    <
        TThis,
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Streams<TThis, TGenology, TParent>
    where TThis : Xerxes_Genology_Group__Ascending_Streams<TThis, TGenology, TParent>
    where TGenology : Xerxes_Genology
    where TParent : Xerxes_Genology_Group<TGenology>
    {
        public Xerxes_Genology_Group__Ascending_Streams<TThis, TGenology, TParent> Extending<SA>()
        where SA : Streamline_Argument
        {
            From_Ancestors__Extend<SA>();
            return this;
        }

        public Xerxes_Genology_Group__Ascending_Streams<TThis, TGenology, TParent> Recieving<SA>(Action<SA> streamline_reciever)
        where SA : Streamline_Argument
        {
            From_Ancestors__Recieve<SA>(streamline_reciever);
            return this;
        }



        public TParent Finish__With_Ancestors
            => Genology_Group_Child__Enclosing_Parent;
    }
}


using System;

namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Descending_Streams
    <
        TThis,
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Streams<TThis, TGenology, TParent>
    where TThis : Xerxes_Genology_Group__Descending_Streams<TThis, TGenology, TParent>
    where TGenology : Xerxes_Genology
    where TParent : Xerxes_Genology_Group__Streamlines<TParent, TGenology>
    {
        public Xerxes_Genology_Group__Descending_Streams<TThis, TGenology, TParent> Extending<SA>()
        where SA : Streamline_Argument
        {
            From_Descendants__Extend<SA>();
            return this;
        }

        public Xerxes_Genology_Group__Descending_Streams<TThis, TGenology, TParent> Recieve<SA>(Action<SA> streamline_reciever)
        where SA : Streamline_Argument
        {
            From_Descendants__Recieve<SA>(streamline_reciever);
            return this;
        }



        public TGenology Finish__With_Descendants
            => Genology_Group__Enclosing_Genology__Internal;
    }
}

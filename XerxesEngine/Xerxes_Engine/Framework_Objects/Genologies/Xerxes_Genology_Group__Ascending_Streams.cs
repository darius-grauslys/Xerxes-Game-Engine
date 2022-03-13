
using System;

namespace Xerxes
{
    public abstract class Xerxes_Genology_Group__Ascending_Streams
    <
        TThis,
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Streams
    <
        TGenology, 
        TParent
    >
    where TThis : 
    Xerxes_Genology_Group__Ascending_Streams
    <
        TThis, 
        TGenology, 
        TParent
    >, new()
    where TGenology : 
    Xerxes_Genology
    where TParent : 
    Xerxes_Genology_Group__Streamlines
    <
        TGenology
    >
    {
        public TThis Extending<SA>()
        where SA : Streamline_Argument
        {
            Protected_Extend__To_Ancestors__Streams<SA>();
            return this as TThis;
        }

        public TThis Recieving<SA>(Action<SA> streamline_reciever)
        where SA : Streamline_Argument
        {
            Protected_Recieve__From_Ancestors__Streams<SA>(streamline_reciever);
            return this as TThis;
        }
    }
}

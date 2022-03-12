
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Descending_Streams
    <
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Descending_Streams
    <
        Xerxes_Genology_Group__Standard_Descending_Streams
        <
            TGenology,
            TParent
        >,
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
        protected internal override void Handle_Linking__Genology_Group()
        {
        }



        public TGenology Finish__With_Descendants
            => Genology_Group__Enclosing_Genology__Internal;
    }
}

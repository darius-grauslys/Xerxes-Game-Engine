
namespace Xerxes
{
    public class Xerxes_Genology_Group__Standard_Ascending_Streams
    <
        TGenology,
        TParent
    > :
    Xerxes_Genology_Group__Ascending_Streams
    <
        Xerxes_Genology_Group__Standard_Ascending_Streams
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
    Xerxes_Genology_Group__Streamlines
    <
        TGenology
    >
    {
        protected internal override void Handle_Linking__Genology_Group()
        {
        }



        public TParent Finish__With_Ancestors
            => Genology_Group_Child__Enclosing_Parent;
    }
}


namespace Xerxes
{
    public class Xerxes_Genology__Standard_Switch<XBase> :
    Xerxes_Genology__Switch
    <
        Xerxes_Genology__Standard_Switch<XBase>,
        XBase,
        Xerxes_Genology_Group__Standard_Mediated_Associations
        <
            Xerxes_Genology__Standard_Switch<XBase>
        >,
        Xerxes_Genology_Group__Standard_Mediated_Streamlines
        <
            Xerxes_Genology__Standard_Switch<XBase>,
            XBase
        >,
        Xerxes_Genology_Group__Standard_Mediated_Ascending_Stream
        <
            Xerxes_Genology__Standard_Switch<XBase>,
            XBase
        >,
        Xerxes_Genology_Group__Standard_Mediated_Descending_Streams
        <
            Xerxes_Genology__Standard_Switch<XBase>,
            XBase
        >
    >
    where XBase :
    Xerxes_Object_Base
    <
        XBase,
        Xerxes_Genology__Standard_Switch<XBase>
    >
    {

    }
}

namespace Xerxes_Engine.UI.Implemented_UI_Containers
{
    public class UI_Unbound_Container : UI_Container
    {
        protected UI_Unbound_Container(UI_Rect boundingRect) : base(boundingRect)
        {
        }

        protected override bool Handle_Check_For__Sort_Integrity__UI_Container
        (
            UI_Anchored_Wrapper anchoredWrapperToSort, 
            OpenTK.Vector3 sortedPosition
        )
        {
            return true;
        }
    }
}

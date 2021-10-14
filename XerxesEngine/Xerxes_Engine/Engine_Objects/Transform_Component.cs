using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Transform_Component : Xerxes_Descendant<Game_Object, Transform_Component> 
    {
        public Vector3 Position { get; set; }

        public Transform_Component()
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>(Private__Handle_Draw__Transform_Component);
            
            Position = new Vector3();
        }

        private void Private__Handle_Draw__Transform_Component(Streamline_Argument_Draw e)
        {
            e.Streamline_Argument_Draw__Position__Internal
                = Position;
        }
    }
}

using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Transform_Component : Game_Object_Component 
    {
        public Vector3 Position { get; set; }

        public Transform_Component()
        {
            Protected_Declare__Downstream_Receiver__Xerxes_Engine_Object
                <SA__Draw>
                (
                    Private__Handle_Draw__Transform_Component
                );
            
            Position = new Vector3();
        }

        private void Private__Handle_Draw__Transform_Component(SA__Draw e)
        {
            e.Draw__Position__Internal
                = Position;
        }
    }
}

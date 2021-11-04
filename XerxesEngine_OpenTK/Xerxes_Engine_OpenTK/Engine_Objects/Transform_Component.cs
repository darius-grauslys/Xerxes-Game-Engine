using OpenTK;

namespace Xerxes_Engine.Export_OpenTK.Engine_Objects
{
    public class Transform_Component : Game_Object_Component 
    {
        public Vector3 Position { get; set; }

        public Transform_Component()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Draw>
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

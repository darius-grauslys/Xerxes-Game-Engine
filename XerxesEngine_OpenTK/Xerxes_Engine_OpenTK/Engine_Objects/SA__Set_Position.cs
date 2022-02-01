using OpenTK;

namespace Xerxes_Engine.Export_OpenTK.Engine_Objects
{
    public class SA__Set_Position :
        SA__Chronical
    {
        public Vector3 SA__Set_Position__POSITIION { get; }

        public SA__Set_Position(SA__Chronical e, Vector3 position)
        : base(e)
        {
            SA__Set_Position__POSITIION = position;
        }
    }
}

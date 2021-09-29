using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Transform_Component : Xerxes_Descendant<Game_Object,Transform_Component> 
    {
        public Vector3 Position
        {
            set => Private_Set__Position__Transform_Component(value);
            get => Private_Get__Position__Transform_Component();
        }

        private void Private_Set__Position__Transform_Component(Vector3 position)
        {
            if (Xerxes_Engine_Object__Is_Disabled)
                return;

            Xerxes_Descendant__Parent__Protected
                .Game_Object__Position__Internal = position;
        }

        private Vector3 Private_Get__Position__Transform_Component()
        {
            if (Xerxes_Engine_Object__Is_Disabled)
                return Vector3.Zero;

            return Xerxes_Descendant__Parent__Protected 
                .Game_Object__Position__Internal;
        }
    }
}

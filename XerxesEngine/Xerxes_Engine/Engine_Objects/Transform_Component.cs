using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Transform_Component : Game_Object_Component
    {
        public Vector3 Position
        {
            get => 
                Game_Object_Component__Attached_Object__Protected?
                .Game_Object__Render_Unit_Position__Internal 
                ?? Vector3.Zero;
            set
            {
                if (Game_Object_Component__Is_Disabled__Protected)
                {
                    
                    return;
                }

                Game_Object_Component__Attached_Object__Protected
                    .Game_Object__Render_Unit_Position__Internal = value;
            }
        }
    }
}

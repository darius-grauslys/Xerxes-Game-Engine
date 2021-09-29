using System.Collections.Generic;
using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Game_Object : Xerxes_Descendant<Scene_Layer,Game_Object>
    {
        internal Vector3 Game_Object__Position__Internal { get; set; }

        private List<object> _Game_Object__COMPONENTS { get; }

        public Game_Object
        (
            Vector3 position
        )
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Update>();
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Render>();
            Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>
                (
                    Private_Handle__Draw__Game_Object
                );

            Game_Object__Position__Internal = position;
        }

        private void Private_Handle__Draw__Game_Object(Streamline_Argument_Draw e)
        {
            e.Streamline_Argument_Draw__Position__Internal = 
                Game_Object__Position__Internal;
        }
    }
}

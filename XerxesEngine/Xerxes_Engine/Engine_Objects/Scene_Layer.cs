using OpenTK;
using System.Collections.Generic;

namespace Xerxes_Engine.Engine_Objects
{
    public class Scene_Layer : 
        Xerxes_Object<Scene_Layer>,
        IXerxes_Descendant_Of<Scene>,
        IXerxes_Ancestor_Of<Game_Object>
    {
        public float Scene_Layer__Width  { get; private set; }
        public float Scene_Layer__Height { get; private set; }

        private Matrix4 _Scene_Layer__Layer_Matrix { get; set; }

        private List<Game_Object> _Scene_Layer__SCENE_OBJECTS { get; }
        public Game_Object[] Scene_Layer__Scene_Objects
            => _Scene_Layer__SCENE_OBJECTS.ToArray();

        public Scene_Layer()
        {
            Protected_Declare__Downstream_Receiver__Xerxes_Engine_Object
                <SA__Resize_2D>
                (
                    Private_Handle__Resize_2D__Scene_Layer
                );

            Protected_Declare__Upstream_Receiver__Xerxes_Engine_Object
                <SA__Draw>
                (
                    Private_Handle__Draw_Child__Scene_Layer
                );
            Protected_Declare__Upstream_Extender__Xerxes_Engine_Object
                <SA__Draw>();

            _Scene_Layer__SCENE_OBJECTS = new List<Game_Object>();
        }

        private void Private_Handle__Resize_2D__Scene_Layer(SA__Resize_2D e)
        {
            Scene_Layer__Width  = e.SA__Resize_2D__WIDTH;
            Scene_Layer__Height = e.SA__Resize_2D__HEIGHT;
            _Scene_Layer__Layer_Matrix = 
                Matrix4.CreateOrthographic
                    (
                    e.SA__Resize_2D__WIDTH, 
                    e.SA__Resize_2D__HEIGHT, 
                    0.01f, 
                    30000f
                    ) 
                * Matrix4.CreateTranslation(0, 0, 1);
        }

        private void Private_Handle__Draw_Child__Scene_Layer(SA__Draw e)
        {
            e.Draw__Projection_Matrix__Internal =
                _Scene_Layer__Layer_Matrix;
            Protected_Invoke__Ascending_Extender__Xerxes_Engine_Object
                (e);
        }
    }
}

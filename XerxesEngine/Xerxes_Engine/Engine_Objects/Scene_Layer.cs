using OpenTK;
using System.Collections.Generic;

namespace Xerxes_Engine.Engine_Objects
{
    public class Scene_Layer : Xerxes_Descendant<Scene, Scene_Layer> 
    {
        public float Scene_Layer__Width  { get; private set; }
        public float Scene_Layer__Height { get; private set; }

        public Matrix4 Scene_Layer__Layer_Matrix { get; protected set; }

        private List<Game_Object> _Scene_Layer__SCENE_OBJECTS { get; }
        public Game_Object[] Scene_Layer__Scene_Objects
            => _Scene_Layer__SCENE_OBJECTS.ToArray();

        public Scene_Layer()
        {
            Protected_Subscribe__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Associate_Game>
                (
                    Private_Associate__To_Game__Scene_Layer
                );
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Update>();
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Render>();
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Resize_2D>
                (
                    Private_Resize__2D__Scene_Layer
                );


            Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>
                (
                    Private_Handle__Draw__Scene_Layer
                );

            _Scene_Layer__SCENE_OBJECTS = new List<Game_Object>();
        }

        private void Private_Resize__2D__Scene_Layer(Streamline_Argument_Resize_2D e)
        {
            Scene_Layer__Width  = e.Streamline_Argument_Resize_2D__WIDTH;
            Scene_Layer__Height = e.Streamline_Argument_Resize_2D__HEIGHT;
            Scene_Layer__Layer_Matrix = 
                Matrix4.CreateOrthographic
                    (
                    e.Streamline_Argument_Resize_2D__WIDTH, 
                    e.Streamline_Argument_Resize_2D__HEIGHT, 
                    0.01f, 
                    30000f
                    ) 
                * Matrix4.CreateTranslation(0, 0, 1);
        }

        private void Private_Associate__To_Game__Scene_Layer(Streamline_Argument_Associate_Game e)
        {
            Protected_Invoke__Descending_Streamline__Xerxes_Engine_Object
            (
                new Streamline_Argument_Resize_2D
                (
                    e.Streamline_Argument__ELAPSED_TIME,
                    e.Streamline_Argument__DELTA_TIME,
                    Xerxes_Descendant__Parent__Protected//Parent Scene
                    .Scene__Width,
                    Xerxes_Descendant__Parent__Protected//Parent Scene
                    .Scene__Height
                )
            );
        }

        private void Private_Handle__Draw__Scene_Layer(Streamline_Argument_Draw e)
        {
            e.Streamline_Argument_Draw__World_Matrix__Internal =
                Scene_Layer__Layer_Matrix;  
        }
    }
}

using OpenTK;
using System.Collections.Generic;

namespace Xerxes_Engine.Engine_Objects
{
    public class Scene_Layer : Xerxes_Engine_Container 
    {
        protected Scene Scene_Layer__Parent_Scene__Protected { get; private set; }

        public int Scene_Layer__LayerLevel { get; internal set; }

        public Matrix4 Scene_Layer__Layer_Matrix { get; protected set; }

        private List<Game_Object> _Scene_Layer__SCENE_OBJECTS { get; }
        public Game_Object[] Scene_Layer__Scene_Objects
            => _Scene_Layer__SCENE_OBJECTS.ToArray();

        public Scene_Layer(int sceneLayerLayerLevel = 0)
            : base(Xerxes_Engine_Object_Association_Type.GAME__SCENE_LAYER)
        {
            _Scene_Layer__SCENE_OBJECTS = new List<Game_Object>();

            Scene_Layer__LayerLevel = sceneLayerLayerLevel;
        }

        protected override bool Handle_Associate__As_Descendant__Xerxes_Engine_Object()
        {
            Scene_Layer__Parent_Scene__Protected =
                Xerxes_Engine_Object__Parent__Internal as Scene;
            return true;
        }

        protected virtual void Add__Scene_Object__Scene_Layer(Game_Object obj)
        {
            bool success = Xerxes_Engine_Object.Internal_Associate__Objects
            (
                obj,
                this
            );

            if(success)
                _Scene_Layer__SCENE_OBJECTS.Add(obj);
        }

        internal override void Internal_Resize__2D__Xerxes_Engine_Container(Event_Argument_Resize_2D e)
        {
            Scene_Layer__Layer_Matrix = 
                Matrix4.CreateOrthographic
                    (
                    e.Event_Argument_Resize_2D__WIDTH, 
                    e.Event_Argument_Resize_2D__HEIGHT, 
                    0.01f, 
                    30000f
                    ) 
                * Matrix4.CreateTranslation(0, 0, 1);
        }
    }
}

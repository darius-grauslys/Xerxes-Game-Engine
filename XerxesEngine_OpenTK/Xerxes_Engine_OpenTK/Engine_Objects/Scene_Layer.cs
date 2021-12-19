﻿using OpenTK;
using System.Collections.Generic;

namespace Xerxes_Engine.Export_OpenTK.Engine_Objects
{
    public class Scene_Layer : 
        Xerxes_Object<Scene_Layer>
    {
        public float Scene_Layer__Width  { get; private set; }
        public float Scene_Layer__Height { get; private set; }

        private Matrix4 _Scene_Layer__Layer_Matrix { get; set; }

        private List<Game_Object> _Scene_Layer__SCENE_OBJECTS { get; }
        public Game_Object[] Scene_Layer__Scene_Objects
            => _Scene_Layer__SCENE_OBJECTS.ToArray();

        public Scene_Layer()
        {
            Declare__Ancestor<Scene>();
            Declare__Descendant<Game_Object>();

            Declare__Streams()
                .Downstream.Receiving<SA__Game_Window_Resized>
                (
                    Private_Handle__Resize_2D__Scene_Layer
                )
                .Upstream  .Receiving<SA__Draw>
                (
                    Private_Handle__Draw_Child__Scene_Layer
                )
                .Upstream  .Extending<SA__Draw>();

            _Scene_Layer__SCENE_OBJECTS = new List<Game_Object>();
        }

        private void Private_Handle__Resize_2D__Scene_Layer(SA__Game_Window_Resized e)
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
            Invoke__Ascending
                (e);
        }
    }
}
using Xerxes_Engine.Systems.Graphics;
using OpenTK;
using System.Collections.Generic;
using System.Linq;

namespace Xerxes_Engine
{
    public class Scene
    {
        public Game Game__REFERENCE { get; private set; }
        public Vector2 Scene__Window_Size__Game
            => Game__REFERENCE.Get__Window_Size__Game();
        
        private Scene_Layer_Dictionary _Scene__LAYER_DICTIONARY { get; }

        private List<Scene_Layer> _Scene__ENABLED_LAYERS { get; }
        protected List<Scene_Layer> Get__Enabled_Layers__Scene() 
            => _Scene__ENABLED_LAYERS.ToList();

        protected void Protected_Disable__Layer__Scene(Scene_Layer_Handle layerHandle) 
        { 
            Scene_Layer layer = _Scene__LAYER_DICTIONARY[layerHandle];
            _Scene__ENABLED_LAYERS.Remove(layer); 
            layer.Internal_Disable__Scene_Layer(); 
        }
        
        protected void Protected_Enable__Layer__Scene(Scene_Layer_Handle layerHandle) 
        { 
            Scene_Layer layer = _Scene__LAYER_DICTIONARY[layerHandle];
            _Scene__ENABLED_LAYERS.Add(layer);
            layer.Internal_Rescale__Scene_Layer();
            layer.Internal_Enable__Scene_Layer(); 
        }

        protected Scene_Layer_Handle Protected_Add__Layer__Scene(Scene_Layer layer, string layerAlias = null) 
        { 
            layer.Internal_Set__Parent__Scene_Layer(this); 

            if(layer.Scene_Layer__Is_Enabled)
                _Scene__ENABLED_LAYERS.Add(layer);

            return _Scene__LAYER_DICTIONARY.Internal_Declare__Layer__Scene_Layer_Dictionary
            (
                layerAlias ?? layer.ToString(),
                layer
            );
        }

        protected void Protected_Add__Layers__Scene(params Scene_Layer[] layers) 
        { 
            foreach (Scene_Layer layer in layers) 
                Protected_Add__Layer__Scene(layer); 
        }

        public Scene(Game game)
        {
            Game__REFERENCE = game;
            _Scene__ENABLED_LAYERS = new List<Scene_Layer>();
            _Scene__LAYER_DICTIONARY = new Scene_Layer_Dictionary();
        }

        internal void Internal_Rescale__Scene()
        {
            Handle_Rescale__Scene();
            foreach (Scene_Layer layer in _Scene__ENABLED_LAYERS)
                layer.Internal_Rescale__Scene_Layer();
        }
        protected virtual void Handle_Rescale__Scene() { }

        internal void Internal_Gain__Focus__Scene()
        {
            Internal_Rescale__Scene();
            Handle_Gain__Focus__Scene();
            foreach (Scene_Layer layer in _Scene__ENABLED_LAYERS)
                layer.Internal_Gain_Focus__Scene_Layer();
        }
        protected virtual void Handle_Gain__Focus__Scene() { }

        internal void Internal_Begin__Render__Scene(Render_Service renderService)
            => Handle_Begin__Render__Scene(renderService);
        /// <summary>
        /// Returns the desired shader ID.
        /// </summary>
        /// <returns></returns>
        protected virtual void Handle_Begin__Render__Scene(Render_Service renderService) { }

        internal void Internal_Render__Scene(Render_Service renderService, Frame_Argument e) 
            => Handle_Render__Scene(renderService, e);
        /// <summary>
        /// Overridable functionality.
        /// </summary>
        /// <param name="renderService"></param>
        /// <param name="e"></param>
        protected virtual void Handle_Render__Scene(Render_Service renderService, Frame_Argument e)
        {
            foreach(Scene_Layer layer in _Scene__ENABLED_LAYERS)
            {
                renderService.CacheMatrix(layer.Scene_Layer__Layer_Matrix);
                layer.Begin_Render__Scene_Layer(renderService);
                layer.Render__Scene_Layer(renderService, e);
            }
        }

        internal void Internal_Update__Scene(Frame_Argument e) => Handle_Update__Scene(e);
        /// <summary>
        /// Overridable functionality.
        /// </summary>
        /// <param name="renderService"></param>
        /// <param name="e"></param>
        protected virtual void Handle_Update__Scene(Frame_Argument e)
        {
            for(int i=0;i<_Scene__ENABLED_LAYERS.Count;i++)
                _Scene__ENABLED_LAYERS[i].Internal_Update__Scene_Layer(e);
        }
    }
}

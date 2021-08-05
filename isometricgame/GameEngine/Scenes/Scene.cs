using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Scenes
{
    public class Scene
    {
        public Game Game { get; private set; }
        public Vector2 Scene__Window_Size__Game
            => Game.Get__Window_Size__Game();
        
        internal List<Scene_Layer> disabledLayers = new List<Scene_Layer>();
        internal List<Scene_Layer> sceneLayers = new List<Scene_Layer>();
        protected List<Scene_Layer> SceneLayers => sceneLayers.ToList();

        internal void _enable_inOrder(Scene_Layer layer)
        {
            disabledLayers.Remove(layer);
            for(int i=0;i< sceneLayers.Count;i++)
            {
                if (sceneLayers[i].SceneLayer__LayerLevel < layer.SceneLayer__LayerLevel)
                {
                    sceneLayers.Insert(i, layer);
                    return;
                }
            }
            sceneLayers.Add(layer);
        }

        internal void DisableLayer(Scene_Layer layer) { sceneLayers.Remove(layer); disabledLayers.Add(layer); layer.Internal_Disable__Scene_Layer(); }
        internal void EnableLayer(Scene_Layer layer) { disabledLayers.Remove(layer); _enable_inOrder(layer); layer.Internal_Enable__Scene_Layer(); }
        protected void DisableLayers<T>() where T : Scene_Layer { foreach (T layer in sceneLayers.ToList().OfType<T>()) { DisableLayer(layer); } }
        protected void EnableLayers<T>() where T : Scene_Layer { foreach(T layer in disabledLayers.ToList().OfType<T>()) { EnableLayer(layer); } }
        protected void AddLayer(Scene_Layer layer) { layer.Internal_Set__Parent__Scene_Layer(this); }
        protected void RemoveLayer(Scene_Layer layer) { layer.Internal_Set__Parent__Scene_Layer(null); }
        protected void AddLayers(params Scene_Layer[] layers) { foreach (Scene_Layer layer in layers) AddLayer(layer); }
        protected void EnableOnlyLayer<T>() where T : Scene_Layer { DisableLayers<Scene_Layer>(); EnableLayers<T>(); }

        public Scene(Game game)
        {
            Game = game;
        }

        internal void RescaleScene()
        {
            Handle_Rescale();
            foreach (Scene_Layer layer in sceneLayers)
                layer.Internal_Rescale__Scene_Layer();
            foreach (Scene_Layer layer in disabledLayers)
                layer.Internal_Rescale__Scene_Layer();
        }
        protected virtual void Handle_Rescale() { }

        internal void GainFocus()
        {
            RescaleScene();
            Handle_GainFocus();
            foreach (Scene_Layer layer in sceneLayers)
                layer.Internal_Gain_Focus__Scene_Layer();
        }
        protected virtual void Handle_GainFocus() { }

        internal void BeginRender(RenderService renderService)
            => HandleBeginRender(renderService);
        /// <summary>
        /// Returns the desired shader ID.
        /// </summary>
        /// <returns></returns>
        protected virtual void HandleBeginRender(RenderService renderService) { }

        internal void RenderScene(RenderService renderService, FrameArgument e) 
            => Handle_RenderScene(renderService, e);
        /// <summary>
        /// Overridable functionality.
        /// </summary>
        /// <param name="renderService"></param>
        /// <param name="e"></param>
        protected virtual void Handle_RenderScene(RenderService renderService, FrameArgument e)
        {
            foreach(Scene_Layer layer in sceneLayers)
            {
                renderService.CacheMatrix(layer.Scene_Layer__Layer_Matrix);
                layer.Begin_Render__Scene_Layer(renderService);
                layer.Render__Scene_Layer(renderService, e);
            }
        }

        internal void UpdateScene(FrameArgument e) => Handle_UpdateScene(e);
        /// <summary>
        /// Overridable functionality.
        /// </summary>
        /// <param name="renderService"></param>
        /// <param name="e"></param>
        protected virtual void Handle_UpdateScene(FrameArgument e)
        {
            for(int i=0;i<sceneLayers.Count;i++)
                sceneLayers[i].Internal_Update__Scene_Layer(e);
        }
    }
}

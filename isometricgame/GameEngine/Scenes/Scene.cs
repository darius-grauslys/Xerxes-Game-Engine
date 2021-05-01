using isometricgame.GameEngine.Components.Rendering;
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

        internal List<SceneLayer> disabledLayers = new List<SceneLayer>();
        internal List<SceneLayer> sceneLayers = new List<SceneLayer>();
        protected List<SceneLayer> SceneLayers => sceneLayers.ToList();

        internal void _enable_inOrder(SceneLayer layer)
        {
            disabledLayers.Remove(layer);
            for(int i=0;i< sceneLayers.Count;i++)
            {
                if (sceneLayers[i].LayerLevel < layer.LayerLevel)
                {
                    sceneLayers.Insert(i, layer);
                    return;
                }
            }
            sceneLayers.Add(layer);
        }

        internal void DisableLayer(SceneLayer layer) { sceneLayers.Remove(layer); disabledLayers.Add(layer); }
        internal void EnableLayer(SceneLayer layer) { disabledLayers.Remove(layer); _enable_inOrder(layer); }
        protected void DisableLayers<T>() where T : SceneLayer { foreach (T layer in sceneLayers.ToList().OfType<T>()) { DisableLayer(layer); } }
        protected void EnableLayers<T>() where T : SceneLayer { foreach(T layer in disabledLayers.ToList().OfType<T>()) { EnableLayer(layer); } }
        protected void AddLayer(SceneLayer layer) { layer.SetParent(this); }
        protected void RemoveLayer(SceneLayer layer) { layer.SetParent(null); }
        protected void AddLayers(params SceneLayer[] layers) { foreach (SceneLayer layer in layers) AddLayer(layer); }
        protected void EnableOnlyLayer<T>() where T : SceneLayer { DisableLayers<SceneLayer>(); EnableLayers<T>(); }

        public Scene(Game game)
        {
            Game = game;
        }

        internal void RescaleScene()
        {
            Handle_Rescale();
            foreach (SceneLayer layer in sceneLayers)
                layer.Rescale();
            foreach (SceneLayer layer in disabledLayers)
                layer.Rescale();
        }
        protected virtual void Handle_Rescale() { }

        internal void GainFocus()
        {
            RescaleScene();
            Handle_GainFocus();
            foreach (SceneLayer layer in sceneLayers)
                layer.SceneGainFocus();
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
            foreach(SceneLayer layer in sceneLayers)
            {
                renderService.CacheMatrix(layer.LayerMatrix);
                layer.BeginRender(renderService);
                layer.RenderLayer(renderService, e);
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
                sceneLayers[i].UpdateLayer(e);
        }
    }
}

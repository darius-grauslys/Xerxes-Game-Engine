
using System.Collections.Generic;

namespace Xerxes.Game_Engine.UI
{
    public class UI_Scene_Layer :
        Scene_Layer
    {
        private List<IFeature_UI> _UI_Scene_Layer__ELEMENTS { get; }

        //Note
        //For UI ancestor resizing, it will not do any modification
        //to child objects.
        //Instead ui scene layer, or containers for that manner
        //will intercept SA__Draw-s going upstream and modify
        //them based on SA state.
        //
        //so in particular, we would have a SA__UI_Draw
        //that has some fields for positional states
        //such as anchors, padding, etc.
        //
        //descendants therefore don't need to react to
        //ancestor state, they focus on their own
        //isolated state and express desires on the upstream.

        /// <summary>
        /// The width in Orthographic screen-space.
        /// </summary>
        protected float UI_Scene_Layer__Width { get; set; }
        /// <summary>
        /// The height in Orthographic screen-space.
        /// </summary>
        protected float UI_Scene_Layer__Height { get; set; }

        public UI_Scene_Layer()
        {
            _UI_Scene_Layer__ELEMENTS =
                new List<IFeature_UI>();
        }
    }
}

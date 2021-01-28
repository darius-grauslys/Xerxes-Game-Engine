using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Rendering.Animation;
using isometricgame.GameEngine.Scenes;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Rendering
{
    public class AnimationComponent : SpriteComponent
    {
        private AnimationSchematic schematic;
        private int node;

        public AnimationComponent(SceneObject parentObject, AnimationSchematic schematic = null) 
            : base(parentObject)
        {
            this.schematic = schematic;
        }

        public void SetSchematic(AnimationSchematic schematic) => this.schematic = schematic;

        public void SetNode(int node)
        {
            this.node = node;
        }

        public void DefineNode(int node, int[] subNodes)
        {
            schematic.DefineNode(node, subNodes);
        }

        public void Pause(double time) => schematic.Pause(time);
        public void Unpause(double time) => schematic.Unpause(time, node);

        protected override void OnUpdate(FrameArgument args)
        {
            sprite.VBO_Index = schematic.GetVBO_Index(args.Time, node);
        }
    }
}

using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Rendering.Animation;
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

        public AnimationComponent(GameObject parentObject, AnimationSchematic schematic) 
            : base(parentObject)
        {
            this.schematic = schematic;
        }

        public void SetNode(int node)
        {
            schematic.SetNode(node);
        }

        public void DefineNode(int node, int[] subNodes)
        {
            schematic.DefineNode(node, subNodes);
        }

        public void Pause(double time) => schematic.Pause(time);
        public void Unpause(double time) => schematic.Unpause(time);

        protected override void OnUpdate(FrameArgument args)
        {
            sprite.VBO_Index = schematic.GetNode(args.Time);
        }

        public override Sprite GetSprite()
        {
            return base.GetSprite();
        }
    }
}

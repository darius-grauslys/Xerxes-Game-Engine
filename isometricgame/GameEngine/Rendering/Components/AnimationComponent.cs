using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Rendering.Animation;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Tools;
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

        public AnimationComponent(AnimationSchematic schematic = null) 
            : base()
        {
            this.schematic = schematic;
        }

        public void SetSchematic(AnimationSchematic schematic) => this.schematic = schematic;

        public void SetNode(int node)
        {
            schematic.Current_Node = node;
        }

        public void DefineNode(
            int nodeIndex, 
            int[] vbo_indices, 
            double speed =-1, 
            bool pausesOnCompletion=false, 
            double loopDelay=-1)
        {
            schematic.DefineNode(
                nodeIndex, 
                vbo_indices,
                speed,
                pausesOnCompletion,
                loopDelay
                );
        }

        public void DefineNode(int nodeIndex, AnimationNode node)
        {
            schematic.DefineNode(nodeIndex, node);
        }

        public void Pause(double time) => schematic.Pause(time);
        public void Unpause() => schematic.Unpause();

        protected override void OnUpdate(FrameArgument args)
        {
            ParentObject.renderUnit.vaoIndex = schematic.GetVBO_Index(args.DeltaTime);
        }

        public void Play(int node)
        {
            schematic.Play(node);
        }
    }
}

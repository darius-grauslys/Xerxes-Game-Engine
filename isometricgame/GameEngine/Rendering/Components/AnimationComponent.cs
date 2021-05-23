﻿using isometricgame.GameEngine.Events.Arguments;
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
        private int node;

        public AnimationComponent(AnimationSchematic schematic = null) 
            : base()
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

        public void Pause(double time, int frame=-1) => schematic.Pause(time, frame);
        public void Unpause(double time) => schematic.Unpause(time, node);

        protected override void OnUpdate(FrameArgument args)
        {
            ParentObject.renderUnit.vaoIndex = schematic.GetVBO_Index(args.Time, node);
        }
    }
}

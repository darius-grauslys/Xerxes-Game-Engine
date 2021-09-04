using XerxesEngine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XerxesEngine.Events;

namespace XerxesEngine.Rendering.Animation
{
    public class AnimationSchematic
    {
        private AnimationNode[] nodes;
        public int Current_Node { get; set; }

        internal double defaultSpeed;
        private int frame = 0;
        private int frameOffset;

        private double animationTime;
        private bool paused = false;

        private int lastNode = 0;
        public int LastNode => lastNode;

        private Timer pauseDuration;
        private bool pauseHasDuration;

        public AnimationSchematic(int nodeCount, double defaultSpeed = 1, int startFrame = 0)
        {
            nodes = new AnimationNode[nodeCount];
            this.defaultSpeed = defaultSpeed;
            frameOffset = startFrame;
        }

        public void SetNodeSpeed(int node, double speed) => nodes[node].Speed = (speed > 0) ? speed : 1;

        public void DefineNode(int node, int[] spriteIndices, double speed=-1, bool pausesOnCompletion=false, double loopDelay=-1)
        {
            DefineNode(node, new AnimationNode(this, spriteIndices, (speed > 0) ? speed : defaultSpeed, pausesOnCompletion, loopDelay));
        }

        public void DefineNode(int nodeIndex, AnimationNode node)
        {
            nodes[nodeIndex] = node;
        }

        public int GetVBO_Index(double deltaTime)
            => GetVBO_Index(deltaTime, Current_Node);

        public int GetVBO_Index(double deltaTime, int node)
        {
            if (!paused)
            {
                animationTime += deltaTime;
                lastNode = nodes[node].Get_VBO_Index(animationTime);
            }
            else if (pauseHasDuration)
            {
                pauseDuration.Progress__Timer(deltaTime);
                if (pauseDuration.Timer__IsFinished)
                    Unpause();
            }

            return lastNode;
        }

        public void Pause(double duration=0)
        {
            paused = true;
            pauseHasDuration = duration > 0;
            pauseDuration = new Timer(duration);
        }

        public void Unpause()
        {
            paused = false;
        }

        public void Play(int node)
        {
            Unpause();
            animationTime = 0;

            Current_Node = node;
        }
    }
}

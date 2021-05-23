using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering.Animation
{
    public class AnimationSchematic
    {
        private int[][] animationNodes;

        private double speed;
        private int frame = 0;
        private int frameOffset;
        private double pauseTime;
        private bool paused = false;

        private int lastNode = 0;
        public int LastNode => lastNode;

        public AnimationSchematic(int nodeCount, double speed = 1, int startFrame = 0)
        {
            animationNodes = new int[nodeCount][];
            this.speed = speed;
            frameOffset = startFrame;
        }

        public void SetSpeed(double speed) => this.speed = (speed > 0) ? speed : 1;

        public void DefineNode(int node, int[] spriteIndices)
        {
            animationNodes[node] = spriteIndices;
        }

        public int GetVBO_Index(double time, int node)
        {
            if (!paused)
                frame = GetFrame(node, time, frameOffset);

            return lastNode = animationNodes[node][frame];
        }

        public void Pause(double pauseTime, int frame = -1)
        {
            this.pauseTime = pauseTime;
            this.frame = (frame >= 0) ? frame : this.frame;
            paused = true;
        }

        public void Unpause(double unpauseTime, int node)
        {
            //Find the time skip
            int unpauseFrame = GetFrame(node, unpauseTime, 0) + animationNodes[node].Length - frame;
            //Offset the time skip so we continue on the same frame.
            frameOffset = unpauseFrame % animationNodes[node].Length;
            pauseTime = 0;
            paused = false;
        }

        private int GetFrame(int node, double time, int givenOffset)
        {
            return (int)((time / speed) + givenOffset) % animationNodes[node].Length;
        }
    }
}

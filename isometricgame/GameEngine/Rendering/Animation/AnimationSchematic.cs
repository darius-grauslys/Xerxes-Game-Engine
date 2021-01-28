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

        private int lastNode = 0;
        public int LastNode => lastNode;

        public AnimationSchematic(int nodeCount, double speed = 1, int startFrame = 0)
        {
            animationNodes = new int[nodeCount][];
            this.speed = speed;
            frameOffset = startFrame;
        }

        public void DefineNode(int node, int[] spriteIndices)
        {
            animationNodes[node] = spriteIndices;
        }

        public int GetVBO_Index(double time, int node)
        {
            if (pauseTime == 0)
                frame = GetFrame(node, time, frameOffset);

            return lastNode = animationNodes[node][frame];
        }

        public void Pause(double pauseTime)
        {
            this.pauseTime = pauseTime;
        }

        public void Unpause(double unpauseTime, int node)
        {
            //Find the time skip
            int unpauseFrame = GetFrame(node, unpauseTime, 0) + animationNodes[node].Length - frame;
            //Offset the time skip so we continue on the same frame.
            frameOffset = unpauseFrame % animationNodes[node].Length;
        }

        private int GetFrame(int node, double time, int givenOffset)
        {
            return (int)((time / speed) + givenOffset) % animationNodes[node].Length;
        }
    }
}

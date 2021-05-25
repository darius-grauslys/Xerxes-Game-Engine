using isometricgame.GameEngine.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Rendering.Animation
{
    public class AnimationNode
    {
        public bool Pauses_OnCompletion { get; set; }
        public bool Transitions_OnComplete { get; set; }
        public int Transitioning_Node { get; set; }
        public bool Is_LoopDelayed => Loop_Delay > 0;

        public double Loop_Delay = 0;
        public int[] VBO_Indices { get; private set; }

        public double Speed { get; set; }

        private AnimationSchematic Animation_Schematic { get; set; }

        public AnimationNode(
            AnimationSchematic animationSchematic, 
            int[] vbo_indices, 
            double speed, 
            bool pausesOnCompletion = false, 
            double loopDelay = -1)
        {
            Animation_Schematic = animationSchematic;
            VBO_Indices = vbo_indices;

            Pauses_OnCompletion = pausesOnCompletion;
            Speed = speed > 0 ? speed : animationSchematic.defaultSpeed;

            Loop_Delay = loopDelay;
        }
        
        public int Get_VBO_Index(double time)
        {
            int frame = (int)(time / Speed) % VBO_Indices.Length;

            CheckEndCondition(frame, time);

            return VBO_Indices[frame];
        }

        private void CheckEndCondition(int frame, double time)
        {
            if (frame >= VBO_Indices.Length-1)
            {
                if (Pauses_OnCompletion)
                    Animation_Schematic.Pause(Loop_Delay);
                else if (Transitions_OnComplete)
                    Animation_Schematic.Play(Transitioning_Node);
            }
        }
    }
}

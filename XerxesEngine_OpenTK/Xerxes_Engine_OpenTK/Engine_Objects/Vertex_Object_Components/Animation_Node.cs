namespace Xerxes.Xerxes_OpenTK.Engine_Objects.Vertex_Object_Components
{
    public class Animation_Node
    {
        public bool Animation_Node__Pauses_OnCompletion { get; set; }
        public bool Animation_Node__Transitions_OnComplete { get; set; }
        public uint Animation_Node__Transitioning_Node { get; set; }
        public bool Animation_Node__Is_LoopDelayed => Animation_Node__Loop_Delay > 0;

        public double Animation_Node__Loop_Delay = 0;
        public int[] Animation_Node__VBO_Indices { get; private set; }

        public double Animation_Node__Speed { get; set; }

        private Animation_Schematic _Animation_Node__Animation_Schematic { get; set; }

        public Animation_Node(
            Animation_Schematic animationSchematic, 
            int[] vbo_indices, 
            double speed, 
            bool pausesOnCompletion = false, 
            double loopDelay = -1)
        {
            _Animation_Node__Animation_Schematic = animationSchematic;
            Animation_Node__VBO_Indices = vbo_indices;

            Animation_Node__Pauses_OnCompletion = pausesOnCompletion;
            Animation_Node__Speed = speed > 0 ? speed : animationSchematic.Internal_Animation_Schematic__Default_Speed;

            Animation_Node__Loop_Delay = loopDelay;
        }
        
        public uint Get__VBO_Index__Animation_Node(double time)
        {
            int frame = (int)(time / Animation_Node__Speed) % Animation_Node__VBO_Indices.Length;

            Private_CheckIf__Meets_End_Condition__Animation_Node(frame, time);

            return (uint)Animation_Node__VBO_Indices[frame];
        }

        private void Private_CheckIf__Meets_End_Condition__Animation_Node(int frame, double time)
        {
            if (frame >= Animation_Node__VBO_Indices.Length-1)
            {
                if (Animation_Node__Pauses_OnCompletion)
                    _Animation_Node__Animation_Schematic.Pause__Animation_Node(Animation_Node__Loop_Delay);
                else if (Animation_Node__Transitions_OnComplete)
                    _Animation_Node__Animation_Schematic.Play__Animation_Node(Animation_Node__Transitioning_Node);
            }
        }
    }
}

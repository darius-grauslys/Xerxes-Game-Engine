namespace Xerxes_Engine.Export_OpenTK.Engine_Objects.Vertex_Object_Components
{
    public class Animation_Schematic
    {
        private Animation_Node[] _Animation_Schematic__Nodes             { get; }
        public int Animation_Schematic__Node_Count                      => _Animation_Schematic__Nodes.Length;
        public uint Animation_Schematic__Current_Node                   { get; set; }

        internal double Internal_Animation_Schematic__Default_Speed     { get; set; }
        private uint _Animation_Schematic__Frame                        { get; set; }
        private uint _Animation_Schematic__Frame_Offset                 { get; set; }

        private double _Animation_Schematic__Time                       { get; set; }
        private bool _Animation_Schematic__Paused                       { get; set; }

        public uint Animation_Schematic__Previous_Node                  { get; private set; }

        private Timer _Animation_Schematic__Pause_Duration              { get; set; }
        private bool _Animation_Schematic__Has_Pause_Duration           { get; set; }

        public Animation_Schematic(int nodeCount, double defaultSpeed = 1, uint startFrame = 0)
        {
            _Animation_Schematic__Nodes = new Animation_Node[nodeCount];
            this.Internal_Animation_Schematic__Default_Speed = defaultSpeed;
            _Animation_Schematic__Frame_Offset = startFrame;
        }

        public void Set__Node_Speed__Animation_Schematic
        (
            uint node, 
            double speed
        ) 
            => _Animation_Schematic__Nodes[node]
                .Animation_Node__Speed = (speed > 0) ? speed : 1;

        public void Define__Node__Animation_Schematic
        (
            uint node, 
            int[] spriteIndices, 
            double speed=-1, 
            bool pausesOnCompletion=false, 
            double loopDelay=-1
        )
        {
            Define__Node__Animation_Schematic
            (
                node, 
                new Animation_Node
                (
                    this, 
                    spriteIndices, 
                    (speed > 0) 
                        ? speed 
                        : Internal_Animation_Schematic__Default_Speed, 
                    pausesOnCompletion, 
                    loopDelay
                )
            );
        }

        public void Define__Node__Animation_Schematic
        (
            uint nodeIndex, 
            Animation_Node node
        )
        {
            _Animation_Schematic__Nodes[nodeIndex] = node;
        }

        public uint Get__VBO_Index__Animation_Node(double deltaTime)
            => Get__VBO_Index__Animation_Node(deltaTime, Animation_Schematic__Current_Node);

        public uint Get__VBO_Index__Animation_Node(double deltaTime, uint node)
        {
            if (!_Animation_Schematic__Paused)
            {
                _Animation_Schematic__Time += deltaTime;
                Animation_Schematic__Previous_Node = _Animation_Schematic__Nodes[node].Get__VBO_Index__Animation_Node(_Animation_Schematic__Time);
            }
            else if (_Animation_Schematic__Has_Pause_Duration)
            {
                _Animation_Schematic__Pause_Duration.Progress__Timer(deltaTime);
                if (_Animation_Schematic__Pause_Duration.Timer__IsFinished)
                    Unpause__Animation_Node();
            }

            return Animation_Schematic__Previous_Node;
        }

        public void Pause__Animation_Node(double duration=0)
        {
            _Animation_Schematic__Paused = true;
            _Animation_Schematic__Has_Pause_Duration = duration > 0;
            _Animation_Schematic__Pause_Duration = new Timer(duration);
        }

        public void Unpause__Animation_Node()
        {
            _Animation_Schematic__Paused = false;
        }

        public void Play__Animation_Node(uint node)
        {
            Unpause__Animation_Node();
            _Animation_Schematic__Time = 0;

            Animation_Schematic__Current_Node = node;
        }
    }
}

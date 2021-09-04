using isometricgame.GameEngine.Rendering.Animation;

namespace isometricgame.GameEngine.Components
{
    public class Animation_Render_Component : GameObject_Component
    {
        private AnimationSchematic schematic;

        public Animation_Render_Component(AnimationSchematic schematic = null) 
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

        protected override void Handle__Update__Component(Frame_Argument args)
        {
            Component__Attached_GameObject.renderUnit.vaoIndex = schematic.GetVBO_Index(args.DeltaTime);
        }

        public void Play(int node)
        {
            schematic.Play(node);
        }
    }
}

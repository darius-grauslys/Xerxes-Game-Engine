using Xerxes_Engine.Systems.Graphics.R2.Animation;

namespace Xerxes_Engine.Systems.Graphics.R2
{
    public class Animation_Render_Component : Game_Object_Component
    {
        private Animation_Schematic _Animation_Render_Component__Schematic { get; set; }

        public Animation_Render_Component(Animation_Schematic schematic = null) 
            : base()
        {
            this._Animation_Render_Component__Schematic = schematic;
        }

        public void Set__Schematic__Animation_Render_Component
        (
            Animation_Schematic schematic
        ) 
            => this._Animation_Render_Component__Schematic = schematic;

        public void Set__Node__Animation_Render_Component(uint node)
        {
            _Animation_Render_Component__Schematic.Animation_Schematic__Current_Node = node;
        }

        public void Define__Node__Animation_Render_Component
        (
            uint nodeIndex, 
            int[] vbo_indices, 
            double speed =-1, 
            bool pausesOnCompletion=false, 
            double loopDelay=-1)
        {
            if (speed < -1)
            {
                Log.Internal_Write__Warning__Log
                (
                    Log.WARNING__ANIMATION_RENDER_COMPONENT__BAD_NEGATIVE_SPEED_1,
                    this,
                    speed
                );
            }
            _Animation_Render_Component__Schematic.Define__Node__Animation_Schematic(
                nodeIndex, 
                vbo_indices,
                speed,
                pausesOnCompletion,
                loopDelay
                );
        }

        public void Define__Node__Animation_Render_Component
        (
            uint nodeIndex, 
            Animation_Node node
        )
        {
            if(nodeIndex > _Animation_Render_Component__Schematic.Animation_Schematic__Node_Count)
            {
                Log.Internal_Write__Log
                (

                    Log_Message_Type.Error__Animation,
                    Log.ERROR__ANIMATION__NODE_DEFINITION__OUT_OF_BOUNDS_2,
                    this,
                    nodeIndex,
                    _Animation_Render_Component__Schematic.Animation_Schematic__Node_Count
                );
                return;
            }
            _Animation_Render_Component__Schematic.Define__Node__Animation_Schematic(nodeIndex, node);
        }

        public void Pause__Animation_Render_Component(double time) 
            => _Animation_Render_Component__Schematic.Pause__Animation_Node(time);
        public void Unpause__Animation_Render_Component() 
            => _Animation_Render_Component__Schematic.Unpause__Animation_Node();

        protected override void Handle_Update__Xerxes_Engine_Object(Event_Argument_Frame e)
        {
            int vbo_index = 
                (int)_Animation_Render_Component__Schematic
                    .Get__VBO_Index__Animation_Node(e.Event_Argument_Frame__DELTA_TIME);

            Game_Object_Component__Attached_Object__Protected
                ._game_Object__Render_Unit.vaoIndex = vbo_index; 
        }

        public void Play__Animation_Render_Component(uint node)
        {
            _Animation_Render_Component__Schematic.Play__Animation_Node(node);
        }
    }
}

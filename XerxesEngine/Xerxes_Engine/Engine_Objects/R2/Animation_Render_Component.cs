namespace Xerxes_Engine.Engine_Objects.R2
{
    public class Animation_Render_Component : Sprite_Render_Component 
    {
        private Animation_Schematic _Animation_Render_Component__Schematic { get; set; }

        public Animation_Render_Component(Animation_Schematic schematic = null) 
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Update>
                (
                    Private_Handle__Update__Animation_Render_Component
                );

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
                Private_Log_Error__Node_Definition_Out_Of_Bounds
                (
                    this,
                    nodeIndex,
                    node.Animation_Node__VBO_Indices.Length
                );
                return;
            }
            _Animation_Render_Component__Schematic.Define__Node__Animation_Schematic(nodeIndex, node);
        }

        public void Play__Animation_Render_Component(uint node)
        {
            _Animation_Render_Component__Schematic.Play__Animation_Node(node);
        }

        public void Pause__Animation_Render_Component(double time) 
            => _Animation_Render_Component__Schematic.Pause__Animation_Node(time);
        public void Unpause__Animation_Render_Component() 
            => _Animation_Render_Component__Schematic.Unpause__Animation_Node();

        private void Private_Handle__Update__Animation_Render_Component(SA__Update e)
        {
            int vbo_index = 
                (int)_Animation_Render_Component__Schematic
                    .Get__VBO_Index__Animation_Node(e.SA___DELTA_TIME);

            Sprite_Render_Component__Sprite__Protected
                .Internal_Set__Active_Vertex_Object__Sprite(vbo_index);
        }

#region Static Logging
        private static void Private_Log_Error__Node_Definition_Out_Of_Bounds
        (
            Animation_Render_Component source,
            uint index,
            int count
        )
        {
            Log.Internal_Write__Log
            (

                Log_Message_Type.Error__Rendering_Setup,
                Log.ERROR__ANIMATION__NODE_DEFINITION__OUT_OF_BOUNDS_2,
                source,
                index,
                count
            );
        }
#endregion
    }
}

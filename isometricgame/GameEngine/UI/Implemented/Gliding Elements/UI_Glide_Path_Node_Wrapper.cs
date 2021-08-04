using OpenTK;

namespace isometricgame.GameEngine.UI.Implemented.Gliding_Elements
{
    public class UI_Glide_Path_Node_Wrapper
    {
        public readonly UI_Glide_Node UI_Glide_Path_Node_Wrapper__WRAPPED_NODE;

        public Vector3 UI_Glide_Path_Node_Wrapper__Node_Position
            => UI_Glide_Path_Node_Wrapper__WRAPPED_NODE.UI_Element__Position;
        
        public UI_Glide_Path_Node_Wrapper UI_Glide_Path_Node_Wrapper__Proceeding_Node { get; private set; }
        public bool UI_Glide_Path_Node_Wrapper__Has_Proceeding_Position
            => UI_Glide_Path_Node_Wrapper__Proceeding_Node != null;
        internal void Internal_Set__Proceeding_Node__UI_Glide_Path_Node_Wrapper(UI_Glide_Path_Node_Wrapper proceedingNode)
            => UI_Glide_Path_Node_Wrapper__Proceeding_Node = proceedingNode;

        public Vector3? UI_Glide_Path_Node_Wrapper__Proceeding_Position
            => UI_Glide_Path_Node_Wrapper__Proceeding_Node?.UI_Glide_Path_Node_Wrapper__Node_Position;

        public Vector3 UI_Glide_Path_Node_Wrapper__Offset_From_Proceeding_Position
            => UI_Glide_Path_Node_Wrapper__Proceeding_Node?.UI_Glide_Path_Node_Wrapper__Node_Position 
                - UI_Glide_Path_Node_Wrapper__Node_Position ?? Vector3.Zero;
        
        public float Get__Distance_To_Next_Node__UI_Glide_Path_Node_Wrapper()
        {
            if (!UI_Glide_Path_Node_Wrapper__Has_Proceeding_Position)
                return 0;
            
            float dist = Vector3.Distance((Vector3) UI_Glide_Path_Node_Wrapper__Proceeding_Position,
                UI_Glide_Path_Node_Wrapper__Node_Position);

            return dist;
        }
        
        public float UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Precursor_Position { get; private set; }
        internal void Internal_Set__Percentage_Of_Total_Path_From_Precursor_Position__UI_Glide_Path_Node_Wrapper(float percentage)
            => UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Precursor_Position = percentage;
        
        public float UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Node_Position { get; private set; }
        internal void Internal_Set__Percentage_Of_Total_Path_From_Node_Position__UI_Glide_Path_Node_Wrapper(float percentage)
            => UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Node_Position = percentage;

        public float UI_Glide_Path_Node_Wrapper__Percentage_Of_Distance_Covered
            => UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Node_Position
               - UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Precursor_Position;
        
        public UI_Glide_Path_Node_Wrapper(UI_Glide_Node node)
        {
            UI_Glide_Path_Node_Wrapper__WRAPPED_NODE = node;
        }
    }
}
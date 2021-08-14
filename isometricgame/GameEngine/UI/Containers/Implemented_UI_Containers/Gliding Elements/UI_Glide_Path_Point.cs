using System;
using OpenTK;

namespace isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers.Gliding_Elements
{
    internal class UI_Glide_Path_Point
    {
        internal UI_Glide_Path_Point UI_Glide_Path_Point__Preceding_Node { get; set; }
        internal float Get__Path_Percentage__Of__Preceding_Node__UI_Glide_Path_Point()
            => UI_Glide_Path_Point__Preceding_Node.UI_Glide_Path_Point__Percentage_Of_Path;
        
        internal UI_Glide_Node UI_Glide_Path_Point__BOUND_NODE { get; }
        public Vector3 Get__UISpace_Position__UI_Glide_Path_Point()
            => UI_Glide_Path_Point__BOUND_NODE.Get__Position_In_UISpace__UI_Element();
        
        internal UI_Glide_Path_Point UI_Glide_Path_Point__Proceeding_Node { get; set; }
        internal float Get__Path_Percentage__Of__Proceeding_Node__UI_Glide_Path_Point()
            => UI_Glide_Path_Point__Proceeding_Node.UI_Glide_Path_Point__Percentage_Of_Path;

        public float UI_Glide_Path_Point__Percentage_Of_Path { get; internal set; }
        
        internal float Internal_Get__UISpace_Distance__UI_Glide_Path_Point()
            => Tools.MathHelper.Get__Safe_Distance
            (
                UI_Glide_Path_Point__BOUND_NODE.Get__Position_In_UISpace__UI_Element(),
                UI_Glide_Path_Point__Proceeding_Node?.Get__UISpace_Position__UI_Glide_Path_Point()
            );

        internal Vector3 Internal_Get__Normalized_Vector3__To_Proceeding_Node__UI_Glide_Path_Point()
        {
            if(UI_Glide_Path_Point__Proceeding_Node == null)
                return Vector3.Zero;

            return Tools.MathHelper.Get__Safe_Normalized
            (
                (
                UI_Glide_Path_Point__Proceeding_Node?.Get__UISpace_Position__UI_Glide_Path_Point()
                -
                UI_Glide_Path_Point__BOUND_NODE.Get__Position_In_UISpace__UI_Element()
                ) 
                ?? Vector3.Zero
            );
        }

        internal UI_Glide_Path_Point
        (
            UI_Glide_Node boundPathPoint
        )
        {
            UI_Glide_Path_Point__BOUND_NODE = boundPathPoint;
        }
    }
}
using System;
using System.Collections.Generic;
using isometricgame.GameEngine.UI.Containers.Implemented.Gliding_Elements;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI.Containers.Implemented.Gliding_Elements
{
    public class UI_Glide_Path
    {
        internal readonly UI_Indexed_Element UI_Glide_Path__BOUND_INDEXED_ELEMENT;

        internal void Internal_Scale__Bound_Element__UI_Glide_Path()
            => UI_Glide_Path__BOUND_INDEXED_ELEMENT.Internal_Scale__Element__UI_Indexed_Element();
        
        private readonly List<UI_Glide_Path_Node_Wrapper> _UI_Glide_Path__NODE_ROUTE = new List<UI_Glide_Path_Node_Wrapper>();

        private UI_Glide_Style_Type _ui_glide_path__Slide_Style_Type;

        public UI_Glide_Style_Type UI_Glide_Path__Slide_Style_Type
        {
            get => _ui_glide_path__Slide_Style_Type;
            set
            {
                _ui_glide_path__Slide_Style_Type = value;
                Private_Update__Route_Values__UI_Glide_Path();
            }
        }
        
        public UI_Glide_Path
        (
            UI_Indexed_Element boundIndexedGlidingElement,
            UI_Glide_Style_Type glideStyleType,
            params UI_Glide_Node[] nodes
        )
        {
            UI_Glide_Path__BOUND_INDEXED_ELEMENT = boundIndexedGlidingElement;

            _ui_glide_path__Slide_Style_Type = glideStyleType;
            
            foreach (UI_Glide_Node node in nodes)
                Add__Glide_Node__UI_Glide_Path(node);
        }

        private void Private_Update__Route_Values__UI_Glide_Path()
        {
            float totalPathDistance = Private_Update__Distances_Between_Nodes__Core__UI_Glide_Path();
            float totalPercentage = 0;

            switch (UI_Glide_Path__Slide_Style_Type)
            {
                case UI_Glide_Style_Type.Clamped:
                case UI_Glide_Style_Type.Wraps_With_Bounce:
                    totalPathDistance = 
                        Private_Conclude__Endpoints_Of_Distance_Update__Clamped_Or_Bounce__UI_Glide_Path(totalPathDistance);
                    Private_Update__Percentages_Of_Total_Path__Core__UI_Glide_Path(totalPathDistance);
                    Private_Conclude__Endpoints_Of_Path_Percentage_Update__Clamped_Or_Bounce__UI_Glide_Path(totalPathDistance);
                    break;
                case UI_Glide_Style_Type.Wraps_Forward:
                    totalPathDistance =
                        Private_Conclude__Endpoints_Of_Distance_Update__Wraps_Forward__UI_Glide_Path(totalPathDistance);
                    Private_Update__Percentages_Of_Total_Path__Core__UI_Glide_Path(totalPathDistance);
                    break;
            }
        }

        private float Private_Update__Distances_Between_Nodes__Core__UI_Glide_Path()
        {
            float totalPathLength = 0;
            
            for (int i = 0; i < _UI_Glide_Path__NODE_ROUTE.Count - 1; i++)
            {
                UI_Glide_Path_Node_Wrapper immediateNode = _UI_Glide_Path__NODE_ROUTE[i];
                UI_Glide_Path_Node_Wrapper proceedingNode = _UI_Glide_Path__NODE_ROUTE[i + 1];

                Vector3 immediatePosition = immediateNode.UI_Glide_Path_Node_Wrapper__Node_Position;
                Vector3 proceedingPosition = proceedingNode.UI_Glide_Path_Node_Wrapper__Node_Position;

                float dist = Vector3.Distance(immediatePosition, proceedingPosition);
                
                immediateNode.Internal_Set__Proceeding_Node__UI_Glide_Path_Node_Wrapper(proceedingNode);

                totalPathLength += dist;
            }

            return totalPathLength;
        }
        
        private float Private_Conclude__Endpoints_Of_Distance_Update__Clamped_Or_Bounce__UI_Glide_Path(float totalPathLength)
        {
            UI_Glide_Path_Node_Wrapper finalNode = _UI_Glide_Path__NODE_ROUTE[_UI_Glide_Path__NODE_ROUTE.Count - 1];
            finalNode.Internal_Set__Proceeding_Node__UI_Glide_Path_Node_Wrapper
            (
                null
            );
            
            return totalPathLength;
        }

        private float Private_Conclude__Endpoints_Of_Distance_Update__Wraps_Forward__UI_Glide_Path(float totalPathLength)
        {
            UI_Glide_Path_Node_Wrapper firstNode = _UI_Glide_Path__NODE_ROUTE[0];
            UI_Glide_Path_Node_Wrapper finalNode = _UI_Glide_Path__NODE_ROUTE[_UI_Glide_Path__NODE_ROUTE.Count - 1];
            finalNode.Internal_Set__Proceeding_Node__UI_Glide_Path_Node_Wrapper
            (
                firstNode
            );
            
            return totalPathLength + finalNode.Get__Distance_To_Next_Node__UI_Glide_Path_Node_Wrapper();
        }

        private void Private_Update__Percentages_Of_Total_Path__Core__UI_Glide_Path(float totalPathDistance)
        {
            float totalPercentage = 0;
            
            for (int i = 0; i < _UI_Glide_Path__NODE_ROUTE.Count; i++)
            {
                UI_Glide_Path_Node_Wrapper immediateNode = _UI_Glide_Path__NODE_ROUTE[i];
                
                float percentage = immediateNode.Get__Distance_To_Next_Node__UI_Glide_Path_Node_Wrapper() /
                                   totalPathDistance;
                percentage += totalPercentage;
                
                immediateNode.Internal_Set__Percentage_Of_Total_Path_From_Node_Position__UI_Glide_Path_Node_Wrapper
                (
                    percentage
                );

                immediateNode.Internal_Set__Percentage_Of_Total_Path_From_Precursor_Position__UI_Glide_Path_Node_Wrapper
                (
                    totalPercentage
                );
                
                totalPercentage = percentage;
            }
        }
        
        private void Private_Conclude__Endpoints_Of_Path_Percentage_Update__Clamped_Or_Bounce__UI_Glide_Path(float totalPathDistance)
        {
            UI_Glide_Path_Node_Wrapper finalNode = _UI_Glide_Path__NODE_ROUTE[_UI_Glide_Path__NODE_ROUTE.Count-1];
            finalNode.Internal_Set__Percentage_Of_Total_Path_From_Node_Position__UI_Glide_Path_Node_Wrapper(1);
        }
        
        public void Add__Glide_Node__UI_Glide_Path(UI_Glide_Node node)
        {
            Insert__Glide_Node__UI_Glide_Path(_UI_Glide_Path__NODE_ROUTE.Count, node);
        }

        public void Insert__Glide_Node__UI_Glide_Path(int index, UI_Glide_Node node)
        {
            index = MathHelper.Clamp__Integer(index, 0, _UI_Glide_Path__NODE_ROUTE.Count);
            
            _UI_Glide_Path__NODE_ROUTE.Insert(index, new UI_Glide_Path_Node_Wrapper(node));
            Private_Update__Route_Values__UI_Glide_Path();
        }

        public void Remove__Glide_Node__UI_Glide_Path(UI_Glide_Node node)
        {
            int index = _UI_Glide_Path__NODE_ROUTE.FindIndex((wrappedNode) =>
                wrappedNode.UI_Glide_Path_Node_Wrapper__WRAPPED_NODE == node);
            
            Remove_At__Glide_Node__UI_Glide_Path(index);
            Private_Update__Route_Values__UI_Glide_Path();
        }

        public void Remove_At__Glide_Node__UI_Glide_Path(int index)
        {
            _UI_Glide_Path__NODE_ROUTE.RemoveAt(index);
        }

        public void Set__Position_By_Percentage__UI_Glide_Path(float pathPercentage)
        {
            float validatedPercentage = pathPercentage;
            
            switch (UI_Glide_Path__Slide_Style_Type)
            {
                case UI_Glide_Style_Type.Clamped:
                case UI_Glide_Style_Type.Wraps_Forward:
                    validatedPercentage = Private_Get__Percentage__Clamped__UI_Glide_Path(pathPercentage);
                    break;
                case UI_Glide_Style_Type.Wraps_With_Bounce:
                    validatedPercentage = Private_Get__Percentage__Wraps_With_Bounce__UI_Glide_Path(pathPercentage);
                    break;
            }
            
            Private_Set__Position_By_Validated_Percentage__UI_Glide_Path(validatedPercentage);
        }

        private void Private_Set__Position_By_Validated_Percentage__UI_Glide_Path(float validatedPercentage)
        {
            UI_Glide_Path_Node_Wrapper nodeToGlideOffOf = null;

            foreach (UI_Glide_Path_Node_Wrapper wrappedNode in _UI_Glide_Path__NODE_ROUTE)
            {
                if (validatedPercentage <
                    wrappedNode.UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Node_Position)
                {
                    nodeToGlideOffOf = wrappedNode;
                    break;
                }
            }

            if (nodeToGlideOffOf == null)
                return;

            Vector3 offset = nodeToGlideOffOf.UI_Glide_Path_Node_Wrapper__Offset_From_Proceeding_Position;

            float deltaPercentage =
                validatedPercentage 
                - nodeToGlideOffOf.UI_Glide_Path_Node_Wrapper__Percentage_Of_Total_Path_From_Precursor_Position;

            float localizedPercentage = deltaPercentage /
                                        nodeToGlideOffOf.UI_Glide_Path_Node_Wrapper__Percentage_Of_Distance_Covered;
            
            Vector3 position =
                nodeToGlideOffOf.UI_Glide_Path_Node_Wrapper__Node_Position
                + (localizedPercentage * offset);
            
            UI_Glide_Path__BOUND_INDEXED_ELEMENT.UI_Indexed_Element__ELEMENT.Internal_Set__Position__UI_Element(position);
        }
        
        private float Private_Get__Percentage__Clamped__UI_Glide_Path(float pathPercentage)
        {
            float percentage = MathHelper.Clamp__UFloat(pathPercentage, 1);

            return percentage;
        }

        private float Private_Get__Percentage__Wraps_With_Bounce__UI_Glide_Path(float pathPercentage)
        {
            int intClamp = (int) pathPercentage;
            int signFlip = (int) Math.Pow(-1, intClamp);

            float rawPercentage = (signFlip * pathPercentage) % 1f;

            float finalPercentage = (signFlip < 0) ? 1 + rawPercentage : rawPercentage;

            return finalPercentage;
        }
    }
}
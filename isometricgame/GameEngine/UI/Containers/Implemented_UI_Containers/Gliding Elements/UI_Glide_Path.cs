using System;
using System.Collections.Generic;
using OpenTK;

namespace isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers.Gliding_Elements
{
    public class UI_Glide_Path
    {
        public UI_Gliding_Wrapper _UI_Glide_Path__GLIDED_WRAPPER { get; }
        private UI_Glide_Path_Point[] _UI_Glide_Path__WRAPPER_NODES { get; }

        public UI_Glide_Type UI_Glide_Path__Glide_Type { get; set; }
        
        public float UI_Glide_Path__Path_Distance { get; private set; }
        
        public float UI_Glide_Path__Element_Path_Percentage { get; private set; }
        public void Set__Element_Path_Percentage__UI_Glide_Path(float percentage)
            => UI_Glide_Path__Element_Path_Percentage = percentage;

        private float Private_Get__Local_Percentage__UI_Glide_Path()
        {
            switch (UI_Glide_Path__Glide_Type)
            {
                case UI_Glide_Type.Clamped:
                    return Private_Get__Modulo_Percentage__UI_Glide_Path();
                default:
                    int index = (int) (UI_Glide_Path__Element_Path_Percentage) - 1;
                    float flip = (float) Math.Pow(-1, index);
                    
                    return Private_Get__Modulo_Percentage__UI_Glide_Path
                        (
                        (1+flip) / 4
                        );
            }
        }

        private float Private_Get__Modulo_Percentage__UI_Glide_Path(float offset = 0)
            => (UI_Glide_Path__Element_Path_Percentage + offset) % 1;
        
        internal UI_Glide_Path
        (
            UI_Glide_Panel glidePanel,
            UI_Element boundElement,
            List<UI_Glide_Node> pathNodes,
            UI_Glide_Type glideType = UI_Glide_Type.Clamped
        )
        {
            _UI_Glide_Path__GLIDED_WRAPPER = new UI_Gliding_Wrapper(boundElement, glidePanel);
            _UI_Glide_Path__WRAPPER_NODES = new UI_Glide_Path_Point[pathNodes.Count];

            UI_Glide_Path__Glide_Type = glideType;

            for (int i = 0; i < pathNodes.Count; i++)
            {
                _UI_Glide_Path__WRAPPER_NODES[i] = new UI_Glide_Path_Point
                (
                    pathNodes[i]
                );
            }

            for (int i = 0; i < _UI_Glide_Path__WRAPPER_NODES.Length; i++)
            {
                UI_Glide_Path_Point precedingNode = Private_Extract__Preceding_Node__UI_Glide_Path(i);
                UI_Glide_Path_Point proceedingNode = Private_Extract__Proceeding_Node__UI_Glide_Path(i);

                UI_Glide_Path_Point node = _UI_Glide_Path__WRAPPER_NODES[i];

                node.UI_Glide_Node_Wrapper__Preceding_Node = precedingNode;
                node.UI_Glide_Node_Wrapper__Proceeding_Node = proceedingNode;
            }
        }

        internal void Internal_Scale__Element__UI_Glide_Path(float? newHypotenuse = null)
        {
            _UI_Glide_Path__GLIDED_WRAPPER.Internal_Scale__Element__UI_Indexed_Element(newHypotenuse);
        }
        
        internal void Internal_Update__Path__UI_Glide_Path()
        {
            Private_Calculate__Path_Distance__UI_Glide_Path();
            Private_Calculate__Path_Percentages__UI_Glide_Path();
            
            Private_Update__Element_Position__UI_Glide_Path();
        }

        private void Private_Update__Element_Position__UI_Glide_Path()
        {
            float anchorPoint_PathPercentage;
            
            UI_Glide_Path_Point anchoringNode = Private_Get__Anchoring_Node__UI_Glide_Path
            (
                out anchorPoint_PathPercentage
            );

            float element_LocalPercentage = UI_Glide_Path__Element_Path_Percentage - anchorPoint_PathPercentage;

            float hypotenuse_Percentage =
                element_LocalPercentage / anchoringNode.UI_Glide_Node_Wrapper__Percentage_Of_Path;

            float hypotenuse_ToProceedingNode = anchoringNode.Internal_Get__UISpace_Distance__UI_Glide_Node_Wrapper()
                                                  * hypotenuse_Percentage;
            
            Vector3 normalizedVector_ToProceedingNode =
                anchoringNode.Internal_Get__Normalized_Vector3__To_Proceeding_Node();

            Vector3 offsetFromNode = normalizedVector_ToProceedingNode * hypotenuse_ToProceedingNode;

            _UI_Glide_Path__GLIDED_WRAPPER.UI_Element_Glide_Wrapper__Position_From_Node = offsetFromNode;
            _UI_Glide_Path__GLIDED_WRAPPER.Internal_Set__Position__UI_Element_Glide_Wrapper();
        }

        private UI_Glide_Path_Point Private_Get__Anchoring_Node__UI_Glide_Path
        (
            out float anchorPoint_PathPercentage
        )
        {
            float totalPercentage = anchorPoint_PathPercentage = 0;
            float localPercentage = Private_Get__Local_Percentage__UI_Glide_Path();

            foreach (UI_Glide_Path_Point node in _UI_Glide_Path__WRAPPER_NODES)
            {
                totalPercentage += node.UI_Glide_Node_Wrapper__Percentage_Of_Path;
                if (totalPercentage > localPercentage)
                    return node;
                anchorPoint_PathPercentage = totalPercentage;
            }

            return _UI_Glide_Path__WRAPPER_NODES[_UI_Glide_Path__WRAPPER_NODES.Length - 1];
        }

        private UI_Glide_Path_Point Private_Extract__Preceding_Node__UI_Glide_Path
        (
            int index
        )
        {
            if (index - 1 > -1)
                return _UI_Glide_Path__WRAPPER_NODES[index - 1];

            if (UI_Glide_Path__Glide_Type == UI_Glide_Type.Wrapped)
                return _UI_Glide_Path__WRAPPER_NODES[_UI_Glide_Path__WRAPPER_NODES.Length - 1];

            return null;
        }
        
        private UI_Glide_Path_Point Private_Extract__Proceeding_Node__UI_Glide_Path
        (
            int index
        )
        {
            if (index < _UI_Glide_Path__WRAPPER_NODES.Length - 1)
                return _UI_Glide_Path__WRAPPER_NODES[index + 1];

            if (UI_Glide_Path__Glide_Type == UI_Glide_Type.Wrapped)
                return _UI_Glide_Path__WRAPPER_NODES[0];

            return null;
        }
        
        private void Private_Calculate__Path_Distance__UI_Glide_Path()
        {
            float totalDistance = 0;
            
            for (int i = 0; i < _UI_Glide_Path__WRAPPER_NODES.Length; i++)
            {
                totalDistance += _UI_Glide_Path__WRAPPER_NODES[i]
                    .Internal_Get__UISpace_Distance__UI_Glide_Node_Wrapper();
            }

            UI_Glide_Path__Path_Distance = totalDistance;
        }

        private void Private_Calculate__Path_Percentages__UI_Glide_Path()
        {
            foreach (UI_Glide_Path_Point wrapperNode in _UI_Glide_Path__WRAPPER_NODES)
            {
                float percentage = wrapperNode.Internal_Get__UISpace_Distance__UI_Glide_Node_Wrapper() /
                                   UI_Glide_Path__Path_Distance;

                wrapperNode.UI_Glide_Node_Wrapper__Percentage_Of_Path = percentage;
            }
        }
    }
}
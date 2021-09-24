﻿using System.Collections.Generic;
using OpenTK;

namespace Xerxes_Engine.UI.Implemented_UI_Containers.Gliding_Elements
{
    public class UI_Glide_Path
    {
        public UI_Gliding_Wrapper _UI_Glide_Path__GLIDED_WRAPPER { get; }
        private UI_Glide_Path_Point[] _UI_Glide_Path__WRAPPER_NODES { get; }

        public UI_Glide_Type UI_Glide_Path__Glide_Type { get; set; }
        
        public float UI_Glide_Path__Path_Distance { get; private set; }
        
        public float UI_Glide_Path__Element_Path_Percentage { get; private set; }

        public void Set__Element_Path_Percentage__UI_Glide_Path(float percentage)
        {
            UI_Glide_Path__Element_Path_Percentage = percentage;
            
            Private_Update__Element_Position__UI_Glide_Path();
        }

        private float Private_Get__Clamped_Percentage__UI_Glide_Path(float percentageToClamp)
        {
            switch (UI_Glide_Path__Glide_Type)
            {
                case UI_Glide_Type.Clamped:
                case UI_Glide_Type.Wrapped:    
                    return Private_Get__Default_Clamped_Percentage__UI_Glide_Path(percentageToClamp);
                default:
                    float basicPercentage = 2 * Private_Get__Default_Clamped_Percentage__UI_Glide_Path(percentageToClamp);
                    float offset = 
                        (basicPercentage > 1f)
                        ? 1 - (basicPercentage - 1)
                        : basicPercentage;
                    offset = Private_Get__Default_Clamped_Percentage__UI_Glide_Path(offset);
                    return offset;
            }
        }

        private float Private_Get__Default_Clamped_Percentage__UI_Glide_Path(float percentageToClamp)
            => Tools.Math_Helper.Clamp__Float(percentageToClamp, 0, 1);
        
        internal UI_Glide_Path
        (
            UI_Glide_Container glideContainer,
            UI_Element boundElement,
            List<UI_Glide_Node> pathNodes,
            UI_Glide_Type glideType = UI_Glide_Type.Clamped
        )
        {
            _UI_Glide_Path__GLIDED_WRAPPER = new UI_Gliding_Wrapper(boundElement, glideContainer);
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

                node.UI_Glide_Path_Point__Preceding_Node = precedingNode;
                node.UI_Glide_Path_Point__Proceeding_Node = proceedingNode;
            }
            
            Internal_Update__Path__UI_Glide_Path();
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
            float anchorPoint_PathPercentage, clampedPercentage;
            
            UI_Glide_Path_Point anchoringNode = Private_Get__Anchoring_Node__UI_Glide_Path
            (
                out anchorPoint_PathPercentage, out clampedPercentage
            );

            float element_LocalPercentage = clampedPercentage - anchorPoint_PathPercentage;

            float hypotenuse_Percentage =
                element_LocalPercentage / anchoringNode.UI_Glide_Path_Point__Percentage_Of_Path;

            float hypotenuse_ToProceedingNode = anchoringNode.Internal_Get__UISpace_Distance__UI_Glide_Path_Point()
                                                  * hypotenuse_Percentage;
            
            Vector3 normalizedVector_ToProceedingNode =
                anchoringNode.Internal_Get__Normalized_Vector3__To_Proceeding_Node__UI_Glide_Path_Point();

            Vector3 offsetFromNode = normalizedVector_ToProceedingNode * hypotenuse_ToProceedingNode;
            
            _UI_Glide_Path__GLIDED_WRAPPER.Internal_Set__Position__UI_Element_Glide_Wrapper
            (
                offsetFromNode + 
                anchoringNode.Get__UISpace_Position__UI_Glide_Path_Point()
            );
        }

        private UI_Glide_Path_Point Private_Get__Anchoring_Node__UI_Glide_Path
        (
            out float anchorPoint_PathPercentage, out float clampedPercentage
        )
        {
            float totalPercentage = anchorPoint_PathPercentage = 0;
            clampedPercentage = Private_Get__Clamped_Percentage__UI_Glide_Path(UI_Glide_Path__Element_Path_Percentage);

            foreach (UI_Glide_Path_Point node in _UI_Glide_Path__WRAPPER_NODES)
            {
                totalPercentage += node.UI_Glide_Path_Point__Percentage_Of_Path;
                if (totalPercentage >= clampedPercentage)
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
                    .Internal_Get__UISpace_Distance__UI_Glide_Path_Point();
            }

            UI_Glide_Path__Path_Distance = totalDistance;
        }

        private void Private_Calculate__Path_Percentages__UI_Glide_Path()
        {
            foreach (UI_Glide_Path_Point wrapperNode in _UI_Glide_Path__WRAPPER_NODES)
            {
                float percentage = wrapperNode.Internal_Get__UISpace_Distance__UI_Glide_Path_Point() /
                                   UI_Glide_Path__Path_Distance;

                wrapperNode.UI_Glide_Path_Point__Percentage_Of_Path = percentage;
            }
        }
    }
}
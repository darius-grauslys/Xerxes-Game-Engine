using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using isometricgame.GameEngine.UI.Containers.Implemented.Gliding_Elements;
using OpenTK;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI.Containers.Implemented.Gliding_Elements
{
    public class UI_Glide_Panel : UI_Container
    {
        private readonly List<UI_Glide_Path> _UI_Gliding_Panel__GLIDE_PATHS;

        protected override List<UI_Indexed_Element> Handle_Internal_Get__CHILD_ELEMENTS__UI_Container()
        {
            List<UI_Indexed_Element> returningElements = base.Handle_Internal_Get__CHILD_ELEMENTS__UI_Container();
            
            foreach(UI_Glide_Path path in _UI_Gliding_Panel__GLIDE_PATHS)
                returningElements.Add(path.UI_Glide_Path__BOUND_INDEXED_ELEMENT);

            return returningElements;
        }

        public UI_Glide_Panel
        (
            UI_Rect boundingRect, 
            
            UI_GameObject associatedGameObject = null
        ) 
            : base
                (
                boundingRect, 
                
                associatedGameObject
                )
        {
            _UI_Gliding_Panel__GLIDE_PATHS = new List<UI_Glide_Path>();
        }

        public bool Add__Glide_Node__UI_Glide_Panel(UI_Glide_Node node, UI_Anchor_Sort_Style sortStyle = null)
        {
            return Add__UI_Element__UI_Container(new UI_Indexed_Element(node, this, sortStyle));
        }
        
        public UI_Glide_Path Define__Glide_Path__UI_Glide_Panel
        (
            UI_GameObject uiGameObject,
            UI_Glide_Style_Type glideStyleType,
            params int[] nodeIndices
        )
            => Define__Glide_Path__UI_Glide_Panel
                (
                uiGameObject.UI_GameObject__UI_Element__Internal,
                glideStyleType,
                nodeIndices
                );
        
        public UI_Glide_Path Define__Glide_Path__UI_Glide_Panel
        (
            UI_Element glidingElement,
            UI_Glide_Style_Type glideStyleType,
            params int[] nodeIndices
        )
        {
            UI_Indexed_Element indexedGlidingElement = new UI_Indexed_Element(glidingElement, this);
            List<UI_Glide_Node> nodes = new List<UI_Glide_Node>();
            
            foreach(int index in nodeIndices)
                if (Check_If__Index_Within_Bounds__UI_Container(index))
                    nodes.Add(Get__Element__UI_Container<UI_Glide_Node>(index));

            if (nodes.Count > 0)
            {
                UI_Glide_Path path = new UI_Glide_Path(indexedGlidingElement, glideStyleType, nodes.ToArray());
                
                _UI_Gliding_Panel__GLIDE_PATHS.Add(path);

                return path;
            }

            return null;
        }

        protected override void Handle_Scale__UI_Element()
        {
            base.Handle_Scale__UI_Element();
            foreach (UI_Glide_Path path in _UI_Gliding_Panel__GLIDE_PATHS)
            {
                path.Internal_Scale__Bound_Element__UI_Glide_Path();
            }
        }
    }
}
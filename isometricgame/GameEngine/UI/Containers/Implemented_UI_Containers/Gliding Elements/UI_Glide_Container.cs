using System.Collections.Generic;
using OpenTK;

namespace isometricgame.GameEngine.UI.Containers.Implemented_UI_Containers.Gliding_Elements
{
    public class UI_Glide_Container : UI_Vague_Container
    {
        private readonly List<UI_Glide_Path> _UI_Glide_Panel__GLIDE_PATHS = new List<UI_Glide_Path>();

        protected override List<UI_Wrapper> Handle_Internal_Get__CHILD_ELEMENTS__UI_Container()
        {
            List<UI_Wrapper> allWrappers = new List<UI_Wrapper>();
            
            foreach(UI_Glide_Path path in _UI_Glide_Panel__GLIDE_PATHS)
                allWrappers.Add(path._UI_Glide_Path__GLIDED_WRAPPER);

            List<UI_Wrapper> baseChildren = base.Handle_Internal_Get__CHILD_ELEMENTS__UI_Container();
            
            allWrappers.AddRange(baseChildren);
            
            return allWrappers;
        }

        public UI_Glide_Container(UI_Rect boundingRect) 
            : base(boundingRect)
        {
        }

        public bool Add__Glide_Node__UI_Glide_Panel(UI_Glide_Node node, UI_Anchor anchor = null)
        {
            return Add__UI_Element__UI_Container(node, anchor);
        }

        public UI_Glide_Path Bind__UI_Element_To_Path__UI_Glide_Panel
        (
            UI_Element element,
            UI_Glide_Type glideType,
            params int[] nodeIndices
        )
        {
            List<UI_Glide_Node> nodes = new List<UI_Glide_Node>();
            
            foreach(int index in nodeIndices)
                if(Check_If__Index_Within_Bounds__UI_Container(index))
                    nodes.Add(Get__Element__UI_Container<UI_Glide_Node>(index));

            UI_Glide_Path path = new UI_Glide_Path
            (
                this,
                element,
                nodes,
                glideType
            );

            _UI_Glide_Panel__GLIDE_PATHS.Add(path);

            return path;
        }

        protected override void Handle_Scale__UI_Element()
        {
            foreach (UI_Glide_Path path in _UI_Glide_Panel__GLIDE_PATHS)
            {
                path.Internal_Scale__Element__UI_Glide_Path();
                path.Internal_Update__Path__UI_Glide_Path();
            }
        }

        protected override void Handle_Reposition__UI_Element()
        {
            foreach (UI_Glide_Path path in _UI_Glide_Panel__GLIDE_PATHS)
            {
                path.Internal_Update__Path__UI_Glide_Path();
            }
        }
    }
}
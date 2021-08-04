using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.ES10;
using MathHelper = isometricgame.GameEngine.Tools.MathHelper;

namespace isometricgame.GameEngine.UI
{
    /// <summary>
    /// A UI_Element that contains other UI_Elements. It automatically positions these
    /// elements into UI_Schematics.
    /// </summary>
    public sealed class UI_Strict_Panel : UI_Container
    {
        public UI_Strict_Panel
        (
            UI_Rect boundingRect,

            UI_Horizontal_Anchor majorAnchor,
            UI_Vertical_Anchor lesserAnchor,
            
            UI_GameObject associatedGameObject = null,
            
            params UI_Element[] childElements
        )
            : this
            (
                boundingRect,
            
                majorAnchor as UI_Anchor,
                lesserAnchor as UI_Anchor,
                
                associatedGameObject,
                
                childElements
            )
        {
            
        }

        public UI_Strict_Panel
        (
            UI_Rect boundingRect,

            UI_Vertical_Anchor majorAnchor,
            UI_Horizontal_Anchor lesserAnchor,
            
            UI_GameObject associatedGameObject = null,

            params UI_Element[] childElements
        )
            : this
            (
                boundingRect,

                majorAnchor as UI_Anchor,
                lesserAnchor as UI_Anchor,
                
                associatedGameObject,

                childElements
            )
        {
            
        }
        
        internal UI_Strict_Panel
            (
            UI_Rect boundingRect,
            
            UI_Anchor majorAnchor,
            UI_Anchor lesserAnchor,
            
            UI_GameObject associatedGameObject = null,
            
            params UI_Element[] childElements
            )
        : base
            (
                boundingRect,
                
                majorAnchor,
                lesserAnchor,
                
                associatedGameObject,
                
                childElements
            )
        {
        }

        public UI_Indexed_Element[] Get__Child_Elements__UI_Strict_Panel()
            => Get__CHILD_ELEMENTS__UI_Container();

        public void Add__Element__UI_Strict_Panel(UI_Element element)
            => Add__UI_Element__UI_Container(new UI_Indexed_Element(this, element));

        public void Add__UI_GameObject__UI_Strict_Panel(UI_GameObject uiGameObject)
            => Add__Element__UI_Strict_Panel(uiGameObject.UI_GameObject__UI_Element__Internal);

        public void Add__UI_GameObjects__UI_Strict_Panel(UI_GameObject[] uiGameObjects)
        {
            foreach(UI_GameObject uiGameObject in uiGameObjects)
                Add__UI_GameObject__UI_Strict_Panel(uiGameObject);
        }
    }
}
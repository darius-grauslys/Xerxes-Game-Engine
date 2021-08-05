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
            
            UI_GameObject associatedGameObject = null
            )
        : base
            (
                boundingRect,
                
                associatedGameObject
            )
        {
        }

        public UI_Indexed_Element[] Get__Child_Elements__UI_Strict_Panel()
            => Get__CHILD_ELEMENTS__UI_Container();

        public bool Add__Element__UI_Strict_Panel(UI_Element element, UI_Anchor bindingAnchor)
            => Add__UI_Element__UI_Container(new UI_Indexed_Element(element, bindingAnchor, this));

        public bool Add__UI_GameObject__UI_Strict_Panel(UI_GameObject uiGameObject, UI_Anchor bindingAnchor)
            => Add__Element__UI_Strict_Panel(uiGameObject.UI_GameObject__UI_Element__Internal, bindingAnchor);
    }
}
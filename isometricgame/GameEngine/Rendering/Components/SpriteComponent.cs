using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Rendering
{
    public class SpriteComponent : GameComponent
    {
        public SpriteComponent() 
            : base()
        {
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public virtual void SetSprite(RenderUnit ru)
        {
            if (ParentObject != null)
                ParentObject.renderUnit = ru;
        }

        public virtual void SetSprite(int spriteId, int vao_Index = 0)
        {
            if (ParentObject != null)
            {
                ParentObject.renderUnit.Id = spriteId;
                ParentObject.renderUnit.VAO_Index = vao_Index;
                ParentObject.renderUnit.IsInitialized = true;
            }
        }
    }
}

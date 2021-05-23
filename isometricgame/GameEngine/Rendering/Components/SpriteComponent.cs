using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems.Rendering;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Components.Rendering
{
    public class SpriteComponent : GameComponent
    {
        private SpriteLibrary spriteLibrary;
        public Sprite Sprite => spriteLibrary?.GetSprite(ParentObject.renderUnit.id);

        public SpriteComponent() 
            : base()
        {
        }

        protected override void Handle_NewParent()
        {
            spriteLibrary = ParentObject.SceneLayer.Game.SpriteLibrary;
        }

        public virtual void SetSprite(string name)
        {
            Vector3 pos = ParentObject.renderUnit.Position;
            ParentObject.renderUnit = spriteLibrary.ExtractRenderUnit(name);
            ParentObject.renderUnit.Position = pos;
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public virtual void SetSprite(RenderUnit ru, bool copyPosition = false)
        {
            if (ParentObject != null)
            {
                if (copyPosition)
                    ru.Position = ParentObject.Position;
                ParentObject.renderUnit = ru;
            }
        }

        public virtual void SetSprite(int spriteId, int vao_Index = 0)
        {
            if (ParentObject != null)
            {
                ParentObject.renderUnit.id = spriteId;
                ParentObject.renderUnit.vaoIndex = vao_Index;
                ParentObject.renderUnit.IsInitialized = true;
            }
        }
    }
}

using isometricgame.GameEngine;
using isometricgame.GameEngine.Attributes.Physics;
using isometricgame.GameEngine.Attributes.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Services;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.Isogame.Implemented.GameObjects.PlayerControlled
{
    public class Player : GameObject
    {
        public Player(Scene scene, Vector3 position) 
            : base(scene, position)
        {
            AddAttribute(new MovementControllerAttribute(this));
            SpriteAttribute sa = new SpriteAttribute(this);
            sa.SetSprite(scene.Game.GetService<SpriteLibrary>().GetSprite("player"));
            AddAttribute(sa);
        }
    }
}

using isometricgame.GameEngine;
using isometricgame.GameEngine.Components.Physics;
using isometricgame.GameEngine.Components.Rendering;
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
            SpriteComponent sa = new SpriteComponent(this);
            sa.SetSprite(scene.Game.GetService<SpriteLibrary>().GetSprite("player"));
            AddAttribute(sa);
        }
    }
}

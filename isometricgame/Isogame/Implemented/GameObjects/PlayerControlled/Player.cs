using isometricgame.GameEngine;
using isometricgame.GameEngine.Components.Physics;
using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Events.Arguments;
using isometricgame.GameEngine.Rendering.Animation;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Systems;
using isometricgame.GameEngine.Systems.Rendering;
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
        MovementControllerComponent movementController;
        AnimationComponent animationComponent;

        public Player(Scene scene, Vector3 position) 
            : base(scene, position)
        {
            //SpriteComponent sa = new SpriteComponent(this);

            AnimationSchematic schem = new AnimationSchematic(8,0.05,0);
            
            for (int i = 0; i < 8; i++)
            {
                int[] subNodes = new int[8];
                for (int j = 0; j < subNodes.Length; j++)
                    subNodes[j] = j + (8 * i);

                schem.DefineNode(i, subNodes);
            }

            animationComponent = new AnimationComponent(this, schem);
            animationComponent.SetSprite(scene.Game.GetSystem<SpriteLibrary>().GetSprite("player"));

            AddAttribute(animationComponent);
            AddAttribute(movementController = new MovementControllerComponent(this));
        }

        public override void OnUpdate(FrameArgument args)
        {
            movementController.Update(args);

            int node = MovementControllerComponent.MovementDirection_ToAnim(movementController.Direction);

            if (node != 0)
                animationComponent.SetNode(node - 1);

            animationComponent.Update(args);
        }
    }
}

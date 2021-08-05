using System.Linq;
using isometricgame.GameEngine.Components;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.Scenes.Components;
using OpenTK;

namespace isometricgame.GameEngine
{
    public class World_Space_GameObject : GameObject
    {
        public readonly Sprite_Render_Component GameObject_World__Sprite_Render;
        public readonly Transform_Component GameObject_World__Transform;

        public Vector3 Position
        {
            get => GameObject_World__Transform.Position;
            set => GameObject_World__Transform.Position = value;
        }

        public World_Space_GameObject
            (
            Scene_Layer sceneLayer,
            Vector3 position, 
            params GameObject_Component[] components
            ) 
            : base
                (
                sceneLayer,
                position, 
                Enumerable
                    .Concat<GameObject_Component>
                        (
                        new GameObject_Component[] { new Sprite_Render_Component(), new Transform_Component() },
                        components
                        )
                    .ToArray()
                )
        {
            GameObject_World__Sprite_Render = Get__Component__GameObject<Sprite_Render_Component>();
            GameObject_World__Transform = Get__Component__GameObject<Transform_Component>();
        }
    }
}
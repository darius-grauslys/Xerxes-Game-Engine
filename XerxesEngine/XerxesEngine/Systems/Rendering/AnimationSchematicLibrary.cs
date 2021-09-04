using XerxesEngine.Rendering.Animation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace XerxesEngine.Systems.Rendering
{
    public class AnimationSchematicLibrary : GameSystem
    {
        private Dictionary<string, AnimationSchematic> animationSchematics = new Dictionary<string, AnimationSchematic>();

        public AnimationSchematicLibrary(Game gameRef) 
            : base(gameRef)
        {
        }

        /// <summary>
        /// Not finished yet.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public void LoadSchematic(string path, string name = "")
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            if (name == String.Empty)
                name = Path.GetFileNameWithoutExtension(path);

            int[][] nodes;
            int nodeCount = -1;
            float animSpeed = 0.1f;

            using (StreamReader reader = File.OpenText(path))
            {
                string line;
                while (!reader.EndOfStream)
                {

                    
                }
            }
        }

        /// <summary>
        /// Remove later.
        /// </summary>
        public void AddSchematic(AnimationSchematic schem, string name = "")
        {
            if (name == "")
                name = "anim_" + animationSchematics.Count;

            animationSchematics.Add(name, schem);
        }

        public AnimationSchematic GetSchematic(string name) => animationSchematics[name];
        public AnimationSchematic GetSchematic(int id) => animationSchematics.Values.ElementAt(id);
    }
}

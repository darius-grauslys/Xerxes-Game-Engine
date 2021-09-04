using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xerxes_Engine.Systems.Graphics.R2.Animation;

namespace Xerxes_Engine.Systems.Graphics.R2
{
    public class Sprite_Animation_Library : Game_System
    {
        private Dictionary<string, Animation_Schematic> animationSchematics = new Dictionary<string, Animation_Schematic>();

        public Sprite_Animation_Library(Game gameRef) 
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
        public void AddSchematic(Animation_Schematic schem, string name = "")
        {
            if (name == "")
                name = "anim_" + animationSchematics.Count;

            animationSchematics.Add(name, schem);
        }

        public Animation_Schematic GetSchematic(string name) => animationSchematics[name];
        public Animation_Schematic GetSchematic(int id) => animationSchematics.Values.ElementAt(id);
    }
}

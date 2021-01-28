using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace isometricgame.GameEngine.Scenes
{
    public class SceneStructure
    {
        private StructureUnit[] structuralUnits;

        public StructureUnit[] StructuralUnits { get => structuralUnits; set => structuralUnits = value; }

        public SceneStructure(int width = 0, int height = 0) 
        {
            structuralUnits = new StructureUnit[width * height];
        }

        /// <summary>
        /// Resizes the array to omit null items.
        /// </summary>
        public virtual void Cull()
        {
            if (structuralUnits == null)
            {
                structuralUnits = new StructureUnit[0];
                return;
            }
            List<StructureUnit> n_structuralUnits = new List<StructureUnit>();
            for (int i = 0; i < structuralUnits.Length; i++)
                if (structuralUnits[i].IsNull)
                    n_structuralUnits.Add(structuralUnits[i]);
            structuralUnits = n_structuralUnits.ToArray();
        }
    }
}

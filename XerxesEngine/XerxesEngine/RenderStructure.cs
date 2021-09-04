using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XerxesEngine.Rendering;
using OpenTK;

namespace XerxesEngine.Scenes
{
    public struct RenderStructure
    {
        internal RenderUnit[][] structuralUnits;
        internal float maximumZ;
        internal float minimumZ;
        private int width, height;
        private bool isValid;

        public RenderUnit[][] StructuralUnits { get => structuralUnits; set => structuralUnits = value; }
        public float MinimumZ { get => minimumZ; private set => minimumZ = value; }
        public float MaximumZ { get => maximumZ; private set => maximumZ = value; }
        public bool IsValid { get => isValid; set => isValid = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public RenderStructure(int width = 0, int height = 0) 
        {
            structuralUnits = new RenderUnit[height][];
            for (int i = 0; i < height; i++)
                structuralUnits[i] = new RenderUnit[width];
            minimumZ = 0;
            maximumZ = 0;
            isValid = true;
            this.width = width;
            this.height = height;
        }

        public void AssertZRange()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float z = structuralUnits[x][y].Position.Z;
                    if (z < minimumZ)
                        minimumZ = z;
                    if (z > maximumZ)
                        maximumZ = z;
                }
            }
                    
        }
    }
}

using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Scenes
{
    /// <summary>
    /// A struct that coresponds to a Global GameObject. Used in SceneStructures that have non-unique objects such as water.
    /// </summary>
    public struct StructureUnit
    {
        private int id;
        private int dataTag;
        private Vector3 position;
        private bool isInitialized;
        public bool IsNull => isInitialized;

        /// <summary>
        /// Corresponds to a Global GameObject position in a scene.
        /// </summary>
        public int Id { get => id; set => id = value; }
        /// <summary>
        /// A mutable value for relaying simple context based information quickly. Example: Water orientation.
        /// </summary>
        public int DataTag { get => dataTag; set => dataTag = value; }
        /// <summary>
        /// Isometric game position relevant for drawing.
        /// </summary>
        public Vector3 Position { get => position; set => position = value; }

        public StructureUnit(int id, int dataTag, Vector3 position)
        {
            this.id = id;
            this.dataTag = dataTag;
            this.position = position;
            isInitialized = true;
        }

        public static bool operator ==(StructureUnit s1, StructureUnit s2) => s1.id == s2.id && s1.dataTag == s2.dataTag && s1.position == s2.position;
        public static bool operator !=(StructureUnit s1, StructureUnit s2) => !(s1 == s2);
    }
}

using OpenTK;

namespace Xerxes_Engine
{
    /// <summary>
    /// A struct that coresponds to a Global Game_Object. Used in SceneStructures that have non-unique objects such as water.
    /// </summary>
    public struct Render_Unit_R2
    {
        internal int id;
        internal int vaoIndex;
        private Vector3 position;
        private bool isInitialized;
        public bool IsInitialized { get => isInitialized; internal set => isInitialized = value; }

        /// <summary>
        /// Corresponds to a Global Game_Object position in a scene.
        /// </summary>
        public uint Id { get => (uint)id; set => id = (int)value; }
        /// <summary>
        /// A mutable value for relaying simple context based information quickly. Example: Water orientation.
        /// </summary>
        public uint VAO_Index { get => (uint)vaoIndex; set => vaoIndex = (int)value; }
        /// <summary>
        /// Isometric game position relevant for drawing.
        /// </summary>
        public Vector3 Position { get => position; set => position = value; }

        public float X { get => Position.X; set => position.X = value; }
        public float Y { get => Position.Y; set => position.Y = value; }
        public float Z { get => Position.Z; set => position.Z = value; }

        public Render_Unit_R2(uint id, uint vaoIndex, Vector3 position)
        {
            this.id = (int)id;
            this.vaoIndex = (int)vaoIndex;
            this.position = position;
            isInitialized = true;
        }

        public static bool operator ==(Render_Unit_R2 s1, Render_Unit_R2 s2) => s1.id == s2.id && s1.vaoIndex == s2.vaoIndex && s1.position == s2.position;
        public static bool operator !=(Render_Unit_R2 s1, Render_Unit_R2 s2) => !(s1 == s2);
    }
}

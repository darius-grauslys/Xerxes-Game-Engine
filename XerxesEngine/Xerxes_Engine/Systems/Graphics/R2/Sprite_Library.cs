using OpenTK;
using System.Collections.Generic;

namespace Xerxes_Engine.Systems.Graphics.R2
{
    public class Sprite_Library : Game_System
    {
        private readonly List<Sprite> _Sprite_Library__Sprites = new List<Sprite>();
        private readonly Dictionary<string, uint> _Sprite_Library__Name_To_Index__Table = new Dictionary<string, uint>();

        public Sprite_Library(Game gameRef) 
            : base(gameRef)
        {
        }

        protected override void Handle_Unload__Game_System()
        {
            base.Handle_Unload__Game_System();
        }

        public int Record__Sprite__Sprite_Library(Sprite s)
        {
            _Sprite_Library__Name_To_Index__Table.Add(s.Name, (uint)_Sprite_Library__Sprites.Count);
            _Sprite_Library__Sprites.Add(s);
            
            return _Sprite_Library__Sprites.Count - 1;
        }

        public Vertex_Array[] Get__Sprite_Arrays__Sprite_Library(string name) => _Sprite_Library__Sprites[(int)_Sprite_Library__Name_To_Index__Table[name]].VertexArrays;

        public bool Has__Sprite__Sprite_Library(string name) 
            => name != null && _Sprite_Library__Name_To_Index__Table.ContainsKey(name);

        public uint Get__Sprite_ID_From_Name__Sprite_Library(string name) 
            => Private_Get__Index_From_Name__Sprite_Library(name);
        public Sprite Get__Sprite_From_Name__Sprite_Library(string name) 
            => _Sprite_Library__Sprites[(int)Private_Get__Index_From_Name__Sprite_Library(name)];
        public Sprite Get__Sprite_From_ID__Sprite_Library(int id) 
            => _Sprite_Library__Sprites[id];
        
        public void Set__Vertex_Array_Object_Column__Sprite_Library(int id, uint vao) 
            => _Sprite_Library__Sprites[id].VAO_Index = vao;
        public void Set__Vertex_Array_Object_Index__Sprite_Library(int id, uint row) 
            => _Sprite_Library__Sprites[id].VAO_Row = row;

        public void Extract__Render_Unit__Sprite_Library(string name, out Render_Unit_R2 renderUnit) 
            => Extract__Render_Unit__Sprite_Library(Private_Get__Index_From_Name__Sprite_Library(name), out renderUnit);
        public void Extract__Render_Unit__Sprite_Library(uint id, out Render_Unit_R2 renderUnit) 
            => Private_Create__Render_Unit__Sprite_Library(Private_Validate__Id__Sprite_Library(id), out renderUnit);

        private void Private_Create__Render_Unit__Sprite_Library(uint id, out Render_Unit_R2 renderUnit)
            => renderUnit = new Render_Unit_R2(id, 0, Vector3.Zero); 

        private uint Private_Validate__Id__Sprite_Library(uint id)
        {
            if (id < _Sprite_Library__Sprites.Count)
                return id;

            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__System,
                Log.ERROR__SYSTEM__SPRITE_LIBRARY__SPRITE_ID_NOT_FOUND_1,
                this,
                id
            );

            return Private_Recover_With__Default_Sprite__Sprite_Library();
        }

        private uint Private_Get__Index_From_Name__Sprite_Library(string name)
        {
            if (name != null && _Sprite_Library__Name_To_Index__Table.ContainsKey(name))
                return _Sprite_Library__Name_To_Index__Table[name];

            return Private_Error_Name__Then_Get_Default__Sprite_Library(name);
        }

        private uint Private_Error_Name__Then_Get_Default__Sprite_Library(string name)
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__System,
                Log.ERROR__SYSTEM__SPRITE_LIBRARY__SPRITE_NOT_FOUND_1,
                this,
                name
            );

            return Private_Recover_With__Default_Sprite__Sprite_Library();
        }

        private uint Private_Recover_With__Default_Sprite__Sprite_Library()
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__SYSTEM__SPRITE_LIBRARY__RECOVERING_TO_DEFAULT,
                this
            );

            return 0;
        }
    }
}

using OpenTK;

namespace Xerxes_Engine.Systems.Graphics.R2
{
    /// <summary>
    /// Gives public exposure to the attached Game_Object's RenderUnit.
    /// </summary>
    public class Sprite_Render_Component : Game_Object_Component
    {
        private const string 
            Sprite_Render_Component__ACTION__SPRITE_SET = "Set__Sprite__Sprite_Render_Component",
            Sprite_Render_Component__ACTION__SPRITE_GET = "Get__Sprite__Sprite_Render_Component";


        private Sprite_Library Game__Sprite_Library__REFERENCE { get; set; } 

        public Sprite Get__Sprite__Sprite_Render_Component()
        {
            if(Protected_Check_If__Rooted__Game_Object_Component())
            {
                Internal_Log_Error__Used_When_Not_Associated_To_Root
                (
                    this,
                    Sprite_Render_Component__ACTION__SPRITE_GET
                );

                return null;
            }

            return Game__Sprite_Library__REFERENCE
                .Get__Sprite_From_ID__Sprite_Library
            (
                Game_Object_Component__Attached_Object__Protected
                ._game_Object__Render_Unit.id
            );
        }

        internal Sprite_Render_Component() 
            : base()
        {
        }

        protected override void Handle_Associate__To_Game__Xerxes_Engine_Object(Event_Argument_Associate_Game e)
        {
            Game__Sprite_Library__REFERENCE = 
                Protected_Get__Root__Xerxes_Engine_Object()?
                .Game__Sprite_Library;
        }
        
        public virtual bool Set__Sprite__Sprite_Render(string name)
        {
            if (Xerxes_Engine_Object__Is_Disabled__Internal)
            {
                Internal_Log_Warning__Used_When_Disabled
                (
                    this,
                    Sprite_Render_Component__ACTION__SPRITE_SET
                );

                return false;
            }

            Vector3 pos = Game_Object_Component__Attached_Object__Protected._game_Object__Render_Unit.Position;
            Game__Sprite_Library__REFERENCE.Extract__Render_Unit__Sprite_Library
            (
                name, 
                out Game_Object_Component__Attached_Object__Protected
                    ._game_Object__Render_Unit
            );
            Game_Object_Component__Attached_Object__Protected
                ._game_Object__Render_Unit.Position = pos;

            return true;
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public virtual bool Set__Sprite__Sprite_Render(Render_Unit_R2 ru, bool copyPosition = false)
        {
            if (Xerxes_Engine_Object__Is_Disabled__Internal)
            {
                Internal_Log_Warning__Used_When_Disabled
                (
                    this,
                    Sprite_Render_Component__ACTION__SPRITE_SET
                );

                return false;
            }

            if (copyPosition)
                ru.Position = 
                    Game_Object_Component__Attached_Object__Protected
                    .Game_Object__Render_Unit_Position__Internal;
            Game_Object_Component__Attached_Object__Protected
                ._game_Object__Render_Unit = ru;

            return true;
        }

        public virtual bool Set__Sprite__Sprite_Render(int spriteId, int vao_Index = 0)
        {
            if (Game_Object_Component__Is_Disabled__Protected)
            {
                Internal_Log_Warning__Used_When_Disabled
                (
                    this,
                    Sprite_Render_Component__ACTION__SPRITE_SET
                );

                return false;
            }

            Game_Object_Component__Attached_Object__Protected._game_Object__Render_Unit.id = spriteId;
            Game_Object_Component__Attached_Object__Protected._game_Object__Render_Unit.vaoIndex = vao_Index;
            Game_Object_Component__Attached_Object__Protected._game_Object__Render_Unit.IsInitialized = true;

            return true;
        }

    }
}

using OpenTK;

namespace Xerxes_Engine.Systems.Graphics.R2
{
    /// <summary>
    /// Gives public exposure to the attached Game_Object's RenderUnit.
    /// </summary>
    public class Sprite_Render_Component : Game_Object_Component
    {
        private const string 
            Sprite_Render_Component__ACTION__SPRITE_SET = "Set__Sprite__Sprite_Render";


        private Sprite_Library Game__Sprite_Library__REFERENCE { get; set; } 

        public Sprite Get__Sprite__Sprite_Render_Component()
        {
            if(!Component__Enabled)
            {
                Private_Log__Used_When_Disabled__Sprite_Component();

                return null;
            }

            return Game__Sprite_Library__REFERENCE
                .Get__Sprite_ID_From_Name__Sprite_Library
            (
                Component__Attached_Game_Object.renderUnit.id
            );
        }

        internal Sprite_Render_Component() 
            : base()
        {
        }

        protected override void Handle_Attach_To__Game_Object__Component()
        {
            base.Handle_Attach_To__Game_Object__Component();
            if(Component__Has_Been_Attached_Once)
                return;

            Game__Sprite_Library__REFERENCE = 
                Component__Attached_Game_Object
                .Game_Object__Scene_Layer
                .Scene_Layer__Game
                .Game__Sprite_Library;
        }
        
        public virtual bool Set__Sprite__Sprite_Render(string name)
        {
            if (!Component__Enabled)
            {
                Private_Log__Used_When_Disabled__Sprite_Component
                (
                    Sprite_Render_Component__ACTION__SPRITE_SET
                );

                return false;
            }

            Vector3 pos = Component__Attached_Game_Object.renderUnit.Position;
            Game__Sprite_Library__REFERENCE.Extract__Render_Unit__Sprite_Library(name, out Component__Attached_Game_Object.renderUnit);
            Component__Attached_Game_Object.renderUnit.Position = pos;

            return true;
        }

        /// <summary>
        /// might need to make this thread safe.
        /// </summary>
        /// <param name="s"></param>
        public virtual bool Set__Sprite__Sprite_Render(Render_Unit_R2 ru, bool copyPosition = false)
        {
            if (!Component__Enabled)
            {
                Private_Log__Used_When_Disabled__Sprite_Component
                (
                    Sprite_Render_Component__ACTION__SPRITE_SET
                );

                return false;
            }

            if (copyPosition)
                ru.Position = Component__Attached_Game_Object.Position;
            Component__Attached_Game_Object.renderUnit = ru;

            return true;
        }

        public virtual bool Set__Sprite__Sprite_Render(int spriteId, int vao_Index = 0)
        {
            if (!Component__Enabled)
            {
                Private_Log__Used_When_Disabled__Sprite_Component
                (
                    Sprite_Render_Component__ACTION__SPRITE_SET
                );

                return false;
            }

            Component__Attached_Game_Object.renderUnit.id = spriteId;
            Component__Attached_Game_Object.renderUnit.vaoIndex = vao_Index;
            Component__Attached_Game_Object.renderUnit.IsInitialized = true;

            return true;
        }

        private void Private_Log__Used_When_Disabled__Sprite_Component(string action)
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__COMPONENT__UTILIZED_WHILE_DISABLED_1,
                this,
                action
            );
        }
    }
}

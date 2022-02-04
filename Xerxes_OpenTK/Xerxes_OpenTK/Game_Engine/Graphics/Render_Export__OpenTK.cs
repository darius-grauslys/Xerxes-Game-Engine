
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Xerxes.Game_Engine.Graphics.Xerxes_OpenTK
{
    public sealed class Render_Export__OpenTK<Args> :
        Render_Export<Args>
        where Args : SA__Configure_Root
    {
        public const string UNIFORM__MODEL = "model";
        public const string UNIFORM__VIEW  = "view";
        public const string UNIFORM__PROJECTION = "projection";

        private Matrix4 _render_export__opentk__view;
        private Matrix4 _render_export__opentk__projection;

        private List<Shader_OpenTK> _Render_Export__SHADERS { get; }

        public Render_Export__OpenTK()
        {
            _Render_Export__SHADERS = new List<Shader_OpenTK>();
        }

        protected override void Handle_Begin__Render_Export(SA__Begin_Render e)
        {
            GL.ClearColor(Color.FromArgb(5, 5, 25));
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void Handle_Draw__Render_Export(SA__Draw e)
        {
            GL.BindTexture
            (
                TextureTarget.Texture2D, 
                e
                .Draw__Render_Target
                .Render_Target__VERTEX_OBJECT
                .Vertex_Object__Texture_2D
                .Texture_2D__ID
            );
            GL.BindVertexArray
            (
                e
                .Draw__Render_Target
                .Render_Target__VERTEX_OBJECT
                .Vertex_Object__Vertex_Object_ID
            );

            GL.UseProgram(e.Draw__Shader_ID);

            int model = GL.GetUniformLocation(e.Draw__Shader_ID, UNIFORM__MODEL);
            int view = GL.GetUniformLocation(e.Draw__Shader_ID, UNIFORM__VIEW);
            int projection = GL.GetUniformLocation(e.Draw__Shader_ID, UNIFORM__PROJECTION);

            Matrix4 translation =
                Matrix4.CreateTranslation
                (
                    e.Draw__Render_Target.Render_Target__WORLD_X,
                    e.Draw__Render_Target.Render_Target__WORLD_Y,
                    e.Draw__Render_Target.Render_Target__WORLD_Z
                );

            GL.UniformMatrix4(model, true, ref translation);
            GL.UniformMatrix4(view, true, ref _render_export__opentk__view);
            GL.UniformMatrix4(projection, true, ref _render_export__opentk__projection);

            GL.DrawArrays
            (
                PrimitiveType.Triangles, 
                0, 
                e
                .Draw__Render_Target
                .Render_Target__VERTEX_OBJECT
                .Vertex_Object__Vertex_Count
            );
        }

        protected override void Handle_End__Render_Export(SA__End_Render e)
        {

        }

        protected override Shader.Shader_ID Handle_Load__Shader__Render_Export
        (SA__Load_Shader e)
        {
            Shader_OpenTK shader = 
                new Shader_OpenTK(e);

            _Render_Export__SHADERS
                .Add(shader);

            return shader.Shader__ID;
        }
    }
}

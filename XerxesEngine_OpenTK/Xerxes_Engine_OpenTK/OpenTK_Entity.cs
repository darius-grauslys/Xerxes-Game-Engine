
using Xerxes.Game_Engine;
using Xerxes.Game_Engine.Physics;

namespace Xerxes.Xerxes_OpenTK
{
    public class OpenTK_Entity :
        Entity,
        IFeature__Render_Target
    {
        public Vertex_Object Vertex_Object { get; set; }

        public OpenTK_Entity
        (
            float x = 0,
            float y = 0,
            float z = 0,
            float aabb_width = 1, 
            float aabb_height = 1,
            float hitbox_padding = 0.01f,
            float hitbox_margin = 0.1f,
            
            bool is_kinematic = false,

            Vertex_Object? vertex_object = null,

            bool is_enabled = true
        )
        :
        base
        (
            x,
            y,
            z,

            aabb_width,
            aabb_height,
            hitbox_padding,
            hitbox_margin,

            is_kinematic,
            is_enabled
        )
        {
            Vertex_Object =
                vertex_object
                ??
                new Vertex_Object();
        }

        public static TEntity Get__Generic_Entity<TEntity>
        (
            float width, float height, 
            Vertex_Object vo, 
            float x = 0, float y = 0, float z = 0,
            float hitbox_padding = 0.1f, float hitbox_margin = 0.1f,
            bool is_kinematic = false
        )
        where TEntity : OpenTK_Entity, new()
        {
            TEntity entity =
                new TEntity();

            entity.X = x;
            entity.Y = y;
            entity.Z = z;

            entity.AABB__Bx = width;
            entity.AABB__By = height;
            entity.Vertex_Object = vo;

            entity.Hitbox_2D__Kinematic = is_kinematic;

            PHYSICS_2D__HITBOX
                .Apply__Hitbox(entity, hitbox_padding, hitbox_margin);

            return entity;
        }

    }
}

using System.Collections.Generic;

namespace Xerxes_Engine
{
    public class Sprite_Batch
    {
        private List<Vertex> _Sprite_Batch__VERTEX_BATCH { get; }

        public Sprite_Batch
        (
            Sprite_Sheet spriteSheet
        )
        {
            _Sprite_Batch__VERTEX_BATCH = new List<Vertex>();
        }

        public void Modify__Sprite_Batch
        (
            Vertex[] vertices, 
            int index, 
            bool logAtypicalIndexing = true
        )
        {
            bool shouldLog = 
                Private_Check_For__Atypical_Indexing__Sprite_Batch
                (
                    index,
                    logAtypicalIndexing
                );
            if (shouldLog)
                Private_Check_For__Atypical_Indexing__Sprite_Batch(this, index);

            for(int i=0;i<vertices.Length;i++)
            {
                
            }
        }

        private bool Private_Check_For__Atypical_Indexing__Sprite_Batch
        (
            int index,
            bool logAtypicalIndexing
        )
        {
            bool shouldLog =
                logAtypicalIndexing
                &&
                Tools.Math_Helper.Divides(index, Vertex_Object.VERTEX_OBJECT__BASE_VERTEX_COUNT);

            return shouldLog;
        }

        public Vertex_Object Create__Vertex_Object__Sprite_Batch()
            => new Vertex_Object(_Sprite_Batch__VERTEX_BATCH.ToArray());


        private static void Private_Log_Warning__Atypical_Indexing
        (
            Sprite_Batch spriteBatch,
            int offendingIndex
        )
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__SPRITE_BATCH__ATYPICAL_INDEXING_2,
                spriteBatch,
                offendingIndex,
                Vertex_Object.VERTEX_OBJECT__BASE_VERTEX_COUNT
            );
        }

        private static void Private_Log_Warning__Modification_Out_Of_Bounds
        (
            Sprite_Batch spriteBatch,
            int initalIndex,
            int length,
            int vertexCount
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Rendering_Setup,
                Log.ERROR__SPRITE_BATCH__MODIFICATION_OUT_OF_BOUNDS_3,
                spriteBatch,
                initalIndex,
                length,
                vertexCount
            );
        }
    }
}

using isometricgame.GameEngine;
using isometricgame.GameEngine.Exceptions.WorldSpace;
using isometricgame.GameEngine.WorldSpace;
using isometricgame.GameEngine.Services;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isometricgame.GameEngine.Components.Rendering;
using isometricgame.GameEngine.Scenes;
using isometricgame.GameEngine.WorldSpace.Generators;
using isometricgame.GameEngine.Rendering;

namespace isometricgame.GameEngine.WorldSpace
{
    public class WorldScene : Scene
    {
        private ChunkDirectory chunkDirectory;
        private Camera clientCamera;

        //temp, remove later
        private SpriteLibrary spriteLibrary;

        private List<GameObject> unsequencedGameObjects = new List<GameObject>();

        /// <summary>
        /// To be replaced.
        /// </summary>
        private GameObject focusObject;

        public Camera ClientCamera { get => clientCamera; private set => clientCamera = value; }
        public ChunkDirectory ChunkDirectory { get => chunkDirectory; set => chunkDirectory = value; }

        public WorldScene(Game game)
            : base(game)
        {
            this.ChunkDirectory = new ChunkDirectory(4, new WorldGenerator(779876));
            this.ClientCamera = new Camera(this);

            spriteLibrary = game.GetService<SpriteLibrary>();
        }

        /// <summary>
        /// REMOVE THIS LATER.
        /// </summary>
        /// <param name="obj"></param>
        public void SetFocus(GameObject obj)
        {
            focusObject = obj;
        }

        //Not responsible for fps drop.
        public override void UpdateFrame(FrameEventArgs e)
        {
            Vector3 pos;
            if (focusObject != null)
                pos = new Vector3(focusObject.GetX(), focusObject.GetY(), focusObject.GetZ());
            else
                pos = new Vector3(0, 0, 0);
            ClientCamera.Pan_Linear((float)e.Time, pos);

            SceneMatrix = ClientCamera.GetView();

            base.UpdateFrame(e);
        }

        //Not responsible for fps drop.
        public override void RenderFrame(RenderService renderService, FrameEventArgs e)
        {
            ChunkDirectory.ChunkCleanup(ClientCamera.Position.Xy);

            //OK so in terms of procedural ground rendering there is only the X and Y axis. The ground however can be offset by a Z value.
            //HOWEVER this does not mean there are more than one tile on a give Z axis. So we only itterate on X and Y for tile
            //rendering. Now game objects on the other hand CAN be on any Z level. So to deal with this we still use Z for layering
            //and checking collisions.

            //for each x value
            //for each y value
            //CS.DelimiateTile(pos(x,y)) -> render
            //Check for obj ?-> render


            //TODO:
            /*
             * 
             * I am going to work on serialization / deserialization.
             * 
             * Worlds will be folders, with a header file and chunkspace files.
             * Each chunkspace file will have... 16x16 chunks? The header file will
             * inform the game which chunkspaces have allocate chunks.
             * 
             * 
             */


            float minX, maxX, minY, maxY;
            minX = ChunkDirectory.MinimalX_ByTileLocation;
            maxX = ChunkDirectory.MaximalX_ByTileLocation;
            minY = ChunkDirectory.MinimalY_ByTileLocation;
            maxY = ChunkDirectory.MaximalY_ByTileLocation; //I get the lowest X,Y coordinate out of the chunks, and the highest, then itterate between them.

            Sprite tile = spriteLibrary.GetSpriteSet<TileSpriteSet>(0).GetSprite(0);
            
            //This wrecks my fps! This loop!
            for (float y = minY; y < maxY; y++)
            {
                for (float x = maxX; x >= minX; x--)
                {
                    Tile t;

                    //Not responsible for FPS drop. (tested by commenting out)
                    t = ChunkDirectory.DeliminateTile(new Vector2(x, y));

                    float tx = Chunk.CartesianToIsometric_X(x, y), ty = Chunk.CartesianToIsometric_Y(x, y, t.Z);

                    //Sprite acquision from library not responsible for fps drop. (same test)
                    renderService.DrawSprite(spriteLibrary.GetSpriteSet<TileSpriteSet>(t.Data).GetTile(t.Orientation), tx, ty);
                }
            }

            //GameObject drawing not responsible for fps drop.
            SpriteComponent sa;
            foreach (GameObject obj in GameObjects)
            {
                if ((sa = obj.GetAttribute<SpriteComponent>()) != null)
                {
                    float z = ChunkDirectory.DeliminateTile(obj.Position.Xy).Z;
                    obj.SetZ(z);

                    Sprite[] ss = sa.GetSprites();
                    foreach (Sprite s in ss)
                    {
                        float x = obj.GetX() + s.OffsetX, y = obj.GetY() + s.OffsetY;
                        float tx = Chunk.CartesianToIsometric_X(x,y), ty = Chunk.CartesianToIsometric_Y(x,y,obj.GetZ());
                        renderService.DrawSprite(s, tx, ty);
                    }
                }
            }
        }

        /*
        private void DrawTexture(Sprite s, float x, float y, float z, Matrix4 cameraView)
        {
            Matrix4 world;

            float tX, tY;

            tX = Chunk.CartesianToIsometric_X(x,y);
            tY = Chunk.CartesianToIsometric_Y(x,y,z);

            GL.BindTexture(TextureTarget.Texture2D, s.Texture.ID);
            world = Matrix4.CreateTranslation(tX, tY, 0);

            world *= Matrix4.Invert(cameraView);

            GL.LoadMatrix(ref world);

            GL.BindBuffer(BufferTarget.ArrayBuffer, s.Texture.ID);
            GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, (IntPtr)0);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector2.SizeInBytes));
            GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector2.SizeInBytes * 2));

            GL.DrawArrays(PrimitiveType.Quads, 0, s.Vertices.Length);
        }
        */

        //private void DrawGameObject<T>(T sa, Matrix4 cameraView) where T : SpriteAttribute
        //{
        //    Matrix4 world;

        //    GL.BindTexture(TextureTarget.Texture2D, sa.Sprite.Texture.ID);
        //    float x = sa.ParentObject.GetX();
        //    float y = sa.ParentObject.GetY();
        //    world = Matrix4.CreateTranslation(sa.ParentObject.GetX(), sa.ParentObject.GetY(), 0);

        //    world *= Matrix4.Invert(cameraView);

        //    GL.LoadMatrix(ref world);

        //    GL.BindBuffer(BufferTarget.ArrayBuffer, sa.Sprite.Texture.ID);
        //    GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, (IntPtr)0);
        //    GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector2.SizeInBytes));
        //    GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector2.SizeInBytes * 2));

        //    GL.DrawArrays(PrimitiveType.Quads, 0, sa.Sprite.Vertices.Length);
        //}
    }
}

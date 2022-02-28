
using System;
using OpenTK;
using Xerxes.Game_Engine.Input;
using Xerxes.Game_Engine;

namespace Xerxes.Xerxes_OpenTK.Engine_Objects
{
    public class Camera_3D_Controller :
        Xerxes_Object<Camera_3D_Controller>
    {
        protected Vector3 Camera_3D_Controller__Position { get; private set; }
        protected Vector3 camera_3d_controller__velocity = new Vector3(); 

        private Vector3 camera_3d_controller__front = new Vector3();

        protected bool Camera_3D_Controller__Mouse__First_Move { get; private set; }
        protected Vector2 Camera_3D_Controller__Mouse__Last_Position { get; private set; }

        protected float Camera_3D_Controller__Yaw { get; set; }
        protected float Camera_3D_Controller__Pitch { get; set; }
        protected float Camera_3D_Controller__Roll { get; set; }

        protected float Camera_3D_Controller__Sensitivity { get; set; }

        public Camera_3D_Controller()
        {
            Camera_3D_Controller__Mouse__First_Move = true;
            Camera_3D_Controller__Sensitivity = 1;

            Declare__Streams()
                .Upstream.Extending<SA__Field_Set<Camera_3D, Camera_3D.Camera_Target>>()
                .Upstream.Extending<SA__Field_Set<Camera_3D, Camera_3D.Camera_Position>>()
                .Downstream.Receiving<SA__Input_Key_Down>
                (Handle_Input__Key_Down__Camera_3D_Controller)
                .Downstream.Receiving<SA__Input_Mouse_Move>
                (Handle_Input__Mouse_Move__Camera_3D_Controller)
                .Downstream.Receiving<SA__Update>
                (Handle_Update__Camera_3D_Controller);
        }

        protected virtual void Handle_Input__Key_Down__Camera_3D_Controller
        (SA__Input_Key_Down e)
        {
            switch(e.Input_Key__Event_Key)
            {
                case Key.W:
                    camera_3d_controller__velocity.Z = 0.1f;
                    break;
                case Key.A:
                    camera_3d_controller__velocity.X = -0.1f;
                    break;
                case Key.S:
                    camera_3d_controller__velocity.Z = -0.1f;
                    break;
                case Key.D:
                    camera_3d_controller__velocity.X = 0.1f;
                    break;
            }
        }

        protected virtual void Handle_Input__Mouse_Move__Camera_3D_Controller
        (SA__Input_Mouse_Move e)
        {
            float mouse_x = e.Input_Mouse_Move__Mouse_X;
            float mouse_y = e.Input_Mouse_Move__Mouse_Y;

            if (Camera_3D_Controller__Mouse__First_Move)
            {
                Camera_3D_Controller__Mouse__Last_Position =
                    new Vector2(mouse_x, mouse_y);
                Camera_3D_Controller__Mouse__First_Move = false;
            }
            else
            {
                float deltaX = 
                    mouse_x 
                    - 
                    Camera_3D_Controller__Mouse__Last_Position.X;
                float deltaY = 
                    mouse_y 
                    - 
                    Camera_3D_Controller__Mouse__Last_Position.Y;

                Camera_3D_Controller__Mouse__Last_Position = 
                    new Vector2(mouse_x, mouse_y);

                Camera_3D_Controller__Yaw += deltaX * Camera_3D_Controller__Sensitivity;
                if(Camera_3D_Controller__Pitch > 89.0f)
                {
                    Camera_3D_Controller__Pitch = 89.0f;
                }
                else if(Camera_3D_Controller__Pitch < -89.0f)
                {
                    Camera_3D_Controller__Pitch = -89.0f;
                }
                else
                {
                    Camera_3D_Controller__Pitch -= deltaX * Camera_3D_Controller__Sensitivity; 
                }
            }

            camera_3d_controller__front.X = 
                (float)Math.Cos
                (
                    MathHelper.DegreesToRadians
                    (
                        Camera_3D_Controller__Pitch
                    )
                ) 
                * 
                (float)Math.Cos
                (
                    MathHelper.DegreesToRadians
                    (
                        Camera_3D_Controller__Yaw
                    )
                );

            camera_3d_controller__front.Y = 
                (float)Math.Sin
                (
                    MathHelper.DegreesToRadians
                    (
                        Camera_3D_Controller__Pitch
                    )
                );
            camera_3d_controller__front.Z = 
                (float)Math.Cos
                (
                    MathHelper.DegreesToRadians
                    (
                        Camera_3D_Controller__Pitch
                    )
                ) 
                * 
                (float)Math.Sin
                (
                    MathHelper.DegreesToRadians
                    (
                        Camera_3D_Controller__Yaw
                    )
                );
            camera_3d_controller__front = 
                Vector3.Normalize
                (
                    camera_3d_controller__front
                );

        }

        protected virtual void Handle_Update__Camera_3D_Controller
        (SA__Update e)
        {
            Camera_3D_Controller__Position += camera_3d_controller__velocity;

            Invoke__Ascending
            (
                new SA__Field_Set
                <Camera_3D, Camera_3D.Camera_Target>
                (
                    camera_3d_controller__front
                )
            );

            Invoke__Ascending
            (
                new SA__Field_Set
                <Camera_3D, Camera_3D.Camera_Position>
                (
                    Camera_3D_Controller__Position
                )
            );
        }
    }
}

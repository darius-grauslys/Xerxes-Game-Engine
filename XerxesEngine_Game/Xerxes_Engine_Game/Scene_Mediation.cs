
using System;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Game_Engine 
{
    public class Scene_Mediation<TScene> :
    Scene_Mediation_Base
    where TScene: Scene
    {
        public SA__Mediate<TScene, SA__Update> Scene_Mediation__UPDATE { get; }
        public SA__Mediate<TScene, SA__Render_Begin> Scene_Mediation__RENDER_BEGIN { get; }
        public SA__Mediate<TScene, SA__Render> Scene_Mediation__RENDER { get; }
        public SA__Mediate<TScene, SA__Input_Key_Down> Scene_Mediation__KEY_DOWN { get; }
        public SA__Mediate<TScene, SA__Input_Key_Up>   Scene_Mediation__KEY_UP { get; }
        public SA__Mediate<TScene, SA__Input_Mouse_Button> Scene_Mediation__MOUSE_BUTTON { get; }
        public SA__Mediate<TScene, SA__Input_Mouse_Move>   Scene_Mediation__MOUSE_MOVE { get; }

        public override void Set__Update(SA__Update e)
            => Scene_Mediation__UPDATE.Mediate__Streamline_Argument = e;
        public override void Set__Render_Begin(SA__Render_Begin e)
            => Scene_Mediation__RENDER_BEGIN.Mediate__Streamline_Argument = e;
        public override void Set__Render(SA__Render e)
            => Scene_Mediation__RENDER.Mediate__Streamline_Argument = e;
        public override void Set__Key_Down(SA__Input_Key_Down e)
            => Scene_Mediation__KEY_DOWN.Mediate__Streamline_Argument = e;
        public override void Set__Key_Up(SA__Input_Key_Up e)
            => Scene_Mediation__KEY_UP.Mediate__Streamline_Argument = e;
        public override void Set__Mouse_Move(SA__Input_Mouse_Move e)
            => Scene_Mediation__MOUSE_MOVE.Mediate__Streamline_Argument = e;
        public override void Set__Mouse_Button(SA__Input_Mouse_Button e)
            => Scene_Mediation__MOUSE_BUTTON.Mediate__Streamline_Argument = e;

        private Action<Scene_Mediation<TScene>> _Scene_Mediation__MEDIATION_CALLBACK { get; }

        public Scene_Mediation(Action<Scene_Mediation<TScene>> mediation_callback)
        {
            _Scene_Mediation__MEDIATION_CALLBACK = mediation_callback;

            Scene_Mediation__UPDATE = new SA__Mediate<TScene, SA__Update>();
            Scene_Mediation__RENDER_BEGIN = new SA__Mediate<TScene, SA__Render_Begin>();
            Scene_Mediation__RENDER = new SA__Mediate<TScene, SA__Render>();
    
            Scene_Mediation__KEY_DOWN = new SA__Mediate<TScene, SA__Input_Key_Down>();
            Scene_Mediation__KEY_UP   = new SA__Mediate<TScene, SA__Input_Key_Up>();

            Scene_Mediation__MOUSE_BUTTON = new SA__Mediate<TScene, SA__Input_Mouse_Button>();
            Scene_Mediation__MOUSE_MOVE   = new SA__Mediate<TScene, SA__Input_Mouse_Move>();
        }

        public override void Mediate()
        {
            _Scene_Mediation__MEDIATION_CALLBACK?
                .Invoke(this);

            Scene_Mediation__UPDATE
                .Mediate__Streamline_Argument = null;
            Scene_Mediation__RENDER_BEGIN
                .Mediate__Streamline_Argument = null;
            Scene_Mediation__RENDER
                .Mediate__Streamline_Argument = null;

            Scene_Mediation__KEY_DOWN
                .Mediate__Streamline_Argument = null;
            Scene_Mediation__KEY_UP
                .Mediate__Streamline_Argument = null;
            
            Scene_Mediation__MOUSE_MOVE
                .Mediate__Streamline_Argument = null;
            Scene_Mediation__MOUSE_BUTTON
                .Mediate__Streamline_Argument = null;
        }
    }

    public abstract class Scene_Mediation_Base
    {
        internal Scene_Mediation_Base(){}

        public abstract void Set__Update(SA__Update e);
        public abstract void Set__Render_Begin(SA__Render_Begin e);
        public abstract void Set__Render(SA__Render e);
        
        public abstract void Set__Key_Down(SA__Input_Key_Down e);
        public abstract void Set__Key_Up(SA__Input_Key_Up e);

        public abstract void Set__Mouse_Move(SA__Input_Mouse_Move e);
        public abstract void Set__Mouse_Button(SA__Input_Mouse_Button e);

        public abstract void Mediate();
    }
}


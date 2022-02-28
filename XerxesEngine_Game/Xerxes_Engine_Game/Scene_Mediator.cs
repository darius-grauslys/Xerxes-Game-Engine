
using System;
using System.Collections.Generic;
using Xerxes.Game_Engine.Input;

namespace Xerxes.Game_Engine 
{
    public class Scene_Mediator :
    Xerxes_Object<Scene_Mediator>
    {
        private Dictionary<Type, Scene_Mediation_Base> _Scene_Mediator__MEDIATION_TABLE { get; }
        private Scene_Mediation_Base _Scene_Mediator__Active_Target { get; set; }

        public Scene_Mediator()
        {
            _Scene_Mediator__MEDIATION_TABLE =
                new Dictionary<Type, Scene_Mediation_Base>();

            Declare__Streams()
                .Upstream.Receiving<SA__Set_Scene>(Handle_Set__Scene__Scene_Manager)
                .Downstream.Receiving<SA__Set_Scene>(Handle_Set__Scene__Scene_Manager)
                .Downstream.Receiving<SA__Update>(Private_Handle__Update__Scene_Manager)
                .Downstream.Receiving<SA__Render_Begin>(Private_Handle__Render_Begin__Scene_Manager)
                .Downstream.Receiving<SA__Render>(Private_Handle__Render__Scene_Manager)
                .Downstream.Receiving<SA__Input_Key_Down>(Private_Handle__Key_Down__Scene_Manager)
                .Downstream.Receiving<SA__Input_Key_Up>(Private_Handle__Key_Up__Scene_Manager)
                .Downstream.Receiving<SA__Input_Mouse_Move>(Private_Handle__Mouse_Move__Scene_Manager)
                .Downstream.Receiving<SA__Input_Mouse_Button>(Private_Handle__Mouse_Button__Scene_Manager);
        }

        protected virtual void Handle_Set__Scene__Scene_Manager(SA__Set_Scene e)
        {
            Set__Scene__Scene_Manager(e.Set_Scene__Scene);
        }

        protected void Set__Scene__Scene_Manager(Type scene)
        {
            if (_Scene_Mediator__MEDIATION_TABLE.ContainsKey(scene))
                _Scene_Mediator__Active_Target =
                    _Scene_Mediator__MEDIATION_TABLE[scene];
        }

        private void Private_Handle__Update__Scene_Manager(SA__Update e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Update(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        private void Private_Handle__Render_Begin__Scene_Manager(SA__Render_Begin e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Render_Begin(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        private void Private_Handle__Render__Scene_Manager(SA__Render e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Render(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        private void Private_Handle__Key_Down__Scene_Manager(SA__Input_Key_Down e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Key_Down(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        private void Private_Handle__Key_Up__Scene_Manager(SA__Input_Key_Up e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Key_Up(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        private void Private_Handle__Mouse_Move__Scene_Manager(SA__Input_Mouse_Move e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Mouse_Move(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        private void Private_Handle__Mouse_Button__Scene_Manager(SA__Input_Mouse_Button e)
        {
            _Scene_Mediator__Active_Target?
                .Set__Mouse_Button(e);

            _Scene_Mediator__Active_Target?
                .Mediate();
        }

        protected void Associate__Scene<TScene>()
        where TScene : Scene, new()
        {
            Declare__Hierarchy()
                .Associate__And_Focus
                <
                    Xerxes_Mediation_Target
                    <
                        TScene,
                        SA__Update,
                        SA__Render_Begin,
                        SA__Render,
                        SA__Input_Key_Down,
                        SA__Input_Key_Up,
                        SA__Input_Mouse_Move,
                        SA__Input_Mouse_Button
                    >
                >()
                    .Associate<TScene>();

            Declare__Streams()
                .Downstream.Extending<SA__Mediate<TScene, SA__Update>>()
                .Downstream.Extending<SA__Mediate<TScene, SA__Render_Begin>>()
                .Downstream.Extending<SA__Mediate<TScene, SA__Render>>()
                .Downstream.Extending<SA__Mediate<TScene, SA__Input_Key_Down>>()
                .Downstream.Extending<SA__Mediate<TScene, SA__Input_Key_Up>>()
                .Downstream.Extending<SA__Mediate<TScene, SA__Input_Mouse_Move>>()
                .Downstream.Extending<SA__Mediate<TScene, SA__Input_Mouse_Button>>();

            Scene_Mediation<TScene> mediation =
                new Scene_Mediation<TScene>
                (
                    Private_Handle__Mediate__Scene_Mediator
                );

            _Scene_Mediator__MEDIATION_TABLE
                .Add(typeof(TScene), mediation);
        }

        private void Private_Handle__Mediate__Scene_Mediator<TScene>(Scene_Mediation<TScene> mediation)
        where TScene : Scene
        {
            if (mediation.Scene_Mediation__UPDATE != null)
                Invoke__Descending(mediation.Scene_Mediation__UPDATE);
            if (mediation.Scene_Mediation__RENDER_BEGIN != null)
                Invoke__Descending(mediation.Scene_Mediation__RENDER_BEGIN);
            if (mediation.Scene_Mediation__RENDER != null)
                Invoke__Descending(mediation.Scene_Mediation__RENDER);

            if (mediation.Scene_Mediation__KEY_DOWN != null)
                Invoke__Descending(mediation.Scene_Mediation__KEY_DOWN);
            if (mediation.Scene_Mediation__KEY_UP != null)
                Invoke__Descending(mediation.Scene_Mediation__KEY_UP);

            if (mediation.Scene_Mediation__MOUSE_MOVE != null)
                Invoke__Descending(mediation.Scene_Mediation__MOUSE_MOVE);
            if (mediation.Scene_Mediation__MOUSE_BUTTON != null)
                Invoke__Descending(mediation.Scene_Mediation__MOUSE_BUTTON);
        }
    }
}

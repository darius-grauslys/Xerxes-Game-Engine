using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Xerxes_Engine.Engine_Objects
{
    public class Game_Object : Xerxes_Engine_Object
    {
        protected Scene_Layer Protected_Get__Attached_Layer__Game_Object()
            => Internal_Get__Parent_As__Xerxes_Engine_Object<Scene_Layer>();

        internal Render_Unit_R2 _game_Object__Render_Unit;

        private List<Game_Object_Component> _Game_Object__COMPONENTS { get; }

        internal Vector3 Game_Object__Render_Unit_Position__Internal
        {
            get => _game_Object__Render_Unit.Position;
            set => _game_Object__Render_Unit.Position = value;
        }

        public Game_Object
            (
            Vector3 position, 
            params Game_Object_Component[] components
            )
        :
            base
            (
                Xerxes_Engine_Object_Association_Type.GAME__OBJECT
            )
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Frame_Update>();
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Frame_Render>();

            Game_Object__Render_Unit_Position__Internal = position;
    
            _Game_Object__COMPONENTS = new List<Game_Object_Component>();

            if(components == null)
                return;

            for(int i=0;i<components.Length;i++)
                Protected_Associate__Component_As_Descendant__Game_Object(components[i]);
        }

        internal override bool Internal_Handle_Associate__As_Descendant__Xerxes_Engine_Object
        (
            Xerxes_Engine_Object ancestorAssociation
        )
        {
            if (ancestorAssociation is Scene_Layer)
                return base.Internal_Handle_Associate__As_Descendant__Xerxes_Engine_Object(ancestorAssociation);

            Private_Log_Error__Ancestor_Is_Not_A_Scene_Layer
            (
                this,
                ancestorAssociation
            );

            return false;
        }

        /// <summary>
        /// Returns true if the assocation was successful,
        /// otherwise returns false.
        /// </summary>
        protected bool Protected_Associate__Component_As_Descendant__Game_Object
        (
            Game_Object_Component component
        )
        {
            bool failure =
                Private_Check_If__Type_Of_Component_Already_Present__Game_Object(component);

            if (failure)
            {
                Private_Log_Error__Fail_To_Associate
                (
                    this,
                    component,
                    "Another component of equivalent type is already associated"
                );
                return false;
            }

            failure = 
                !Xerxes_Engine_Object.Internal_Associate__Objects
                (
                    component,
                    this
                );
            
            if (failure)
            {
                Private_Log_Error__Fail_To_Associate
                (
                    this,
                    component,
                    "General Failure"
                );
                return false;
            }

            return true;
        }

        private bool Private_Check_If__Type_Of_Component_Already_Present__Game_Object
        (
            Game_Object_Component component
        )
        {
            return _Game_Object__COMPONENTS.Exists
                ((c) => c.GetType() == component.GetType());
        }

        protected T Protected_Get__Component__Game_Object<T>() where T : Game_Object_Component
        {
            T[] components = _Game_Object__COMPONENTS.OfType<T>().ToArray();
            return (components.Length > 0) ? components[0] : null;
        }

        protected virtual Game_Object Protected_Clone__Game_Object()
        {
            Game_Object_Component[] cloned_Components = 
                new Game_Object_Component[_Game_Object__COMPONENTS.Count];

            for(int i=0;i<_Game_Object__COMPONENTS.Count;i++)
            {
                cloned_Components[i] =
                    _Game_Object__COMPONENTS[i].Clone__Game_Object_Component();
            }

            Game_Object newObj = new Game_Object(Game_Object__Render_Unit_Position__Internal, cloned_Components);

            return newObj;
        }

        private static void Private_Log_Error__Fail_To_Associate
        (
            Game_Object obj, 
            Game_Object_Component component,
            string context
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__GAME_OBJECT__FAILED_TO_ASSOCIATE_COMPONENT_2C,
                obj,
                component,
                context
            );
        }

        private static void Private_Log_Error__Ancestor_Is_Not_A_Scene_Layer
        (
            Game_Object obj,
            Xerxes_Engine_Object ancestorAssociation
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__GAME_OBJECT__ASSOCIATED_ANCESTOR_IS_INVALID_1,
                obj,
                ancestorAssociation
            );
        }
    }
}

namespace Xerxes_Engine.Engine_Objects
{
    /// <summary>
    /// Attributes are added to Game_Objects to give additional functionalities. Such as hitboxes, physics, and more.
    /// </summary>
    public class Game_Object_Component : Xerxes_Engine_Object
    {
        protected Game_Object Protected_Get__Attached_Object__Game_Object_Component()
            => Internal_Get__Parent_As__Xerxes_Engine_Object<Game_Object>();
        protected bool Protected_Check_If__Associated__Game_Object_Component()
            => Xerxes_Engine_Object__Parent__Internal != null;
        protected bool Protected_Check_If__Rooted__Game_Object_Component()
            => Internal_Check_If__Rooted__Xerxes_Engine_Object();

        protected bool Game_Object_Component__Is_Disabled__Protected
        {
            get => Xerxes_Engine_Object__Is_Disabled__Internal;
            set => Xerxes_Engine_Object__Is_Disabled__Internal = value;
        }

        protected void Game_Object_Component__Seal__Protected()
            => Internal_Seal__Xerxes_Engine_Object();

        public Game_Object_Component()
        : base
        (
            Xerxes_Engine_Object_Association_Type.GAME__COMPONENT
        )
        {
            Xerxes_Engine_Object__Is_Disabled__Internal = true;
        }

        internal override void Internal_Associate__To_Game__Xerxes_Engine_Object(Event_Argument_Associate_Game e)
        {
            Xerxes_Engine_Object__Is_Disabled__Internal = false;

            base.Internal_Associate__To_Game__Xerxes_Engine_Object(e);
        }

        internal override bool Internal_Handle_Associate__As_Ancestor__Xerxes_Engine_Object
        (
            Xerxes_Engine_Object association
        )
        {
            if (association is Game_Object)
            {
                return base.Internal_Handle_Associate__As_Ancestor__Xerxes_Engine_Object(association);
            }

            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__GAME_OBJECT_COMPONENT__FAILED_TO_ASSOCIATE_1,
                this,
                association
            );

            return false;
        }

        public virtual Game_Object_Component Clone__Game_Object_Component()
        {
            Game_Object_Component newComp = new Game_Object_Component();

            return newComp;
        }

        internal static void Internal_Log_Warning__Used_When_Disabled
        (
            Game_Object_Component component, 
            string contextualMessage
        )
        {
            Log.Internal_Write__Warning__Log
            (
                Log.WARNING__GAME_OBJECT_COMPONENT__UTILIZED_WHILE_DISABLED_1C,
                component,
                contextualMessage
            );
        }

        internal static void Internal_Log_Error__Used_When_Disabled
        (
            Game_Object_Component component,
            string contextualMessage
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__GAME_OBJECT_COMPONENT__UTILIZED_WHILE_DISABLED_1C,
                component,
                contextualMessage
            );
        }

        internal static void Internal_Log_Error__Used_When_Not_Associated_To_Root
        (
            Game_Object_Component component,
            string contextualMessage
        )
        {
            Log.Internal_Write__Log
            (
                Log_Message_Type.Error__Engine_Object,
                Log.ERROR__GAME_OBJECT_COMPONENT__NOT_ASSOCIATED_TO_ROOT_1C,
                component,
                contextualMessage
            );
        }
    }
}

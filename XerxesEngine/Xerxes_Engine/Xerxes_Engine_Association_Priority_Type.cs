namespace Xerxes_Engine
{
    internal enum Xerxes_Engine_Object_Association_Type
    {
        // Container objects.
        GAME = 0,

        GAME__SCENE = 2,

        GAME__SCENE_LAYER = 3,

        // Leveled-Flow Meta objects &? Container objects.
        // UI_Game_Object: Container
        // WS_Game_Object: Meta object
        GAME__OBJECT = 4,

        // Up-Flow Meta objects.
        GAME__COMPONENT = 5,

        GAME__EVENT_SCHEDULER = 6,

        GAME__STATE_MACHINE = 7
    }
}

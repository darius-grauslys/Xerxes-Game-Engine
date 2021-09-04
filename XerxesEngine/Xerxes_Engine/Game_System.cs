namespace Xerxes_Engine
{
    public class Game_System
    {
        protected Game Game { get; set; }
        public bool Accessable { get; private set; }

        public Game_System(Game game, bool accessable = true)
        {
            Game = game;
            Accessable = accessable;
        }

        internal void Internal_Load__Game_System()
            => Handle_Load__Game_System();
        protected virtual void Handle_Load__Game_System()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__SYSTEM__LOAD, this);
        }

        internal void Internal_Unload__Game_System()
            => Handle_Unload__Game_System();
        protected virtual void Handle_Unload__Game_System()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__SYSTEM__UNLOAD, this);
        }
    }
}

namespace XerxesEngine.Systems
{
    public class GameSystem
    {
        protected Game Game { get; set; }
        public bool Accessable { get; private set; }

        public GameSystem(Game game, bool accessable = true)
        {
            Game = game;
            Accessable = accessable;
        }

        public virtual void Load()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__SYSTEM__LOAD, this);

        }

        public virtual void Unload()
        {
            Log.Internal_Write__Verbose__Log(Log.VERBOSE__SYSTEM__UNLOAD, this);
        }
    }
}

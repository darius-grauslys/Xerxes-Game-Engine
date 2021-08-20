using isometricgame.GameEngine.Tools.Collections.Recurring_Dictionary;

namespace isometricgame.GameEngine.State_Management
{
    public sealed class State_Handle : Recurring_Handle
    {
        public static State_Handle DEFAULT_HANDLE = new State_Handle("default");
        
        internal State_Handle(string internalHandle)
            : base (internalHandle)
        {
        }
    }
}
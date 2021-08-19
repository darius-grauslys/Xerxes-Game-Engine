namespace isometricgame.GameEngine.Events
{
    public sealed class Event_Handle
    {
        private string _Event_Handle__HANDLE { get; }

        internal Event_Handle(string recurringEventHandle)
        {
            _Event_Handle__HANDLE = recurringEventHandle;
        }

        public override string ToString()
        {
            return _Event_Handle__HANDLE;
        }

        public static implicit operator string(Event_Handle eventHandle)
            => eventHandle._Event_Handle__HANDLE;
    }
}
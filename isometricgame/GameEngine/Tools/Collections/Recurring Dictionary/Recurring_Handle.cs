namespace isometricgame.GameEngine.Tools.Collections.Recurring_Dictionary
{
    public class Recurring_Handle
    {
        private string _Recurring_Handle__HANDLE { get; }

        protected Recurring_Handle(string internalHandle)
        {
            _Recurring_Handle__HANDLE = internalHandle;
        }

        public override string ToString()
        {
            return _Recurring_Handle__HANDLE;
        }

        public static implicit operator string(Recurring_Handle recurringHandle)
            => recurringHandle._Recurring_Handle__HANDLE;
    }
}
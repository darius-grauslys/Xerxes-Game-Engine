using System;
using System.Collections.Generic;

namespace isometricgame.GameEngine.Tools.Collections.Recurring_Dictionary
{
    public abstract class Recurring_Handle_Catalog<T, Y> where T : Recurring_Handle
    {
        public const string DEFAULT_RECURRING_FORMAT = "{0}_{1}";
        
        private Dictionary<T,Y> _Recurring_Handle_Catalog__DICTIONARY { get; }
        private Dictionary<string, int> _Recurring_Handle_Catalog__RECURRING_COUNTS { get; }

        protected Y Protected_Get__Entry__Recurring_Handle_Catalog(T recurringHandle)
        {
            return _Recurring_Handle_Catalog__DICTIONARY[recurringHandle];
        }

        protected T Protected_Add__Entry__Recurring_Handle_Catalog(string desiredHandle, Y entry)
        {
            T recurringHandle = Private_Validate__Desired_Handle__Recurring_Handle_Catalog(desiredHandle);
            
            _Recurring_Handle_Catalog__DICTIONARY.Add(recurringHandle, entry);

            return recurringHandle;
        }

        private T Private_Validate__Desired_Handle__Recurring_Handle_Catalog(string desiredHandle)
        {
            int index = Private_Record__Handle_Occurrence__Recurring_Handle_Catalog(desiredHandle);

            return Handle_Format__Internal_Handle__Recurring_Handle_Catalog(desiredHandle, index);
        }

        protected abstract T Handle_Format__Internal_Handle__Recurring_Handle_Catalog(string handle, int index);
        
        private int Private_Record__Handle_Occurrence__Recurring_Handle_Catalog(string desiredHandle)
        {
            if (_Recurring_Handle_Catalog__RECURRING_COUNTS.ContainsKey(desiredHandle))
            {
                return _Recurring_Handle_Catalog__RECURRING_COUNTS[desiredHandle]++;
            }
            
            _Recurring_Handle_Catalog__RECURRING_COUNTS.Add(desiredHandle, 0);
            return 0;
        }
    }
}
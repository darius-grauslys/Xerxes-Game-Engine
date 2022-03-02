
using System.Collections.Generic;

namespace Xerxes
{
    public abstract class Xerxes_Genology
    {
        internal Xerxes_Object_Base Genology__Enclosing_Object__Internal { get; set; }

        internal List<Xerxes_Genology> Genology__DESCENDANT_GENOLOGIES__Internal { get; }

        public Xerxes_Genology()
        {
            Genology__DESCENDANT_GENOLOGIES__Internal =
                new List<Xerxes_Genology>();
        }

        protected TGroup Protected_Link__Genology_Group__Genology<TThis, TGroup>()
        where TGroup : Xerxes_Genology_Group<TThis>, new()
        where TThis : Xerxes_Genology
        {
            if (!(this is TThis))
            {
                //TODO: const log
                Log.Write__Error__Log($"The Genology Group {typeof(TGroup)} is not compatable with the Genology, {GetType()}!", this);
                return null;
            }
    
            TGroup group = new TGroup();

            group.Genology_Group__Enclosing_Genology__Internal =
                this as TThis;

            return group;
        }
    }
}

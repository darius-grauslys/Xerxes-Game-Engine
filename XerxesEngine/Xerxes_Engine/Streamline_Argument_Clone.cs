using System.Collections.Generic;
using System.Linq;

namespace Xerxes_Engine
{
    public class Streamline_Argument_Clone : Streamline_Argument
    {
        private List<Xerxes_Object> _Streamline_Argument_Clone__CLONES { get; }
        internal List<Xerxes_Object> Internal_Get__Clones__Streamline_Argument_Clone()
            => _Streamline_Argument_Clone__CLONES.ToList();
        internal void Internal_Append__Clone__Streamline_Argument_Clone(Xerxes_Object clone)
            => _Streamline_Argument_Clone__CLONES.Add(clone);

        internal Streamline_Argument_Clone()
            : base (0,0)
        { 
            _Streamline_Argument_Clone__CLONES = new List<Xerxes_Object>();
        }
    }
}

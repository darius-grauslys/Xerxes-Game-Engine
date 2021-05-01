using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isometricgame.GameEngine.Exceptions.Components
{
    /// <summary>
    /// This exception is thrown whenever an Attribute is added to an object that already posessed an attribute of that type.
    /// This is thrown because Attributes should be determined during object construction. Only the needed attributes should be added.
    /// Additionally, attributes which should be disabled should be disabled via Attribute.Toggle();
    /// </summary>
    public class ExistingAttributeException : Exception
    {

    }
}

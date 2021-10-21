using OpenTK;
using Xerxes_Engine;
using Xerxes_Engine.Tools;

namespace Xerxes_UI 
{
    public class SA__UI_Transformed :
        Streamline_Argument
    {
        internal Vector3 SA_UI_Transformed__Old_Ancestor_Scale__Internal    { get; private set; }
        internal Vector3 SA_UI_Transformed__New_Ancestor_Scale__Internal    { get; private set; }
        internal Vector3 SA_UI_Transformed__Old_Ancestor_Position__Internal { get; private set; }
        internal Vector3 SA_UI_Transformed__New_Ancestor_Position__Internal { get; private set; }

        internal Vector3 SA_UI_Transformed__Delta_Scale__Internal           { get; private set; }
        internal Vector3 SA_UI_Transformed__Delta_Position__Internal        { get; private set; }

        public SA__UI_Transformed
        (
            SA__Update e,
            Vector3 oldAncestorScale,
            Vector3 newAncestorScale,
            Vector3 oldAncestorPosition,
            Vector3 newAncestorPosition
        )
            : base (e)
        {
            SA_UI_Transformed__Delta_Position__Internal =
                newAncestorPosition
                -
                oldAncestorPosition;
            SA_UI_Transformed__Delta_Scale__Internal =
                Math_Helper.Get__Hadamard_Product
                (
                    newAncestorScale,
                    Math_Helper.Get__Safe_Hadamard_Inverse
                    (oldAncestorScale)
                );
        }

        internal void Internal_Assume__Argument__SA__UI_Transformed
        (
            Vector3 oldScale,
            Vector3 newScale,
            Vector3 oldPosition,
            Vector3 newPosition
        )
        {
            SA_UI_Transformed__Old_Ancestor_Scale__Internal =
                oldScale;
            SA_UI_Transformed__New_Ancestor_Scale__Internal =
                newScale;
            SA_UI_Transformed__Old_Ancestor_Position__Internal =
                oldPosition;
            SA_UI_Transformed__New_Ancestor_Position__Internal =
                newPosition;
        }
    }
}

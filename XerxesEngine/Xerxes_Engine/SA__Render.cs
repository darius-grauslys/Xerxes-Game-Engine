using OpenTK;

namespace Xerxes_Engine
{
    public class SA__Render
        : Streamline_Argument
    {
        internal Matrix4 SA__Render__World_Matrix__Internal { get; set; }

        internal SA__Render
        (
            double deltaTime, 
            double senderTime
        ) 
        : 
        base
        (
            deltaTime, 
            senderTime
        )
        {
        }
    }
}

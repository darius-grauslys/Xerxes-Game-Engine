using OpenTK;

namespace Xerxes_Engine
{
    internal class Streamline_Argument_Render
        : Streamline_Argument
    {
        internal Matrix4 Streamline_Argument_Render__World_Matrix__Internal { get; set; }

        internal Streamline_Argument_Render
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

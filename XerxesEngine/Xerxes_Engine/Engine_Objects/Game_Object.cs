namespace Xerxes_Engine.Engine_Objects
{
    /// <summary>
    /// Mediates Update and Draw streamlines to descending
    /// assocations. Inherit this for further streamline mediations.
    /// </summary>
    public class Game_Object : Xerxes_Descendant<Scene_Layer, Game_Object>
    {
        public Game_Object
        ()
        {
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Update>();
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Render>(Private__Handle_Render__Game_Object);

            // Declare draw mediator channel between associated components.
            Protected_Declare__Ascending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>();
            Protected_Declare__Descending_Streamline__Xerxes_Engine_Object
                <Streamline_Argument_Draw>();
        }

        /// <summary>
        /// Controls the Render streamline, then mediates a
        /// Draw streamline among associated components.
        /// Afterwards, sends the mediated argument upstream
        /// towards Render_Service.
        /// </summary>
        private void Private__Handle_Render__Game_Object(Streamline_Argument_Render e)
        {
            Streamline_Argument_Draw streamline_Argument_Draw =
                new Streamline_Argument_Draw(e);

            // Perform draw mediation, and send upstream to Render_Service.
            Protected_Invoke__Descending_Streamline__Xerxes_Engine_Object
                (streamline_Argument_Draw);
            Protected_Invoke__Ascending_Streamline__Xerxes_Engine_Object
                (streamline_Argument_Draw);
        }
    }
}

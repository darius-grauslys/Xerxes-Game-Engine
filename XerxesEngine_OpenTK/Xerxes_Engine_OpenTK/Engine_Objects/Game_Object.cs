namespace Xerxes_Engine.Export_OpenTK.Engine_Objects
{
    /// <summary>
    /// Mediates Update and Draw streamlines to descending
    /// assocations. Inherit this for further streamline mediations.
    /// </summary>
    public class Game_Object :
        Xerxes_Object<Game_Object>
    {
        public Game_Object
        ()
        {
            Declare__Streams()
                .Downstream.Receiving<SA__Render>
                (
                    Private_Mediate__Draw__Game_Object
                )
                .Downstream.Extending<SA__Draw>()
                .Upstream  .Extending<SA__Draw>();
        }

        /// <summary>
        /// Controls the Render streamline, then mediates a
        /// Draw streamline among associated components.
        /// Afterwards, sends the mediated argument upstream
        /// towards Render_Service.
        /// </summary>
        private void Private_Mediate__Draw__Game_Object(SA__Render e)
        {
            SA__Draw streamline_Argument_Draw =
                new SA__Draw(e);

            // Perform draw mediation, and send upstream to Render_Service.
            Invoke__Descending
                (streamline_Argument_Draw);
            Invoke__Ascending
                (streamline_Argument_Draw);
        }
    }
}

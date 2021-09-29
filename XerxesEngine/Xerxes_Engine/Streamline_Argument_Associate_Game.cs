namespace Xerxes_Engine
{
    public class Streamline_Argument_Associate_Game : Streamline_Argument
    {
        internal Game Streamline_Argument_Associate_Game__GAME { get; }

        internal Streamline_Argument_Associate_Game
        (
            double elapsedTime,
            double deltaTime,
            Game game
        )
        : base
        (
            elapsedTime,
            deltaTime
        )
        {
            Streamline_Argument_Associate_Game__GAME = game;
        }
    }
}

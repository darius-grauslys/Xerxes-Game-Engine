namespace Xerxes_Engine
{
    public class SA__Associate_Game : Streamline_Argument
    {
        internal Game SA__Associate_Game__GAME { get; }

        internal SA__Associate_Game
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
            SA__Associate_Game__GAME = game;
        }
    }
}

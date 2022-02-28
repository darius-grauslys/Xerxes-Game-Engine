
namespace Xerxes.Xerxes_OpenTK
{
    public static class Xerxes_OpenTK
    {
        public static void Run
        <Game_Root>(SA__Configure_Root e)
        where Game_Root : OpenTK_Game, new()
        {
            Xerxes_Suite.Run
                <
                Game_Root, 
                Root_Association_Event,
                Root_Dissassociation_Event 
                >
                (e);
        }

        public static void Run
        <Game_Root>(params string[] args)
        where Game_Root : OpenTK_Game, new()
        {
            Xerxes_Suite.Run
            <
            Game_Root,
            Root_Association_Event,
            Root_Dissassociation_Event 
            >
            (new SA__Configure_Root(arguments: args));
        } 
    }
}

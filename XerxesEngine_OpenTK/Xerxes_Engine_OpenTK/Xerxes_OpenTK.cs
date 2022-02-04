
namespace Xerxes.Xerxes_OpenTK
{
    public static class Xerxes_OpenTK
    {
        public static void Run<Game_Root>(SA__Configure_OpenTK_Game e)
        where Game_Root : Game, new()
        {
            Xerxes.Run
                <
                Game_Root, 
                SA__Configure_OpenTK_Game, 
                SA__Associate_Game_OpenTK, 
                SA__Dissassociate_Game_OpenTK
                >
                (e);
        }
    }
}

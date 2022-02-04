
namespace Xerxes.Game_Engine
{
    public abstract class Game<Args> :
        Root<Args, SA__Associate_Game, SA__Dissassociate_Game>
        where Args : SA__Configure_Root
    {
    }
}

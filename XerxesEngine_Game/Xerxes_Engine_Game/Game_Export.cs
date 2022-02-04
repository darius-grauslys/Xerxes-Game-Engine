
namespace Xerxes.Game_Engine
{
    public abstract class Game_Export<Args> :
        Xerxes_Export<Args, SA__Associate_Game, SA__Dissassociate_Game>
        where Args : SA__Configure_Root
    {

    }
}

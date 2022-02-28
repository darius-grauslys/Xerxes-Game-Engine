
using System;

namespace Xerxes.Game_Engine 
{
    public sealed class SA__Set_Scene :
    Streamline_Argument
    {
        public Type Set_Scene__Scene { get; private set; }
        public void Select__Scene<TScene>()
        where TScene : Scene
            => Set_Scene__Scene = typeof(TScene);
    }
}

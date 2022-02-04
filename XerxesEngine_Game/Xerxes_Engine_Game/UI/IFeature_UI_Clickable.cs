
using System;

namespace Xerxes.Game_Engine.UI
{
    public interface IFeature_UI_Clickable : 
        IFeature_UI
    {
        Action UI_Clickable__Click_Callback { get; set; }
    }
}

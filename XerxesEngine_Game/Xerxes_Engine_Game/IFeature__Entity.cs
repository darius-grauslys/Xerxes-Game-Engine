
using System;

namespace Xerxes.Game_Engine
{
    public interface IFeature__Entity<TThis> : 
    IFeature, IDisposable
    where TThis : IFeature__Entity<TThis>
    {
    }
}

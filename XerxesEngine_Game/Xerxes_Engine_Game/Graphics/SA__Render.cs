
using System;

namespace Xerxes.Game_Engine.Graphics
{
    public sealed class SA__Render :
        SA__Chronical
    {
        private Action<Render_Target> _Render__Render_Callback { get; }

        internal SA__Render(Action<Render_Target> render_callback)
        {
            _Render__Render_Callback = render_callback;
        }

        public void Render(Render_Target render_target)
            => _Render__Render_Callback?.Invoke(render_target);
    }
}

using System;
using Odin.Core;

namespace Odin.Behaviors
{
    public interface IBehavior:IDisposable
    {
        bool IsDisposed();
        void Attach(OView oView);
        void Detach();
        void Update();
    }
}

#if DIRECTX
using SharpDX.Direct3D11;
#endif
using System;

namespace MonoCompute
{
    public abstract class ComputeTexture : IDisposable
    {
#if DIRECTX
        internal UnorderedAccessView _uav;
#endif
        public virtual void Dispose()
        {
#if DIRECTX
            _uav.Dispose();
#else
            throw new NotImplementedException(); 
#endif
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoCompute
{
    public class ComputeKernel : IDisposable
    {
#if DIRECTX
       public SharpDX.Direct3D11.ComputeShader shader { get; private set; }
#endif
        public ComputeKernel(GraphicsDevice device, byte[] byteCodes)
        {
#if DIRECTX
            shader = new SharpDX.Direct3D11.ComputeShader(device.Handle as SharpDX.Direct3D11.Device,byteCodes);
#endif
        }

        public void Dispose()
        {
#if DIRECTX
            shader.Dispose();
#endif
        }
    }
}

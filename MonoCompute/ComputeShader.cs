using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;

namespace MonoCompute
{
    //http://kylehalladay.com/blog/tutorial/2014/06/27/Compute-Shaders-Are-Nifty.html
    public class ComputeShader : IDisposable
    {
        private GraphicsDevice _graphics;
        private ComputeShaderResource _computeShaderResource;
#if DIRECTX
        private SharpDX.Direct3D11.DeviceContext DeviceContext
        {
            get { return (_graphics.Handle as SharpDX.Direct3D11.Device).ImmediateContext; }
        }

#endif
        public  ComputeShader(GraphicsDevice graphics, ComputeShaderResource resource)
        {
            _graphics = graphics;
            _computeShaderResource = resource;
            _computeShaderResource.PrepareShader(graphics);
        }

        public void SetRWTexture(int startSlot, ComputeTexture2D buffer, int uavInitialCount = -1)
        {
#if DIRECTX
            DeviceContext.ComputeShader.SetUnorderedAccessView(0, buffer._uav, uavInitialCount);
#else
            throw new NotImplementedException();
#endif
        }
        public void SetBuffer(int startSlot, ComputeBuffer buffer, int uavInitialCount = -1)
        {
#if DIRECTX
            DeviceContext.ComputeShader.SetUnorderedAccessView(0, buffer._uav, uavInitialCount);
#else
             throw new NotImplementedException();
#endif
        }
        public void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
        {
            var kernel = _computeShaderResource.kernels.ElementAt(kernelIndex).Value;
#if DIRECTX
            DeviceContext.ComputeShader.Set(kernel.shader);
            DeviceContext.Dispatch(threadGroupsX, threadGroupsY, threadGroupsZ);
#else
            throw new NotImplementedException();
#endif
        }
        public void Dispatch(string kernel_EntryName, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
        {

            var kernel = _computeShaderResource.kernels[kernel_EntryName];
#if DIRECTX
            DeviceContext.ComputeShader.Set(kernel.shader);
            DeviceContext.Dispatch(threadGroupsX, threadGroupsY, threadGroupsZ);
#else
            throw new NotImplementedException();
#endif
        }
        public void Dispose()
        {
            _computeShaderResource.Dispose();
        }
    }
}


using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoCompute
{
    public class ComputeShaderResource : IDisposable
    {
        Dictionary<string, byte[]> _byteCodes;
        public Dictionary<string, ComputeKernel> kernels;
        public ComputeShaderResource(Dictionary<string, byte[]> byteCodes) 
        {
            _byteCodes = byteCodes;
        }
        public void PrepareShader(GraphicsDevice device) 
        {
            kernels = new Dictionary<string, ComputeKernel>();
            foreach (var item in _byteCodes)
            {
                kernels.Add(item.Key, new ComputeKernel(device, item.Value));
            }
        }

        public void Dispose()
        {
            _byteCodes.Clear();
            foreach (var item in kernels)
                item.Value.Dispose();
            kernels.Clear();
        }
    }
}

#if DIRECTX
using SharpDX.Direct3D11;
#endif
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonoCompute
{
    public class ComputeBuffer : IDisposable
    {
        SharpDX.Direct3D11.Device _device;
        SharpDX.Direct3D11.DeviceContext _deviceContext;
        SharpDX.Direct3D11.Buffer _buffer;
        SharpDX.Direct3D11.Buffer _resultBuffer;
        bool hasBeenProcessed;
#if DIRECTX
        internal UnorderedAccessView _uav;
#endif
        public ComputeBuffer(GraphicsDevice device, int count, int stride) 
        {
            _device = (device.Handle as SharpDX.Direct3D11.Device);
            _deviceContext = (device.Handle as SharpDX.Direct3D11.Device).ImmediateContext;

            _uav = CreateUAV(_device, count, stride ,out _buffer);

            _resultBuffer = CreateStaging(count, stride);
        }

        public T[] GetData<T>(int count) where T : struct
        {
            _deviceContext.CopyResource(_buffer, _resultBuffer);

            SharpDX.DataStream stream;
            SharpDX.DataBox box = _deviceContext.MapSubresource(_resultBuffer, 0, MapMode.Read, MapFlags.None, out stream);
            T[] result = stream.ReadRange<T>(count);
            _deviceContext.UnmapSubresource(_buffer, 0);
           
            return result;
        }
        private UnorderedAccessView CreateUAV(Device _d3dDevice, int count, int stride, out SharpDX.Direct3D11.Buffer buffer)
        {
            int size = stride;
            BufferDescription bufferDescription = new BufferDescription()
            {
                BindFlags = BindFlags.UnorderedAccess | BindFlags.ShaderResource,
                Usage = ResourceUsage.Default,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.BufferStructured,
                StructureByteStride = size,
                SizeInBytes = size * count
            };

            buffer = new SharpDX.Direct3D11.Buffer(_d3dDevice, bufferDescription);

            UnorderedAccessViewDescription uavDescription = new UnorderedAccessViewDescription()
            {
                Buffer = new UnorderedAccessViewDescription.BufferResource() { FirstElement = 0, Flags = UnorderedAccessViewBufferFlags.None, ElementCount = count },
                Format = SharpDX.DXGI.Format.Unknown,
                Dimension = UnorderedAccessViewDimension.Buffer
            };

            return new UnorderedAccessView(_d3dDevice, buffer, uavDescription);
        }

        private SharpDX.Direct3D11.Buffer CreateStaging(int count, int stride)
        {
            int size = stride * count;
            BufferDescription bufferDescription = new BufferDescription()
            {
                SizeInBytes = size,
                BindFlags = BindFlags.None,
                CpuAccessFlags = CpuAccessFlags.Read | CpuAccessFlags.Write,
                Usage = ResourceUsage.Staging,
                OptionFlags = ResourceOptionFlags.None,
            };
            return new SharpDX.Direct3D11.Buffer(_device, bufferDescription);
        }
        public void Release()
        {
#if DIRECTX
            _uav?.Dispose();
#endif
        }
        public void Dispose()
        {
            Release();
        }
    }
}

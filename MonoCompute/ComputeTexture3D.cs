#if DIRECTX
using SharpDX.Direct3D11;
using SharpDX.DXGI;
#endif

using Microsoft.Xna.Framework.Graphics;

namespace MonoCompute
{
    public class ComputeTexture3D : ComputeTexture
    {
        public Texture3D_DX Texture { get; private set; }
        public ComputeTexture3D(GraphicsDevice graphics, int width, int height, int depth, bool mipMap, SurfaceFormat surfaceFormat)
        {
               Texture = new Texture3D_DX(graphics, width, height, depth, mipMap, surfaceFormat, true);
#if DIRECTX
            _uav = new UnorderedAccessView(graphics.Handle as SharpDX.Direct3D11.Device, Texture.CreateResource());
#else
           Texture?.Dispose(); 
           throw new NotImplementedException();
#endif
        }

        public override void Dispose()
        {
            Texture?.Dispose();
            base.Dispose();
        }
    }
}

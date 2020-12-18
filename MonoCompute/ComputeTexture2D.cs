#if DIRECTX
using SharpDX.Direct3D11;
using SharpDX.DXGI;
#endif

using Microsoft.Xna.Framework.Graphics;

namespace MonoCompute
{
    public class ComputeTexture2D : ComputeTexture
    {
        public Texture2D_DX Texture { get; private set; }
        public ComputeTexture2D(GraphicsDevice graphics, int width, int height, bool mipmap, SurfaceFormat surfaceFormat)
        {
            Texture = new Texture2D_DX(graphics, width, height, mipmap, surfaceFormat, true);
#if DIRECTX
            _uav = new UnorderedAccessView(graphics.Handle as SharpDX.Direct3D11.Device,  Texture.CreateResource());
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

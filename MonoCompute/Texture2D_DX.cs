using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.MediaFoundation.DirectX;
using System;

namespace MonoCompute
{
    public partial class Texture2D_DX : Microsoft.Xna.Framework.Graphics.Texture2D
    {
        bool EnableReadWrite;
        public Texture2D_DX(GraphicsDevice graphicsDevice, int width, int height, bool mipmap, SurfaceFormat format, bool enableReadWrite = false)
      : base(graphicsDevice, width, height, mipmap, format, SurfaceType.Texture, false, 1)
        {
            EnableReadWrite = enableReadWrite;
        }

        protected override Texture2DDescription GetTexture2DDescription()
        {
            var desc = new Texture2DDescription();
            desc.Width = Width;
            desc.Height = Height;
            desc.MipLevels = 0;
            desc.ArraySize = 1;
            desc.Format = ToFormat(Format);

            desc.CpuAccessFlags = CpuAccessFlags.None;
            desc.SampleDescription = SampleDescription;
            desc.Usage = ResourceUsage.Default;
            desc.OptionFlags = ResourceOptionFlags.None;

            if (Shared)
                desc.OptionFlags |= ResourceOptionFlags.Shared;

            if (EnableReadWrite)
                desc.BindFlags = BindFlags.UnorderedAccess | BindFlags.ShaderResource;
            else
                desc.BindFlags = BindFlags.ShaderResource;

            return desc;
        }
        public override SharpDX.Direct3D11.Resource CreateTexture()
        {
            // TODO: Move this to SetData() if we want to make Immutable textures!
            var desc = GetTexture2DDescription();
            return new SharpDX.Direct3D11.Texture2D(GraphicsDevice.Handle as SharpDX.Direct3D11.Device, desc);
        }
        static public SharpDX.DXGI.Format ToFormat(SurfaceFormat format)
        {
            switch (format)
            {
                case SurfaceFormat.Color:
                default:
                    return SharpDX.DXGI.Format.R8G8B8A8_UNorm;

                case SurfaceFormat.Bgr565:
                    return SharpDX.DXGI.Format.B5G6R5_UNorm;
                case SurfaceFormat.Bgra5551:
                    return SharpDX.DXGI.Format.B5G5R5A1_UNorm;
                case SurfaceFormat.Bgra4444:
#if WINDOWS_UAP
                    return SharpDX.DXGI.Format.B4G4R4A4_UNorm;
#else
                    return (SharpDX.DXGI.Format)115;
#endif
                case SurfaceFormat.Dxt1:
                    return SharpDX.DXGI.Format.BC1_UNorm;
                case SurfaceFormat.Dxt3:
                    return SharpDX.DXGI.Format.BC2_UNorm;
                case SurfaceFormat.Dxt5:
                    return SharpDX.DXGI.Format.BC3_UNorm;
                case SurfaceFormat.NormalizedByte2:
                    return SharpDX.DXGI.Format.R8G8_SNorm;
                case SurfaceFormat.NormalizedByte4:
                    return SharpDX.DXGI.Format.R8G8B8A8_SNorm;
                case SurfaceFormat.Rgba1010102:
                    return SharpDX.DXGI.Format.R10G10B10A2_UNorm;
                case SurfaceFormat.Rg32:
                    return SharpDX.DXGI.Format.R16G16_UNorm;
                case SurfaceFormat.Rgba64:
                    return SharpDX.DXGI.Format.R16G16B16A16_UNorm;
                case SurfaceFormat.Alpha8:
                    return SharpDX.DXGI.Format.A8_UNorm;
                case SurfaceFormat.Single:
                    return SharpDX.DXGI.Format.R32_Float;
                case SurfaceFormat.HalfSingle:
                    return SharpDX.DXGI.Format.R16_Float;
                case SurfaceFormat.HalfVector2:
                    return SharpDX.DXGI.Format.R16G16_Float;
                case SurfaceFormat.Vector2:
                    return SharpDX.DXGI.Format.R32G32_Float;
                case SurfaceFormat.Vector4:
                    return SharpDX.DXGI.Format.R32G32B32A32_Float;
                case SurfaceFormat.HalfVector4:
                    return SharpDX.DXGI.Format.R16G16B16A16_Float;

                case SurfaceFormat.HdrBlendable:
                    // TODO: This needs to check the graphics device and 
                    // return the best hdr blendable format for the device.
                    return SharpDX.DXGI.Format.R16G16B16A16_Float;

                case SurfaceFormat.Bgr32:
                    return SharpDX.DXGI.Format.B8G8R8X8_UNorm;
                case SurfaceFormat.Bgra32:
                    return SharpDX.DXGI.Format.B8G8R8A8_UNorm;

                case SurfaceFormat.ColorSRgb:
                    return SharpDX.DXGI.Format.R8G8B8A8_UNorm_SRgb;
                case SurfaceFormat.Bgr32SRgb:
                    return SharpDX.DXGI.Format.B8G8R8X8_UNorm_SRgb;
                case SurfaceFormat.Bgra32SRgb:
                    return SharpDX.DXGI.Format.B8G8R8A8_UNorm_SRgb;
                case SurfaceFormat.Dxt1SRgb:
                    return SharpDX.DXGI.Format.BC1_UNorm_SRgb;
                case SurfaceFormat.Dxt3SRgb:
                    return SharpDX.DXGI.Format.BC2_UNorm_SRgb;
                case SurfaceFormat.Dxt5SRgb:
                    return SharpDX.DXGI.Format.BC3_UNorm_SRgb;
            }
        }
    }
}

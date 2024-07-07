using FFXIVClientStructs.FFXIV.Client.Graphics.Kernel;

namespace FFXIVClientStructs.FFXIV.Client.Graphics.Render;

// Client::Graphics::Render::RenderTargetManager
//   Client::Graphics::Singleton
//   Client::Graphics::Kernel::Notifier
// WARNING: THIS IS OUT OF DATE
[GenerateInterop]
[Inherits<Notifier>]
[StructLayout(LayoutKind.Explicit, Size = 0x4A0)]
public unsafe partial struct RenderTargetManager {

    // the first 65 fields seem to be render target pointers
    [FieldOffset(0x20), FixedSizeArray] internal FixedSizeArray64<Pointer<Texture>> _renderTargets;

    // specific ones i can name
    // offscreen renderer is used to render models for UI elements like the character window
    [FieldOffset(0x1E0), CExportIgnore] public Texture* OffscreenRenderTarget_1;
    [FieldOffset(0x1E8), CExportIgnore] public Texture* OffscreenRenderTarget_2;
    [FieldOffset(0x1F0), CExportIgnore] public Texture* OffscreenRenderTarget_3;
    [FieldOffset(0x1F8), CExportIgnore] public Texture* OffscreenRenderTarget_4;
    [FieldOffset(0x200), CExportIgnore] public Texture* OffscreenGBuffer;
    [FieldOffset(0x208), CExportIgnore] public Texture* OffscreenDepthStencil;

    [FieldOffset(0x210), CExportIgnore]
    public Texture* OffscreenRenderTarget_Unk1; // these are related to offscreen renderer due to their size

    [FieldOffset(0x218), CExportIgnore] public Texture* OffscreenRenderTarget_Unk2;
    [FieldOffset(0x220), CExportIgnore] public Texture* OffscreenRenderTarget_Unk3;

    [FieldOffset(0x248)] public uint Resolution_Width;
    [FieldOffset(0x24C)] public uint Resolution_Height;
    [FieldOffset(0x250)] public uint ShadowMap_Width;
    [FieldOffset(0x254)] public uint ShadowMap_Height;
    [FieldOffset(0x258)] public uint NearShadowMap_Width;
    [FieldOffset(0x25C)] public uint NearShadowMap_Height;
    [FieldOffset(0x260)] public uint FarShadowMap_Width;
    [FieldOffset(0x264)] public uint FarShadowMap_Height;
    [FieldOffset(0x268)] public bool UnkBool_1;

    [FieldOffset(0x270), FixedSizeArray] internal FixedSizeArray49<Pointer<Texture>> _renderTargets2;

    [FieldOffset(0x470)] public ushort DynamicResolutionActualTargetHeight; // seems to copy TargetHeight into ActualTargetHeight?
    [FieldOffset(0x472)] public ushort DynamicResolutionTargetHeight;
    [FieldOffset(0x474)] public ushort DynamicResolutionMaximumHeight;
    [FieldOffset(0x476)] public ushort DynamicResolutionMinimumHeight;

    [StaticAddress("48 8B 05 ?? ?? ?? ?? 48 8B 70 70 48 85 F6 74 09 48 8B 06 48 8B CE FF 50 10", 3, isPointer: true)]
    public static partial RenderTargetManager* Instance();

    [MemberFunction("E8 ?? ?? ?? ?? 48 8B 4F 30 48 8B D0 FF 57 38")]
    public partial Texture* GetCharaViewTexture(uint clientObjectIndex);
}

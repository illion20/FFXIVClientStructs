using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.FFXIV.Client.UI;

// Client::UI::AddonAetherCurrent
//   Component::GUI::AtkUnitBase
//     Component::GUI::AtkEventListener
[Addon("AetherCurrent")]
[GenerateInterop]
[Inherits<AtkUnitBase>]
[StructLayout(LayoutKind.Explicit, Size = 0x278)]
public unsafe partial struct AddonAetherCurrent {
    [FieldOffset(0x238), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkComponentRadioButton>> _tabPointers;

    [FieldOffset(0x264)] public int TabIndex;
    [FieldOffset(0x268)] public int TabCount;

    [MemberFunction("E8 ?? ?? ?? ?? 84 C0 74 64 8B 85 ?? ?? ?? ??")]
    public partial void SetTab(int tab);
}

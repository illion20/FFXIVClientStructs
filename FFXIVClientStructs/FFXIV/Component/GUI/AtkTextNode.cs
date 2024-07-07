using FFXIVClientStructs.FFXIV.Client.Graphics;
using FFXIVClientStructs.FFXIV.Client.System.Memory;
using FFXIVClientStructs.FFXIV.Client.System.String;

namespace FFXIVClientStructs.FFXIV.Component.GUI;

// Component::GUI::AtkTextNode
//   Component::GUI::AtkResNode
//     Component::GUI::AtkEventTarget
// common CreateAtkNode function "E8 ?? ?? ?? ?? 49 8B 55 08 48 89 04 13"
// type 3
// simple text node
[GenerateInterop]
[Inherits<AtkResNode>]
[StructLayout(LayoutKind.Explicit, Size = 0x160)]
[VirtualTable("E8 ?? ?? ?? ?? 49 8B 55 08 48 89 04 13", [1, 144])]
public unsafe partial struct AtkTextNode : ICreatable {
    [FieldOffset(0xB0)] public uint TextId;
    [FieldOffset(0xB4)] public ByteColor TextColor;
    [FieldOffset(0xB8)] public ByteColor EdgeColor;
    [FieldOffset(0xBC)] public ByteColor BackgroundColor;
    [FieldOffset(0xC0)] public Utf8String NodeText;

    [FieldOffset(0x130)] public void* UnkPtr_1;

    // if text is "asdf" and you selected "sd" this is 2, 3
    [FieldOffset(0x138)] public uint SelectStart;
    [FieldOffset(0x13C)] public uint SelectEnd;

    [FieldOffset(0x152)] public byte LineSpacing;
    [FieldOffset(0x153)] public byte CharSpacing;
    /// <remarks>Alignment bits 0-3, Font Type bits 4-7</remarks>
    [FieldOffset(0x154)] public byte AlignmentFontType;
    [FieldOffset(0x155)] public byte FontSize;
    [FieldOffset(0x156)] public byte SheetType;

    [FieldOffset(0x158)] public ushort FontCacheHandle;
    [FieldOffset(0x15A)] public byte TextFlags;
    [FieldOffset(0x15B)] public byte TextFlags2;

    // 7.0 inlines this ctor
    public void Ctor() {
        AtkResNode.Ctor();
        VirtualTable = StaticVirtualTablePointer;
        NodeText.Ctor();
    }

    [MemberFunction("E8 ?? ?? ?? ?? 8D 4E 32"), GenerateStringOverloads]
    public partial void SetText(byte* str);

    [MemberFunction("E8 ?? ?? ?? ?? 4A 8B 9C F6 ?? ?? ?? ??")]
    public partial byte* GetText();

    [MemberFunction("E8 ?? ?? ?? ?? 8D 4E 5A")]
    public partial void SetNumber(int num, bool showCommaDelimiters = false, bool showPlusSign = false, byte digits = 0, bool addZeroPadding = false);

    [MemberFunction("E8 ?? ?? ?? ?? 48 83 C4 28 5F 5D")]
    public partial void ResizeNodeForCurrentText();

    [MemberFunction("E8 ?? ?? ?? ?? 0F B7 6D 08")]
    public partial void GetTextDrawSize(ushort* outWidth, ushort* outHeight, byte* text = null, int start = 0, int end = -1, bool considerScale = false);

    [MemberFunction("E8 ?? ?? ?? ?? 8B D7 45 85 FF")]
    public partial void SetAlignment(AlignmentType alignmentType);

    [MemberFunction("E8 ?? ?? ?? ?? 45 33 C0 B2 18")]
    public partial void SetFont(FontType fontType);

    public AlignmentType AlignmentType {
        get => (AlignmentType)(AlignmentFontType & 0x0F);
        set => SetAlignment(value);
    }

    public FontType FontType {
        get => (FontType)((AlignmentFontType & 0xF0) >> 4);
        set => SetFont(value);
    }
}

[Flags]
public enum TextFlags {
    AutoAdjustNodeSize = 0x01,
    Bold = 0x02,
    Italic = 0x04,
    Edge = 0x08,
    Glare = 0x10,
    Emboss = 0x20,
    WordWrap = 0x40,
    MultiLine = 0x80
}

[Flags]
public enum TextFlags2 {
    Ellipsis = 0x04,
    FixedFontResolution = 0x10
}

public enum FontType : byte {
    Axis = 0x0,
    MiedingerMed = 0x1,
    Miedinger = 0x2,
    TrumpGothic = 0x3,
    Jupiter = 0x4,
    JupiterLarge = 0x5,
}

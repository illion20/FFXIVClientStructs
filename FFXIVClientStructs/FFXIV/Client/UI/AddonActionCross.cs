using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.FFXIV.Client.UI;

// Client::UI::AddonActionCross
//   Client::UI::AddonActionBarBase
//     Component::GUI::AtkUnitBase
//       Component::GUI::AtkEventListener
[Addon("_ActionCross")]
[GenerateInterop]
[Inherits<AddonActionBarBase>]
[StructLayout(LayoutKind.Explicit, Size = 0x720)]
public unsafe partial struct AddonActionCross {
    [FieldOffset(0x258)] public ChangeSetUI ChangeSet;
    [FieldOffset(0x3B0)] public AtkComponentNode* PadlockNode;
    [FieldOffset(0x3B8)] public AtkComponentCheckBox* PadlockCheckbox;

    [FieldOffset(0x3C0), FixedSizeArray] internal FixedSizeArray4<SlotGroup> _slotGroups;

    [FieldOffset(0x540)] public ControlGuide ControlGuideDpad;
    [FieldOffset(0x588)] public ControlGuide ControlGuideActionButtons;
    [FieldOffset(0x5D0)] public AtkTextNode* SetNumIconNode;
    [FieldOffset(0x5D8)] public AtkComponentBase* ControlGuideEditComponent;
    [FieldOffset(0x6D8)] public AtkResNode* AACRootNode;
    [FieldOffset(0x6E0)] public AtkResNode* ContainerNode;
    [FieldOffset(0x6F0)] public byte ButtonMask; // not raw input data; represents input state after keybinds/mappings

    // values from 1-20 can be converted to a hotbar ID and left/right bool using GetMappedHotbar()
    [FieldOffset(0x6F8)] public uint ExpandedHoldMapValueLR;
    [FieldOffset(0x6FC)] public uint ExpandedHoldMapValueRL;

    [FieldOffset(0x700)] public bool SelectedDoubleCrossLeft;
    [FieldOffset(0x704)] public bool SelectedDoubleCrossRight;
    [FieldOffset(0x710)] public bool InEditMode;
    [FieldOffset(0x711)] public bool SelectedLeft;
    [FieldOffset(0x712)] public bool SelectedRight;
    [FieldOffset(0x713)] public bool DisplayChangeSet;
    [FieldOffset(0x714)] public bool DisplayPetBarCross;
    [FieldOffset(0x716)] public bool AlternateDisplayType;
    [FieldOffset(0x717)] public bool OverrideHidden; // if the XHB is hidden via HUD options, this field indicates whether it should be temporarily revealed

    [FieldOffset(0x718)] public byte AlphaStandard;
    [FieldOffset(0x719)] public byte AlphaActive;
    [FieldOffset(0x71A)] public byte AlphaInactive;

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x158)]
    public partial struct ChangeSetUI { // in control guide arrays: 0=top, 1=right, 2=bottom, 3=left

        [FieldOffset(0x000), FixedSizeArray] internal FixedSizeArray8<HelpMessage> _helpMessages; // hidden "hotbar help" style nodes attached to the Change Set display for some reason
        [FieldOffset(0x080), FixedSizeArray] internal FixedSizeArray8<Pointer<AtkComponentNode>> _numIcons;
        [FieldOffset(0x0C0)] public AtkResNode* ContainerNode;
        [FieldOffset(0x0D0), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkComponentBase>> _dpadComponents;
        [FieldOffset(0x0F0), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkResNode>> _dpadNodes;
        [FieldOffset(0x118), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkComponentBase>> _actionButtonComponents;
        [FieldOffset(0x138), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkResNode>> _actionButtonNodes;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x60)]
    public partial struct SlotGroup {
        [FieldOffset(0x00), FixedSizeArray] internal FixedSizeArray4<HelpMessage> _hotbarHelp;

        [FieldOffset(0x40)] public AtkComponentNode* SlotContainer;
        [FieldOffset(0x48)] public AtkComponentNode* HotbarHelpContainer;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x10)]
    public partial struct HelpMessage {
        [FieldOffset(0x00)] public AtkComponentBase* HelpComponent;
        [FieldOffset(0x08)] public AtkTextNode* HelpText;
    }

    [GenerateInterop]
    [StructLayout(LayoutKind.Explicit, Size = 0x48)] // in control guide arrays: 0=top, 1=right, 2=bottom, 3=left
    public partial struct ControlGuide {
        [FieldOffset(0x00)] public AtkComponentBase* ComponentBase;
        [FieldOffset(0x08), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkComponentBase>> _components;
        [FieldOffset(0x28), FixedSizeArray] internal FixedSizeArray4<Pointer<AtkResNode>> _nodes;
    }

    /// <summary>The current selection state of the Cross hotbar.</summary>
    public ActionCrossSelect Selected =>
        SelectedLeft ? ActionCrossSelect.Left :
        SelectedRight ? ActionCrossSelect.Right :
        ExpandedHoldMapValueLR > 0 ? ActionCrossSelect.LR :
        ExpandedHoldMapValueRL > 0 ? ActionCrossSelect.RL :
        SelectedDoubleCrossLeft ? ActionCrossSelect.DoubleCrossLeft :
        SelectedDoubleCrossRight ? ActionCrossSelect.DoubleCrossRight :
        ActionCrossSelect.None;

    public uint ExpandedHoldMapValue => ExpandedHoldMapValueLR > 0 ? ExpandedHoldMapValueLR : ExpandedHoldMapValueRL;

    public uint GetExpandedHoldBarTarget(bool* useLeftSide) => GetAdjustedBarTarget(ExpandedHoldMapValue, useLeftSide);

    /// <summary>Calls <see cref="GetBarTarget"/> but adjusts for the "cycle" options.</summary>
    /// <remarks>Anytime the client calls GetBarTarget(), it always adjusts the result in this way before actually using it.</remarks>
    public uint GetAdjustedBarTarget(uint mapValue, bool* useLeftSide) {
        var target = GetBarTarget(mapValue, useLeftSide);
        if (target == 19) target = (uint)(AddonActionBarBase.RaptureHotbarId - 1);
        if (target == 18) target = (uint)(AddonActionBarBase.RaptureHotbarId + 1);
        return target >= 18 ? 10 :
               target < 10 ? 17 :
               target;
    }

    /// <summary>
    /// Accepts a value representing the Expanded Hold Controls or WXHB configuration, and converts that value into the corresponding hotbar ID and a bool indicating which half of the bar to reference.
    /// </summary>
    /// <param name="mapValue">A value from 1-20.<br/>1-16 represent each half of the eight Cross Hotbar sets.<br/>17-20 represent the Cycle Up/Down options.</param>
    /// <param name="useLeftSide">True = left side of target bar, False = right side of target bar</param>
    /// <returns>The hotbar ID (<see cref="AddonActionBarBase.RaptureHotbarId"/>) of the set of actions assigned to this input.<br/>
    /// The Cycle Up/Down options will return the values 18 or 19, which are not actual bar IDs. You can use <see cref="GetAdjustedBarTarget"/> instead to account for this.</returns>
    [MemberFunction("8D 41 FF 83 F8 13")]
    public static partial uint GetBarTarget(uint mapValue, bool* useLeftSide);
}

/// <summary>Possible selection states for the Cross Hotbar.</summary>
public enum ActionCrossSelect { None, Left, Right, LR, RL, DoubleCrossLeft, DoubleCrossRight }

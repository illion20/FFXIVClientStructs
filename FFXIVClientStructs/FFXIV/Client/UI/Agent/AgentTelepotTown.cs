namespace FFXIVClientStructs.FFXIV.Client.UI.Agent;

// Client::UI::Agent::AgentTelepotTown
//   Client::UI::Agent::AgentInterface
//     Component::GUI::AtkModuleInterface::AtkEventInterface
[Agent(AgentId.TelepotTown)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 0x30)]
public unsafe partial struct AgentTelepotTown {
    [FieldOffset(0x28)] public AgentTelepotTownData* Data;

    [MemberFunction("48 89 5C 24 ?? 57 48 83 EC 50 0F B6 FA")]
    public partial void TeleportToAetheryte(byte index);
}

[StructLayout(LayoutKind.Explicit, Size = 0xD168)]
public struct AgentTelepotTownData {
    [FieldOffset(0x4)] public byte CurrentAetheryte; // the one you're standing at

    [FieldOffset(0x70A)] public byte SelectedAetheryte;

    [FieldOffset(0x70C)] public byte Flags;
}

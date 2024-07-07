namespace FFXIVClientStructs.FFXIV.Client.UI.Agent;

// Client::UI::Agent::AgentHUDLayout
//   Client::UI::Agent::AgentInterface
//     Component::GUI::AtkModuleInterface::AtkEventInterface
// ctor "40 53 48 83 EC 20 48 8B D9 E8 ?? ?? ?? ?? 48 8D 05 ?? ?? ?? ?? 48 89 03 33 C0 48 89 43 28 48 89 43 30 89 43 38 48 89 43 40"
[Agent(AgentId.HudLayout)]
[GenerateInterop]
[Inherits<AgentInterface>]
[StructLayout(LayoutKind.Explicit, Size = 0x78)]
public unsafe partial struct AgentHUDLayout {
    [FieldOffset(0x70)] public bool NeedToSave;
}

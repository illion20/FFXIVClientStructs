namespace FFXIVClientStructs.FFXIV.Client.Game.Character;

// Client::Game::Character::Companion
//   Client::Game::Character::Character
//     Client::Game::Object::GameObject
//     Client::Graphics::Vfx::VfxDataListenner
//     Client::Game::Character::CharacterData
// ctor "E8 ?? ?? ?? ?? 48 8B C8 EB 03 48 8B CD 48 8B 43 08"
// companion = minion
[GenerateInterop]
[Inherits<Character>]
[StructLayout(LayoutKind.Explicit, Size = 0x2340)]
public partial struct Companion;

using System.Runtime.CompilerServices;
using FFXIVClientStructs.FFXIV.Application.Network.WorkDefinitions;

namespace FFXIVClientStructs.FFXIV.Client.Game;

// Client::Game::QuestManager
[GenerateInterop]
[StructLayout(LayoutKind.Explicit, Size = 0xF58)]
public unsafe partial struct QuestManager {
    [MemberFunction("E8 ?? ?? ?? ?? 66 BA 10 0C")]
    public static partial QuestManager* Instance();

    [FieldOffset(0x10), FixedSizeArray] internal FixedSizeArray30<QuestWork> _normalQuests;
    [FieldOffset(0x5B8), FixedSizeArray] internal FixedSizeArray12<DailyQuestWork> _dailyQuests;
    [FieldOffset(0x6A8), FixedSizeArray] internal FixedSizeArray5<TrackingWork> _trackedQuests;
    [FieldOffset(0xBC8), FixedSizeArray] internal FixedSizeArray17<BeastReputationWork> _beastReputation;
    [FieldOffset(0xCD8), FixedSizeArray] internal FixedSizeArray16<LeveWork> _leveQuests;
    [FieldOffset(0xE58)] public byte NumLeveAllowances;

    [FieldOffset(0xF40)] public byte NumAcceptedQuests;
    [FieldOffset(0xF50)] public byte NumAcceptedLeveQuests;

    [MemberFunction("E8 ?? ?? ?? ?? 43 88 84 3E ?? ?? ?? ??")]
    public static partial bool IsQuestComplete(ushort questId);
    public static bool IsQuestComplete(uint questId) => IsQuestComplete((ushort)(questId & 0xFFFF));

    /**
     * Get the current step in a specific quest. Will return 0 if the quest is not active (even if the quest has been
     * completed prior).
     * <param name="questId">The quest ID to check.</param>
     * <returns>Returns a byte representing the current progression of the specified quest.</returns>
     */
    [MemberFunction("E8 ?? ?? ?? ?? 3A 43 06")]
    public static partial byte GetQuestSequence(ushort questId);

    /**
     * <inheritdoc cref="GetQuestSequence(ushort)"/>
     * <remarks>This is a helper method to handle trimming uints down to the game-requested ushort.</remarks>
     */
    public static byte GetQuestSequence(uint questId) => GetQuestSequence((ushort)(questId & 0xFFFF));

    /**
     * Checks if a specified quest has been accepted (and is active). This method does not check if the quest has been
     * completed or not.
     * <param name="questId">The quest ID to check.</param>
     * <returns>Returns <c>true</c> if the quest has been accepted, <c>false</c> otherwise.</returns>
     */
    [MemberFunction("45 33 C0 48 8D 41 18 66 39 10")]
    public partial bool IsQuestAccepted(ushort questId);

    /**
     * <inheritdoc cref="IsQuestAccepted(ushort)"/>
     * <remarks>This is a helper method to handle trimming uints down to the game-requested ushort.</remarks>
     */
    public bool IsQuestAccepted(uint questId) => IsQuestAccepted((ushort)(questId & 0xFFFF));

    /// <summary>
    /// Check if a recipe has been crafted (= completed) before.
    /// </summary>
    /// <param name="recipeId">The RowId of the Recipe Sheet.</param>
    /// <returns>Returns <c>true</c> if the recipe has been completed, <c>false</c> otherwise.</returns>
    [MemberFunction("40 53 48 83 EC 20 8B D9 81 F9")]
    public static partial bool IsRecipeComplete(uint recipeId);

    /// <summary>
    /// Check if a specific levequest has been completed.
    /// </summary>
    /// <param name="levequestId">The RowId of the Leve Sheet.</param>
    /// <returns>Returns <c>true</c> if the levequest has been completed, <c>false</c> otherwise.</returns>
    [MemberFunction("44 0F B7 C2 4C 8B C9 49 C1 E8 03")]
    public partial bool IsLevequestComplete(ushort levequestId);

    /// <summary>
    /// Get the time when the player will receive new leve allowances.
    /// </summary>
    /// <remarks>
    /// Has to be multiplied by 60 for a unix timestamp.<br/>
    /// Use <see cref="GetNextLeveAllowancesUnixTimestamp"/> or <see cref="GetNextLeveAllowancesDateTime"/> instead.
    /// </remarks>
    [MemberFunction("E8 ?? ?? ?? ?? 41 8D 74 24 ?? 8B D8")]
    private static partial int GetNextLeveAllowancesTimestamp();

    /// <summary>
    /// Get the time when the player will receive new leve allowances.
    /// </summary>
    /// <returns>A unix timestamp as <see cref="int"/>.</returns>
    public static int GetNextLeveAllowancesUnixTimestamp() => GetNextLeveAllowancesTimestamp() * 60;

    /// <summary>
    /// Get the time when the player will receive new leve allowances.
    /// </summary>
    /// <returns>A <see cref="DateTime"/> in the local time zone.</returns>
    public static DateTime GetNextLeveAllowancesDateTime() => DateTime.UnixEpoch.AddSeconds(GetNextLeveAllowancesUnixTimestamp()).ToLocalTime();

    [MemberFunction("48 89 5C 24 ?? 48 89 74 24 ?? 57 48 83 EC 50 48 8B D1 48 8D 4C 24 ?? E8 ?? ?? ?? ?? 48 8B 4C 24 ?? BA ?? ?? ?? ?? E8 ?? ?? ?? ?? BB")]
    public partial uint GetBeastTribeAllowance();

    public bool IsDailyQuestCompleted(ushort questId) {
        var quest = GetDailyQuestById(questId);
        return quest != null && (quest->Flags & 1) != 0;
    }

    public QuestWork* GetQuestById(ushort questId) {
        var span = NormalQuests;
        for (var i = 0; i < span.Length; i++) {
            if (span[i].QuestId == questId)
                return (QuestWork*)Unsafe.AsPointer(ref span[i]);
        }
        return null;
    }

    public DailyQuestWork* GetDailyQuestById(ushort questId) {
        var span = DailyQuests;
        for (var i = 0; i < span.Length; i++) {
            if (span[i].QuestId == questId)
                return (DailyQuestWork*)Unsafe.AsPointer(ref span[i]);
        }
        return null;
    }

    public LeveWork* GetLeveQuestById(ushort leveId) {
        var span = LeveQuests;
        for (var i = 0; i < span.Length; i++) {
            if (span[i].LeveId == leveId)
                return (LeveWork*)Unsafe.AsPointer(ref span[i]);
        }
        return null;
    }

    public BeastReputationWork* GetBeastReputationById(uint beastTribeId) {
        var index = beastTribeId - 1;
        var span = BeastReputation;
        if (index >= span.Length) return null;
        return (BeastReputationWork*)Unsafe.AsPointer(ref span[(int)index]);
    }
}

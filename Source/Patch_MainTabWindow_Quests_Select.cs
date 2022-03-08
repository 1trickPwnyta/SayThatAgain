using HarmonyLib;
using RimWorld;
using Verse;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(MainTabWindow_Quests))]
    [HarmonyPatch(nameof(MainTabWindow_Quests.Select))]
    public static class Patch_MainTabWindow_Quests_Select
    {
        public static void Postfix(Quest quest)
        {
            SayThatAgainMod.LastOpenedLetter = false;
            SayThatAgainMod.LastQuest = quest;
        }
    }
}

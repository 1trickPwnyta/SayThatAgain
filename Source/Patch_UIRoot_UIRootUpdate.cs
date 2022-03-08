using HarmonyLib;
using RimWorld;
using Verse;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(UIRoot))]
    [HarmonyPatch(nameof(UIRoot.UIRootUpdate))]
    public static class Patch_UIRoot_UIRootUpdate
    {
        public static void Postfix()
        {
            if (KeyBindingDefOf.OpenLastLetter.JustPressed)
            {
                if (SayThatAgainMod.LastOpenedLetter && SayThatAgainMod.LastLetter != null)
                {
                    SayThatAgainMod.LastLetter.OpenLetter();
                }
                else if (!SayThatAgainMod.LastOpenedLetter && SayThatAgainMod.LastQuest != null)
                {
                    Find.MainTabsRoot.SetCurrentTab(MainButtonDefOf.Quests, true);
                    ((MainTabWindow_Quests)MainButtonDefOf.Quests.TabWindow).Select(SayThatAgainMod.LastQuest);
                }
            }
        }
    }
}

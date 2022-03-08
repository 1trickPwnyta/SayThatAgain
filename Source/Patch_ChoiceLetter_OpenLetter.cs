using HarmonyLib;
using Verse;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(ChoiceLetter))]
    [HarmonyPatch(nameof(ChoiceLetter.OpenLetter))]
    public static class Patch_ChoiceLetter_OpenLetter
    {
        public static void Postfix(ChoiceLetter __instance)
        {
            SayThatAgainMod.LastOpenedLetter = true;
            SayThatAgainMod.LastLetter = __instance;
        }
    }
}

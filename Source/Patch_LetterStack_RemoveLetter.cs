using HarmonyLib;
using Verse;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(LetterStack))]
    [HarmonyPatch(nameof(LetterStack.RemoveLetter))]
    public static class Patch_LetterStack_RemoveLetter
    {
        public static void Postfix(Letter let)
        {
            if (let != null)
            {
                SayThatAgainMod.LastLetter = let;
            }
        }
    }
}

using HarmonyLib;
using Verse;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(UIRoot))]
    [HarmonyPatch(nameof(UIRoot.UIRootUpdate))]
    [StaticConstructorOnStartup]
    public static class Patch_UIRoot_UIRootUpdate
    {
        private static KeyBindingDef OpenLastLetter = KeyBindingDef.Named("OpenLastLetter");

        public static void Postfix()
        {
            if (OpenLastLetter.JustPressed)
            {
                if (SayThatAgainMod.LastLetter != null)
                {
                    SayThatAgainMod.LastLetter.OpenLetter();
                }
            }
        }
    }
}

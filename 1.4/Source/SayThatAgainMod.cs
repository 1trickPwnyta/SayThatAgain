using Verse;
using HarmonyLib;
using RimWorld;

namespace SayThatAgain
{
    public class SayThatAgainMod : Mod
    {
        public const string PACKAGE_ID = "saythatagain.1trickPonyta";
        public const string PACKAGE_NAME = "Say That Again";

        public static Letter LastLetter = null;

        public SayThatAgainMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }
    }
}

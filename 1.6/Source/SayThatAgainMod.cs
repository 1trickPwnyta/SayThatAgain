using Verse;
using HarmonyLib;
using UnityEngine;

namespace SayThatAgain
{
    public class SayThatAgainMod : Mod
    {
        public const string PACKAGE_ID = "saythatagain.1trickPonyta";
        public const string PACKAGE_NAME = "Say That Again";

        public static SayThatAgainSettings Settings;

        public static Letter LastLetter = null;

        public SayThatAgainMod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<SayThatAgainSettings>();

            var harmony = new Harmony(PACKAGE_ID);
            harmony.PatchAll();

            Log.Message($"[{PACKAGE_NAME}] Loaded.");
        }

        public override string SettingsCategory() => PACKAGE_NAME;

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            SayThatAgainSettings.DoSettingsWindowContents(inRect);
        }
    }
}

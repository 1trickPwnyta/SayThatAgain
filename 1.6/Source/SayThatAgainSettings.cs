using UnityEngine;
using Verse;

namespace SayThatAgain
{
    public class SayThatAgainSettings : ModSettings
    {
        public static int ArchiveLimit = 200;

        public static void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.Label("SayThatAgain_ArchiveLimit".Translate());
            string buffer = ArchiveLimit.ToString();
            listingStandard.IntEntry(ref ArchiveLimit, ref buffer);
            ArchiveLimit = Mathf.Clamp(ArchiveLimit, 1, 999999);

            listingStandard.End();
        }

        public override void ExposeData()
        {
            Scribe_Values.Look(ref ArchiveLimit, "ArchiveLimit", 200);
        }
    }
}

using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(MainTabWindow_History))]
    [HarmonyPatch("DoArchivableRow")]
    public static class Patch_MainTabWindow_History
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            bool finished = false;

            LocalBuilder rectLocal = il.DeclareLocal(typeof(Rect));

            foreach (CodeInstruction instruction in instructions)
            {
                if (!finished && instruction.opcode == OpCodes.Call && (MethodInfo)instruction.operand == typeof(Color).Method("get_white"))
                {
                    yield return instruction;
                    yield return new CodeInstruction(OpCodes.Ldloc_0);
                    yield return new CodeInstruction(OpCodes.Stloc_S, rectLocal);
                    yield return new CodeInstruction(OpCodes.Ldloca_S, rectLocal);
                    yield return new CodeInstruction(OpCodes.Ldc_R4, 30f);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Rect).Method("set_width"));
                    yield return new CodeInstruction(OpCodes.Ldloca_S, 0);
                    yield return new CodeInstruction(OpCodes.Dup);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Rect).Method("get_xMin"));
                    yield return new CodeInstruction(OpCodes.Ldc_R4, 35f);
                    yield return new CodeInstruction(OpCodes.Add);
                    yield return new CodeInstruction(OpCodes.Call, typeof(Rect).Method("set_xMin"));
                    yield return new CodeInstruction(OpCodes.Ldloc_S, rectLocal);
                    yield return new CodeInstruction(OpCodes.Ldarg_2);
                    yield return new CodeInstruction(OpCodes.Call, typeof(PatchUtility_MainTabWindow_History).Method(nameof(PatchUtility_MainTabWindow_History.DoStackButton)));
                    finished = true;
                    continue;
                }

                if (instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 200)
                {
                    instruction.opcode = OpCodes.Ldsfld;
                    instruction.operand = typeof(SayThatAgainSettings).Field(nameof(SayThatAgainSettings.ArchiveLimit));
                }

                yield return instruction;
            }
        }
    }

    [StaticConstructorOnStartup]
    public static class PatchUtility_MainTabWindow_History
    {
        private static Texture2D stackIcon = ContentFinder<Texture2D>.Get("UI/SayThatAgain_Stack");

        public static void DoStackButton(Rect rect, IArchivable archivable)
        {
            if (archivable is Letter && !Find.LetterStack.LettersListForReading.Contains((Letter)archivable))
            {
                Rect imageRect = new Rect(rect.x + (rect.width - 22f) / 2f, rect.y + (rect.height - 22f) / 2f, 22f, 22f).Rounded();
                GUI.color = Mouse.IsOver(rect) ? Color.white : Color.gray;
                GUI.DrawTexture(imageRect, stackIcon);
                TooltipHandler.TipRegionByKey(rect, "SayThatAgain_StackDesc");
                if (Widgets.ButtonInvisible(rect))
                {
                    Find.LetterStack.LettersListForReading.Add((Letter)archivable);
                    SoundDefOf.Click.PlayOneShot(null);
                }
            }
        }
    }
}

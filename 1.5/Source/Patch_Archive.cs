using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SayThatAgain
{
    [HarmonyPatch(typeof(Archive))]
    [HarmonyPatch("CheckCullArchivables")]
    public static class Patch_Archive
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 200)
                {
                    instruction.opcode = OpCodes.Ldsfld;
                    instruction.operand = typeof(SayThatAgainSettings).Field(nameof(SayThatAgainSettings.ArchiveLimit));
                }

                yield return instruction;
            }
        }
    }
}

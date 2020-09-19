using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Verse;

namespace FasterDevSpeed
{
    [StaticConstructorOnStartup]
    static class FasterDevSpeed
    {
        static FasterDevSpeed()
        {
            Log.Message("FasterDevSpeed starting up");

            Harmony harmony = new Harmony("rimworld.fasterdevspeed");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(TickManager))]
    [HarmonyPatch("get_TickRateMultiplier")]
    static class Patch_get_TickRateMultiplier
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
            for (int i = 5; i < codes.Count; i++)
            {
                if (
                    codes[i].opcode == OpCodes.Ret &&
                    (float)codes[i - 1].operand == -1f && // found `return -1f`
                    codes[i - 5].opcode == OpCodes.Ldc_R4 &&
                    codes[i - 4].opcode == OpCodes.Ret &&
                    codes[i - 3].opcode == OpCodes.Ldc_R4 &&
                    codes[i - 2].opcode == OpCodes.Ret // hopefully found the end of `case TimeSpeed.Ultrafast:`
                    )
                {
                    codes[i - 5].operand = (float)codes[i - 5].operand * 3;
                    codes[i - 3].operand = (float)codes[i - 3].operand * 3;
                    break;
                }
            }
            return codes.AsEnumerable();
        }
    }
}
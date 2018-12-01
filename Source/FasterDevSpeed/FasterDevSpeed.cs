using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;

namespace FasterDevSpeed
{
    [Verse.StaticConstructorOnStartup]
    static class FasterDevSpeed
    {

        static FasterDevSpeed()
        {
            Verse.Log.Message("FasterDevSpeed starting up");

            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.sparr.fasterdevspeed");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }


    }

    [HarmonyPatch(typeof(Verse.TickManager))]
    [HarmonyPatch("get_TickRateMultiplier")]
    static class Patch_get_TickRateMultiplier
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            int i;
            for (i = 5; i < codes.Count; i++)
            {
                if (
                    codes[i].opcode == OpCodes.Ret &&
                    (float)codes[i-1].operand == -1f && // found `return -1f`
                    codes[i-5].opcode == OpCodes.Ldc_R4 &&
                    codes[i-4].opcode == OpCodes.Ret &&
                    codes[i-3].opcode == OpCodes.Ldc_R4 &&
                    codes[i-2].opcode == OpCodes.Ret // hopefully found the end of `case TimeSpeed.Ultrafast:`
                    )
                {
                    codes[i-5].operand = (float)codes[i - 5].operand * 4;
                    codes[i-3].operand = (float)codes[i - 3].operand * 4;
                    break;
                }
            }
            return codes.AsEnumerable();
        }
    }
}
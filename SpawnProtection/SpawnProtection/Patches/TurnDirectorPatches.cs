using BattleTech;
using Harmony;
using SpawnProtection.Helper;

namespace SpawnProtection.Patches {

    [HarmonyPatch(typeof(TurnDirector), "BeginNewRound")]
    public class TurnDirector_BeginNewRound {
        static void Postfix(TurnDirector __instance) {
            Mod.Log.Info($"Protecting lances on firstContact during first round:{__instance.CurrentRound}");
            if (__instance.CurrentRound == 1) {
                ProtectionHelper.ProtectOnFirstRound();
            }
        }
    }
}

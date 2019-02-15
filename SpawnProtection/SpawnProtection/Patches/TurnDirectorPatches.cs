using BattleTech;
using Harmony;
using SpawnProtection.Helper;

namespace SpawnProtection.Patches {

    [HarmonyPatch(typeof(TurnDirector), "OnFirstContact")]
    public class TurnDirectorOnFirstContactPatch {
        static void Postfix(TurnDirector __instance) {
            SpawnProtection.Logger.Log($"Protecting lances on firstContact during first round:{__instance.CurrentRound}");
            if (__instance.CurrentRound == 0 && __instance.DoAnyUnitsHaveContactWithEnemy) {
                ProtectionHelper.ProtectOnFirstRound(); ;
            }
        }
    }
}

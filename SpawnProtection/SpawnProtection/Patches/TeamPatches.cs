using BattleTech;
using Harmony;
using SpawnProtection.Helper;
using System.Collections.Generic;

namespace SpawnProtection.Patches {

    [HarmonyPatch(typeof(Team), "AddUnit")]
    public static class Team_AddUnit {

        public static void Postfix(Team __instance, AbstractActor unit) {
            // The added unit is a reinforcement if round > 0
            if (__instance.Combat.TurnDirector.CurrentRound > 1 && Mod.Config.ApplyToReinforcements) {
                HostilityMatrix hm = __instance.Combat.HostilityMatrix;

                Mod.Log.Info($"Checking actor:{CombatantHelper.Label(unit)} that belongs to team:{unit?.team?.Name}");

                List<AbstractActor> actor = new List<AbstractActor>();
                if (hm.IsLocalPlayerEnemy(unit.TeamId) && Mod.Config.ApplyToEnemies) {
                    actor.Add(unit);
                } else if (hm.IsLocalPlayerNeutral(unit.TeamId) && Mod.Config.ApplyToNeutrals) {
                    actor.Add(unit);
                } else if (hm.IsLocalPlayerFriendly(unit.TeamId) && Mod.Config.ApplyToAllies) {
                    actor.Add(unit);
                }

                if (actor.Count == 1) {
                    Mod.Log.Info($"Applying protection to reinforcement:{CombatantHelper.Label(unit)}");
                    ProtectionHelper.ProtectActors(actor);
                }
            }
        }
    }
}

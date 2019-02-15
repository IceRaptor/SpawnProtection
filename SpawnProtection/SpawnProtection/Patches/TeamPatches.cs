using BattleTech;
using Harmony;
using SpawnProtection.Helper;
using System.Collections.Generic;

namespace SpawnProtection.Patches {

    [HarmonyPatch(typeof(Team), "AddUnit")]
    public static class Team_AddUnit {

        public static void Prefix(Team __instance, AbstractActor unit) {
            // The added unit is a reinforcement if round > 0
            if (__instance.Combat.TurnDirector.CurrentRound > 1 && SpawnProtection.Config.ApplyToReinforcements) {
                HostilityMatrix hm = __instance.Combat.HostilityMatrix;

                List<AbstractActor> actor = new List<AbstractActor>();
                if (hm.IsLocalPlayerEnemy(unit.TeamId) && SpawnProtection.Config.ApplyToEnemies) {
                    actor.Add(unit);
                } else if (hm.IsLocalPlayerNeutral(unit.TeamId) && SpawnProtection.Config.ApplyToNeutrals) {
                    actor.Add(unit);
                } else if (hm.IsLocalPlayerFriendly(unit.TeamId) && SpawnProtection.Config.ApplyToAllies) {
                    actor.Add(unit);
                }

                if (actor.Count == 1) {
                    SpawnProtection.Logger.Log($"Applying protection to reinforcement:{CombatantHelper.Label(unit)}");
                    ProtectionHelper.ProtectActors(actor);
                }
            }
        }
    }
}

using BattleTech;
using Harmony;
using SpawnProtection.Helper;
using System.Collections.Generic;
using System.Linq;

namespace SpawnProtection.Helper {

    public static class ProtectionHelper {
        public static void ProtectOnFirstRound() {
            SpawnProtection.Logger.Log($"Protecting units on first turn'");
            CombatGameState combatState = UnityGameInstance.BattleTechGame.Combat;
            HostilityMatrix hm = combatState.HostilityMatrix;
            Team playerTeam = combatState.LocalPlayerTeam;

            // Includes player units
            List<AbstractActor> actors = new List<AbstractActor>();
            foreach (AbstractActor actor in combatState.AllActors) {
                if (hm.IsLocalPlayerEnemy(actor.TeamId) && SpawnProtection.Config.ApplyToEnemies) {
                    actors.Add(actor);
                } else if (hm.IsLocalPlayerNeutral(actor.TeamId) && SpawnProtection.Config.ApplyToNeutrals) {
                    actors.Add(actor);
                } else if (hm.IsLocalPlayerFriendly(actor.TeamId) && SpawnProtection.Config.ApplyToAllies) {
                    actors.Add(actor);
                }
            }
            actors.AddRange(playerTeam.units);
            List<AbstractActor> actorsToProtect = actors.Distinct().ToList();

            ProtectActors(actorsToProtect);
        }

        public static void ProtectActors(List<AbstractActor> actorsToProtect) {
            if (SpawnProtection.Config.ApplyGuard) {
                BraceAll(actorsToProtect);
            }

            if (SpawnProtection.Config.EvasionPips > 0) {
                AddEvasion(actorsToProtect);
            }
        }

        public static void BraceAll(List<AbstractActor> actors) {
            foreach (AbstractActor actor in actors) {
                actor.ApplyBraced();
            }
        }

        public static void AddEvasion(List<AbstractActor> actors) {
            int evasionToAdd = SpawnProtection.Config.EvasionPips;
            CombatGameState combatState = UnityGameInstance.BattleTechGame.Combat;
            
            foreach (AbstractActor actor in actors) {
                SpawnProtection.Logger.Log($"Adding '{evasionToAdd}' evasion pips to actor:{CombatantHelper.Label(actor)}");

                actor.EvasivePipsCurrent += evasionToAdd;
                AccessTools.Property(typeof(AbstractActor), "EvasivePipsTotal").SetValue(actor, actor.EvasivePipsCurrent, null);
                combatState.MessageCenter.PublishMessage(new EvasiveChangedMessage(actor.GUID, actor.EvasivePipsCurrent));
            }
        }
    }
}

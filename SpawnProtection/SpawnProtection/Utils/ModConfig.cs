
namespace SpawnProtection {

    public class ModConfig {

        public bool Debug = false;
        public bool Trace = false;

        public bool ApplyGuard = true;

        public int EvasionPips = 6;

        public bool ApplyToEnemies = true;
        public bool ApplyToAllies = true;
        public bool ApplyToNeutrals = true;

        public bool ApplyToReinforcements = false;
        
        public override string ToString() {
            return $"Debug:{Debug}, ApplyGuard:{ApplyGuard}, EvasionPips:{EvasionPips}, ApplyToReinforcements:{ApplyToReinforcements}, " +
                $"ApplyToAllies:{ApplyToAllies}, ApplyToEnemies:{ApplyToEnemies}, ApplyToNeutrals:{ApplyToNeutrals}";
        }
    }
}

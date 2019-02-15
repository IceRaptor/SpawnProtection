using Harmony;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Reflection;

namespace SpawnProtection {
    public class SpawnProtection {

        public const string HarmonyPackage = "us.frostraptor.SpawnProtection";

        public static Logger Logger;
        public static string ModDir;
        public static ModConfig Config;

        public static string CampaignSeed;

        public static readonly Random Random = new Random();

        public static void Init(string modDirectory, string settingsJSON) {
            ModDir = modDirectory;

            Exception settingsE;
            try {
                SpawnProtection.Config = JsonConvert.DeserializeObject<ModConfig>(settingsJSON);
            } catch (Exception e) {
                settingsE = e;
                SpawnProtection.Config = new ModConfig();
            }

            Logger = new Logger(modDirectory, "spawn_protection");
            
            Assembly asm = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
            Logger.Log($"Assembly version: {fvi.ProductVersion}");

            Logger.LogIfDebug($"ModDir is:{modDirectory}");
            Logger.LogIfDebug($"mod.json settings are:({settingsJSON})");
            Logger.Log($"mergedConfig is:{SpawnProtection.Config}");

            var harmony = HarmonyInstance.Create(HarmonyPackage);
            harmony.PatchAll(asm);
        }

    }
}

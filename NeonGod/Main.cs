using HarmonyLib;
using MelonLoader;
using NeonGod.Mods;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeonGod
{
    public class Main : MelonMod
    {
        public static Game Game { get; private set; }
        public static bool OriginalDontUploadToLeaderboard;

        public static LevelRushStats RushStats { get; private set; }

        public override void OnApplicationLateStart()
        {
            AntiCheat.Anticheat.TriggerAnticheat();
            PatchGame();
            Game game = Singleton<Game>.Instance;

            if (game == null)
                return;
            Game = game;
            Game.OnLevelLoadComplete += OnLevelLoadComplete;

            if (RM.drifter)
                OnLevelLoadComplete();
        }

        private void PatchGame()
        {
            HarmonyLib.Harmony harmony = new("de.MOPSKATER.NeonHack");

            MethodInfo target = typeof(LevelGate).GetMethod("SetUnlocked");
            HarmonyMethod patch = new(typeof(DemonKillSkip).GetMethod("UnlockGate"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("UseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreUseMiracle"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("CanUseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreCanUseMiracle"));
            harmony.Patch(target, patch);

            target = typeof(MechController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            patch = new HarmonyMethod(typeof(UIZipline).GetMethod("PostUpdate"));
            harmony.Patch(target, null, patch);
        }

        private void OnLevelLoadComplete()
        {
            RushStats = LevelRush.GetCurrentLevelRush();

            if (SceneManager.GetActiveScene().name.Equals("Heaven_Environment"))
                return;

            GameObject modObject = new("Mod Manager");
            modObject.AddComponent<ModManager>();
        }
    }
}

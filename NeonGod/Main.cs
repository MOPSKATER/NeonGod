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

            MethodInfo target = typeof(LevelStats).GetMethod("UpdateTimeMicroseconds");
            HarmonyMethod patch = new(typeof(Mod).GetMethod("PreventNewScore"));
            harmony.Patch(target, patch);

            target = typeof(Game).GetMethod("OnLevelWin");
            patch = new(typeof(Mod).GetMethod("PreventNewGhost"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("IsCurrentLevelRushScoreBetter", BindingFlags.NonPublic | BindingFlags.Static);
            patch = new(typeof(Mod).GetMethod("PreventNewGhost"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("ClearLevelRushStats");
            patch = new(typeof(Mod).GetMethod("ResetCheatFlagOnRushInit"));
            harmony.Patch(target, patch);

            target = typeof(LevelGate).GetMethod("SetUnlocked");
            patch = new(typeof(DemonKillSkip).GetMethod("UnlockGate"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("UseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreUseMiracle"));
            harmony.Patch(target, patch);

            target = typeof(LevelRush).GetMethod("CanUseMiracle");
            patch = new HarmonyMethod(typeof(Katana).GetMethod("PreCanUseMiracle"));
            harmony.Patch(target, patch);
        }

        private void OnLevelLoadComplete()
        {
            RushStats = LevelRush.GetCurrentLevelRush();
            if (RushStats.levelRushType == LevelRush.LevelRushType.None)
            {
                // Reset AC
                GameDataManager.powerPrefs.dontUploadToLeaderboard = OriginalDontUploadToLeaderboard;
                Mod.ANTICHEAT_TRIGGERED = false;
            }

            if (SceneManager.GetActiveScene().name.Equals("Heaven_Environment"))
                return;

            GameObject hackObject = new("HackManager");
            hackObject.AddComponent<ModManager>();
        }
    }
}

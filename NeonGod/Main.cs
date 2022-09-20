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
        public static bool DontUploadToLeaderboard;

        public override void OnApplicationLateStart()
        {
            Game game = Singleton<Game>.Instance;
            HarmonyLib.Harmony harmony = new("de.MOPSKATER.NeonHack");

            if (game == null)
                return;
            Game = game;
            Game.OnLevelLoadComplete += OnLevelLoadComplete;

            if (RM.drifter)
                OnLevelLoadComplete();

            MethodInfo target = typeof(LevelStats).GetMethod("UpdateTimeMicroseconds");
            HarmonyMethod patch = new(typeof(Mod).GetMethod("PreventNewScore"));
            harmony.Patch(target, patch);

            target = typeof(LevelGate).GetMethod("SetUnlocked");
            patch = new(typeof(DemonKillSkip).GetMethod("UnlockGate"));
            harmony.Patch(target, patch);

            target = typeof(Game).GetMethod("OnLevelWin");
            patch = new(typeof(Mod).GetMethod("PreventNewGhost"));
            harmony.Patch(target, patch);

            target = typeof(Game).GetMethod("OnLevelWin");
            patch = new HarmonyMethod(typeof(DeltaPB).GetMethod("PreOnLevelWin"));
            harmony.Patch(target, patch);

            target = typeof(MenuScreenResults).GetMethod("OnSetVisible");
            patch = new HarmonyMethod(typeof(DeltaPB).GetMethod("PostOnSetVisible"));
            harmony.Patch(target, null, patch);
        }

        private static void OnLevelLoadComplete()
        {
            GameDataManager.powerPrefs.dontUploadToLeaderboard = DontUploadToLeaderboard;
            if (SceneManager.GetActiveScene().name.Equals("Heaven_Environment"))
                return;

            Mod.ANTICHEAT_TRIGGERED = false;
            GameObject hackObject = new("HackManager");
            hackObject.AddComponent<ModManager>();
        }
    }
}

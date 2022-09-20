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
            HarmonyMethod prefix = new(typeof(Mod).GetMethod("PreventNewScore"));
            harmony.Patch(target, prefix);

            MethodInfo target1 = typeof(LevelGate).GetMethod("SetUnlocked");
            HarmonyMethod prefix1 = new(typeof(DemonKillSkip).GetMethod("UnlockGate"));
            harmony.Patch(target1, prefix1);

            MethodInfo target2 = typeof(Game).GetMethod("OnLevelWin");
            HarmonyMethod prefix2 = new(typeof(Mod).GetMethod("PreventNewGhost"));
            harmony.Patch(target2, prefix2);
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

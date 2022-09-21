using UnityEngine;

namespace NeonGod.Mods
{
    internal abstract class Mod : MonoBehaviour
    {
        public static bool ANTICHEAT_TRIGGERED { get; set; }

        public string Name { get; private set; }
        public string[] DisplayInfo { get; private set; }

        protected void TriggerAnticheat()
        {
            if (ANTICHEAT_TRIGGERED)
                return;

            ANTICHEAT_TRIGGERED = true;
            GameDataManager.powerPrefs.dontUploadToLeaderboard = true;
            return;

            /* Old Anticheat attempt

            Game game = Singleton<Game>.Instance;
            FieldInfo info = game.GetType().GetField("_currentPlaythrough", BindingFlags.NonPublic | BindingFlags.Instance);
            LevelPlaythrough currentLevel = (LevelPlaythrough)info.GetValue(game);
            info = currentLevel.GetType().GetField("microseconds", BindingFlags.NonPublic | BindingFlags.Instance);
            info.SetValue(currentLevel, 600000000);

            EnemyWave wave = FindObjectOfType<EnemyWave>();
            LevelGate gate = FindObjectOfType<LevelGate>();
            BookOfLife book = FindObjectOfType<BookOfLife>();
            BossEncounter boss = FindObjectOfType<BossEncounter>();

            if (wave != null)
            {
                info = wave.GetType().GetField("enemiesRemaining", BindingFlags.NonPublic | BindingFlags.Instance);
                if (info != null)
                    info.SetValue(wave, 1000);
            }

            if (gate != null)
                gate.SetUnlocked(false);

            if (book != null)
            {
                book.bookOpenDistance = -1f;
                book._collider.enabled = false;
            }

            if (boss != null)
                boss.BossDataContainer.hasWonLevel = true;
            */
        }

        public static bool PreventNewScore(LevelStats __instance, ref long newTime)
        {
            if (newTime < __instance._timeBestMicroseconds)
            {
                if (!ANTICHEAT_TRIGGERED)
                    __instance._timeBestMicroseconds = newTime;
                __instance._newBest = true;
            }
            else
                __instance._newBest = false;
            __instance._timeLastMicroseconds = newTime;
            return false;
        }

        public static bool PreventNewGhost(Game __instance)
        {
            if (ANTICHEAT_TRIGGERED)
                __instance.winAction = null;
            return true;
        }
    }
}

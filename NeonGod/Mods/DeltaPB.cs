using System.Reflection;
using TMPro;
using UnityEngine;

namespace NeonGod.Mods
{
    internal class DeltaPB : Mod
    {
        private static string delta = "";
        private static bool newbest;

        public static bool PreOnLevelWin()
        {
            LevelInformation levelInformation = Main.Game.GetGameData().GetLevelInformation(Main.Game.GetCurrentLevel());
            long besttime = GameDataManager.levelStats[levelInformation.levelID].GetTimeBestMicroseconds();
            FieldInfo fi = Main.Game.GetType().GetField("_currentPlaythrough", BindingFlags.Instance | BindingFlags.NonPublic);
            LevelPlaythrough currentPlaythrough = (LevelPlaythrough)fi.GetValue(Main.Game);
            long newtime = currentPlaythrough.GetCurrentTimeMicroseconds();

            long deltatime = (besttime - newtime) / 1000;
            newbest = deltatime < 0;

            TimeSpan t = TimeSpan.FromMilliseconds((double)Math.Abs(deltatime));
            delta = (newbest ? "+" : "-") + string.Format("{0:0}:{1:00}.{2:000}",
                                                t.Minutes,
                                                t.Seconds,
                                                t.Milliseconds);
            return true;
        }

        public static void PostOnSetVisible()
        {
            GameObject bestText = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Results Panel/New Best Text");
            GameObject deltaTime = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Results Panel/Delta Time");

            if (deltaTime == null)
            {
                deltaTime = UnityEngine.Object.Instantiate(bestText, bestText.transform.parent);
                deltaTime.name = "Delta Time";
                deltaTime.transform.localPosition += new Vector3(-5, -30, 0);
                deltaTime.SetActive(true);
            }
            TextMeshProUGUI text = deltaTime.GetComponent<TextMeshProUGUI>();
            text.SetText(delta);
            text.color = newbest ? Color.red : Color.green;
        }
    }
}

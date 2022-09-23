using UnityEngine.InputSystem;

namespace NeonGod.Mods
{
    internal class Katana : Mod
    {
        void Update()
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                TriggerAnticheat();
                GS.AddCard("KATANA_MIRACLE");
            }
        }

        public static bool PreUseMiracle()
        {
            if (!ANTICHEAT_TRIGGERED) return true;
            return false;
        }

        public static bool PreCanUseMiracle(ref bool __result)
        {
            if (!ANTICHEAT_TRIGGERED) return true;
            __result = true;
            return false;
        }
    }
}

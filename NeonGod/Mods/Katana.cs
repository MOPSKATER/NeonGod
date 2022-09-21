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

        public static bool OnPreCanUseMiracle(ref bool __result)
        {
            if (!ANTICHEAT_TRIGGERED) return true;

            __result = true;
            return false;
        }
    }
}

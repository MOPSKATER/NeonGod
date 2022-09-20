using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonGod.Hacks
{
    internal class DemonKillSkip : Mod
    {
        public static bool active = false;

        void Update()
        {
            if (active)
                TriggerAnticheat();

            if (Keyboard.current.uKey.wasPressedThisFrame)
                active = !active;
        }

        public static bool UnlockGate(LevelGate __instance, ref bool u)
        {
            if (active)
            {
                if (__instance.Unlocked)
                    return false;
                u = true;
            }
            return true;
        }
    }
}

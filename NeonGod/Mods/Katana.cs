using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonGod.Hacks
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
    }
}

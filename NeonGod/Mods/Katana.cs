using UnityEngine.InputSystem;

namespace NeonGod.Mods
{
    internal class Katana : Mod
    {
        void Update()
        {
            if (ANTICHEAT_TRIGGERED && Main.RushStats.miraclesLeft == 0)
                Main.RushStats.miraclesLeft = int.MaxValue;

            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                TriggerAnticheat();
                GS.AddCard("KATANA_MIRACLE");
            }
        }
    }
}

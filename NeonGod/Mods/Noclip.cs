using UnityEngine.InputSystem;

namespace NeonGod.Mods
{
    internal class Noclip : Mod
    {
        private bool _active;
        void Update()
        {
            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                TriggerAnticheat();
                _active = !_active;
                RM.drifter.SetNoclip(_active);
            }
        }
    }
}

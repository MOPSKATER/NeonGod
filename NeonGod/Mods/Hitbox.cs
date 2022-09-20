using UnityEngine.InputSystem;

namespace NeonGod.Mods
{
    internal class Hitbox : Mod
    {

        private bool _enabled;

        void Update()
        {
            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                if (!_enabled)
                {
                    TriggerAnticheat();
                    CollisionVisualizer.Enable(Keyboard.current.ctrlKey.IsPressed());
                }
                else
                    CollisionVisualizer.Disable();
                _enabled = !_enabled;
            }
        }
    }
}

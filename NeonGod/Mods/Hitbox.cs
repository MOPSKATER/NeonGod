using UnityEngine.InputSystem;

namespace NeonGod.Hacks
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
                    CollisionVisualizer.enable(Keyboard.current.ctrlKey.IsPressed());
                }
                else
                    CollisionVisualizer.disable();
                _enabled = !_enabled;
            }
        }
    }
}

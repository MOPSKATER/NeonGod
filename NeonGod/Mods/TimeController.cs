using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonGod.Mods
{
    internal class TimeController : Mod
    {
        private const float scaleStep = 0.05f;

        void Update()
        {

            if (Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                TriggerAnticheat();
                float newTime = RM.time.GetCurrentTimeScale() + scaleStep;
                if (newTime <= 2f)
                {
                    RM.time.SetTargetTimescale(newTime);
                    Time.fixedDeltaTime = newTime / 60;
                }
            }
            else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
            {
                TriggerAnticheat();
                float newTime = RM.time.GetCurrentTimeScale() - scaleStep;
                if (newTime > 0f)
                {
                    RM.time.SetTargetTimescale(newTime);
                    Time.fixedDeltaTime = newTime / 60;
                }
            }
        }
    }
}

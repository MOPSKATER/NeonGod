using UnityEngine;

namespace NeonGod.Mods
{
    internal abstract class Mod : MonoBehaviour
    {
        public string Name { get; private set; }
        public string[] DisplayInfo { get; private set; }

        protected void TriggerAnticheat()
        {
            AntiCheat.Anticheat.TriggerAnticheat();
        }
    }
}

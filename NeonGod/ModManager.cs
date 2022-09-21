using NeonGod.Mods;
using UnityEngine;

namespace NeonGod
{
    internal class ModManager : MonoBehaviour
    {
        private readonly Type[] mods = { typeof(Hitbox), typeof(Killall), typeof(Noclip), typeof(Teleport),
            typeof(Katana), typeof(CharakterInfo), typeof(Help), typeof(DemonKillSkip),
            typeof(DeltaPB) };

        void Awake()
        {
            foreach (Type type in mods)
                gameObject.AddComponent(type);
        }

        void Update()
        {
            // TODO
        }
    }
}

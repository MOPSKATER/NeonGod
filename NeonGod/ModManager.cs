using System;
using MelonLoader;
using NeonGod.Hacks;
using UnityEngine;

namespace NeonGod
{
    internal class ModManager : MonoBehaviour
    {
        private readonly Type[] mods = { typeof(Hitbox), typeof(Killall), typeof(Noclip), typeof(TeleportHack), typeof(Katana), typeof(RevealMomentum), typeof(Help), typeof(DemonKillSkip) };

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

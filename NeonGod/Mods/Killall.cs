﻿using UnityEngine;
using UnityEngine.InputSystem;

namespace NeonGod.Hacks
{
    internal class Killall : Mod
    {
        void Update()
        {
            if (Keyboard.current.xKey.wasPressedThisFrame)
            {
                TriggerAnticheat();
                foreach (var enemy in FindObjectsOfType<Enemy>())
                    enemy.Die();
            }
        }
    }
}

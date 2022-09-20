using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NeonGod.Hacks
{
    internal class State
    {
        public Vector3 Position { get; }
        public (float, float) Rotation { get; }
        public List<(string, int)> Cards = new List<(string, int)>();

        public State(Vector3 position, (float, float) rotation, List<(string, int)> cards)
        {
            Position = position;
            Rotation = rotation;
            Cards = cards;
        }
    }
}

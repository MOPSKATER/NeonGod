using System;
using System.Collections.Generic;
using System.Text;
using NeonGod.Hacks;

namespace NeonGod
{
    internal class PersistentDataStore
    {
        public static Dictionary<int, State> States { get; set; }
    }
}

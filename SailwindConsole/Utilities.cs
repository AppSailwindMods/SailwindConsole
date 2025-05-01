using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole
{
    public static class Utilities
    {
        internal static Transform playerTransform;
        public static Transform PlayerTransform => playerTransform;

        public static bool GamePaused { get; internal set; }
    }
}

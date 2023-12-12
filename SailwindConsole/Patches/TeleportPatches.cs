using HarmonyLib;
using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailwindConsole.Patches
{
    internal static class TeleportPatches
    {
        [HarmonyPatch(typeof(Port), "Update")]
        public static class Update
        {
            [HarmonyPrefix]
            public static bool Prefix()
            {
                return !Utilities.GamePaused;
            }
        }
    }
}

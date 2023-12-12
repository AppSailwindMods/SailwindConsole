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
            public static bool Prefix(Port __instance)
            {
                if (!Utilities.GamePaused) return true;
                if (__instance.teleportPlayer)
                {
                    FloatingOriginManager.ShiftingThisFrame = true;
                }
                return false;
            }
        }
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
                if (__instance.teleportPlayer && !Utilities.GamePaused)
                {
                    Refs.charController.transform.position = __instance.transform.position + Vector3.up * 501f;
                    __instance.teleportPlayer = false;
                    var floatingOriginManager = FloatingOriginManager.instance;
                    Debug.Log("Debug teleporting player to " + __instance.GetPrivateField<string>("portName"));
                    var shiftDistance = floatingOriginManager.GetPrivateField<float>("shiftDistance");
                    var shifterObject = floatingOriginManager.GetPrivateField<Transform>("shifterObject");
                    while (shifterObject.transform.position.x > shiftDistance)
                    {
                        floatingOriginManager.InvokePrivateMethod("Shift", -1, 0);
                    }
                    while (shifterObject.transform.position.x < -shiftDistance)
                    {
                        floatingOriginManager.InvokePrivateMethod("Shift",1, 0);
                    }
                    while (shifterObject.transform.position.z > shiftDistance)
                    {
                        floatingOriginManager.InvokePrivateMethod("Shift", 0, -1);
                    }
                    while (shifterObject.transform.position.z < -shiftDistance)
                    {
                        floatingOriginManager.InvokePrivateMethod("Shift", 0, 1);
                    }
                    FloatingOriginManager.ShiftingThisFrame = false;
                }
                return false;
            }
        }
    }
}

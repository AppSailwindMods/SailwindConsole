using Crest;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Patches
{
    internal static class LoadConsolePatches
    {
        internal static StartMenu startMenu;

        [HarmonyPatch(typeof(Sun), "Start")]
        private static class GameStartPatch
        {
            [HarmonyPostfix]
            public static void Postfix(Sun __instance)
            {
                Utilities.playerTransform = Sun.sun.GetPrivateField<Transform>("player");
            }
        }

        [HarmonyPatch(typeof(StartMenu), "Awake")]
        private static class SetStartMenu
        {
            private static void Prefix(StartMenu __instance)
            {
                startMenu = __instance;
            }
        }

        [HarmonyPatch(typeof(StartMenu), "LateUpdate")]
        private static class LateUpdatePatch
        {
            private static void Postfix(StartMenu __instance)
            {
                if (!Utilities.GamePaused) return;
                if (SailwindConsoleMain.instance.openConsoleKeybind.Value.IsDown() && !ModConsole.consoleInput.isFocused)
                {
                    ModConsole.ToggleConsole();
                }

                if (ModConsole.visibleConsole && ModConsole.inputFocused && Input.GetKeyDown(KeyCode.Return))
                {
                    ModConsole.OnEndEdit();
                }
                ModConsole.inputFocused = ModConsole.consoleInput.isFocused;
            }
        }

        [HarmonyPatch(typeof(StartMenu), "GameToSettings")]
        private static class ConsoleOpenPatch
        {
            private static void Postfix(StartMenu __instance)
            {
                Utilities.GamePaused = true;
                if (ModConsole.visibleConsole)
                {
                    ModConsole.ShowConsole();
                }
            }
        }

        [HarmonyPatch(typeof(StartMenu), "SettingsToGame")]
        private static class ConsoleClosePatch
        {
            private static void Postfix(StartMenu __instance)
            {
                ModConsole.HideConsole();
                Utilities.GamePaused = false;
            }
        }

        [HarmonyPatch(typeof(MouseButtonPointer), "DoRaycast")]
        private static class BackgroundNotInteractablePatch
        {
            [HarmonyPrefix]
            private static bool Prefix(MouseButtonPointer __instance)
            {
                if (ModConsole.visibleConsole && Utilities.GamePaused)
                {
                    __instance.SetPrivateField("pointedAtButton", null);
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Sun), "Start")]
        private static class StartPatch
        {
            private static void Postfix(Sun __instance)
            {
                SailwindConsoleMain.logSource.LogInfo("Starting console init");
                ModConsole.InitConsole();
                ModConsole.HideConsole();
                GameData.ocean = GameObject.FindObjectOfType<OceanRenderer>().gameObject;
                GameData.defaultSeaLevel = GameData.ocean.transform.position.y;
                GameData.initialTimeStep = Time.fixedDeltaTime;
                GameData.wind = GameObject.FindObjectOfType<Wind>();
            }
        }
    }
}

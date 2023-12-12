using Crest;
using HarmonyLib;
using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SailwindConsole.Patches
{
    internal static class ConsolePatches
    {
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

                if(ModConsole.visibleConsole && ModConsole.inputFocused && Input.GetKeyDown(KeyCode.Return))
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

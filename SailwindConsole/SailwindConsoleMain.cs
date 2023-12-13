using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using SailwindModdingHelper;
using System.Reflection;
using UnityEngine;

namespace SailwindConsole
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(SailwindModdingHelperMain.GUID, "2.0.0")]
    public class SailwindConsoleMain : BaseUnityPlugin
    {
        public const string GUID = "com.app24.sailwindconsole";
        public const string NAME = "Sailwind Console";
        public const string VERSION = "1.0.1";

        internal static ManualLogSource logSource;

        internal static SailwindConsoleMain instance;

        internal ConfigEntry<KeyboardShortcut> openConsoleKeybind;

        private void Awake()
        {
            instance = this;
            logSource = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);

            openConsoleKeybind = Config.Bind("Hotkeys", "Open Console Key", new KeyboardShortcut(KeyCode.BackQuote));
        }
    }
}

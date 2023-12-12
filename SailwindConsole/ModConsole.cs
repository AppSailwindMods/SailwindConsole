using SailwindConsole.Commands;
using SailwindModdingHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SailwindConsole
{
    public static class ModConsole
    {
        private static bool initialised;
        private static CanvasGroup canvasGroup;
        internal static InputField consoleInput;
        internal static Text logText;
        private static ScrollRect scrollRect;
        private static Canvas consoleCanvas;
        private static GameObject modConsoleObject;
        internal static List<Command> commands = new List<Command>();
        internal static bool visibleConsole;
        internal static bool inputFocused;

        private const string RE_ARG_MATCHER_PATTERN = @"""[^""\\]*(?:\\.[^""\\]*)*""|'[^'\\]*(?:\\.[^'\\]*)*'|\S+";
        private const string RE_QUOTE_STRIP_PATTERN = @"^""+|""+$|^'+|'+$";

        internal static List<string> previousCommands = new List<string>();
        internal static int previousCommandIndex = -1;

        #region Initialisation
        internal static void InitConsole()
        {
            if (initialised)
                return;
            SailwindConsoleMain.logSource.LogInfo("Setting up console");

            var asset = AssetBundle.LoadFromFile(Path.Combine(SailwindConsoleMain.instance.Info.GetFolderLocation(), "assets", "console"));
            if (asset == null)
            {
                SailwindConsoleMain.logSource.LogError($"Failed to load asset bundle");
                return;
            }

            var prefab = asset.LoadAsset<GameObject>("Console");
            modConsoleObject = GameObject.Instantiate(prefab);
            consoleCanvas = modConsoleObject.GetComponent<Canvas>();

            logText = modConsoleObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();

            logText.text = "";

            consoleInput = modConsoleObject.transform.GetChild(1).GetComponent<InputField>();

            consoleInput.Select();
            consoleInput.ActivateInputField();
            inputFocused = true;

            consoleInput.gameObject.AddComponent<CommandHistoryChanger>();

            scrollRect = modConsoleObject.transform.GetChild(0).GetComponent<ScrollRect>();

            scrollRect.scrollSensitivity = 15;

            //InitCanvasScaler();
            //InitLog();
            //InitInputField();

            canvasGroup = modConsoleObject.GetComponent<CanvasGroup>();

            initialised = true;
            SailwindConsoleMain.logSource.LogInfo("Console is created");

            HideConsole();

            AddDefaultCommands();
        }
        #endregion

        private static void AddDefaultCommands()
        {
            AddCommand(new SetGoldCommand());
            AddCommand(new AddGoldCommand());
            AddCommand(new SetThirstCommand());
            AddCommand(new SetHungerCommand());
            AddCommand(new SetSleepCommand());
            AddCommand(new SetAlcoholCommand());
            AddCommand(new SetTimeCommand());
            AddCommand(new GetWeightCommand());
            AddCommand(new CurrentTimeCommand());
            AddCommand(new WorldCoordsCommand());
            AddCommand(new LatLongCommand());
            AddCommand(new AddReputation());
            AddCommand(new ShowPortsCommand());
            AddCommand(new ShowRegionsCommand());
            AddCommand(new TeleportCommand());
            AddCommand(new SeaLevelCommand());
            AddCommand(new RespawnShopsCommand());
            AddCommand(new GodModeCommand());
            AddCommand(new GameSpeedCommand());
            AddCommand(new SetStormCommand());
            AddCommand(new SetWindSpeedCommand());
            AddCommand(new CookFoodCommand());
            AddCommand(new SetWaveHeightCommand());
            //AddCommand(new ListSpawnableObjectsCommand());
            //AddCommand(new SpawnObjectCommand());
            AddCommand(new HelpCommand(commands));
        }

        public static void MoveScrollToEnd()
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0;
            Canvas.ForceUpdateCanvases();
        }

        internal static void OnEndEdit()
        {
            Regex reg = new Regex(RE_ARG_MATCHER_PATTERN);
            string text = consoleInput.text;
            if (string.IsNullOrEmpty(text)) return;
            MatchCollection matches = reg.Matches(text);
            List<string> commandData = new List<string>();
            foreach (Match match in matches)
            {
                commandData.Add(Regex.Replace(match.Value, RE_QUOTE_STRIP_PATTERN, ""));
            }
            if (commandData.Count <= 0) return;

            string commandName = commandData[0].ToLower();
            List<string> args = commandData.GetRange(1, commandData.Count - 1);

            Command command = commands.Find(c => c.Name.ToLower().Equals(commandName) || c.Aliases.Map(x => x.ToLower()).Contains(commandName));

            if (command != null)
            {
                if (args.Count < command.MinArgs)
                {
                    ModConsoleLog.Error($"\"{command.Name}\" requires {command.MinArgs} arguments!");
                    if (!string.IsNullOrEmpty(command.Usage))
                    {
                        ModConsoleLog.Error($"Usage: {command.Usage}");
                    }
                }
                else
                {
                    command.OnRun(args);
                }
            }
            else
            {
                ModConsoleLog.Error($"\"{commandName}\" is not a valid command!");
            }
            previousCommands.Add(text);
            previousCommandIndex = previousCommands.Count;
            consoleInput.text = "";
            consoleInput.Select();
            consoleInput.ActivateInputField();
            inputFocused = true;
        }

        public static void AddCommand(Command command)
        {
            commands.Add(command);
        }

        internal static void HideConsole()
        {
            consoleInput.DeactivateInputField();
            canvasGroup.alpha = 0;
        }

        internal static void ShowConsole()
        {
            consoleInput.DeactivateInputField();
            canvasGroup.alpha = 1;
        }

        internal static void ToggleConsole()
        {
            if (canvasGroup.alpha <= 0)
            {
                ShowConsole();
                visibleConsole = true;
            }
            else
            {
                HideConsole();
                visibleConsole = false;
            }
        }
    }
}
